using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;
using Datos.AccesoDatos;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.reportes
{
    public class ReporteDA
    {


        Logger logger = new Logger();




        public List<ParametroGenerico> ListarEstadosPorTipoTramite(int idTipoTramite)
        {

            try
            {

                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico paramAux = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstadosTramite";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);


                DataTable dt = cnn.Execute();


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramAux = new ParametroGenerico(Convert.ToInt32(row["idEstadoSolicitud"]), row["nombreEstado"].ToString());
                        resp.Add(paramAux);
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



        public DataTable ReporteTramiteDocumentacion01(int idTipoTramite, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteTramiteDocumentacion01";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                if (idTipoRelocalizacion > 0)
                {
                    cnn.parametros.Add("@idTipoRelocalizacion", idTipoRelocalizacion);
                }
                if (idTipoModificacion > 0)
                {
                    cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);
                }
                if (idRegion > 0) {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if(especies != null && !especies.Trim().Equals("")) 
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if(numPert != null && !numPert.Trim().Equals("")) 
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if(codigoCentro != null && !codigoCentro.Trim().Equals("")) 
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }

        public DataTable ReporteTramiteDocumentacion02(int idTipoTramite, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteTramiteDocumentacion02";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idTipoRelocalizacion > 0)
                {
                    cnn.parametros.Add("@idTipoRelocalizacion", idTipoRelocalizacion);
                }
                if (idTipoModificacion > 0)
                {
                    cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }


        public DataTable ReporteTramiteDocumentacion03(int idTipoTramite, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteTramiteDocumentacion03";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idTipoRelocalizacion > 0)
                {
                    cnn.parametros.Add("@idTipoRelocalizacion", idTipoRelocalizacion);
                }
                if (idTipoModificacion > 0)
                {
                    cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }



        public DataTable ReporteTramiteCoordenadas(int idTipoTramite, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteTramiteCoordenadas";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idTipoRelocalizacion > 0)
                {
                    cnn.parametros.Add("@idTipoRelocalizacion", idTipoRelocalizacion);
                }
                if (idTipoModificacion > 0)
                {
                    cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }



        public DataTable ReporteUECoordenadas(int idTipoUE, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteUECoordenadas";
                cnn.parametros.Add("@idTipoUE", idTipoUE);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }



        public DataTable ReporteUEDocumentos(int idTipoUE, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteUEDocumentacion01";
                cnn.parametros.Add("@idTipoUE", idTipoUE);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@barrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }




        public DataTable ReporteUEAnalisisVigencia(int idTipoUE, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro, DateTime fechaInicio, DateTime fechaFin)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteUEAnalisisVigencia";
                cnn.parametros.Add("@idTipoUE", idTipoUE);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                if (fechaInicio != default(DateTime))
                {

                    int mesInicio = fechaInicio.Month;
                    int anioInicio = fechaInicio.Year;

                    cnn.parametros.Add("@mesInicio", mesInicio);
                    cnn.parametros.Add("@anioInicio", anioInicio);
                }

                if (fechaFin != default(DateTime))
                {

                    int mesFin = fechaFin.Month;
                    int anioFin = fechaFin.Year;

                    cnn.parametros.Add("@mesFin", mesFin);
                    cnn.parametros.Add("@anioFin", anioFin);
                    
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }


        public DataTable ReporteUECentroCultivo(int idTipoUE, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {

            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteUECentroCultivo";
                cnn.parametros.Add("@idTipoUE", idTipoUE);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }


                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };


        }



        public DataTable ReporteTramiteSSP_SSFFAA(int idTipoTramite, int idTipoRelocalizacion, int idTipoModificacion, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, int idEstado, string numPert, string codigoCentro)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteSSP_SSFFAA";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                if (idTipoRelocalizacion > 0)
                {
                    cnn.parametros.Add("@idTipoRelocalizacion", idTipoRelocalizacion);
                }
                if (idTipoModificacion > 0)
                {
                    cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);
                }
                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (idEstado > 0)
                {
                    cnn.parametros.Add("@idEstado", idEstado);
                }
                if (numPert != null && !numPert.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", numPert);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }

                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

        public DataTable ReporteUENoVigentesResoluciones(int idTipoUE, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, string codigoCentro)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteUEVigenteResoluciones";
                
                cnn.parametros.Add("@idTipoUE", idTipoUE);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }


                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        public DataTable ReporteUENOVigentesTramites(int idTipoUE, int idRegion, int idProvincia, int idComuna, int macrozona, int barrio, string especies, int idGrupoEspecie, string codigoCentro)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReporteUENoVigenteTramites";

                cnn.parametros.Add("@idTipoUE", idTipoUE);

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                if (idComuna > 0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
                }
                if (macrozona > 0)
                {
                    cnn.parametros.Add("@macrozona", macrozona);
                }
                if (barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrio);
                }
                if (especies != null && !especies.Trim().Equals(""))
                {
                    cnn.parametros.Add("@especies", especies);
                }
                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupoEspecie);
                }
                if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codigoCentro);
                }


                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


    }
}
