using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;
using Datos.AccesoDatos;
using Datos.Entidades;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class GrupoSuspendidoDA
    {
        Logger logger = new Logger();

        public List<GrupoSuspendidos> ListarGrupoSuspendido(int idGrupoSuspend, int idTipoAgrupacion, int idEstadoVigencia, string nombreGrupoSuspend)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbGrupoSuspendido";

            List<GrupoSuspendidos> resp = new List<GrupoSuspendidos>();
            GrupoSuspendidos grupo = null;


            if (idGrupoSuspend > 0)
            {
                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
            }
            if (idTipoAgrupacion > 0)
            {
                cnn.parametros.Add("@idTipoAgrupacion", idTipoAgrupacion);
            }
            if (idEstadoVigencia > 0)
            {
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
            }
            if (nombreGrupoSuspend != null && !nombreGrupoSuspend.Equals(""))
            {
                cnn.parametros.Add("@nombreGrupoSuspend", nombreGrupoSuspend);
            }
            
            
            DataTable dt = cnn.Execute();
            if (dt != null)
            {

                foreach (DataRow row in dt.Rows)
                {
                    grupo = new GrupoSuspendidos();
                    grupo.idGrupoSuspend = Convert.ToInt32(row["idGrupoSuspend"]);
                    grupo.tipoAgrupacion = new ParametroGenerico(Convert.ToInt32(row["idTipoAgrupacion"]), row["nombreTipo"].ToString());
                    grupo.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                    grupo.solicitudConcesion = new SolicitudConcesion();
                    grupo.solicitudConcesion.unidadEspacial = new UnidadEspacial();
                    grupo.solicitudConcesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                    grupo.solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                    grupo.solicitudConcesion.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreGrupoSuspend"].ToString();
                    grupo.nombreGrupoSuspend = row["nombreGrupoSuspend"].ToString();
                    grupo.pertAsignados = row["pertAsociados"].ToString();
                    resp.Add(grupo);
                }
            }
            return resp;
        }

       
        public List<AsocGrupoSolicitud> ListarAsocGrupoSolicitud(int idGrupoSuspend, int idSolConcesion, int idEstadoVigencia)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbAsocGrupoSolicitud";

            List<AsocGrupoSolicitud> resp = new List<AsocGrupoSolicitud>();
            AsocGrupoSolicitud grupo = null;


            if (idGrupoSuspend > 0)
            {
                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
            }
            if (idSolConcesion > 0)
            {
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
            }
            if (idEstadoVigencia > 0)
            {
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
            }

            DataTable dt = cnn.Execute();
            if (dt != null)
            {
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grupo = new AsocGrupoSolicitud();
                    grupo.idAsocGrupoSolicitud = Convert.ToInt32(row["idAsocGrupoSolicitud"]);
                    grupo.grupoSuspendido = new GrupoSuspendidos();
                    grupo.grupoSuspendido.idGrupoSuspend = Convert.ToInt32(row["idGrupoSuspend"]);
                    grupo.grupoSuspendido.nombreGrupoSuspend = row["nombreGrupoSuspend"].ToString();
                    grupo.solicitudConcesion = new SolicitudConcesion();
                    grupo.solicitudConcesion.idConcesion = Convert.ToInt32(row["idSolConcesion"]);
                    grupo.solicitudConcesion.numPert = row["numPert"].ToString();
                    grupo.solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                    if (!row.IsNull("fechaRecepcion")){
                        grupo.solicitudConcesion.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                    }
                    if (!row.IsNull("fechaIngresoTramite")){
                        grupo.solicitudConcesion.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                    }
                    if (!row.IsNull("idEvaluacionUOT"))
                    {
                        grupo.evaluacionUOT_UE = new EvaluacionUOT_UE();
                        grupo.evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                    }
                    if (!row.IsNull("idDocPestana"))
                    {
                        grupo.docPestana = new DocumentoAmbito();
                        grupo.docPestana.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                    }
                    grupo.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                    grupo.index = i;
                    grupo.accion = accion.LISTADO;

                    resp.Add(grupo);
                    i++;

                }
            }
            return resp;
        }
        

        public List<AsocGrupoSolicitud> ListarAsocGrupoSolicitud_Mantenedor(int idGrupoSuspend, int idSolConcesion, int idEstadoVigencia)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbAsocGrupoSolicitud_Mantenedor";

            List<AsocGrupoSolicitud> resp = new List<AsocGrupoSolicitud>();
            AsocGrupoSolicitud grupo = null;


            if (idGrupoSuspend > 0)
            {
                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
            }
            if (idSolConcesion > 0)
            {
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
            }
            if (idEstadoVigencia > 0)
            {
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
            }

            DataTable dt = cnn.Execute();
            if (dt != null)
            {
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grupo = new AsocGrupoSolicitud();
                    grupo.idAsocGrupoSolicitud = Convert.ToInt32(row["idAsocGrupoSolicitud"]);
                    grupo.grupoSuspendido = new GrupoSuspendidos();
                    grupo.grupoSuspendido.idGrupoSuspend = Convert.ToInt32(row["idGrupoSuspend"]);
                    grupo.grupoSuspendido.nombreGrupoSuspend = row["nombreGrupoSuspend"].ToString();
                    grupo.solicitudConcesion = new SolicitudConcesion();
                    grupo.solicitudConcesion.idConcesion = Convert.ToInt32(row["idSolConcesion"]);
                    grupo.solicitudConcesion.numPert = row["numPert"].ToString();
                    grupo.solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                    if (!row.IsNull("fechaRecepcion"))
                    {
                        grupo.solicitudConcesion.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                    }
                    if (!row.IsNull("fechaIngresoTramite"))
                    {
                        grupo.solicitudConcesion.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                    }
                    if (!row.IsNull("idEvaluacionUOT"))
                    {
                        grupo.evaluacionUOT_UE = new EvaluacionUOT_UE();
                        grupo.evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                    }
                    if (!row.IsNull("idDocPestana"))
                    {
                        grupo.docPestana = new DocumentoAmbito();
                        grupo.docPestana.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                    }
                    grupo.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                    grupo.index = i;
                    grupo.accion = accion.LISTADO;

                    resp.Add(grupo);
                    i++;

                }
            }
            return resp;
        }

        public bool GuardarAsocGrupoSolicitud(AsocGrupoSolicitud asocGrupoSol)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsbAsocGrupoSolicitud";
                cnn.parametros.Add("@idAsocGrupoSolicitud", asocGrupoSol.idAsocGrupoSolicitud);
                if (asocGrupoSol.grupoSuspendido != null && asocGrupoSol.grupoSuspendido.idGrupoSuspend>0)
                {
                    cnn.parametros.Add("@idGrupoSuspend", asocGrupoSol.grupoSuspendido.idGrupoSuspend);
                }
                if (asocGrupoSol.solicitudConcesion != null && asocGrupoSol.solicitudConcesion.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", asocGrupoSol.solicitudConcesion.idSolConcesion);
                }
                if (asocGrupoSol.evaluacionUOT_UE != null && asocGrupoSol.evaluacionUOT_UE.idEvaluacionUOT > 0)
                {
                    cnn.parametros.Add("@idEvaluacionUOT", asocGrupoSol.evaluacionUOT_UE.idEvaluacionUOT);
                }
                if (asocGrupoSol.docPestana != null && asocGrupoSol.docPestana.idDocPestana > 0)
                {
                    cnn.parametros.Add("@idDocPestana", asocGrupoSol.docPestana.idDocPestana);
                }
                cnn.parametros.Add("@idEstadoVigencia", asocGrupoSol.estadoVigencia.id);
               
                DataTable dt = cnn.Execute();
                asocGrupoSol.idAsocGrupoSolicitud = Convert.ToInt32(dt.Rows[0]["idAsocGrupoSolicitud"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarGrupoSuspendido(GrupoSuspendidos grupoSusp)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbGrupoSuspendido";

                if (grupoSusp.idGrupoSuspend > 0)
                {
                    cnn.parametros.Add("@idGrupoSuspend", grupoSusp.idGrupoSuspend);
                }
                cnn.parametros.Add("@idTipoAgrupacion", grupoSusp.tipoAgrupacion.id);
                cnn.parametros.Add("@idEstadoVigencia", grupoSusp.estadoVigencia.id);
                cnn.parametros.Add("@codigoCentro", grupoSusp.solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro);
                cnn.parametros.Add("@nombreGrupoSuspend", grupoSusp.nombreGrupoSuspend);

                DataTable dt = cnn.Execute();
                grupoSusp.idGrupoSuspend = Convert.ToInt32(dt.Rows[0]["idGrupoSuspend"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardaAsocArchivoGrupoSusp(int idGrupoSuspend, int idArchivoSusp, bool docFinal, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbAsocArchivoGrupoSusp";

                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
                cnn.parametros.Add("@idArchivoSusp", idArchivoSusp);
                cnn.parametros.Add("@docFinal", docFinal);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

                cnn.Execute();
                
                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarVigenciaAsocArchivoGrupoSusp(int idGrupoSuspend, int idArchivoSusp,int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbAsocArchivoGrupoSusp";

                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
                cnn.parametros.Add("@idArchivoSusp", idArchivoSusp);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

                cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarArchivoGrupoSusp(ArchivoBinario archivoBinario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbArchivoGrupoSusp";

                cnn.parametros.Add("@idArchivoSusp", archivoBinario.idArchivo);
                cnn.parametros.Add("@nombreFisico", archivoBinario.nombreFisico);
                cnn.parametros.Add("@nombreArchivo", archivoBinario.nombreArchivo);
                cnn.parametros.Add("@fechaDeCarga", DateTime.Now);
                cnn.parametros.Add("@tamano", archivoBinario.tamano);
                cnn.parametros.Add("@formato", archivoBinario.formato);
                cnn.parametros.Add("@numero", archivoBinario.numero);


                byte[] file = new byte[archivoBinario.archivo.InputStream.Length];
                archivoBinario.archivo.InputStream.Read(file, 0, file.Length);

                cnn.parametros.Add("@contenido", file);
                if (archivoBinario.observaciones != null)
                {
                    cnn.parametros.Add("@observaciones", archivoBinario.observaciones);
                }

                DataTable dt = cnn.Execute();
                archivoBinario.idArchivo = Convert.ToInt32(dt.Rows[0]["idArchivoSusp"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarAsocArchivoGrupoSusp(int idGrupoSuspend, int idArchivoSusp)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbAsocArchivoGrupoSusp";
                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
                if (idArchivoSusp>0){
                    cnn.parametros.Add("@idArchivoSusp", idArchivoSusp);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizaAsocGrupoSolicitudVigencia(int idAsocGrupoSolicitud, int idGrupoSuspend, int idSolConcesion, int idEvaluacionUOT, int idEstadoVigencia, int idDocPestana)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbAsocGrupoSolicitudVigencia";
                
                if (idAsocGrupoSolicitud > 0)
                {
                    cnn.parametros.Add("@idAsocGrupoSolicitud", idAsocGrupoSolicitud);
                }
                if (idGrupoSuspend > 0)
                {
                    cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
                }
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                if (idEvaluacionUOT > 0)
                {
                    cnn.parametros.Add("@idEvaluacionUOT", idEvaluacionUOT);
                }
                if (idDocPestana > 0)
                {
                    cnn.parametros.Add("@idDocPestana", idDocPestana);
                }

                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;
                
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public List<ArchivoBinario> ListarArchivoGrupoSusp(int idGrupoSuspend, int idArchivoSusp)
        {
            try
            {
                List<ArchivoBinario> resp = new List<ArchivoBinario>();
                ArchivoBinario archivoBinario = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbAsocArchivoGrupoSusp";
                if (idGrupoSuspend>0){
                    cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
                }
                if (idArchivoSusp>0){
                    cnn.parametros.Add("@idArchivoSusp", idArchivoSusp);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        archivoBinario = new ArchivoBinario();
                        archivoBinario.idGrupoSusp = Convert.ToInt32(row["idGrupoSuspend"]);
                        archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoSusp"]);
                        archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        if (!row.IsNull("fechaDeCarga"))
                        {
                            archivoBinario.fecha= Convert.ToDateTime(row["fechaDeCarga"]);
                        }
                         
                        archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoBinario.bytes = (byte[])row["contenido"];
                        if (!row.IsNull("observaciones"))
                        {
                            archivoBinario.observaciones = Convert.ToString(row["observaciones"]);
                        }
                        if (!row.IsNull("numero"))
                        {
                            archivoBinario.numero = Convert.ToString(row["numero"]);
                        }
                        archivoBinario.docFinal = Convert.ToBoolean(row["docFinal"]);
                        archivoBinario.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        resp.Add(archivoBinario);
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

        public bool ActualizaGrupoSuspendidoVigencia(int idGrupoSuspend, int idEstadoVigencia)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbGrupoSuspendidoVigencia";

                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
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

        public bool GuardarEvaluacionUOT_UE(EvaluacionUOT_UE evUOT)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEvaluacionUOT_UE";
                cnn.parametros.Add("@idEvaluacionUOT", evUOT.idEvaluacionUOT);

                if (evUOT.solicitudConcesion != null && evUOT.solicitudConcesion.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", evUOT.solicitudConcesion.idSolConcesion);
                }
                if (evUOT.requiereIT_UOT != null && evUOT.requiereIT_UOT.id >= 0)
                {
                    cnn.parametros.Add("@idRequiereIT_UOT", evUOT.requiereIT_UOT.id);
                }
                if (evUOT.estadoResultado != null && evUOT.estadoResultado.id > 0)
                {
                    cnn.parametros.Add("@idEstadoResultado", evUOT.estadoResultado.id);
                }
                if (evUOT.observaciones != null && !evUOT.observaciones.Equals(""))
                {
                    cnn.parametros.Add("@observaciones", evUOT.observaciones);
                }
                
                DataTable dt = cnn.Execute();
                evUOT.idEvaluacionUOT = Convert.ToInt32(dt.Rows[0]["idEvaluacionUOT"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public EvaluacionUOT_UE ObtieneEvaluacionUOT_UE(int idEvaluacionUOT, int idSolicitud)
        {
            try
            {
                EvaluacionUOT_UE evUOT = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEvaluacionUOT_UE";

                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                if (idEvaluacionUOT > 0)
                {
                    cnn.parametros.Add("@idEvaluacionUOT", idEvaluacionUOT);
                }
               
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        evUOT = new EvaluacionUOT_UE();
                        evUOT.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                        evUOT.solicitudConcesion = new SolicitudConcesion();
                        evUOT.solicitudConcesion.idConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        evUOT.requiereIT_UOT = new ParametroGenerico(Convert.ToInt32(row["idRequiereIT_UOT"]), row["requiereIT_UOT"].ToString());

                        if (!row.IsNull("idEstadoResultado"))
                        {
                            evUOT.estadoResultado = new ParametroGenerico(Convert.ToInt32(row["idEstadoResultado"]), row["estadoResultado"].ToString());
                        }
                        evUOT.observaciones = row["observaciones"].ToString(); 

                    }
                }

                return evUOT;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarEvaluacionUOT_UE(int idSolConcesion, int idEvaluacionUOT)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEvaluacionUOT_UE";
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                if (idEvaluacionUOT > 0)
                {
                    cnn.parametros.Add("@idEvaluacionUOT", idEvaluacionUOT);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }
        public bool GuardarDependenciaSupeditados(DependenciaSupeditados dependenciaSup)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDependenciaSupeditados";
                cnn.parametros.Add("@idDepSupeditado", dependenciaSup.idDepSupeditado);

                if (dependenciaSup.tipoSupeditado != null && dependenciaSup.tipoSupeditado.id > 0)
                {
                    cnn.parametros.Add("@idTipoSupeditado", dependenciaSup.tipoSupeditado.id);
                }
                if (dependenciaSup.solicitudConcesionDep != null && dependenciaSup.solicitudConcesionDep.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesionDep", dependenciaSup.solicitudConcesionDep.idSolConcesion);
                }
                if (dependenciaSup.evaluacionUOT_UE != null && dependenciaSup.evaluacionUOT_UE.idEvaluacionUOT > 0)
                {
                    cnn.parametros.Add("@idEvaluacionUOT", dependenciaSup.evaluacionUOT_UE.idEvaluacionUOT);
                }
                if (dependenciaSup.docPestana != null && dependenciaSup.docPestana.idDocPestana>0)
                {
                    cnn.parametros.Add("@idDocPestana", dependenciaSup.docPestana.idDocPestana);
                }
                if (dependenciaSup.observaciones != null && !dependenciaSup.observaciones.Equals(""))
                {
                    cnn.parametros.Add("@observaciones", dependenciaSup.observaciones);
                }

                DataTable dt = cnn.Execute();
                dependenciaSup.idDepSupeditado = Convert.ToInt32(dt.Rows[0]["idDepSupeditado"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }
        public DependenciaSupeditados ObtieneDependenciaSupeditados(int idSolConcesion, int idDepSupeditado)
        {
            try
            {
                DependenciaSupeditados dependencia = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDependenciaSupeditados";
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                if (idDepSupeditado > 0)
                {
                    cnn.parametros.Add("@idDepSupeditado", idDepSupeditado);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        dependencia = new DependenciaSupeditados();
                        dependencia.idDepSupeditado = Convert.ToInt32(row["idDepSupeditado"]);
                        dependencia.tipoSupeditado = new ParametroGenerico(Convert.ToInt32(row["idTipoSupeditado"]), row["nombreTipoSup"].ToString());
                        dependencia.solicitudConcesionDep = new SolicitudConcesion();
                        dependencia.solicitudConcesionDep.idConcesion = Convert.ToInt32(row["idSolConcesionDep"]);
                        dependencia.solicitudConcesionDep.resolucionSSP = row["descripSSP"].ToString();
                        dependencia.solicitudConcesionDep.itc = row["descripITC"].ToString();
                        dependencia.evaluacionUOT_UE = new EvaluacionUOT_UE();
                        dependencia.evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                        dependencia.docPestana = new DocumentoAmbito();
                        dependencia.docPestana.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        dependencia.observaciones = row["observaciones"].ToString();

                    }
                }

                return dependencia;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public bool EliminarDependenciaSupeditados(int idDepSupeditado, int idSolConcesionDep, int idEvaluacionUOT, int idDocPestana)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDependenciaSupeditados";
                if (idDepSupeditado > 0)
                {
                    cnn.parametros.Add("@idDepSupeditado", idDepSupeditado);
                }
                if (idSolConcesionDep > 0)
                {
                    cnn.parametros.Add("@idSolConcesionDep", idSolConcesionDep);
                }
                if (idEvaluacionUOT > 0)
                {
                    cnn.parametros.Add("@idEvaluacionUOT", idEvaluacionUOT);
                }
                if (idDocPestana > 0)
                {
                    cnn.parametros.Add("@idDocPestana", idDocPestana);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public List<DependenciaSupeditados> ListarDependenciaSupeditados(int idSolConcesion, int idDepSupeditado)
        {
            try
            {
                DependenciaSupeditados dependencia = null;
                List<DependenciaSupeditados> resp = new List<DependenciaSupeditados>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDependenciaSupeditados";
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                if (idDepSupeditado > 0)
                {
                    cnn.parametros.Add("@idDepSupeditado", idDepSupeditado);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        dependencia = new DependenciaSupeditados();
                        dependencia.idDepSupeditado = Convert.ToInt32(row["idDepSupeditado"]);
                        dependencia.tipoSupeditado = new ParametroGenerico(Convert.ToInt32(row["idTipoSupeditado"]), row["nombreTipoSup"].ToString());
                        dependencia.solicitudConcesionDep = new SolicitudConcesion();
                        dependencia.solicitudConcesionDep.idConcesion = Convert.ToInt32(row["idSolConcesionDep"]);
                        dependencia.solicitudConcesionDep.resolucionSSP = row["descripSSP"].ToString();
                        dependencia.solicitudConcesionDep.itc = row["descripITC"].ToString();

                        if (!row.IsNull("idEvaluacionUOT"))
                        {
                            dependencia.evaluacionUOT_UE = new EvaluacionUOT_UE();
                            dependencia.evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                        }

                        if (!row.IsNull("idDocPestana"))
                        {
                            dependencia.docPestana = new DocumentoAmbito();
                            dependencia.docPestana.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        }
                        
                        dependencia.observaciones = row["observaciones"].ToString();
                        dependencia.index = i;
                        dependencia.accion = accion.LISTADO;

                        resp.Add(dependencia);
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
            }
        }

        //procedimiento para listar la solicitud que fué aprobada y las solicitudes que colgaban de esta y que ahora deben rechazarse.
        public List<DependenciaSupeditados> ListarDependenciaSupeditados(int idSolConcesion)
        {
            try
            {
                DependenciaSupeditados dependencia = null;
                List<DependenciaSupeditados> resp = new List<DependenciaSupeditados>();

                SolicitudConcesion solConces = null;
                EvaluacionUOT_UE ev = null;
                DocumentoAmbito doc = null;

                int idSolConcesionDep = 0;
                int idSolConcesionDepAux = 0;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDepSupeditados_SolicitudAprobada";
                
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
               
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        idSolConcesionDep = Convert.ToInt32(row["idSolConcesionDep"]);
                        if(idSolConcesionDep!=idSolConcesionDepAux){
                            dependencia = new DependenciaSupeditados();
                            dependencia.solicitudConcesionDep = new SolicitudConcesion();
                            dependencia.solicitudConcesionDep.idSolConcesion = Convert.ToInt32(row["idSolConcesionDep"]);
                            dependencia.solicitudConcesionDep.numPert = row["numPert"].ToString();
                            dependencia.solicitudConcesionDep.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                            dependencia.solicitudConcesionDep.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                            dependencia.solicitudConcesionDep.resolucionSSP = row["numero"].ToString();
                            dependencia.observaciones = row["observaciones"].ToString();

                            dependencia.evaluacionUOT_UE_List = new List<EvaluacionUOT_UE>();
                            dependencia.docPestana_List = new List<DocumentoAmbito>();

                            idSolConcesionDepAux = idSolConcesionDep;
                        }
                        
                        solConces = new SolicitudConcesion();

                        if (!row.IsNull("idEvaluacionUOT"))
                        {
                            ev = new EvaluacionUOT_UE();
                            ev.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                            ev.solicitudConcesion = new SolicitudConcesion();
                            ev.solicitudConcesion.idSolConcesion = Convert.ToInt32(row["idSolConcesion_EvUOT"]);
                            ev.solicitudConcesion.numPert = row["numPert_EvUOT"].ToString();
                            ev.solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite_EvUOT"]), row["nombreTipoTramite_EvUOT"].ToString());
                            ev.solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial_EvUOT"]), row["nombreTipoUE_EvUOT"].ToString());
                            dependencia.evaluacionUOT_UE_List.Add(ev);
                        }
                        if (!row.IsNull("idDocPestana"))
                        {
                            doc = new DocumentoAmbito();
                            doc.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                            doc.solicitudConcesion_sub = new SolicitudConcesion();
                            doc.solicitudConcesion_sub.idSolConcesion = Convert.ToInt32(row["idSolConcesion_Doc"]);
                            doc.solicitudConcesion_sub.numPert = row["numPert_Doc"].ToString();
                            doc.solicitudConcesion_sub.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite_Doc"]), row["nombreTipo_Doc"].ToString());
                            doc.solicitudConcesion_sub.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial_Doc"]), row["nombreTipoUE_Doc"].ToString());
                            dependencia.docPestana_List.Add(doc);
                        }
                        
                        resp.Add(dependencia);
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
            }
        }

        //procedimiento para listar la solicitud que fué rechazada y las solicitudes que colgaban de esta y que ahora puede avanzar una.
        public List<DependenciaSupeditados> ListarDependenciaSupeditados_Rechazo(int idSolConcesion)
        {
            try
            {
                DependenciaSupeditados dependencia = null;
                List<DependenciaSupeditados> resp = new List<DependenciaSupeditados>();

                SolicitudConcesion solConces = null;
                EvaluacionUOT_UE ev = null;
                DocumentoAmbito doc = null;

                int idSolConcesionDep = 0;
                int idSolConcesionDepAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDepSupeditados_SolicitudRechazada";

                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        idSolConcesionDep = Convert.ToInt32(row["idSolConcesionDep"]);
                        if (idSolConcesionDep != idSolConcesionDepAux)
                        {
                            dependencia = new DependenciaSupeditados();
                            dependencia.solicitudConcesionDep = new SolicitudConcesion();
                            dependencia.solicitudConcesionDep.idSolConcesion = Convert.ToInt32(row["idSolConcesionDep"]);
                            dependencia.solicitudConcesionDep.numPert = row["numPert"].ToString();
                            dependencia.solicitudConcesionDep.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                            dependencia.solicitudConcesionDep.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                            dependencia.solicitudConcesionDep.resolucionSSP = row["numero"].ToString();
                            dependencia.observaciones = row["observaciones"].ToString();

                            dependencia.evaluacionUOT_UE_List = new List<EvaluacionUOT_UE>();
                            dependencia.docPestana_List = new List<DocumentoAmbito>();

                            idSolConcesionDepAux = idSolConcesionDep;
                        }

                        solConces = new SolicitudConcesion();

                        if (!row.IsNull("idEvaluacionUOT"))
                        {
                            ev = new EvaluacionUOT_UE();
                            ev.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                            ev.solicitudConcesion = new SolicitudConcesion();
                            ev.solicitudConcesion.idSolConcesion = Convert.ToInt32(row["idSolConcesion_EvUOT"]);
                            ev.solicitudConcesion.numPert = row["numPert_EvUOT"].ToString();
                            ev.solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite_EvUOT"]), row["nombreTipoTramite_EvUOT"].ToString());
                            ev.solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial_EvUOT"]), row["nombreTipoUE_EvUOT"].ToString());
                            dependencia.evaluacionUOT_UE_List.Add(ev);
                        }
                        if (!row.IsNull("idDocPestana"))
                        {
                            doc = new DocumentoAmbito();
                            doc.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                            doc.solicitudConcesion_sub = new SolicitudConcesion();
                            doc.solicitudConcesion_sub.idSolConcesion = Convert.ToInt32(row["idSolConcesion_Doc"]);
                            doc.solicitudConcesion_sub.numPert = row["numPert_Doc"].ToString();
                            doc.solicitudConcesion_sub.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite_Doc"]), row["nombreTipo_Doc"].ToString());
                            doc.solicitudConcesion_sub.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial_Doc"]), row["nombreTipoUE_Doc"].ToString());
                            dependencia.docPestana_List.Add(doc);
                        }

                        resp.Add(dependencia);
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
            }
        }

        public List<DependenciaSupeditados> ListarDependenciaSupeditadosFiltro(int idSolConcesion, int idDepSupeditado, int idEvalUOT, int idDocPestana)
        {
            try
            {
                DependenciaSupeditados dependencia = null;
                List<DependenciaSupeditados> resp = new List<DependenciaSupeditados>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDependenciaSupeditados_Filtro";
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                if (idDepSupeditado > 0)
                {
                    cnn.parametros.Add("@idDepSupeditado", idDepSupeditado);
                }
                if (idEvalUOT > 0)
                {
                    cnn.parametros.Add("@idEvalUOT", idEvalUOT);
                }

                if (idDocPestana > 0)
                {
                    cnn.parametros.Add("@idDocPestana", idDocPestana);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        dependencia = new DependenciaSupeditados();
                        dependencia.idDepSupeditado = Convert.ToInt32(row["idDepSupeditado"]);
                        dependencia.tipoSupeditado = new ParametroGenerico(Convert.ToInt32(row["idTipoSupeditado"]), row["nombreTipoSup"].ToString());
                        dependencia.solicitudConcesionDep = new SolicitudConcesion();

                        if (!row.IsNull("idSolConcesionDep"))
                        {
                            dependencia.solicitudConcesionDep.idConcesion = Convert.ToInt32(row["idSolConcesionDep"]);
                        }

                        if (!row.IsNull("numPert"))
                        {
                            dependencia.solicitudConcesionDep.numPert = row["numPert"].ToString();
                        }

                        if (!row.IsNull("descripSSP"))
                        {
                            dependencia.solicitudConcesionDep.resolucionSSP = row["descripSSP"].ToString();
                        }

                        if (!row.IsNull("descripITC"))
                        {
                            dependencia.solicitudConcesionDep.itc = row["descripITC"].ToString();
                        }

                        if (!row.IsNull("idEvaluacionUOT"))
                        {
                            dependencia.evaluacionUOT_UE = new EvaluacionUOT_UE();
                            dependencia.evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                        }

                        if (!row.IsNull("idDocPestana"))
                        {
                            dependencia.docPestana = new DocumentoAmbito();
                            dependencia.docPestana.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                        }

                        if (!row.IsNull("observaciones"))
                        {
                            dependencia.observaciones = row["observaciones"].ToString();
                        }

                        dependencia.index = i;
                        dependencia.accion = accion.LISTADO;

                        resp.Add(dependencia);
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
            }
        }

        public List<AsocGrupoSolicitud> ListarAsocGrupoSolicitudFiltro(int idGrupoSuspend, int idSolConcesion, int idEstadoVigencia, int idDocPestana, int idEvalUOT)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbAsocGrupoSolicitud_Filtro";

            List<AsocGrupoSolicitud> resp = new List<AsocGrupoSolicitud>();
            AsocGrupoSolicitud grupo = null;


            if (idGrupoSuspend > 0)
            {
                cnn.parametros.Add("@idGrupoSuspend", idGrupoSuspend);
            }
            if (idSolConcesion > 0)
            {
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
            }
            if (idDocPestana > 0)
            {
                cnn.parametros.Add("@idDocPestana", idDocPestana);
            }
            if (idEvalUOT > 0)
            {
                cnn.parametros.Add("@idEvalUOT", idEvalUOT);
            }
            if (idEstadoVigencia > 0)
            {
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
            }

            DataTable dt = cnn.Execute();
            if (dt != null)
            {
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grupo = new AsocGrupoSolicitud();
                    grupo.idAsocGrupoSolicitud = Convert.ToInt32(row["idAsocGrupoSolicitud"]);
                    grupo.grupoSuspendido = new GrupoSuspendidos();
                    grupo.grupoSuspendido.idGrupoSuspend = Convert.ToInt32(row["idGrupoSuspend"]);
                    grupo.grupoSuspendido.nombreGrupoSuspend = row["nombreGrupoSuspend"].ToString();
                    grupo.solicitudConcesion = new SolicitudConcesion();
                    grupo.solicitudConcesion.idConcesion = Convert.ToInt32(row["idSolConcesion"]);
                    grupo.solicitudConcesion.numPert = row["numPert"].ToString();
                    if (!row.IsNull("fechaRecepcion"))
                    {
                        grupo.solicitudConcesion.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                    }
                    if (!row.IsNull("fechaIngresoTramite"))
                    {
                        grupo.solicitudConcesion.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                    }
                    if (!row.IsNull("idEvaluacionUOT"))
                    {
                        grupo.evaluacionUOT_UE = new EvaluacionUOT_UE();
                        grupo.evaluacionUOT_UE.idEvaluacionUOT = Convert.ToInt32(row["idEvaluacionUOT"]);
                    }
                    if (!row.IsNull("idDocPestana"))
                    {
                        grupo.docPestana = new DocumentoAmbito();
                        grupo.docPestana.idDocPestana = Convert.ToInt32(row["idDocPestana"]);
                    }
                    grupo.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                    grupo.index = i;
                    grupo.accion = accion.LISTADO;

                    resp.Add(grupo);
                    i++;

                }
            }
            return resp;
        }

    }
}
