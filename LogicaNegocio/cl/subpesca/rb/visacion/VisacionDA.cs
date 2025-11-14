using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.visacion
{
    public class VisacionDA
    {
        Logger logger = new Logger();


        /**
         * FILTRA LA TABLA rbSubReq_Visa EN BASE A FILTROS OBTIENE SOLICITUDES 
         */
        public List<VisacionMasiva> ListarSolicitudInicioVisacion(VisacionMasiva filtro)
        {
            try
            {

                VisacionMasiva visacionMasiva = null;
                List<VisacionMasiva> resp = new List<VisacionMasiva>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudVisacion";

                if (filtro.pertFiltro != null && !filtro.pertFiltro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.pertFiltro);
                }

                if (filtro.region != null && filtro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", filtro.region.id);
                }

                if (filtro.provincia != null && filtro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", filtro.provincia.id);
                }

                if (filtro.comunaFiltro != null && filtro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", filtro.comunaFiltro.id);
                }

                
                cnn.parametros.Add("@idTipoTramite", filtro.tipoTramite.id);
                cnn.parametros.Add("@idUsuario", filtro.usuario.id_usuario);
                    

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        visacionMasiva = new VisacionMasiva();

                        visacionMasiva.tipoVisacion = filtro.tipoVisacion;
                        visacionMasiva.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        visacionMasiva.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                        visacionMasiva.estado = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                        visacionMasiva.tipoVisacion = filtro.tipoVisacion;

                        if (!row.IsNull("subTipoTramite"))
                        {
                            visacionMasiva.subTipoTramite = row["subTipoTramite"].ToString();
                        }

                        if (!row.IsNull("IdRegion"))
                        { 
                            visacionMasiva.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                        }

                        if (!row.IsNull("IdProvincia"))
                        {
                            visacionMasiva.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                        }

                        if (!row.IsNull("comunasCad"))
                        {
                            visacionMasiva.comunas = row["comunasCad"].ToString();
                        }

                        if (!row.IsNull("numPert"))
                        {
                            visacionMasiva.pert = row["numPert"].ToString();
                        }

                        resp.Add(visacionMasiva);
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



        /**
         * FILTRA LA TABLA rbSubReq_Visa EN BASE A FILTROS OBTIENE REQUERIMIENTOS PARA VISAR O FIRMAR
         */
        public List<VisacionMasiva> ListarSolicitudVisacionFirma(VisacionMasiva filtro)
        {
            try
            {

                VisacionMasiva visacionMasiva = null;
                List<VisacionMasiva> resp = new List<VisacionMasiva>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudVisacionFirma";

                
                cnn.parametros.Add("@idTipoVisacion", filtro.tipoVisacion.id);

                if (filtro.resultado != null && filtro.resultado.id > 0)
                {
                    cnn.parametros.Add("@idResultado", filtro.resultado.id);
                }

                if (filtro.tipoVisacion.id  == rbSubRequerimiento.INFORME_TECNICO &&  filtro.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
                {
                    cnn.parametros.Add("@idTipoUE", rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA);
                }
                

                if (filtro.pertFiltro != null && !filtro.pertFiltro.Trim().Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.pertFiltro);
                }

                if (filtro.region != null && filtro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", filtro.region.id);
                }

                if (filtro.provincia != null && filtro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", filtro.provincia.id);
                }

                if (filtro.comunaFiltro != null && filtro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", filtro.comunaFiltro.id);
                }

                cnn.parametros.Add("@idTipoTramite", filtro.tipoTramite.id);


                if (filtro.buscaVisaOFirma > 0)
                {
                    cnn.parametros.Add("@buscaVisaOFirma", filtro.buscaVisaOFirma);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        visacionMasiva = new VisacionMasiva();

                        visacionMasiva.tipoVisacion = filtro.tipoVisacion;
                        visacionMasiva.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);

                        visacionMasiva.idDocGeneral = Convert.ToInt32(row["idDocGeneral"]);
                        visacionMasiva.idDocPestana = Convert.ToInt32(row["idDocPestana"]);


                        visacionMasiva.requerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());

                        visacionMasiva.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                        visacionMasiva.estado = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                        visacionMasiva.tipoVisacion = filtro.tipoVisacion;

                        if (!row.IsNull("subTipoTramite"))
                        {
                            visacionMasiva.subTipoTramite = row["subTipoTramite"].ToString();
                        }

                        if (!row.IsNull("IdRegion"))
                        {
                            visacionMasiva.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                        }

                        if (!row.IsNull("IdProvincia"))
                        {
                            visacionMasiva.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                        }

                        if (!row.IsNull("comunasCad"))
                        {
                            visacionMasiva.comunas = row["comunasCad"].ToString();
                        }

                        if (!row.IsNull("numPert"))
                        {
                            visacionMasiva.pert = row["numPert"].ToString();
                        }

                        resp.Add(visacionMasiva);
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



        /**
        * FILTRA VALIDACION LA TABLA rbSubReq_Visa PARA OBTENER EL REQUERIMIENTO DE INICIO DE VISACION
        */
        public Requerimiento ObtenerDocumentoInicioVisacion(int idSubRequerimiento, int idResultado,  int idTipoTramite)
        {
            try
            {

                Requerimiento requerimiento = null;
                DocumentoAmbito documentoAmbito = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSubReq_VisaValidacion";

                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

                if (idResultado > 0)
                {
                    cnn.parametros.Add("@idResultado", idResultado);
                }

                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                



                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        requerimiento = new Requerimiento();
                        requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                        documentoAmbito = new DocumentoAmbito();

                        requerimiento.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]));

                        if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
                        {
                            requerimiento.tipoSalida = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]));
                            requerimiento.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]));
                        }

                        if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                        {
                            requerimiento.tipoEntrada = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]));
                            requerimiento.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]));
                        }

                        requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]));



                        documentoAmbito.accion = accion.INGRESAR;
                        documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]));
                        documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]));
                        documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                        documentoAmbito.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);
                        

                        requerimiento.ambitoTipo.Add(documentoAmbito);


                        if (Convert.ToBoolean(row["numero"]))
                        {
                            requerimiento.numero = "9999";
                        }
                        if (Convert.ToBoolean(row["fecha"]))
                        {
                            requerimiento.fecha = new DateTime(1950, 1, 1);
                        }
                        if (Convert.ToBoolean(row["numeroCI"]))
                        {
                            requerimiento.numeroCI = 9999;
                        }
                        if (Convert.ToBoolean(row["fechaCI"]))
                        {
                            requerimiento.fechaCI = new DateTime(1950, 1, 1);
                        }

                    }
                        
                    

                }

                return requerimiento;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        /**
        * FILTRA VALIDACION LA TABLA rbSubReq_Visa PARA OBTENER EL REQUERIMIENTO DE INICIO DE VISACION
        */
        public Requerimiento ObtenerDocumentoVisacionFirma(int idSubRequerimiento, int idSubReqAbierto, int idResultado, int idTipoTramite, bool corrige, int idDocPestanaAbierta)
        {
            try
            {

                Requerimiento requerimiento = null;
                DocumentoAmbito documentoAmbito = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSubReq_VisaFirmaCorrigeValidacion";

                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);
                cnn.parametros.Add("@idSubReqAbierto", idSubReqAbierto);

                if (idResultado > 0)
                {
                    cnn.parametros.Add("@idResultado", idResultado);
                }

                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                cnn.parametros.Add("@corrige", corrige);
                


                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (Convert.ToBoolean(row["esRespuesta"])) //si es verdadero, entonces es la respuesta a una firma de jefatura
                        {


                            requerimiento = new Requerimiento();
                            requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                            documentoAmbito = new DocumentoAmbito();

                            requerimiento.flujoDocumental = new ParametroGenerico(rbTipo.ENTRADA);
                            requerimiento.tipoEntrada = new ParametroGenerico(rbTipo.RESPUESTA_A_UN_REQUERIMIENTO);
                            requerimiento.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]));
                            requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]));

                            documentoAmbito.accion = accion.INGRESAR;
                            documentoAmbito.idDocPestana = idDocPestanaAbierta; //es la el id de la firma que se esta respondiendo
                            documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]));
                            documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]));
                            documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                            documentoAmbito.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);


                            requerimiento.ambitoTipo.Add(documentoAmbito);


                            if (Convert.ToBoolean(row["numero"]))
                            {
                                requerimiento.numero = "9999";
                            }
                            if (Convert.ToBoolean(row["fecha"]))
                            {
                                requerimiento.fecha = new DateTime(1950, 1, 1);
                            }
                            if (Convert.ToBoolean(row["numeroCI"]))
                            {
                                requerimiento.numeroCI = 9999;
                            }
                            if (Convert.ToBoolean(row["fechaCI"]))
                            {
                                requerimiento.fechaCI = new DateTime(1950, 1, 1);
                            }


                        }
                        else {

                            requerimiento = new Requerimiento();
                            requerimiento.ambitoTipo = new List<DocumentoAmbito>();
                            documentoAmbito = new DocumentoAmbito();

                            requerimiento.flujoDocumental = new ParametroGenerico(rbTipo.SALIDA);
                            requerimiento.tipoSalida = new ParametroGenerico(rbTipo.REQUERIMIENTO_CON_RESPUESTA);
                            requerimiento.destinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]));
                            requerimiento.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]));

                            documentoAmbito.accion = accion.INGRESAR;
                            documentoAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]));
                            documentoAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]));
                            documentoAmbito.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]));
                            documentoAmbito.estadoVigencia = new ParametroGenerico(rbEstadosGenerales.VIGENTE);


                            requerimiento.ambitoTipo.Add(documentoAmbito);


                            if (Convert.ToBoolean(row["numero"]))
                            {
                                requerimiento.numero = "9999";
                            }
                            if (Convert.ToBoolean(row["fecha"]))
                            {
                                requerimiento.fecha = new DateTime(1950, 1, 1);
                            }
                            if (Convert.ToBoolean(row["numeroCI"]))
                            {
                                requerimiento.numeroCI = 9999;
                            }
                            if (Convert.ToBoolean(row["fechaCI"]))
                            {
                                requerimiento.fechaCI = new DateTime(1950, 1, 1);
                            }
                        
                        }
                    }
                }

                return requerimiento;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
        * VALIDA LA EXISTENCIA DE UN GRUPO DE PERTS
        **/
        public String ValidarExistenciaPerts(VisacionMasiva visacionMasiva)
        {
            try
            {
                String mensaje = "";

                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionValidacion";

                cnn.parametros.Add("@idTipoTramite", visacionMasiva.tipoTramite.id);
                cnn.parametros.Add("@numPert", visacionMasiva.pertFiltro);

                if (visacionMasiva.region != null && visacionMasiva.region.id > 0) {
                    cnn.parametros.Add("@idRegion", visacionMasiva.region.id);
                }

                if (visacionMasiva.provincia != null && visacionMasiva.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", visacionMasiva.provincia.id);
                }

                if (visacionMasiva.comunaFiltro != null && visacionMasiva.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", visacionMasiva.comunaFiltro.id);
                }


                DataTable dt = cnn.Execute();

                string pertAux = "";

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                       
                        pertAux = Convert.ToString(row["pert"]);

                        if(mensaje.Equals(""))
                        {
                            mensaje = pertAux;
                        }
                        else
                        {
                            mensaje = mensaje + ", " + pertAux;
                        }
                    }

                }

                return mensaje;
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
