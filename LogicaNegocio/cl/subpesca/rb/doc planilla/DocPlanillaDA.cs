using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.doc_planilla
{
    public class DocPlanillaDA
    {

        Logger logger = new Logger();

        public int GuardarDocPlanilla(DocPlanilla docPlanilla)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocPlanilla";

                cnn.parametros.Add("@idEstadoVigencia", docPlanilla.vigencia.id);
                cnn.parametros.Add("@nombreDocPlanilla", docPlanilla.nombreDocPlanilla);
                
                byte[] file = new byte[docPlanilla.archivo.InputStream.Length];
                docPlanilla.archivo.InputStream.Read(file, 0, file.Length);

                cnn.parametros.Add("@contenido", file);
                if (docPlanilla.descripcion != null)
                {
                    cnn.parametros.Add("@descripcion", docPlanilla.descripcion);
                }

                DataTable dt = cnn.Execute();
                docPlanilla.idDocPlanilla = Convert.ToInt32(dt.Rows[0]["idDocPlanilla"]);

                int Id = docPlanilla.idDocPlanilla;

                return Id;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            };
                
        }

        public bool ActualizaDocPlanillaVigencia(int idDocPlanilla, int idEstadoVigencia)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbDocPlanillaVigencia";
                cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

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


        public DocPlanilla ObtenerDocPlanilla(int idDocPlanilla)
        {

            try
            {

                DocPlanilla docPlanilla = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocPlanilla_Contenido";
                cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);

                DataTable dt = cnn.Execute();


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        docPlanilla = new DocPlanilla();
                        docPlanilla.idDocPlanilla = Convert.ToInt32(row["idDocPlanilla"]);
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            docPlanilla.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }
                        docPlanilla.nombreDocPlanilla = Convert.ToString(row["nombreDocPlanilla"]);
                        if (!row.IsNull("fechaInsercion"))
                        {
                            docPlanilla.fechaIngresoSistema = Convert.ToDateTime(row["fechaInsercion"]);
                        }
                        if (!row.IsNull("Column1"))
                        {
                            docPlanilla.tramiteCad = row["Column1"].ToString();
                        }

                        docPlanilla.bytes = (byte[])row["contenido"];

                    }
                }

                return docPlanilla;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

        public List<DocPlanilla> ListarDocPlanilla(int idDocPlanilla)
        {

            try
            {

                List<DocPlanilla> docPlanillaList  = new List<DocPlanilla>();
                DocPlanilla docPlanilla = null;
                int idDocPlan = 0;
                int idDocPlanAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocPlanilla_Contenido";
                if (idDocPlanilla > 0)
                {
                    cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
                }                

                DataTable dt = cnn.Execute();


                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idDocPlan = Convert.ToInt32(row["idDocPlanilla"]);

                        if(idDocPlan!=idDocPlanAux){
                            docPlanilla = new DocPlanilla();
                            docPlanilla.idDocPlanilla = idDocPlan;
                            if (!row.IsNull("idEstadoVigencia"))
                            {
                                docPlanilla.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                            }
                            docPlanilla.nombreDocPlanilla = Convert.ToString(row["nombreDocPlanilla"]);
                            if (!row.IsNull("fechaInsercion"))
                            {
                                docPlanilla.fechaIngresoSistema = Convert.ToDateTime(row["fechaInsercion"]);
                            }
                            if (!row.IsNull("Column1"))
                            {
                                docPlanilla.tramiteCad = Convert.ToString(row["Column1"]);
                            }

                            docPlanilla.bytes = (byte[])row["contenido"];

                            idDocPlanAux = idDocPlan;
                            docPlanillaList.Add(docPlanilla);
                        }
                    }
                }

                return docPlanillaList;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        public bool EliminarDocPlanilla(int idDocPlanilla)
        {
            try
            {
                int resultado = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDocPlanilla";
                cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idDocPlanilla"))
                        {
                            resultado = Convert.ToInt32(row["idDocPlanilla"]);
                        }
                    }
                }

                if (resultado < 0)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }
        
        
        //ASOCIACIÓN DOC_PLANILLA - TRÁMITE
        public bool GuardarDocPlanillaTramite(int idDocPlanilla, int idTipoTramite)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocPlanillaTramite";
                cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                
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

        public DataTable ObtenerReemplazoPlanilla(int idSolicitud, string claveReemplazo)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbReemplazoPlanilla";
            cnn.parametros.Add("@idSolicitud", idSolicitud);
            cnn.parametros.Add("@claveReemplazo", claveReemplazo);
            
            DataTable dt = cnn.Execute();

            return dt;
        }

        public DataTable ListarDocPlanillaTramite(int idDocPlanilla)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbDocPlanillaTramite";
            cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
            
            DataTable dt = cnn.Execute();

            return dt;
        }

        public bool EliminarDocPlanillaTramite(int idDocPlanilla, int idTipoTramite)
        {
            try
            {
                int resultado = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDocPlanillaTramite";
                cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
                if(idTipoTramite >0){
                    cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idDocPlanilla"))
                        {
                            resultado = Convert.ToInt32(row["idDocPlanilla"]);
                        }
                    }
                }

                if (resultado < 0)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public DataTable ListarDocPlanillaFiltro(int idDocPlanilla, int idTipoTramite)
        {

            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbDocPlanilla_filtro";
            if(idDocPlanilla >0){
                cnn.parametros.Add("@idDocPlanilla", idDocPlanilla);
            }
            cnn.parametros.Add("@idTipoTramite", idTipoTramite);
            

            DataTable dt = cnn.Execute();

            return dt;
        }


    }
}
