using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Datos.AccesoDatos;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.errores;

namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class DataExternaDA
    {
        Logger logger = new Logger();

        public DataTable Listar_SSP_DatosProduccion(int codCentro)
        {

            try { 

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_Produccion";
                cnn.parametros.Add("@codCentro", codCentro);
            
                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable Listar_SSP_DatosInfa(int codCentro)
        {

            try{

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_AmbientalInfas";
                cnn.parametros.Add("@codCentro", codCentro);

                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }
        
        public DataTable Listar_SSP_DatosInformacionSanitaria(int codCentro)
        {

            try{

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_InformacionSanitaria";
                cnn.parametros.Add("@codCentro", codCentro);

                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ParametroGenerico> ObtieneListaAMERB_Externo(string nombreAmerb)
        {
            try
            {
                ParametroGenerico paramResp = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_AMERB_Filtro";
                if (nombreAmerb != null && !nombreAmerb.Equals(""))
                {
                    cnn.parametros.Add("@nombreAmerb", nombreAmerb);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramResp = new ParametroGenerico(Convert.ToInt32(row["COD_SNP"]), row["descripcionAmerb"].ToString());
                        resp.Add(paramResp);
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

        public List<ParametroGenerico> ObtieneListaEcmpo_Externo(string nombreAmerb)
        {
            try
            {
                ParametroGenerico paramResp = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_Ecmpo_Filtro";
                if (nombreAmerb != null && !nombreAmerb.Equals(""))
                {
                    cnn.parametros.Add("@nombreEcmpo", nombreAmerb);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramResp = new ParametroGenerico(Convert.ToInt32(row["id_Emcpo"]), row["descripcionEcmpo"].ToString());
                        resp.Add(paramResp);
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

        public DataExterna Obtener_SSP_DatosAmerb(string nombreAmerb)
        {
            try
            {
                DataExterna data = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_AMERB";
                if (!nombreAmerb.Equals(""))
                {
                    cnn.parametros.Add("@nombreAmerb", nombreAmerb);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new DataExterna();
                        
                        data.codigo = Convert.ToInt32(row["COD_SNP"]);
                 
                        if (!row.IsNull("Region"))
                        {
                            data.region = row["Region"].ToString();
                        }
                        if (!row.IsNull("AMERB"))
                        {
                            data.nombre = row["AMERB"].ToString();
                        }
                        if (!row.IsNull("ESTADO"))
                        {
                            data.estado = row["ESTADO"].ToString();
                        }
                        if (!row.IsNull("CDU01"))
                        {
                            data.cdu01 = row["CDU01"].ToString();
                        }
                        if (!row.IsNull("CDU02"))
                        {
                            data.cdu02 = row["CDU02"].ToString();
                        }
                        if (!row.IsNull("CDU03"))
                        {
                            data.cdu03 = row["CDU03"].ToString();
                        }
                        if (!row.IsNull("FCDU01"))
                        {
                            data.fcdu01 = row["FCDU01"].ToString();
                        }
                        if (!row.IsNull("FCDU02"))
                        {
                            data.fcdu02 = row["FCDU02"].ToString();
                        }
                        if (!row.IsNull("FCDU03"))
                        {
                            data.fcdu03 = row["FCDU03"].ToString();
                        }
                        if (!row.IsNull("Superficie_Hectareas"))
                        {
                            data.superficie = row["Superficie_Hectareas"].ToString();
                        }
                        if (!row.IsNull("UltimoPLazo"))
                        {
                            data.ultimoPlazo = Convert.ToDateTime(row["UltimoPLazo"]);
                        }
                        if (!row.IsNull("Informe"))
                        {
                            data.informe = row["Informe"].ToString();
                        }

                       
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataExterna Obtener_SSP_DatosEcmpo(string nombreEcmpo)
        {
            try
            {
              
                DataExterna data = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSSP_ECMPO";
                if (!nombreEcmpo.Equals(""))
                {
                    cnn.parametros.Add("@nombreEcmpo", nombreEcmpo);
                }
            
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new DataExterna();
                        
                        data.codigo = Convert.ToInt32(row["id_Emcpo"]);
                        if (!row.IsNull("Solicitud_Emcpo"))
                        {
                            data.nombre = row["Solicitud_Emcpo"].ToString();
                        }
                        if (!row.IsNull("ComunaIndigena"))
                        {
                            data.comunaIndigena = row["ComunaIndigena"].ToString();
                        }
                        if (!row.IsNull("Comuna"))
                        {
                            data.comuna = row["Comuna"].ToString();
                        }

                        if (!row.IsNull("Region"))
                        {
                            data.region = row["Region"].ToString();
                        }

                        if (!row.IsNull("Estado"))
                        {
                            data.estado = row["Estado"].ToString();
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable Listar_SSP_TramiteArriendoRCA(int codCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTramiteArriendoRCA";
                cnn.parametros.Add("@codSiep", codCentro);

                DataTable dt = cnn.Execute();
                return dt;
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
