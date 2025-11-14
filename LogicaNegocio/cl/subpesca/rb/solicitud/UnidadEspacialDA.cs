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
    
    public class UnidadEspacialDA
    {

        Logger logger = new Logger();

        public UnidadEspacialDA()
        { }


            //Guarda un registro de unidad espacial asociado a la solicitud
        public bool GuardarUnidadEspacial(UnidadEspacial unidEspacial, int idUsuario)
            {
	            try{

                      Conexion cnn = new Conexion();
                      cnn.procedimiento = "paInsRbUnidadesEspaciales";

                      cnn.parametros.Add("@idUnidadEspacial", unidEspacial.idUnidadEspacial);
                      cnn.parametros.Add("@idSolConcesion", unidEspacial.idSolicitud);

                      if (unidEspacial.capitaniaDePuerto!=null && unidEspacial.capitaniaDePuerto.idCapitaDePuerto > 0)
                      {
                        cnn.parametros.Add("@idCapitania", unidEspacial.capitaniaDePuerto.idCapitaDePuerto);
                      }

                      if (unidEspacial.centrosDeCultivo != null && unidEspacial.centrosDeCultivo.codigoCentro != null && !unidEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                      {
                        cnn.parametros.Add("@codigoCentro", unidEspacial.centrosDeCultivo.codigoCentro);
                      }

                      if (unidEspacial.centrosDeCultivo.nombreCentro != null && !unidEspacial.centrosDeCultivo.nombreCentro.Equals(""))
                      {
                          cnn.parametros.Add("@nombreCentro", unidEspacial.centrosDeCultivo.nombreCentro);
                      }

                      if (unidEspacial.numeroDiarioOficial > 0)
                      {
                          cnn.parametros.Add("@numDiarioOficial", unidEspacial.numeroDiarioOficial);
                      }
                      
                      if (unidEspacial.numeroActaEntrega > 0)
                      {
                        cnn.parametros.Add("@numActaEntrega", unidEspacial.numeroActaEntrega);
                      }

                      if (unidEspacial.fechaActaEntrega != null && unidEspacial.fechaActaEntrega!= default(DateTime))
                      {
                        cnn.parametros.Add("@fechaActaEntrega", unidEspacial.fechaActaEntrega);                          
                      }

                      if (unidEspacial.fechaDiarioOficial != null && unidEspacial.fechaDiarioOficial != default(DateTime))
                      {
                          cnn.parametros.Add("@fechaDiarioOficial", unidEspacial.fechaDiarioOficial);
                      }
                      if (unidEspacial.fechaInicioPerAut != null && unidEspacial.fechaInicioPerAut != default(DateTime))
                      {
                          cnn.parametros.Add("@fechaInicioPerAut", unidEspacial.fechaInicioPerAut);
                      }
                      if (unidEspacial.fechaFinPerAut != null && unidEspacial.fechaFinPerAut != default(DateTime))
                      {
                          cnn.parametros.Add("@fechaFinPerAut", unidEspacial.fechaFinPerAut);
                      }
                      if (unidEspacial.mesesAut>0)
                      {
                          cnn.parametros.Add("@mesesAut", unidEspacial.mesesAut);
                      }
                      if (unidEspacial.tipoPlazoNominal!=null && unidEspacial.tipoPlazoNominal.id > 0)
                      {
                          cnn.parametros.Add("@idTipoPlazoNominal", unidEspacial.tipoPlazoNominal.id);
                      }
                      if (unidEspacial.numPlazo > 0)
                      {
                          cnn.parametros.Add("@numPlazo", unidEspacial.numPlazo);
                      }
                      if (unidEspacial.plazoInicio != null && unidEspacial.plazoInicio != default(DateTime))
                      {
                          cnn.parametros.Add("@plazoInicio", unidEspacial.plazoInicio);
                      }
                      if (unidEspacial.plazoVencimiento != null && unidEspacial.plazoVencimiento != default(DateTime))
                      {
                          cnn.parametros.Add("@plazoVencimiento", unidEspacial.plazoVencimiento);
                      }
                      if (idUsuario > 0)
                      {
                          cnn.parametros.Add("@idUsuario", idUsuario);
                      }
                        
                      
	                  DataTable dt = cnn.Execute();
                      unidEspacial.idUnidadEspacial = Convert.ToInt32(dt.Rows[0]["idUnidadEspacial"]);

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
        * Obtiene un registro de unidad espacial asociado a la solicitud
        */
           public UnidadEspacial ObtieneUnidadEspacial(int idSolicitud, int idUnidEspacial)
             {
            try
            {
                UnidadEspacial unidEspResp = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUnidadesEspaciales";
               
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if(idUnidEspacial>0){
                    cnn.parametros.Add("@idUnidadEspacial", idUnidEspacial);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                           unidEspResp = new UnidadEspacial();
                           unidEspResp.idUnidadEspacial = Convert.ToInt32(row["idUnidadEspacial"]);
                           unidEspResp.idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                           unidEspResp.capitaniaDePuerto = new CapitaniaDePuerto();

                            if(!row.IsNull("idCapitania")){
                                unidEspResp.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(row["idCapitania"]); 
                            }

                            if (!row.IsNull("CapitaniaPuerto"))
                            {
                                unidEspResp.capitaniaDePuerto.nombreCapitaniaDePuerto = row["CapitaniaPuerto"].ToString();
                            }
                      
                           
                            unidEspResp.centrosDeCultivo = new CentrosDeCultivo();

                            if (!row.IsNull("codigoCentro"))
                            {
                                unidEspResp.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("nombreCentro"))
                            {
                            unidEspResp.centrosDeCultivo.nombreCentro = row["nombreCentro"].ToString();
                            }
                            if (!row.IsNull("numDiarioOficial"))
                            {
                               unidEspResp.numeroDiarioOficial = Convert.ToInt32(row["numDiarioOficial"]);
                            }
                            if (!row.IsNull("numActaEntrega"))
                            {
                            unidEspResp.numeroActaEntrega = Convert.ToInt32(row["numActaEntrega"]); 
                            }
                            if (!row.IsNull("fechaActaEntrega"))
                            {
                               unidEspResp.fechaActaEntrega = Convert.ToDateTime(row["fechaActaEntrega"]);
                            }
                            if (!row.IsNull("fechaDiarioOficial"))
                            {
                               unidEspResp.fechaDiarioOficial = Convert.ToDateTime(row["fechaDiarioOficial"]);
                            }
                            if (!row.IsNull("fechaInicioPerAut"))
                            {
                                unidEspResp.fechaInicioPerAut = Convert.ToDateTime(row["fechaInicioPerAut"]);
                            }
                            if (!row.IsNull("fechaFinPerAut"))
                            {
                                unidEspResp.fechaFinPerAut = Convert.ToDateTime(row["fechaFinPerAut"]);
                            }
                            if (!row.IsNull("mesesAut"))
                            {
                                unidEspResp.mesesAut = Convert.ToInt32(row["mesesAut"]);
                            }
                            if (!row.IsNull("idTipoPlazoNominal"))
                            {
                                unidEspResp.tipoPlazoNominal = new ParametroGenerico(Convert.ToInt32(row["idTipoPlazoNominal"]), row["nombreTipoPlazo"].ToString());
                            }
                            if (!row.IsNull("numPlazo"))
                            {
                                unidEspResp.numPlazo = Convert.ToInt32(row["numPlazo"]);
                            }
                            if (!row.IsNull("plazoInicio"))
                            {
                                unidEspResp.plazoInicio = Convert.ToDateTime(row["plazoInicio"]);
                            }
                            if (!row.IsNull("plazoVencimiento"))
                            {
                                unidEspResp.plazoVencimiento = Convert.ToDateTime(row["plazoVencimiento"]);
                            }

                    }
                }

                return unidEspResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ParametroGenerico> ListaUnidadesEspacialesCentro(string centroAcuicola)
        {
            try
            {
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUnidadesEspacialesCentro";
                cnn.parametros.Add("@centroAcuicola", centroAcuicola);

                ParametroGenerico param = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        param = new ParametroGenerico();

                        param.id = Convert.ToInt32(row["codigoCentro"]);
                        param.descripcion = row["centroAcuicola"].ToString();

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

        public bool GuardarUnidadEspacialCodSiep(UnidadEspacial unidEspacial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUnidadesEspacialesCodSiep";

                cnn.parametros.Add("@idUnidadEspacial", unidEspacial.idUnidadEspacial);
                cnn.parametros.Add("@idSolConcesion", unidEspacial.idSolicitud);

                if (unidEspacial.centrosDeCultivo != null && !unidEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", unidEspacial.centrosDeCultivo.codigoCentro);
                }

                DataTable dt = cnn.Execute();
                unidEspacial.idUnidadEspacial = Convert.ToInt32(dt.Rows[0]["idUnidadEspacial"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarUnidadEspacialModificacion(UnidadEspacial unidEspacial, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUnidadesEspacialesMod";

                cnn.parametros.Add("@idUnidadEspacial", unidEspacial.idUnidadEspacial);
                cnn.parametros.Add("@idSolConcesion", unidEspacial.idSolicitud);

                if (unidEspacial.capitaniaDePuerto != null && unidEspacial.capitaniaDePuerto.idCapitaDePuerto > 0)
                {
                    cnn.parametros.Add("@idCapitania", unidEspacial.capitaniaDePuerto.idCapitaDePuerto);
                }

                if (unidEspacial.centrosDeCultivo != null && !unidEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", unidEspacial.centrosDeCultivo.codigoCentro);
                }

                if (unidEspacial.centrosDeCultivo.nombreCentro != null && !unidEspacial.centrosDeCultivo.nombreCentro.Equals(""))
                {
                    cnn.parametros.Add("@nombreCentro", unidEspacial.centrosDeCultivo.nombreCentro);
                }

                if (unidEspacial.numeroDiarioOficial>0)
                {
                    cnn.parametros.Add("@numDiarioOficial", unidEspacial.numeroDiarioOficial);
                }

                if (unidEspacial.numeroActaEntrega != null && !unidEspacial.numeroActaEntrega.Equals(""))
                {
                    cnn.parametros.Add("@numActaEntrega", unidEspacial.numeroActaEntrega);
                }

                if (unidEspacial.fechaActaEntrega != null && unidEspacial.fechaActaEntrega != default(DateTime))
                {
                    cnn.parametros.Add("@fechaActaEntrega", unidEspacial.fechaActaEntrega);
                }

                if (unidEspacial.fechaDiarioOficial != null && unidEspacial.fechaDiarioOficial!=default(DateTime))
                {
                    cnn.parametros.Add("@fechaDiarioOficial", unidEspacial.fechaDiarioOficial);
                }
                if (unidEspacial.tipoPlazoNominal != null && unidEspacial.tipoPlazoNominal.id > 0)
                {
                    cnn.parametros.Add("@idTipoPlazoNominal", unidEspacial.tipoPlazoNominal.id);
                }
                if (unidEspacial.numPlazo != null && unidEspacial.numPlazo > 0)
                {
                    cnn.parametros.Add("@numPlazo", unidEspacial.numPlazo);
                }
                if (unidEspacial.plazoInicio != null && unidEspacial.plazoInicio != default(DateTime))
                {
                    cnn.parametros.Add("@plazoInicio", unidEspacial.plazoInicio);
                }
                if (unidEspacial.plazoVencimiento != null && unidEspacial.plazoVencimiento != default(DateTime))
                {
                    cnn.parametros.Add("@plazoVencimiento", unidEspacial.plazoVencimiento);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                unidEspacial.idUnidadEspacial = Convert.ToInt32(dt.Rows[0]["idUnidadEspacial"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public UnidadEspacial ObtieneUnidadEspacialMod(int idSolicitud, int idUnidEspacial)
        {
            try
            {
                UnidadEspacial unidEspResp = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUnidadesEspacialesMod";

                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if (idUnidEspacial > 0)
                {
                    cnn.parametros.Add("@idUnidadEspacial", idUnidEspacial);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        unidEspResp = new UnidadEspacial();
                        unidEspResp.idUnidadEspacial = Convert.ToInt32(row["idUnidEspacialMod"]);
                        unidEspResp.idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                        unidEspResp.capitaniaDePuerto = new CapitaniaDePuerto();

                        if (!row.IsNull("IdCapitaniaPuerto"))
                        {
                            unidEspResp.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(row["IdCapitaniaPuerto"]);
                        }

                        if (!row.IsNull("CapitaniaPuerto"))
                        {
                            unidEspResp.capitaniaDePuerto.nombreCapitaniaDePuerto = row["CapitaniaPuerto"].ToString();
                        }

                        unidEspResp.centrosDeCultivo = new CentrosDeCultivo();
                        if (!row.IsNull("codigoCentro"))
                        {
                            unidEspResp.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                        }
                        if (!row.IsNull("nombreCentro"))
                        {
                            unidEspResp.centrosDeCultivo.nombreCentro = row["nombreCentro"].ToString();
                        }

                        if (!row.IsNull("numDiarioOficial"))
                        {
                            unidEspResp.numeroDiarioOficial = Convert.ToInt32(row["numDiarioOficial"]);
                        }
                        if (!row.IsNull("numActaEntrega"))
                        {
                            unidEspResp.numeroActaEntrega = Convert.ToInt32(row["numActaEntrega"]);
                        }
                        if (!row.IsNull("fechaActaEntrega"))
                        {
                            unidEspResp.fechaActaEntrega = Convert.ToDateTime(row["fechaActaEntrega"]);
                        }
                        if (!row.IsNull("fechaDiarioOficial"))
                        {
                            unidEspResp.fechaDiarioOficial = Convert.ToDateTime(row["fechaDiarioOficial"]);
                        }
                        
                        if (!row.IsNull("idTipoPlazoNominal"))
                        {
                            unidEspResp.tipoPlazoNominal = new ParametroGenerico(Convert.ToInt32(row["idTipoPlazoNominal"]), row["nombreTipo"].ToString());
                        }
                        if (!row.IsNull("numPlazo"))
                        {
                            unidEspResp.numPlazo = Convert.ToInt32(row["numPlazo"]);
                        }
                        if (!row.IsNull("plazoInicio"))
                        {
                            unidEspResp.plazoInicio = Convert.ToDateTime(row["plazoInicio"]);
                        }
                        if (!row.IsNull("plazoVencimiento"))
                        {
                            unidEspResp.plazoVencimiento = Convert.ToDateTime(row["plazoVencimiento"]);
                        }

                    }
                }

                return unidEspResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public UnidadEspacial ObtieneUnidadEspacialRel(int idSolicitud, int idUnidEspacialRel)
        {
            try
            {
                UnidadEspacial unidEspResp = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUnidadesEspacialesRel";

                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if (idUnidEspacialRel > 0)
                {
                    cnn.parametros.Add("@idUnidadEspacialRel", idUnidEspacialRel);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        unidEspResp = new UnidadEspacial();
                        unidEspResp.idUnidadEspacial = Convert.ToInt32(row["idUnidEspacialRel"]);
                        unidEspResp.idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                        unidEspResp.capitaniaDePuerto = new CapitaniaDePuerto();

                        if (!row.IsNull("idCapitaniaPuerto"))
                        {
                            unidEspResp.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(row["idCapitaniaPuerto"]);
                        }

                        if (!row.IsNull("CapitaniaPuerto"))
                        {
                            unidEspResp.capitaniaDePuerto.nombreCapitaniaDePuerto = row["CapitaniaPuerto"].ToString();
                        }

                        unidEspResp.centrosDeCultivo = new CentrosDeCultivo();
                        if (!row.IsNull("codigoCentro"))
                        {
                            unidEspResp.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                        }
                        if (!row.IsNull("nombreCentro"))
                        {
                            unidEspResp.centrosDeCultivo.nombreCentro = row["nombreCentro"].ToString();
                        }

                        if (!row.IsNull("numDiarioOficial"))
                        {
                            unidEspResp.numeroDiarioOficial = Convert.ToInt32(row["numDiarioOficial"]);
                        }

                        if (!row.IsNull("numActaEntrega"))
                        {
                            unidEspResp.numeroActaEntrega = Convert.ToInt32(row["numActaEntrega"]);
                        }
                        if (!row.IsNull("fechaActaEntrega"))
                        {
                            unidEspResp.fechaActaEntrega = Convert.ToDateTime(row["fechaActaEntrega"]);
                        }
                        if (!row.IsNull("fechaDiarioOficial"))
                        {
                            unidEspResp.fechaDiarioOficial = Convert.ToDateTime(row["fechaDiarioOficial"]);
                        }
                    }
                }

                return unidEspResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool GuardarUnidadEspacialRel(UnidadEspacial unidEspacial, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUnidadesEspacialesRel";

                cnn.parametros.Add("@idUnidadEspacialRel", unidEspacial.idUnidadEspacial);
                cnn.parametros.Add("@idSolConcesion", unidEspacial.idSolicitud);

                if (unidEspacial.capitaniaDePuerto != null && unidEspacial.capitaniaDePuerto.idCapitaDePuerto > 0)
                {
                    cnn.parametros.Add("@idCapitaniaPuerto", unidEspacial.capitaniaDePuerto.idCapitaDePuerto);
                }

                if (unidEspacial.centrosDeCultivo != null && !unidEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", unidEspacial.centrosDeCultivo.codigoCentro);
                }

                if (unidEspacial.centrosDeCultivo.nombreCentro != null && !unidEspacial.centrosDeCultivo.nombreCentro.Equals(""))
                {
                    cnn.parametros.Add("@nombreCentro", unidEspacial.centrosDeCultivo.nombreCentro);
                }

                if (unidEspacial.numeroDiarioOficial>0)
                {
                    cnn.parametros.Add("@numDiarioOficial", unidEspacial.numeroDiarioOficial);
                }
                if (unidEspacial.numeroActaEntrega != null && !unidEspacial.numeroActaEntrega.Equals(""))
                {
                    cnn.parametros.Add("@numActaEntrega", unidEspacial.numeroActaEntrega);
                }

                if (unidEspacial.fechaActaEntrega != null && unidEspacial.fechaActaEntrega != default(DateTime))
                {
                    cnn.parametros.Add("@fechaActaEntrega", unidEspacial.fechaActaEntrega);
                }
                if (unidEspacial.fechaDiarioOficial != null && unidEspacial.fechaDiarioOficial != default(DateTime))
                {
                    cnn.parametros.Add("@fechaDiarioOficial", unidEspacial.fechaDiarioOficial);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }


                DataTable dt = cnn.Execute();
                unidEspacial.idUnidadEspacial = Convert.ToInt32(dt.Rows[0]["idUnidadEspacialRel"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ValidaCentroUnidadesEspaciales(string codigoCentro, int idUnidadEspacial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCentroUnidadesEspaciales";
                cnn.parametros.Add("@codigoCentro", codigoCentro);
                cnn.parametros.Add("@idUnidadEspacial", idUnidadEspacial);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row.IsNull("idUnidadEspacial") && Convert.ToInt32(row["idUnidadEspacial"])>0)
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
            };
        }

        public bool ActualizarUnidadEspacialActa(UnidadEspacial unidEspacial, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUnidEspacialActa";

                cnn.parametros.Add("@idSolConcesion", unidEspacial.idSolicitud);
                cnn.parametros.Add("@numActaEntrega", unidEspacial.numeroActaEntrega);

                if (unidEspacial.fechaActaEntrega != null && unidEspacial.fechaActaEntrega != default(DateTime))
                {
                    cnn.parametros.Add("@fechaActaEntrega", unidEspacial.fechaActaEntrega);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                unidEspacial.idUnidadEspacial = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        //Copia la solicitud que pasará a Concesión, la copia pasará a ser una Concesión
        // y la Solicitud se mantendrá en ese estado para efectos de historial.   
        public bool GuardarSolicitud_Clon(int idSolicitud, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesion_Clon";
                cnn.parametros.Add("@idSolicitudConcesion", idSolicitud);
                cnn.parametros.Add("@idUsuario", idUsuario);
                
                DataTable dt = cnn.Execute();

                int resp = 0;
                resp = Convert.ToInt32(dt.Rows[0]["num_errorAux"]);
                if(resp == -1)
                    return false;
                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarUnidadEspacial_UE_Mod(int idSolicitudMod, int idConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbUnidadesEspaciales_SolicitudMod";

                cnn.parametros.Add("@idSolConcesion", idSolicitudMod);
                cnn.parametros.Add("@idConcesion", idConcesion);

                DataTable dt = cnn.Execute();
                idSolicitudMod = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarUnidadEspacial_UE_Mod(int idSolicitudMod, int idConcesion, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbUnidEspacial_UE_Mod";

                cnn.parametros.Add("@idSolConcesion", idSolicitudMod);
                cnn.parametros.Add("@idConcesion", idConcesion);

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                idSolicitudMod = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarUnidadEspacial_UE_Rel(int idSolicitudMod, int idConcesion, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbUnidEspacial_UE_Rel";

                cnn.parametros.Add("@idSolConcesion", idSolicitudMod);
                cnn.parametros.Add("@idConcesion", idConcesion);

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                idSolicitudMod = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarUnidadEspacial_Plazo(UnidadEspacial unidEspacial, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbUnidadesEspaciales_Plazo";

                cnn.parametros.Add("@idSolConcesion", unidEspacial.idSolicitud);

                if (unidEspacial.tipoPlazoNominal != null && unidEspacial.tipoPlazoNominal.id > 0)
                {
                    cnn.parametros.Add("@idTipoPlazoNominal", unidEspacial.tipoPlazoNominal.id);
                }
                if (unidEspacial.numPlazo > 0)
                {
                    cnn.parametros.Add("@numPlazo", unidEspacial.numPlazo);
                }
                if (unidEspacial.plazoInicio != null && unidEspacial.plazoInicio != default(DateTime))
                {
                    cnn.parametros.Add("@plazoInicio", unidEspacial.plazoInicio);
                }
                if (unidEspacial.plazoVencimiento != null && unidEspacial.plazoVencimiento != default(DateTime))
                {
                    cnn.parametros.Add("@plazoVencimiento", unidEspacial.plazoVencimiento);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }


                DataTable dt = cnn.Execute();
                unidEspacial.idSolicitud = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }
        public bool GuardarCapPuertoUnidadEspacial(int idSolicitud, int idCapitania, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbCapUnidadesEspaciales";

                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if(idCapitania>0){
                    cnn.parametros.Add("@idCapitania", idCapitania);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                
                DataTable dt = cnn.Execute();
                idSolicitud = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public ParametroGenerico ObtenerCapPuertoSolicitud(int idSolConcesion)
        {
            try
            {
                ParametroGenerico capPuerto = null;

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbCapUnidadesEspaciales";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        capPuerto = new ParametroGenerico(Convert.ToInt32(row["idCapitania"]), row["CapitaniaPuerto"].ToString());
                    }
                }
                return capPuerto;
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
