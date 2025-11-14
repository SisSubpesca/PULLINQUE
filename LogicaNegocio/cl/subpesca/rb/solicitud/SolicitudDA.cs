using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades;
using Datos.Contantes;
using System.Globalization;
using Datos.Entidades.Relocalizacion;


namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class SolicitudDA
    {
        Logger logger = new Logger();
        
        public object ListarSolicitudesPendientes(int IdSolicitud)
        {
            throw new NotImplementedException();
        }

        public bool GuardarSolicitud(SolicitudConcesion solicitudConcesion, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesion";

                cnn.parametros.Add("@idSolConcesion", solicitudConcesion.idSolConcesion);

                //if (solicitudConcesion.tipoBarrio != null && solicitudConcesion.tipoBarrio.id > 0)
                //{
                //    cnn.parametros.Add("@idTipoBarrio", solicitudConcesion.tipoBarrio.id);
                //}
                if (solicitudConcesion.provincia != null && solicitudConcesion.provincia.id > 0)
                {
                    cnn.parametros.Add("@IdProvincia", solicitudConcesion.provincia.id);
                }
                //if (solicitudConcesion.macrozona != null && solicitudConcesion.macrozona.id_macrozona > 0)
                //{
                //    cnn.parametros.Add("@IdMacrozona", solicitudConcesion.macrozona.id_macrozona);
                //}
                if (solicitudConcesion.barrio != null && solicitudConcesion.barrio.id_barrio > 0)
                {
                    cnn.parametros.Add("@IdBarrio", solicitudConcesion.barrio.id_barrio); //ACS
                }
                if (solicitudConcesion.acm != null && solicitudConcesion.acm.id_barrio > 0)
                {
                    cnn.parametros.Add("@idACM", solicitudConcesion.acm.id_barrio); //ACM
                }
                if (solicitudConcesion.estadoActual != null && solicitudConcesion.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstadoActual", solicitudConcesion.estadoActual.id);
                }
                if (solicitudConcesion.estadoAnterior != null && solicitudConcesion.estadoAnterior.id > 0)
                {
                    cnn.parametros.Add("@idEstadoAnterior", solicitudConcesion.estadoAnterior.id);
                }
                if (solicitudConcesion.estadoPosterior != null && solicitudConcesion.estadoPosterior.id > 0)
                {
                    cnn.parametros.Add("@idEstadoPosterior", solicitudConcesion.estadoPosterior.id);
                }
                if (solicitudConcesion.numPert != null && !solicitudConcesion.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudConcesion.numPert);
                }

                cnn.parametros.Add("@reqAntecTerreno", solicitudConcesion.reqAntecTerreno);
                cnn.parametros.Add("@reqRegularizacion", solicitudConcesion.reqRegularizacion);
                if (solicitudConcesion.fechaRecepcion != null && solicitudConcesion.fechaRecepcion!=default(DateTime))
                {
                    cnn.parametros.Add("@fechaRecepcion", solicitudConcesion.fechaRecepcion);
                }
                if (solicitudConcesion.fechaIngresoTramite != null && solicitudConcesion.fechaIngresoTramite != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramite", solicitudConcesion.fechaIngresoTramite);
                }
                cnn.parametros.Add("@idTipoUnidEspacial", solicitudConcesion.tipoUnidadEspacial.id);
                if (solicitudConcesion.tipoTramite != null && solicitudConcesion.tipoTramite.id>0)
                {
                    cnn.parametros.Add("@idTipoTramite", solicitudConcesion.tipoTramite.id);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                solicitudConcesion.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public SolicitudConcesion ObtieneResolucionSSP_ITDAC_Solicitud(int idSolConcesion)
        {
            try
            {
                SolicitudConcesion solicitudResp = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionSSP_ITDAC_Solicitud";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        solicitudResp = new SolicitudConcesion();
                        solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        solicitudResp.tieneIT = Convert.ToBoolean(row["suma2"]);//si es CERO ESTÁ PENDIENTE
                        solicitudResp.tieneSSP = Convert.ToBoolean(row["suma"]);//si es CERO ESTÁ PENDIENTE
                    }
                }
                return solicitudResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        //GUARDA ASOCIACION SOLICITUD-COMUNA
        public bool GuardarComunaSolicitud(int idSolicitud, int idComuna)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbComunaSolicitud";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                cnn.parametros.Add("@IdComuna", idComuna);

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
        //ELIMINA ASOCIACION SOLICITUD-COMUNA
        public bool EliminarComunaSolicitud(int idSolicitud, int idComuna)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbComunaSolicitud";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if (idComuna>0)
                {
                    cnn.parametros.Add("@idComuna", idComuna);
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

        /**
        * OBTIENE UNA SOLICITUD DE CONCESION
        */
        public SolicitudConcesion ObtieneSolicitudConcesion(int idSolConcesion, int idUsuario)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                EstadoIsla islaAux = null;
                HashSet<int> clavesIsla = new HashSet<int>();
                //bool seaAux;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesion";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                if (idUsuario>0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.traspasoOk = Convert.ToBoolean(row["traspasoOk"]);
                            solicitudResp.tieneAsignadaSolicitud = Convert.ToBoolean(row["tienePermiso"]);
                            if (!row.IsNull("recursoReposicion"))
                            {
                                solicitudResp.recursoReposicion = Convert.ToBoolean(row["recursoReposicion"]);
                            }

                            //if (!row.IsNull("idTipoBarrio"))
                            //{
                            //    solicitudResp.tipoBarrio = new ParametroGenerico(Convert.ToInt32(row["idTipoBarrio"]), row["nombreTipoBarrio"].ToString());
                            //}
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            
                            if (!row.IsNull("verificaCertOperacion"))
                            {
                                solicitudResp.verificaCertOperacion = new ParametroGenerico(Convert.ToInt32(row["verificaCertOperacion"]), "");
                            }
                            if (!row.IsNull("tramitaUTS"))
                            {
                                solicitudResp.tramitaUTS = new ParametroGenerico(Convert.ToInt32(row["tramitaUTS"]), "");
                            }
                            if (!row.IsNull("evaluaUOT"))
                            {
                                solicitudResp.evaluaUOT = new ParametroGenerico(Convert.ToInt32(row["evaluaUOT"]), "");
                            }
                            if (!row.IsNull("sspaNivelCentral"))
                            {
                                solicitudResp.sspaNivelCentral = new ParametroGenerico(Convert.ToInt32(row["sspaNivelCentral"]), "");
                            }
                            
                            /*if (!row.IsNull("evaluaUOT_Amb"))
                            {
                                solicitudResp.evaluaUOT_Amb = new ParametroGenerico(Convert.ToInt32(row["evaluaUOT_Amb"]), "");
                            }*/
                            
                            if (!row.IsNull("idCuerpoAgua"))
                            {
                                solicitudResp.cuerpoAgua = new CuerpoDeAgua();
                                solicitudResp.cuerpoAgua.idCuerpoDeAgua = Convert.ToInt32(row["idCuerpoAgua"]);
                                solicitudResp.cuerpoAgua.nombreCuerpoAgua = row["nombreCuerpoAgua"].ToString();
                                if (!row.IsNull("idTipoCuerpoAgua"))
                                {
                                    solicitudResp.cuerpoAgua.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(row["idTipoCuerpoAgua"]), row["nombreTipoCA"].ToString());
                                }
                            }

                            //if (!row.IsNull("IdMacrozona"))
                            //{
                            //    solicitudResp.macrozona = new Macrozona();
                            //    solicitudResp.macrozona.id_macrozona = Convert.ToInt32(row["IdMacrozona"]);
                            //    solicitudResp.macrozona.macrozona = row["Macrozona"].ToString();
                            //}
                            if (!row.IsNull("IdBarrio"))
                            {
                                solicitudResp.barrio = new Barrio();
                                solicitudResp.barrio.id_barrio = Convert.ToInt32(row["IdBarrio"]);
                                solicitudResp.barrio.barrio = row["Barrio"].ToString();
                            }
                            if (!row.IsNull("idACM"))
                            {
                                solicitudResp.acm = new Barrio();
                                solicitudResp.acm.id_barrio = Convert.ToInt32(row["idACM"]);
                                solicitudResp.acm.barrio = row["nombreACM"].ToString();
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstadoActual"].ToString());
                                if (!row.IsNull("nodoCantidad"))
                                {
                                    solicitudResp.estadoActual.clave = row["nodoCantidad"].ToString();
                                }
                                
                            }
                            if (!row.IsNull("docReq"))
                            {
                                solicitudResp.docRequerido = row["docReq"].ToString();
                            }

                            if (!row.IsNull("idEstadoAnterior"))
                            {
                                solicitudResp.estadoAnterior = new ParametroGenerico(Convert.ToInt32(row["idEstadoAnterior"]), row["nombreEstadoAnterior"].ToString());
                            }
                            if (!row.IsNull("idEstadoPosterior"))
                            {
                                solicitudResp.estadoPosterior = new ParametroGenerico(Convert.ToInt32(row["idEstadoPosterior"]), row["nombreEstadoPosterior"].ToString());
                            }
                            if (!row.IsNull("idEstadoVigencia"))
                            {
                                solicitudResp.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                            }

                            if (!row.IsNull("numPert"))
                            {
                                solicitudResp.numPert = row["numPert"].ToString();
                            }
                            
                            if (!row.IsNull("reqAntecTerreno"))
                            {
                                solicitudResp.reqAntecTerreno = Convert.ToBoolean(row["reqAntecTerreno"]);
                            }

                            if (!row.IsNull("reqRegularizacion"))
                            {
                                solicitudResp.reqRegularizacion = Convert.ToBoolean(row["reqRegularizacion"]);
                            }

                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("fechaIngresoSistema"))
                            {
                                solicitudResp.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                            }

                            if (!row.IsNull("idTipoUnidEspacial"))
                            {
                                solicitudResp.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipo"].ToString());
                            }

                            if (!row.IsNull("idAplicaDependencia"))
                            {
                                solicitudResp.aplicaDependencia = Convert.ToInt32(row["idAplicaDependencia"]);                                
                            }

                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }

                            if (!row.IsNull("idTipoTramite"))
                            {
                                solicitudResp.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                            }

                            if (!row.IsNull("sometimientoSEA"))
                            {
                                solicitudResp.sometimientoSEA = new ParametroGenerico(Convert.ToInt32(row["sometimientoSEA"]), row["nomSometeSEA"].ToString());
                                
                            }
                            solicitudResp.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);


                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.islas = new List<EstadoIsla>();
                            clavesIsla = new HashSet<int>();
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                            solicitudResp.comuna.Add(comunaResp);
                        }
                        if (!row.IsNull("idEstadoActualIsla"))
                        {
                            if (clavesIsla.Count == 0 || !clavesIsla.Contains(Convert.ToInt32(row["idEstadoActualIsla"])))
                            {
                                clavesIsla.Add(Convert.ToInt32(row["idEstadoActualIsla"]));
                                //--
                                islaAux = new EstadoIsla();
                                islaAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActualIsla"]), row["nombreEstadoIsla"].ToString());
                                islaAux.tipoIsla = new ParametroGenerico(Convert.ToInt32(row["idTipoIsla"]), row["nombreTipoIsla"].ToString());
                                solicitudResp.islas.Add(islaAux);
                            }
                        }

                        idSolConcesionAux = idSolConcesionVar;

                    }
                }
                return solicitudResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        /**
       * Lista las solicitudes de acuerdo a los filtros definidos en interfaz de administrador.
       */
        public List<SolicitudConcesion> ListarSolicitudConcesionAdmin(SolicitudConcesion filtro)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                Persona pers = null;
                ParametroGenerico comuna = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionAdmin";

                if (filtro.numPert!= null && !filtro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.numPert);
                }
                if (filtro.region!=null && filtro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", filtro.region.id);
                }
                if (filtro.provincia!=null && filtro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", filtro.provincia.id);
                }
                if (filtro.comunaFiltro != null && filtro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", filtro.comunaFiltro.id);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                {
                    cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante !=null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                }

                if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                }
                if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                }
                if (filtro.estadoActual != null && filtro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", filtro.estadoActual.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                        
                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudAux.numPert = row["numPert"].ToString();
                            }
                            
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }

                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);
                            
                            solicitudAux.comuna = new List<ParametroGenerico>();
                            solicitudAux.titularesSolConcesion = new List<Persona>();
                            
                            resp.Add(solicitudAux);
                            clavesComuna = new HashSet<int>();
                            clavesTitular = new HashSet<int>();
                        }
                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count==0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                            
                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudAux.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudAux.titularesSolConcesion.Add(pers);
                            }
                        }
                        
                        idSolicitudAux = idSolicitud;

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
       * Lista las solicitudes de centro de acopio de acuerdo a los filtros definidos en interfaz de administrador.
       */
        public List<SolicitudConcesion> ListarSolicitudCentroAcopioAdmin(SolicitudConcesion filtro)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                Persona pers = null;
                ParametroGenerico comuna = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudAcopioAdmin";

                if (filtro.numPert != null && !filtro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.numPert);
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
                if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                {
                    cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante !=null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                }

                if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                }
                if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                }
                if (filtro.estadoActual != null && filtro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", filtro.estadoActual.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudAux.numPert = row["numPert"].ToString();
                            }

                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }

                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                            solicitudAux.comuna = new List<ParametroGenerico>();
                            solicitudAux.titularesSolConcesion = new List<Persona>();

                            resp.Add(solicitudAux);
                            clavesComuna = new HashSet<int>();
                            clavesTitular = new HashSet<int>();
                        }
                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);

                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudAux.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudAux.titularesSolConcesion.Add(pers);
                            }
                        }

                        idSolicitudAux = idSolicitud;

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
 * Lista las solicitudes de centro de faenamiento de acuerdo a los filtros definidos en interfaz de administrador.
 */
        public List<SolicitudConcesion> ListarSolicitudCentroFaenamientoAdmin(SolicitudConcesion filtro)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                Persona pers = null;
                ParametroGenerico comuna = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudFaenamientoAdmin";

                if (filtro.numPert != null && !filtro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.numPert);
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
                if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                {
                    cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                }

                if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                }
                if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                }
                if (filtro.estadoActual != null && filtro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", filtro.estadoActual.id);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudAux.numPert = row["numPert"].ToString();
                            }

                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }

                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                            solicitudAux.comuna = new List<ParametroGenerico>();
                            solicitudAux.titularesSolConcesion = new List<Persona>();

                            resp.Add(solicitudAux);
                            clavesComuna = new HashSet<int>();
                            clavesTitular = new HashSet<int>();
                        }
                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);

                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudAux.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudAux.titularesSolConcesion.Add(pers);
                            }
                        }

                        idSolicitudAux = idSolicitud;

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
 * Lista las solicitudes de centro de faenamiento de acuerdo a los filtros definidos en interfaz de administrador.
 */
        public List<SolicitudConcesion> ListarSolicitudAcuiculturaAmerbAdmin(SolicitudConcesion filtro)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                Persona pers = null;
                ParametroGenerico comuna = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudAmerbAdmin";

                if (filtro.numPert != null && !filtro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.numPert);
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
                if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                {
                    cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                }

                if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                }
                if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                }
                if (filtro.estadoActual != null && filtro.estadoActual.id>0)
                {
                    cnn.parametros.Add("@idEstado", filtro.estadoActual.id);
                }

                if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.amerbVista != null && filtro.datosSolicitudUE.amerbVista.id>0)
                {
                    cnn.parametros.Add("@codAmerbSNP", filtro.datosSolicitudUE.amerbVista.id);
                }
                if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.numeroCI != null && filtro.datosSolicitudUE.numeroCI > 0)
                {
                    cnn.parametros.Add("@numeroCI", filtro.datosSolicitudUE.numeroCI);
                }
                if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.fechaCI != default(DateTime))
                {
                    cnn.parametros.Add("@fechaCI", filtro.datosSolicitudUE.fechaCI);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                        
                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudAux.numPert = row["numPert"].ToString();
                            }

                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }

                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            if (!row.IsNull("numeroCI"))
                            {
                                solicitudAux.datosSolicitudUE.numeroCI = Convert.ToInt32(row["numeroCI"]);
                            }
                            if (!row.IsNull("fechaCI"))
                            {
                                solicitudAux.datosSolicitudUE.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                            }
                            if (!row.IsNull("codAmerbSNP"))
                            {
                                solicitudAux.datosSolicitudUE.codAmerb = row["codAmerbSNP"].ToString();
                                solicitudAux.datosSolicitudUE.amerbVista = new ParametroGenerico(Convert.ToInt32(row["codAmerbSNP"]), "");
                                if (!row.IsNull("descripcionAmerb"))
                                {
                                    solicitudAux.datosSolicitudUE.amerbVista.descripcion = row["descripcionAmerb"].ToString();
                                }
                            }
                           

                            solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                            solicitudAux.comuna = new List<ParametroGenerico>();
                            solicitudAux.titularesSolConcesion = new List<Persona>();

                            resp.Add(solicitudAux);
                            clavesComuna = new HashSet<int>();
                            clavesTitular = new HashSet<int>();
                        }
                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);

                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudAux.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudAux.titularesSolConcesion.Add(pers);
                            }
                        }

                        idSolicitudAux = idSolicitud;

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
* Lista las solicitudes de centro de faenamiento de acuerdo a los filtros definidos en interfaz de administrador.
*/
        public List<SolicitudConcesion> ListarSolicitudColectorSemillaAdmin(SolicitudConcesion filtro)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                Persona pers = null;
                ParametroGenerico comuna = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudColectoresAdmin";

                if (filtro.numPert != null && !filtro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", filtro.numPert);
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
                if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                }
                if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.numIdentSolicitud > 0)
                {
                    cnn.parametros.Add("@numIdentSolicitud", filtro.datosSolicitudUE.numIdentSolicitud);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                {
                    cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                }
                if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                }

                if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                }
                if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                }
                if (filtro.estadoActual != null && filtro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", filtro.estadoActual.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }

                            if (!row.IsNull("numIdentSolicitud"))
                            {
                                solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                solicitudAux.datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(row["numIdentSolicitud"]);
                            }

                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }

                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                            solicitudAux.comuna = new List<ParametroGenerico>();
                            solicitudAux.titularesSolConcesion = new List<Persona>();

                            resp.Add(solicitudAux);
                            clavesComuna = new HashSet<int>();
                            clavesTitular = new HashSet<int>();
                        }
                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);

                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudAux.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudAux.titularesSolConcesion.Add(pers);
                            }
                        }

                        idSolicitudAux = idSolicitud;

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
     * Obtiene vertice asoiciado a un poligono en particular.
     */
        public List<ParametroGenerico> ObtieneListaTitulares(string nombreTitular)
        {
            try
            {
                ParametroGenerico paramResp = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTitularesSolicitud";
                if(nombreTitular!=null && !nombreTitular.Equals("")){
                    cnn.parametros.Add("@nombre", nombreTitular);
                }
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        paramResp = new ParametroGenerico(Convert.ToInt32(row["rutPersona"]), row["descripcionPersona"].ToString());
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


      
        //GUARDA SOLICITUD DE CONCESION INICIAL
        public bool GuardarSolicitudConcesionInicial(SolicitudConcesion solicitudInicial)
        {
	        try{

                    Conexion cnn = new Conexion();
                    cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                    cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                    cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                    cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                    cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                    cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_CONCESION);
                    cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                    DataTable dt = cnn.Execute();
                    solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                    return true;
                 }
                 catch (Exception ex)
                 {
                     logger.PrintError(ex);
                     logger.SendMailError(ex);
                     return false;
                 };
        }

        //GUARDA SOLICITUD DE CENTRO DE ACOPIO
        public bool GuardarSolicitudCentroAcopio(SolicitudConcesion solicitudInicial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO);
                cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                DataTable dt = cnn.Execute();
                solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        //GUARDA SOLICITUD DE CENTRO DE FAENAMIENTO
        public bool GuardarSolicitudCentroFaenamiento(SolicitudConcesion solicitudInicial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO);
                cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                DataTable dt = cnn.Execute();
                solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }



        //GUARDA SOLICITUD DE ACUICULTURA AMERB
        public bool GuardarSolicitudAcuiculturaAmerb(SolicitudConcesion solicitudInicial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);
                cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                DataTable dt = cnn.Execute();
                solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        //GUARDA SOLICITUD DE EXPERIMENTALES EN  AMERB
        public bool GuardarSolicitudExperimentalesAmerb(SolicitudConcesion solicitudInicial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB);
                cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                DataTable dt = cnn.Execute();
                solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }





        //GUARDA SOLICITUD DE COLECTORES DE SEMILLA
        public bool GuardarSolicitudColectoresSemilla(SolicitudConcesion solicitudInicial)
        {
            try
            { 

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                if (solicitudInicial.numPert != null)
                {
                    cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                }
                if (solicitudInicial.fechaRecepcion != null && solicitudInicial.fechaRecepcion != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                }
                if (solicitudInicial.fechaIngresoTramite != null && solicitudInicial.fechaIngresoTramite != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                }

                cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA);
                cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                DataTable dt = cnn.Execute();
                solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);


                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        



        //lista de requerimientos pendientes asociados a los titulares de una solicitud
        public List<Requerimiento> ListarPequerimientosPendPersona(int idSolicitud, int rutPersona)
        {
            try
            {
                Persona persAux = null;
                Requerimiento requAux = null;
                DocumentoAmbito docAmbito = null;
                List<Requerimiento> resp = new List<Requerimiento>();
                int tramite = 0;
                int TramiteAux = 0;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDocumentosGeneralesPersona";
                if (rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutPersona);
                }
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        tramite = Convert.ToInt32(row["idSolConcesion"]);
                        if (tramite != TramiteAux)
                        {
                            requAux = new Requerimiento();
                            requAux.ambitoTipo = new List<DocumentoAmbito>();
                            docAmbito = new DocumentoAmbito();
                            if (!row.IsNull("idSubRequerimiento"))
                            {
                                docAmbito.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                            }
                            requAux.archivoAdjunto = new ArchivoBinario();
                            
                            if (!row.IsNull("idArchivoBinSC"))
                            {                             
                                requAux.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                            }

                            docAmbito.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                            requAux.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                            docAmbito.idRequerimiento = Convert.ToInt32(row["idDocGeneral"]);
                            requAux.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipo"].ToString());
                            requAux.solicitud = new SolicitudConcesion();
                            requAux.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            requAux.solicitud.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTram"].ToString());
                            if (!row.IsNull("numPert"))
                            {
                                requAux.solicitud.numPert = row["numPert"].ToString();
                            }
                            
                            requAux.ambitoTipo.Add(docAmbito);
                            requAux.personas = new List<Persona>();
                            resp.Add(requAux);

                        }
                        requAux.titularesCad = (row["titularesCad"].ToString());

                        /*persAux = new Persona();
                        persAux.rutPersona = Convert.ToInt32(row["rut"]);
                        persAux.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                        persAux.nombreSolicitante = row["nombre"].ToString();
                        requAux.personas.Add(persAux);*/

                        TramiteAux = tramite;

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

        public bool ActualizaSolicitudSEA(int idSolicitud, int aplicaSEA,int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbSolicitudConcesionSEA";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if (aplicaSEA > 0) {
                    cnn.parametros.Add("@seaSi", aplicaSEA);
                }
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

        public bool aplicaPertExistente(string numPert)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionPert";
                cnn.parametros.Add("@numPert", numPert);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       if(!row.IsNull("idSolConcesion")&& !row.IsNull("numPert")){
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
                return true;
            }
        }

        public SolicitudConcesion ObtieneConcesionExistente(string codigoSiep, int idTipoTramite)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                SolicitudConcesion solicitudResp = null;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();
                Persona pers = null;
                ParametroGenerico comuna = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCentroExistente";
                cnn.parametros.Add("@codigoSiep", codigoSiep);
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }

                            if (!row.IsNull("numPert"))
                            {
                                 solicitudResp.numPert = row["numPert"].ToString();
                            }
                            
                            solicitudResp.unidadEspacial = new UnidadEspacial();
                            solicitudResp.unidadEspacial.idSolicitud = solicitudResp.idSolConcesion;
                            solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                            solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            if (!row.IsNull("nombreCentro"))
                            {
                                solicitudResp.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentro"].ToString();
                            }
                            
                            if (!row.IsNull("IdRegion"))
                            { 
                               solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString()); 
                            }

                            if (!row.IsNull("IdBarrio"))
                            {
                                solicitudResp.barrio = new Barrio();
                                solicitudResp.barrio.id_barrio = Convert.ToInt32(row["IdBarrio"]);
                                solicitudResp.barrio.barrio = row["Barrio"].ToString();
                            }
                            
                            solicitudResp.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                            solicitudResp.superficieCalculada = Convert.ToSingle(row["superficieCalculada"]);
                            solicitudResp.superficieCalculadaCultivo = Convert.ToSingle(row["superficieCalculadaCultivo"]);
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                          
                           
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count==0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                            
                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(pers);
                            }
                        }


                        idSolConcesionAux = idSolConcesionVar;

                    }
                }
                return solicitudResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public SolicitudConcesion Obtiene_UE_Existente(string codigoSiep, int idUnidEspacial)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                SolicitudConcesion solicitudResp = null;
                int idComuna = 0;
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();
                Persona pers = null;
                ParametroGenerico comuna = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRb_UE_Existente";
                cnn.parametros.Add("@codigoSiep", codigoSiep);
                cnn.parametros.Add("@idTipoUnidadEspacial", idUnidEspacial);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }

                            if (!row.IsNull("numPert"))
                            {
                                solicitudResp.numPert = row["numPert"].ToString();
                            }

                            solicitudResp.unidadEspacial = new UnidadEspacial();
                            solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                            solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            if (!row.IsNull("nombreCentro"))
                            {
                                solicitudResp.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentro"].ToString();
                            }

                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }

                            if (!row.IsNull("IdBarrio"))
                            {
                                solicitudResp.barrio = new Barrio();
                                solicitudResp.barrio.id_barrio = Convert.ToInt32(row["IdBarrio"]);
                                solicitudResp.barrio.barrio = row["Barrio"].ToString();
                            }

                            solicitudResp.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                            solicitudResp.superficieCalculada = Convert.ToSingle(row["superficieCalculada"]);
                            solicitudResp.superficieCalculadaCultivo = Convert.ToSingle(row["superficieCalculadaCultivo"]);
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.titularesSolConcesion = new List<Persona>();


                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);

                                comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comuna);
                            }
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(pers);
                            }
                        }


                        idSolConcesionAux = idSolConcesionVar;

                    }
                }
                return solicitudResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public bool GuardarTipoModificacionSolicitud(int idSolConcesion, int idTipoModificacion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoModificacionSolicitud";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);

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

        public bool GuardarSolModificacionConcesInicial(SolicitudConcesion solicitudInicial)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolModificacionConcesInicial";
                cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                if (solicitudInicial.numPert != null && !solicitudInicial.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                }
                if(solicitudInicial.fechaRecepcion!=null && solicitudInicial.fechaRecepcion!=default(DateTime)){
                    cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                }
                if (solicitudInicial.fechaIngresoTramite != null && solicitudInicial.fechaIngresoTramite!=default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                }

                cnn.parametros.Add("@idTipoUnidEspacial", solicitudInicial.tipoUnidadEspacial.id);
                cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                DataTable dt = cnn.Execute();
                solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        
        public SolicitudConcesion VerificaUnidadEspacialTitularVigente(int idTipoUnidadEspacial, string codSiep, int rutTitular)
        {
            try
            {
                SolicitudConcesion solicitudConcesion = null;
                Persona persona = null;

                int idSolConcesion = 0;
                int idSolConcesionAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbConcesionCodSiepValido";
                if (idTipoUnidadEspacial > 0)
                {
                    cnn.parametros.Add("@idTipoUnidEspacial", idTipoUnidadEspacial);
                }
                if (!codSiep.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", codSiep);
                }
                if (rutTitular > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutTitular);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesion != idSolConcesionAux)
                        {

                            solicitudConcesion = new SolicitudConcesion();
                            solicitudConcesion.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudConcesion.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), "");
                            if (!row.IsNull("numPert"))
                            {
                                solicitudConcesion.numPert = row["numPert"].ToString();
                            }
                            solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), "");
                            solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), "");
                            solicitudConcesion.unidadEspacial = new UnidadEspacial();
                            solicitudConcesion.unidadEspacial.idUnidadEspacial = Convert.ToInt32(row["idUnidadEspacial"]);
                            solicitudConcesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                            solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();

                            solicitudConcesion.titularesSolConcesion = new List<Persona>();
                        }

                        if (!row.IsNull("rutPersona"))
                        {
                            persona = new Persona();
                            persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                            persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                            persona.nombreSolicitante = row["nombre"].ToString();
                            solicitudConcesion.titularesSolConcesion.Add(persona);
                        }
                        idSolConcesionAux = idSolConcesion;

                    }
                }

                return solicitudConcesion;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }
        
        /**
        public SolicitudConcesion ObtieneCodSiepTitulares(int codSiep, int rutTitular)
        {
            try
            {
                SolicitudConcesion solicitudConcesion = null;
                Persona persona = null;

                int idSolConcesion = 0;
                int idSolConcesionAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbConcesionCodSiepTitulares";
                if(codSiep>0){
                    cnn.parametros.Add("@codigoCentro", codSiep);
                }
                if(rutTitular>0){
                    cnn.parametros.Add("@rutPersona", rutTitular);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                       idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                       if (idSolConcesion!= idSolConcesionAux){

                           solicitudConcesion = new SolicitudConcesion();
                           solicitudConcesion.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                           solicitudConcesion.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), "");
                           if (!row.IsNull("numPert"))
                           {
                                solicitudConcesion.numPert = row["numPert"].ToString();
                           }
                           solicitudConcesion.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), "");
                           solicitudConcesion.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), "");
                           solicitudConcesion.unidadEspacial = new UnidadEspacial();
                           solicitudConcesion.unidadEspacial.idUnidadEspacial = Convert.ToInt32(row["idUnidadEspacial"]);
                           solicitudConcesion.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                           solicitudConcesion.unidadEspacial.centrosDeCultivo.codigoCentro = Convert.ToInt32(row["codigoCentro"]);
                               
                           solicitudConcesion.titularesSolConcesion = new List<Persona>();
                        }
                       persona = new Persona();
                       persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                       persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                       persona.nombreSolicitante = row["nombre"].ToString();
                       solicitudConcesion.titularesSolConcesion.Add(persona);

                       idSolConcesionAux = idSolConcesion;

                    }
                }

                return solicitudConcesion;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }
         * 
         */

        public bool aplicaPertExistenteModConcesion(string numPert)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudPert_ModConcesion";
                cnn.parametros.Add("@numPert", numPert);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idSolConcesion") && !row.IsNull("numPert"))
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
                return true;
            }
        }

        public SolicitudConcesion ObtieneSolicitudConcesionMod(int idSolConcesion, int idUsuario)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idTipoModificacion = 0;
                int idComuna = 0;
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipo = null;
                HashSet<int> clavesTipoMod = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionMod";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.tieneAsignadaSolicitud = Convert.ToBoolean(row["tienePermiso"]);
                            //if (!row.IsNull("idTipoBarrio"))
                            //{
                            //    solicitudResp.tipoBarrio = new ParametroGenerico(Convert.ToInt32(row["idTipoBarrio"]), row["nombreTipoBarrio"].ToString());
                            //}
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }

                            //if (!row.IsNull("sometimientoCPS_INFAS"))
                            //{
                            //    solicitudResp.sometimientoCPS_INFAS = new ParametroGenerico(Convert.ToInt32(row["sometimientoCPS_INFAS"]), "");
                            //}
                            if (!row.IsNull("verificaCertOperacion"))
                            {
                                solicitudResp.verificaCertOperacion = new ParametroGenerico(Convert.ToInt32(row["verificaCertOperacion"]), "");
                            }








                            if (!row.IsNull("tramitaUTS"))
                            {
                                solicitudResp.tramitaUTS = new ParametroGenerico(Convert.ToInt32(row["tramitaUTS"]), "");
                            }
                            if (!row.IsNull("evaluaUOT"))
                            {
                                solicitudResp.evaluaUOT = new ParametroGenerico(Convert.ToInt32(row["evaluaUOT"]), "");
                            }
                            if (!row.IsNull("sspaNivelCentral"))
                            {
                                solicitudResp.sspaNivelCentral = new ParametroGenerico(Convert.ToInt32(row["sspaNivelCentral"]), "");
                            }
                           
                            //if (!row.IsNull("IdMacrozona"))
                            //{
                            //    solicitudResp.macrozona = new Macrozona();
                            //    solicitudResp.macrozona.id_macrozona = Convert.ToInt32(row["IdMacrozona"]);
                            //    solicitudResp.macrozona.macrozona = row["Macrozona"].ToString();
                            //}
                            if (!row.IsNull("IdBarrio"))
                            {
                                solicitudResp.barrio = new Barrio();
                                solicitudResp.barrio.id_barrio = Convert.ToInt32(row["IdBarrio"]);
                                solicitudResp.barrio.barrio = row["Barrio"].ToString();
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]));
                            }
                            if (!row.IsNull("idEstadoAnterior"))
                            {
                                solicitudResp.estadoAnterior = new ParametroGenerico(Convert.ToInt32(row["idEstadoAnterior"]));
                            }
                            if (!row.IsNull("idEstadoPosterior"))
                            {
                                solicitudResp.estadoPosterior = new ParametroGenerico(Convert.ToInt32(row["idEstadoPosterior"]));
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudResp.numPert = row["numPert"].ToString();
                            }

                            if (!row.IsNull("reqAntecTerreno"))
                            {
                                solicitudResp.reqAntecTerreno = Convert.ToBoolean(row["reqAntecTerreno"]);
                            }

                            if (!row.IsNull("reqRegularizacion"))
                            {
                                solicitudResp.reqRegularizacion = Convert.ToBoolean(row["reqRegularizacion"]);
                            }

                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("fechaIngresoSistema"))
                            {
                                solicitudResp.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                            }

                            if (!row.IsNull("idTipoUnidEspacial"))
                            {
                                solicitudResp.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipo"].ToString());
                            }

                            if (!row.IsNull("sometimientoSEA"))
                            {
                                solicitudResp.sometimientoSEA = new ParametroGenerico(Convert.ToInt32(row["sometimientoSEA"]), row["nomSometeSEA"].ToString());

                            }

                            if (!row.IsNull("superficieTramReq"))
                            {
                                solicitudResp.superficieTramReq =Convert.ToSingle(row["superficieTramReq"]);
                            }

                            if (!row.IsNull("superficieTramFinal"))
                            {
                                solicitudResp.superficieTramFinal = Convert.ToSingle(row["superficieTramFinal"]);
                            }

                            if (!row.IsNull("idTramiteMod"))
                            {
                                solicitudResp.tramiteModConcesion = new TramiteModConcesion();
                                solicitudResp.tramiteModConcesion.idTramiteMod = Convert.ToInt32(row["idTramiteMod"]);
                                solicitudResp.tramiteModConcesion.centro = new ParametroGenerico();
                                solicitudResp.tramiteModConcesion.centro.id = Convert.ToInt32(row["codigoSiep"]);
                                solicitudResp.tramiteModConcesion.titular = new Persona();
                                solicitudResp.tramiteModConcesion.titular.rutPersona = Convert.ToInt32(row["rutTitular"]);
                                solicitudResp.tramiteModConcesion.titular.dvPersona = Convert.ToChar(row["dvTitular"]);
                            }

                            if (!row.IsNull("superficieCalculada"))
                            {
                                solicitudResp.superficieCalculada = Convert.ToSingle(row["superficieCalculada"]);
                            }


                            solicitudResp.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);


                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            clavesTipoMod = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }

                        
                        if (!row.IsNull("idTipoModificacion"))
                        {
                            idTipoModificacion = Convert.ToInt32(row["idTipoModificacion"]);
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(idTipoModificacion))
                            {
                                clavesTipoMod.Add(idTipoModificacion);

                                tipo = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nombreTipoModifcacion"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipo);
                            }
                        }


                        idSolConcesionAux = idSolConcesionVar;

                    }
                }
                return solicitudResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool GuardarSolicitudMod(SolicitudConcesion solicitudConcesion, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSolicitudConcesionMod";

                cnn.parametros.Add("@idSolConcesion", solicitudConcesion.idSolConcesion);

                //if (solicitudConcesion.tipoBarrio != null && solicitudConcesion.tipoBarrio.id > 0)
                //{
                //    cnn.parametros.Add("@idTipoBarrio", solicitudConcesion.tipoBarrio.id);
                //}
                if (solicitudConcesion.provincia != null && solicitudConcesion.provincia.id > 0)
                {
                    cnn.parametros.Add("@IdProvincia", solicitudConcesion.provincia.id);
                }
                //if (solicitudConcesion.macrozona != null && solicitudConcesion.macrozona.id_macrozona > 0)
                //{
                //    cnn.parametros.Add("@IdMacrozona", solicitudConcesion.macrozona.id_macrozona);
                //}
                if (solicitudConcesion.barrio != null && solicitudConcesion.barrio.id_barrio > 0)
                {
                    cnn.parametros.Add("@IdBarrio", solicitudConcesion.barrio.id_barrio);
                }
                if (solicitudConcesion.estadoActual != null && solicitudConcesion.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstadoActual", solicitudConcesion.estadoActual.id);
                }
                if (solicitudConcesion.estadoAnterior != null && solicitudConcesion.estadoAnterior.id > 0)
                {
                    cnn.parametros.Add("@idEstadoAnterior", solicitudConcesion.estadoAnterior.id);
                }
                if (solicitudConcesion.estadoPosterior != null && solicitudConcesion.estadoPosterior.id > 0)
                {
                    cnn.parametros.Add("@idEstadoPosterior", solicitudConcesion.estadoPosterior.id);
                }
                if (solicitudConcesion.numPert != null && !solicitudConcesion.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudConcesion.numPert);
                }

                cnn.parametros.Add("@reqAntecTerreno", solicitudConcesion.reqAntecTerreno);
                cnn.parametros.Add("@reqRegularizacion", solicitudConcesion.reqRegularizacion);
                if (solicitudConcesion.fechaRecepcion != null && solicitudConcesion.fechaRecepcion != default(DateTime))
                {
                    cnn.parametros.Add("@fechaRecepcion", solicitudConcesion.fechaRecepcion);
                }
                if (solicitudConcesion.fechaIngresoTramite != null && solicitudConcesion.fechaIngresoTramite != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramite", solicitudConcesion.fechaIngresoTramite);
                }
                cnn.parametros.Add("@idTipoUnidEspacial", solicitudConcesion.tipoUnidadEspacial.id);

                if (solicitudConcesion.superficieTramFinal != null && solicitudConcesion.superficieTramFinal > 0)
                {
                    cnn.parametros.Add("@superficieTramFinal", solicitudConcesion.superficieTramFinal);
                }
                if (solicitudConcesion.superficieTramReq != null && solicitudConcesion.superficieTramReq > 0)
                {
                    cnn.parametros.Add("@superficieTramReq", solicitudConcesion.superficieTramReq);
                }

                cnn.parametros.Add("@idTipoTramite", solicitudConcesion.tipoTramite.id);

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }


                DataTable dt = cnn.Execute();
                solicitudConcesion.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarTramiteModConcesion(TramiteModConcesion tramiteModConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTramiteModConcesion";
                cnn.parametros.Add("@idTramiteMod", tramiteModConcesion.idTramiteMod);
                cnn.parametros.Add("@idSolConcesion", tramiteModConcesion.idSolConcesion);
                cnn.parametros.Add("@codigoSiep", tramiteModConcesion.centro.id);
                cnn.parametros.Add("@rutTitular", tramiteModConcesion.titular.rutPersona);
                cnn.parametros.Add("@dvTitular", tramiteModConcesion.titular.dvPersona);

                DataTable dt = cnn.Execute();
                tramiteModConcesion.idTramiteMod = Convert.ToInt32(dt.Rows[0]["idTramiteMod"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public List<SolicitudConcesion> ListarSolicitudConcesionTramite(int codigoCentro, int idSolConcesion)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                ParametroGenerico tipoMod = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionTramites";

                cnn.parametros.Add("@codigoCentro", codigoCentro);
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudAux.unidadEspacial = new UnidadEspacial();
                            solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                            solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            solicitudAux.unidadEspacial.centrosDeCultivo.nombreCentro = row["codigoCentro"].ToString();
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudAux.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudAux.numPert = row["numPert"].ToString();
                            }

                            solicitudAux.tipoModificacionesTram = new List<ParametroGenerico>();
                            resp.Add(solicitudAux);
                        }
                            //--
                        tipoMod = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nombreTipoMod"].ToString());
                        solicitudAux.tipoModificacionesTram.Add(tipoMod);

                        idSolicitudAux = idSolicitud;
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

        public List<SolicitudConcesion> ListarSolicitud_UE_Tramite(int codigoCentro, int idSolConcesion, int idTipoTramite,int idTipoUnidEspacial)
        {
            try
            {
                SolicitudConcesion solicitudAux = null;
                ParametroGenerico tipoMod = null;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                int idSolicitud = 0;
                int idSolicitudAux = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitud_UE_Tramites";

                cnn.parametros.Add("@codigoCentro", codigoCentro);
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                cnn.parametros.Add("@idTipoUnidEspacial", idTipoUnidEspacial);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                        if (idSolicitud != idSolicitudAux)
                        {
                            solicitudAux = new SolicitudConcesion();
                            solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudAux.unidadEspacial = new UnidadEspacial();
                            solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                            solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            solicitudAux.unidadEspacial.centrosDeCultivo.nombreCentro = row["codigoCentro"].ToString();
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudAux.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            if (!row.IsNull("numPert"))
                            {
                                solicitudAux.numPert = row["numPert"].ToString();
                            }

                            solicitudAux.tipoModificacionesTram = new List<ParametroGenerico>();
                            resp.Add(solicitudAux);
                        }
                        //--
                        tipoMod = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nombreTipoMod"].ToString());
                        solicitudAux.tipoModificacionesTram.Add(tipoMod);

                        idSolicitudAux = idSolicitud;
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

        public List<SolicitudConcesion> ListarSolicitudConcesionModAdmin_Tramite(SolicitudConcesion solicitudFiltro)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idComuna = 0;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipoModifica = null;
                Persona persona = null;
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTipoMod = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionModAdmin_Tramite";
                
                if(solicitudFiltro.numPert!=null && !solicitudFiltro.numPert.Equals("")){
                    cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                }
                if(solicitudFiltro.unidadEspacial!=null && solicitudFiltro.unidadEspacial.centrosDeCultivo!=null && !solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals("")){
                    cnn.parametros.Add("@codigoCentro", solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                if(solicitudFiltro.comunaFiltro!=null && solicitudFiltro.comunaFiltro.id>0){
                    cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                }
                if(solicitudFiltro.provincia!=null && solicitudFiltro.provincia.id>0){
                    cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                }
                if(solicitudFiltro.region!=null && solicitudFiltro.region.id>0){
                    cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                }
                if(solicitudFiltro.titularFiltro!=null && solicitudFiltro.titularFiltro.rutPersona>0){
                    cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                }
                if(solicitudFiltro.titularFiltro!=null && solicitudFiltro.titularFiltro.dvPersona!=null){
                    cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                }
               if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals("")){
                    cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                }
                
                if(solicitudFiltro.fechaRangoFiltro1!=null && solicitudFiltro.fechaRangoFiltro1!=default(DateTime)){
                    cnn.parametros.Add("@fechaIngresoTramiteIni", solicitudFiltro.fechaRangoFiltro1);
                }
                if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteFin", solicitudFiltro.fechaRangoFiltro2);
                }
                if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                }
                if (solicitudFiltro.tipoModificacion != null && solicitudFiltro.tipoModificacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoMod", solicitudFiltro.tipoModificacion.id);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.numPert = row["numPert"].ToString();
                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            resp.Add(solicitudResp);
                            clavesTipoMod = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersona"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersona"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersona"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                persona.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(persona);
                            }
                        }

                        if (!row.IsNull("idTipoModificacion"))
                        {
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(Convert.ToInt32(row["idTipoModificacion"])))
                            {
                                clavesTipoMod.Add(Convert.ToInt32(row["idTipoModificacion"]));
                                tipoModifica = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nomTipoMod"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipoModifica);
                            }

                        }
                            
                        idSolConcesionAux = idSolConcesionVar;

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

        public List<SolicitudConcesion> ListarSolicitud_UE_ModAdmin_Tramite(SolicitudConcesion solicitudFiltro)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idComuna = 0;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipoModifica = null;
                Persona persona = null;
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTipoMod = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitud_UE_ModAdmin_Tramite";

                cnn.parametros.Add("@idTipoTramite", solicitudFiltro.tipoTramite.id);
                cnn.parametros.Add("@idTipoUnidEspacial", solicitudFiltro.tipoUnidadEspacial.id);

                if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                }
                if (solicitudFiltro.unidadEspacial != null && solicitudFiltro.unidadEspacial.centrosDeCultivo != null && !solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                }
                if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                }
                if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona != null)
                {
                    cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                }

                if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteIni", solicitudFiltro.fechaRangoFiltro1);
                }
                if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteFin", solicitudFiltro.fechaRangoFiltro2);
                }
                if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                }
                if (solicitudFiltro.tipoModificacion != null && solicitudFiltro.tipoModificacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoMod", solicitudFiltro.tipoModificacion.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.numPert = row["numPert"].ToString();
                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            resp.Add(solicitudResp);
                            clavesTipoMod = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersona"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersona"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersona"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                persona.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(persona);
                            }
                        }

                        if (!row.IsNull("idTipoModificacion"))
                        {
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(Convert.ToInt32(row["idTipoModificacion"])))
                            {
                                clavesTipoMod.Add(Convert.ToInt32(row["idTipoModificacion"]));
                                tipoModifica = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nomTipoMod"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipoModifica);
                            }

                        }

                        idSolConcesionAux = idSolConcesionVar;

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

        public bool ActualizaSolicitud_AplicaDep(int idSolicitud, int idAplicaDependencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbSolicitudConcesion_AplicaDep";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                if (idAplicaDependencia>0)
                {
                    cnn.parametros.Add("@idAplicaDependencia", idAplicaDependencia);
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

        public bool EliminarSolicitudConcesionMod(int idSolConcesion, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbSolicitudConcesionMod";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idUsuario", idUsuario);
               
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


        public bool EliminarSolicitud_UE_Mod(int idSolConcesion, int idTipoTramSolicitud, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "dbo.paDelRbSolicitud_UE_Mod";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                cnn.parametros.Add("@idTipoTramSolicitud", idTipoTramSolicitud);
                cnn.parametros.Add("@idUsuario", idUsuario);

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

        
        public List<SolicitudConcesion> ListarSolicitudConcesionModAdmin_Aprobada(SolicitudConcesion solicitudFiltro)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idComuna = 0;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipoModifica = null;
                Persona persona = null;
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTipoMod = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionModAdmin_Aprobada";

                if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                }
                if (solicitudFiltro.unidadEspacial != null && solicitudFiltro.unidadEspacial.centrosDeCultivo != null && !solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                }
                if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                }
                if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona != null)
                {
                    cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                }

                if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteIni", solicitudFiltro.fechaRangoFiltro1);
                }
                if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteFin", solicitudFiltro.fechaRangoFiltro2);
                }
                if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                }
                if (solicitudFiltro.tipoModificacion != null && solicitudFiltro.tipoModificacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoMod", solicitudFiltro.tipoModificacion.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.numPert = row["numPert"].ToString();
                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            resp.Add(solicitudResp);
                            clavesTipoMod = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersona"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersona"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersona"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                persona.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(persona);
                            }
                        }
                        if (!row.IsNull("idTipoModificacion"))
                        {
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(Convert.ToInt32(row["idTipoModificacion"])))
                            {
                                clavesTipoMod.Add(Convert.ToInt32(row["idTipoModificacion"]));
                                tipoModifica = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nomTipoMod"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipoModifica);
                            }
                        
                        }

                        idSolConcesionAux = idSolConcesionVar;

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
        public List<SolicitudConcesion> ListarSolicitud_UE_ModAdmin_Aprobada(SolicitudConcesion solicitudFiltro)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idComuna = 0;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipoModifica = null;
                Persona persona = null;
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTipoMod = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitud_UE_ModAdmin_Aprobada";

                cnn.parametros.Add("@idTipoTramite", solicitudFiltro.tipoTramite.id);
                cnn.parametros.Add("@idTipoUnidEspacial", solicitudFiltro.tipoUnidadEspacial.id);
                if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                }
                if (solicitudFiltro.unidadEspacial != null && solicitudFiltro.unidadEspacial.centrosDeCultivo != null && !solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                }
                if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                }
                if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona != null)
                {
                    cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                }

                if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteIni", solicitudFiltro.fechaRangoFiltro1);
                }
                if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteFin", solicitudFiltro.fechaRangoFiltro2);
                }
                if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                }
                if (solicitudFiltro.tipoModificacion != null && solicitudFiltro.tipoModificacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoMod", solicitudFiltro.tipoModificacion.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.numPert = row["numPert"].ToString();
                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            resp.Add(solicitudResp);
                            clavesTipoMod = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersona"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersona"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersona"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                persona.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(persona);
                            }
                        }
                        if (!row.IsNull("idTipoModificacion"))
                        {
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(Convert.ToInt32(row["idTipoModificacion"])))
                            {
                                clavesTipoMod.Add(Convert.ToInt32(row["idTipoModificacion"]));
                                tipoModifica = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nomTipoMod"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipoModifica);
                            }

                        }

                        idSolConcesionAux = idSolConcesionVar;

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

        public List<SolicitudConcesion> ListarSolicitudConcesionModAdmin_Rechazada(SolicitudConcesion solicitudFiltro)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idComuna = 0;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipoModifica = null;
                Persona persona = null;
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTipoMod = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionModAdmin_Rechazada";

                if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                }
                if (solicitudFiltro.unidadEspacial != null && solicitudFiltro.unidadEspacial.centrosDeCultivo != null && !solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                }
                if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                }
                if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona != null)
                {
                    cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                }

                if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteIni", solicitudFiltro.fechaRangoFiltro1);
                }
                if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteFin", solicitudFiltro.fechaRangoFiltro2);
                }
                if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                }
                if (solicitudFiltro.tipoModificacion != null && solicitudFiltro.tipoModificacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoMod", solicitudFiltro.tipoModificacion.id);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.numPert = row["numPert"].ToString();
                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            clavesTipoMod = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                            
                            resp.Add(solicitudResp);
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersona"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersona"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersona"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                persona.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(persona);
                            }
                        }

                        if (!row.IsNull("idTipoModificacion"))
                        {
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(Convert.ToInt32(row["idTipoModificacion"])))
                            {
                                clavesTipoMod.Add(Convert.ToInt32(row["idTipoModificacion"]));
                                tipoModifica = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nomTipoMod"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipoModifica);
                            }

                        }

                        idSolConcesionAux = idSolConcesionVar;

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

        public List<SolicitudConcesion> ListarSolicitud_UE_ModAdmin_Rechazada(SolicitudConcesion solicitudFiltro)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                int idComuna = 0;
                List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                SolicitudConcesion solicitudResp = null;
                ParametroGenerico comunaResp = null;
                ParametroGenerico tipoModifica = null;
                Persona persona = null;
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTipoMod = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitud_UE_ModAdmin_Rechazada";

                cnn.parametros.Add("@idTipoTramite", solicitudFiltro.tipoTramite.id);
                cnn.parametros.Add("@idTipoUnidEspacial", solicitudFiltro.tipoUnidadEspacial.id);

                if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                {
                    cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                }
                if (solicitudFiltro.unidadEspacial != null && solicitudFiltro.unidadEspacial.centrosDeCultivo != null && !solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codigoCentro", solicitudFiltro.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                }
                if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                {
                    cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                }
                if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona != null)
                {
                    cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                }
                if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                {
                    cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                }

                if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteIni", solicitudFiltro.fechaRangoFiltro1);
                }
                if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIngresoTramiteFin", solicitudFiltro.fechaRangoFiltro2);
                }
                if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                {
                    cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                }
                if (solicitudFiltro.tipoModificacion != null && solicitudFiltro.tipoModificacion.id > 0)
                {
                    cnn.parametros.Add("@idTipoMod", solicitudFiltro.tipoModificacion.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new SolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            solicitudResp.numPert = row["numPert"].ToString();
                            if (!row.IsNull("codigoCentro"))
                            {
                                solicitudResp.unidadEspacial = new UnidadEspacial();
                                solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                            }
                            if (!row.IsNull("IdRegion"))
                            {
                                solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                            }
                            if (!row.IsNull("IdProvincia"))
                            {
                                solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                            }
                            if (!row.IsNull("fechaRecepcion"))
                            {
                                solicitudResp.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                            }
                            if (!row.IsNull("fechaIngresoTramite"))
                            {
                                solicitudResp.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                            }
                            solicitudResp.titularesSolConcesion = new List<Persona>();
                            solicitudResp.tipoModificacionesTram = new List<ParametroGenerico>();
                            solicitudResp.comuna = new List<ParametroGenerico>();
                            clavesTipoMod = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                            clavesComuna = new HashSet<int>();

                            resp.Add(solicitudResp);
                        }

                        if (!row.IsNull("IdComuna"))
                        {
                            idComuna = Convert.ToInt32(row["IdComuna"]);
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                            {
                                clavesComuna.Add(idComuna);
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                solicitudResp.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersona"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersona"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersona"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                persona.nombreSolicitante = row["nombre"].ToString();
                                solicitudResp.titularesSolConcesion.Add(persona);
                            }
                        }

                        if (!row.IsNull("idTipoModificacion"))
                        {
                            if (clavesTipoMod.Count == 0 || !clavesTipoMod.Contains(Convert.ToInt32(row["idTipoModificacion"])))
                            {
                                clavesTipoMod.Add(Convert.ToInt32(row["idTipoModificacion"]));
                                tipoModifica = new ParametroGenerico(Convert.ToInt32(row["idTipoModificacion"]), row["nomTipoMod"].ToString());
                                solicitudResp.tipoModificacionesTram.Add(tipoModifica);
                            }

                        }

                        idSolConcesionAux = idSolConcesionVar;

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

        public bool aplicaBotonSolicitudUE(int idSolicitud)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudAplicaBotonUE";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idSolAux") && row.IsNull("reqOk"))
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
                return true;
            }
        }

        public bool ActualizaSolicitud_Traspaso(int idSolicitud, bool traspasoOk)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbSolicitudConcesionTraspaso";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                cnn.parametros.Add("@traspasoOk", traspasoOk);

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


        /*
         * REPLICA UNA SOLICITUD DE CONCESION DE A PARTIR DE UN SECTOR "CREA" DE RELOCALIZACION
         */
        public int GuardarNuevaConcesion(int idSolicitud, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                int idSolConcesNueva;
                cnn.procedimiento = "paInsRbNuevaConcesion";
                cnn.parametros.Add("@idSolTramiteMod", idSolicitud);
                cnn.parametros.Add("@idUsuario", idUsuario);

                DataTable dt = cnn.Execute();
                idSolConcesNueva = Convert.ToInt32(dt.Rows[0]["idConcesionSol"]);

                return idSolConcesNueva;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            };
        }


        public bool GuardarDatosSolicitudUE(DatosSolicitudUE datosSolicitud, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDatosSolicitudUE";
                
                cnn.parametros.Add("@idDatosSolicitud", datosSolicitud.idDatosSolicitud);
                cnn.parametros.Add("@idSolConcesion", datosSolicitud.idSolConcesion);
                if (datosSolicitud.personaVerificacion != null && datosSolicitud.personaVerificacion.rutPersona > 0)
                {
                    cnn.parametros.Add("@rutVerificacion", datosSolicitud.personaVerificacion.rutPersona);
                }
                cnn.parametros.Add("@numIdentSolicitud", datosSolicitud.numIdentSolicitud);
                cnn.parametros.Add("@numeroCI", datosSolicitud.numeroCI);
                
                if (datosSolicitud.fechaCI != null && datosSolicitud.fechaCI != default(DateTime))
                {
                    cnn.parametros.Add("@fechaCI", datosSolicitud.fechaCI);
                }
                if (datosSolicitud.personaVerificacion!= null && datosSolicitud.personaVerificacion.dvPersona != null)
                {
                    cnn.parametros.Add("@digitoVerificador", datosSolicitud.personaVerificacion.dvPersona);
                }
                if (datosSolicitud.oficina != null && datosSolicitud.oficina.clave != null && !datosSolicitud.oficina.clave.Trim().Equals("") && !datosSolicitud.oficina.clave.Trim().Equals("0"))
                {
                    cnn.parametros.Add("@codDirZonal", datosSolicitud.oficina.clave);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
             
                DataTable dt = cnn.Execute();
                datosSolicitud.idDatosSolicitud = Convert.ToInt32(dt.Rows[0]["idDatosSolicitud"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool aplicaNumIdentificadorExistente(int identificador)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDatosSolicitudNumIdent";
                cnn.parametros.Add("@numIdentSolicitud", identificador);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idDatosSolicitud") && !row.IsNull("numIdentSolicitud"))
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
                return true;
            }
        }

         public DatosSolicitudUE ObtieneDatosSolicitudUE(int idSolicitud)
        {
            try
            {
                DatosSolicitudUE datoSol = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDatosSolicitudUE";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       datoSol = new DatosSolicitudUE();
                       datoSol.idDatosSolicitud = Convert.ToInt32(row["idDatosSolicitud"]);
                       datoSol.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (!row.IsNull("rutVerificacion")){
                            datoSol.personaVerificacion = new Persona();
                            datoSol.personaVerificacion.rutPersona = Convert.ToInt32(row["rutVerificacion"]);
                            datoSol.personaVerificacion.dvPersona = Convert.ToChar(row["dvVerificacion"]); 
                        }
                        if (!row.IsNull("numIdentSolicitud"))
                        {
                            datoSol.numIdentSolicitud = Convert.ToInt32(row["numIdentSolicitud"]); 
                        }
                        if (!row.IsNull("numeroCI"))
                        {
                            datoSol.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if (!row.IsNull("fechaCI"))
                        {
                            datoSol.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        if (!row.IsNull("cultivoExperimental"))
                        {
                            datoSol.cultivoExperimental = new ParametroGenerico(Convert.ToInt32(row["cultivoExperimental"]), "");
                        }
                        if (!row.IsNull("estadoFirmaConvenio"))
                        {
                            datoSol.estadoFirmaConvenio = new ParametroGenerico(Convert.ToInt32(row["estadoFirmaConvenio"]), "");
                        }
                        if (!row.IsNull("estadoSeguimientoDia"))
                        {
                            datoSol.estadoSeguimientoDia = new ParametroGenerico(Convert.ToInt32(row["estadoSeguimientoDia"]), "");
                        }
                        if (!row.IsNull("idTipoNumeroZonal"))
                        {
                            datoSol.tipoNumeroZonal = new ParametroGenerico(Convert.ToInt32(row["idTipoNumeroZonal"]), "");
                        }
                        if (!row.IsNull("idTipoCentro"))
                        {
                            datoSol.tipoCentro = new ParametroGenerico(Convert.ToInt32(row["idTipoCentro"]), row["tipoCentro"].ToString());
                        }
                        if (!row.IsNull("idTipoEvaluacion"))
                        {
                            datoSol.tipoCentro = new ParametroGenerico(Convert.ToInt32(row["idTipoEvaluacion"]), row["tipoEvaluacion"].ToString());
                        }
                        if (!row.IsNull("superficieSectorAmerb"))
                        {
                            datoSol.superficieSectorAmerb = Convert.ToSingle(row["superficieSectorAmerb"]);
                        }
                        if (!row.IsNull("porcentSectorAmerb"))
                        {
                            datoSol.porcentSectorAmerb = Convert.ToSingle(row["porcentSectorAmerb"]);
                        }
                        if (!row.IsNull("profundidadMin"))
                        {
                            datoSol.profundidadMin = Convert.ToSingle(row["profundidadMin"]);
                        }
                        if (!row.IsNull("periodoOperacionCol"))
                        {
                            datoSol.periodoOperacionCol = row["periodoOperacionCol"].ToString();
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            datoSol.observaciones = row["observaciones"].ToString();
                        }
                        if (!row.IsNull("vigenciaColector"))
                        {
                            datoSol.vigenciaColector = Convert.ToDateTime("vigenciaColector");
                        } 
                        
                        if (!row.IsNull("codAmerbSNP"))
                        {
                            datoSol.amerbSSP = new DataExterna();
                            datoSol.amerbSSP.codigo = Convert.ToInt32(row["codAmerbSNP"]);
                            if (!row.IsNull("regionAmerb"))
                            {
                                datoSol.amerbSSP.region = row["regionAmerb"].ToString();
                            }
                            if (!row.IsNull("AMERB"))
                            {
                                datoSol.amerbSSP.nombre = row["AMERB"].ToString();
                            }
                            if (!row.IsNull("descripcionAmerb"))
                            {
                                datoSol.amerbSSP.descripcion = row["descripcionAmerb"].ToString();
                            }
                            if (!row.IsNull("estadoAmerb"))
                            {
                                datoSol.amerbSSP.estado = row["estadoAmerb"].ToString();
                            }
                            if (!row.IsNull("CDU01"))
                            {
                                datoSol.amerbSSP.cdu01 = row["CDU01"].ToString();
                            }
                            if (!row.IsNull("FCDU01"))
                            {
                                datoSol.amerbSSP.fcdu01 = row["FCDU01"].ToString();
                            }
                            if (!row.IsNull("CDU02"))
                            {
                                datoSol.amerbSSP.cdu01 = row["CDU02"].ToString();
                            }
                            if (!row.IsNull("FCDU02"))
                            {
                                datoSol.amerbSSP.fcdu01 = row["FCDU02"].ToString();
                            }
                            if (!row.IsNull("CDU03"))
                            {
                                datoSol.amerbSSP.cdu01 = row["CDU03"].ToString();
                            }
                            if (!row.IsNull("FCDU03"))
                            {
                                datoSol.amerbSSP.fcdu01 = row["FCDU03"].ToString();
                            }
                            if (!row.IsNull("Superficie_Hectareas"))
                            {
                                datoSol.amerbSSP.superficie = row["Superficie_Hectareas"].ToString();
                            }
                            if (!row.IsNull("UltimoPLazo"))
                            {
                                datoSol.amerbSSP.ultimoPlazo = Convert.ToDateTime(row["UltimoPLazo"]);
                            }
                            if (!row.IsNull("Informe"))
                            {
                                datoSol.amerbSSP.informe = row["Informe"].ToString();
                            }
                        }
                        if (!row.IsNull("idEcmpo"))
                        {
                            if (!row.IsNull("idEcmpo"))
                            {
                                datoSol.ecmpoSSP = new DataExterna();
                                datoSol.ecmpoSSP.codigo = Convert.ToInt32(row["idEcmpo"]);
                            }
                            if (!row.IsNull("Solicitud_Emcpo"))
                            {
                                datoSol.ecmpoSSP.nombre = row["Solicitud_Emcpo"].ToString();
                            }
                            if (!row.IsNull("descripcionEcmpo"))
                            {
                                datoSol.ecmpoSSP.descripcion = row["descripcionEcmpo"].ToString();
                            }
                            if (!row.IsNull("ComunaIndigena"))
                            {
                                datoSol.ecmpoSSP.comunaIndigena = row["ComunaIndigena"].ToString();
                            }
                            if (!row.IsNull("Comuna"))
                            {
                                datoSol.ecmpoSSP.comuna = row["Comuna"].ToString();
                            }
                        }
                        if (!row.IsNull("codCentro"))
                        {
                            datoSol.codCentro = row["codCentro"].ToString();
                        }
                        if (!row.IsNull("codAmerb"))
                        {
                            datoSol.codAmerb = row["codAmerb"].ToString();
                        }
                        if (!row.IsNull("codDirZonal"))
                        {
                            datoSol.oficina = new ParametroGenerico(row["codDirZonal"].ToString(), row["nombreDirZonal"].ToString());
                        }
                    }
                }

                return datoSol;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

         public List<SolicitudConcesion> ListarSolicitudCierreForzado(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudCierreForzado";

                 if (filtro.numPert != null && !filtro.numPert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert);
                 }
                 if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.numIdentSolicitud>0)
                 {
                     cnn.parametros.Add("@numIdentSolicitud", filtro.datosSolicitudUE.numIdentSolicitud);
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
                 if (filtro.tipoUnidadEspacial != null && filtro.tipoUnidadEspacial.id > 0)
                 {
                     cnn.parametros.Add("@idTipoUnidEspacial", filtro.tipoUnidadEspacial.id);
                 }
                 if (filtro.superficieCalculada > 0)
                 {
                     cnn.parametros.Add("@areaTotalCalculada", filtro.superficieCalculada);
                 }
                 
                 cnn.parametros.Add("@rca", filtro.tieneRCA);

                 if (filtro.especieCadFiltro != null && !filtro.especieCadFiltro.Equals(""))
                 {
                     cnn.parametros.Add("@EspecieCultivo", filtro.especieCadFiltro);
                 }
                 if (filtro.tipoTramite!=null && filtro.tipoTramite.id>0)
                 {
                     cnn.parametros.Add("@idTipoTramite", filtro.tipoTramite.id);
                 }
                 
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                             if (!row.IsNull("numIdentSolicitud"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.idSolConcesion = solicitudAux.idSolConcesion;
                                 solicitudAux.datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(row["numIdentSolicitud"]);
                             }

                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }

                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             solicitudAux.tieneRCA = Convert.ToInt32(row["rca"]);
                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                         
                             if (!row.IsNull("areaTotalCalculada"))
                             {
                                 solicitudAux.superficieCalculada = Convert.ToSingle(row["areaTotalCalculada"]);
                             }
                             if (!row.IsNull("especiesCad"))
                             {
                                 solicitudAux.especieCadFiltro = row["especiesCad"].ToString();
                             }

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.especiesSolicitud = new List<ParametroGenerico>();
                            
                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             
                             
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }


                         if (!row.IsNull("tipoRelocalizacion")) {
                             solicitudAux.subTipoTramite = new ParametroGenerico(0, row["tipoRelocalizacion"].ToString());
                         }

                         if (!row.IsNull("tipoModificacion"))
                         {
                             solicitudAux.subTipoTramite = new ParametroGenerico(0, row["tipoModificacion"].ToString());
                         }
                         
                         

                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarInformeTecnicoCierreForzado(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 ParametroGenerico comuna = null;
                 ParametroGenerico especie = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesEspecie = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbInformeTecnicoCierreForzado";


                 cnn.parametros.Add("@idDocITCierre", filtro.solicitudCierre.docITCierre);
                 if (filtro.numPert != null && !filtro.numPert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert);
                 }
                 if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.numIdentSolicitud > 0)
                 {
                     cnn.parametros.Add("@numIdentSolicitud", filtro.datosSolicitudUE.numIdentSolicitud);
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
                 if (filtro.tipoUnidadEspacial != null && filtro.tipoUnidadEspacial.id > 0)
                 {
                     cnn.parametros.Add("@idTipoUnidEspacial", filtro.tipoUnidadEspacial.id);
                 }
                 if (filtro.superficieCalculada > 0)
                 {
                     cnn.parametros.Add("@areaTotalCalculada", filtro.superficieCalculada);
                 }
                 if (filtro.tieneRCA >= 0)
                 {
                     cnn.parametros.Add("@rca", filtro.tieneRCA);
                 }
                 if (filtro.especie != null && filtro.especie.id > 0)
                 {
                     cnn.parametros.Add("@EspecieCultivo", filtro.especie.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("numIdentSolicitud"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.idSolConcesion = solicitudAux.idSolConcesion;
                                 solicitudAux.datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(row["numIdentSolicitud"]);
                             }

                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }

                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             solicitudAux.tieneRCA = Convert.ToInt32(row["rca"]);
                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }

                             if (!row.IsNull("areaTotalCalculada"))
                             {
                                 solicitudAux.superficieCalculada = Convert.ToSingle(row["areaTotalCalculada"]);
                             }


                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.especiesSolicitud = new List<ParametroGenerico>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesEspecie = new HashSet<int>();

                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("idEspecie"))
                         {
                             if (clavesEspecie.Count == 0 || !clavesEspecie.Contains(Convert.ToInt32(row["idEspecie"])))
                             {
                                 clavesEspecie.Add(Convert.ToInt32(row["idEspecie"]));

                                 especie = new ParametroGenerico(Convert.ToInt32(row["idEspecie"]), row["EspecieCultivo"].ToString());
                                 solicitudAux.especiesSolicitud.Add(especie);
                             }
                         }

                         idSolicitudAux = idSolicitud;

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

         public DataTable ListarTramitesSolicitudes(int idTipoTramite,int idUsuario ,string pert)
         {

             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelTramitesSolicitudes";
                 cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                 cnn.parametros.Add("@idUsuario", idUsuario);
                 if (pert!=null && !pert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", pert);
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

         public bool ActualizaSolicitud_RecReposicion(int idSolicitud, bool recursoReposicion, int idUsuario)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbSolicitudConcesion_RecReposicion";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 cnn.parametros.Add("@recursoReposicion", recursoReposicion);
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

         public List<SolicitudConcesion> ListarDocGralCierre_SolicitudesIT(int idDocITCierre)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 ParametroGenerico comuna = null;
                 ParametroGenerico especie = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesEspecie = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbDocGralCierre_SolicitudesIT";


                 cnn.parametros.Add("@idDocITCierre", idDocITCierre);
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);


                             //INDICA SI LA SOLICITUD ESTA EN CURSO DE CIERRE O SE ANULO/BORRO EL INFORME MANUALMENTE EN A SOLICITUD
                            if(Convert.ToBoolean(row["tieneIT"]) == true){
                                solicitudAux.estadoCierreForzado = new ParametroGenerico(0,"Activa");
                            }else{
                                solicitudAux.estadoCierreForzado = new ParametroGenerico(0,"No Activa (IT anulado o IT borrado)");
                            }
                             
                             

                             solicitudAux.solicitudCierre = new DocSolicitudCierre();
                             solicitudAux.solicitudCierre.docITCierre = new Requerimiento();
                             solicitudAux.solicitudCierre.docITCierre.idRequerimiento = Convert.ToInt32(row["idDocITCierre"]);
                             
                             if (!row.IsNull("numIdentSolicitud"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.idSolConcesion = solicitudAux.idSolConcesion;
                                 solicitudAux.datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(row["numIdentSolicitud"]);
                             }

                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }

                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             solicitudAux.tieneRCA = Convert.ToInt32(row["rca"]);
                             solicitudAux.tieneIT = Convert.ToBoolean(row["tieneIT"]);
                             solicitudAux.tieneSSP = Convert.ToBoolean(row["tieneSSP"]);

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }

                             if (!row.IsNull("areaTotalCalculada"))
                             {
                                 solicitudAux.superficieCalculada = Convert.ToSingle(row["areaTotalCalculada"]);
                             }


                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.especiesSolicitud = new List<ParametroGenerico>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesEspecie = new HashSet<int>();

                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("idEspecie"))
                         {
                             if (clavesEspecie.Count == 0 || !clavesEspecie.Contains(Convert.ToInt32(row["idEspecie"])))
                             {
                                 clavesEspecie.Add(Convert.ToInt32(row["idEspecie"]));

                                 especie = new ParametroGenerico(Convert.ToInt32(row["idEspecie"]), row["EspecieCultivo"].ToString());
                                 solicitudAux.especiesSolicitud.Add(especie);
                             }
                         }


                         if (!row.IsNull("tipoTramite"))
                         {
                             solicitudAux.tipoTramite = new ParametroGenerico(0, row["tipoTramite"].ToString());
                         }

                         if (!row.IsNull("tipoRelocalizacion"))
                         {
                             solicitudAux.subTipoTramite = new ParametroGenerico(0, row["tipoRelocalizacion"].ToString());
                         }

                         if (!row.IsNull("tipoModificacion"))
                         {
                             solicitudAux.subTipoTramite = new ParametroGenerico(0, row["tipoModificacion"].ToString());
                         }
                         

                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudConcesionTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudConcesionTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }
                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }
                 

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();
                            

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }


                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }

                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudExperimentalesConcesionTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                     
                 
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public List<SolicitudConcesion> ListarSolicitudFaenamientoTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudFaenamientoTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }

                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }  
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }




                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudColectorTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudColectorTraspaso";

                 if (filtro.datosSolicitudUE != null && filtro.datosSolicitudUE.numIdentSolicitud>0)
                 {
                     cnn.parametros.Add("@numIdentSolicitud", filtro.datosSolicitudUE.numIdentSolicitud);
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numIdentSolicitud"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.numIdentSolicitud = Convert.ToInt32(row["numIdentSolicitud"]); 
                             }

                             if (!row.IsNull("fechaCI"))
                             {
                                 solicitudAux.datosSolicitudUE.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }

                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }  
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudAmerbTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudAmerbTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }

                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }
                 if (filtro.amerbFiltro != null && filtro.amerbFiltro.id > 0)
                 {
                     cnn.parametros.Add("@codAmerbSNP", filtro.amerbFiltro.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("codAmerbSNP"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.codAmerb = row["codAmerbSNP"].ToString();
                                 solicitudAux.datosSolicitudUE.amerbVista = new ParametroGenerico(Convert.ToInt32(row["codAmerbSNP"]), "");
                                 if (!row.IsNull("descripcionAmerb"))
                                 {
                                     solicitudAux.datosSolicitudUE.amerbVista.descripcion = row["descripcionAmerb"].ToString();
                                 }
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }   
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }


                         idSolicitudAux = idSolicitud;

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



         public List<SolicitudConcesion> ListarSolicitudExprimentalesAmerbTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudAmerbTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }


                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudECMPOTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudECMPOTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }
                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }

                 if (filtro.ecmpoFiltro != null && filtro.ecmpoFiltro.id > 0)
                 {
                     cnn.parametros.Add("@idEcmpo", filtro.ecmpoFiltro.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("id_Emcpo"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.ecmpoVista = new ParametroGenerico(Convert.ToInt32(row["id_Emcpo"]), "");
                                 if (!row.IsNull("descripcionEcmpo"))
                                 {
                                     solicitudAux.datosSolicitudUE.ecmpoVista.descripcion = row["descripcionEcmpo"].ToString();
                                 }
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             } 
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }


                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudAcopioTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudAcopioTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }

                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }

                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }

                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }




                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudExConcesionTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudExConcesionTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }
                 
                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }


                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudExAmerbTraspaso(SolicitudConcesion filtro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudExAmerbTraspaso";

                 if (filtro.unidadEspacial != null && filtro.unidadEspacial.centrosDeCultivo != null && !filtro.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                 {
                     cnn.parametros.Add("@codigoCentro", filtro.unidadEspacial.centrosDeCultivo.codigoCentro);
                 }

                 if (filtro.numPert != null && !filtro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", filtro.numPert.Trim());
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
                 if (filtro.titularFiltro != null && filtro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", filtro.titularFiltro.rutPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", filtro.titularFiltro.dvPersona);
                 }
                 if (filtro.titularFiltro != null && filtro.titularFiltro.nombreSolicitante != null && !filtro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", filtro.titularFiltro.nombreSolicitante);
                 }

                 if (filtro.fechaRangoFiltro1 != null && filtro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", filtro.fechaRangoFiltro1);
                 }
                 if (filtro.fechaRangoFiltro2 != null && filtro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", filtro.fechaRangoFiltro2);
                 }
                 if (filtro.estadoVigencia != null && filtro.estadoVigencia.id > 0)
                 {
                     cnn.parametros.Add("@idEstadoVigencia", filtro.estadoVigencia.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEstadoVigencia"))
                             {
                                 solicitudAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codigoCentro"))
                         {

                             if (solicitudAux.unidadEspacial == null)
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             }
                         }


                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarHistorialTramiteConcesion(string codigoCentro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbHistorialTramiteConcesion";

                 cnn.parametros.Add("@codigoCentro", codigoCentro);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.unidadEspacial = new UnidadEspacial();
                         solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                         solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                         solicitudAux.numPert = row["numPert"].ToString();
                         if (!row.IsNull("fechaIngresoTramite"))
                         {
                             solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                         }
                         
                         resp.Add(solicitudAux);
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

         public SolicitudConcesion ObtieneCamposSolicitudAviso(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudResp = null;
               
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbCamposSolicitudAviso";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         solicitudResp = new SolicitudConcesion();
                         solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudResp.numPert = row["numPert"].ToString();
                         if (!row.IsNull("idTipoTramite"))
                         {
                            solicitudResp.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                         }
                         solicitudResp.titularesCad = row["titulares"].ToString();
                         solicitudResp.tipoSolicitudCad = row["tipoConcesion"].ToString();
                         if (!row.IsNull("idTipoRelocalizacion"))
                         {
                             solicitudResp.sectorRelocalizacion = new DetalleSector();
                             solicitudResp.sectorRelocalizacion.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipoRel"].ToString());
                         }
                         solicitudResp.resolucionSSFFAA = row["SSFFAA"].ToString();
                         solicitudResp.resolucionSSP = row["SSP"].ToString();
                         
                      }
                 }
                 return solicitudResp;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public bool aplicaTitularesOtroColector(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbTitularesOtroColector";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("idSolConcesion"))
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
                 return true;
             }
         }

         public bool aplicaSolicitudConCapitaniaPuerto(int idSolicitud)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConCapitaniaPuerto";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idSolConcesion"))
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
                return true;
            }
        }
        //10
         public List<SolicitudConcesion> ListarSolicitud_SubReqPlazo_Estado(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubReqPlazo_Estado";

                 if (idSolConcesion>0)
                 {
                     cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 }
                
                 DataTable dt = cnn.Execute();
                 //DataTable dt = cnn.Execute2();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         if (!row.IsNull("idEstadoActual"))
                         {
                             solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                         }
                         solicitudAux.diasCantidad = new ParametroGenerico(Convert.ToInt32(row["cant_dia_mes"]), row["dia_mes"].ToString());
                         solicitudAux.fechaRangoFiltro1 = Convert.ToDateTime(row["fecha"]);
                         solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                         solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUnidEspacial"].ToString());

                         resp.Add(solicitudAux);

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
        //19
         public List<SolicitudConcesion> ListarSolicitud_SubReqPlazo_Vencido(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubReqPlazo_Vencido";

                 if (idSolConcesion > 0)
                 {
                     cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 }

                 DataTable dt = cnn.Execute();
                 //DataTable dt = cnn.Execute2();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         if (!row.IsNull("titulares"))
                         {
                            solicitudAux.titularesCad = row["titulares"].ToString();
                         }
                         if (!row.IsNull("idTipoUnidEspacial"))
                         {
                             solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                         }
                         if (!row.IsNull("idTipoTramite"))
                         {
                             solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                         }
                         if (!row.IsNull("idSubRequerimiento"))
                         {
                             solicitudAux.temaReq = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                         }
                         
                         resp.Add(solicitudAux);

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
        //4
         public List<SolicitudConcesion> ListarSolicitud_SubReqPlazo_EstadoVencido(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubReqPlazo_EstadoVencido";

                 if (idSolConcesion > 0)
                 {
                     cnn.parametros.Add("@idSolicitud", idSolConcesion);
                 }

                 DataTable dt = cnn.Execute();
                 //DataTable dt = cnn.Execute2();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         if (!row.IsNull("titulares"))
                         {
                             solicitudAux.titularesCad = row["titulares"].ToString();
                         }
                         if (!row.IsNull("idSubReqOrigen"))
                         {
                             solicitudAux.temaReq = new ParametroGenerico(Convert.ToInt32(row["idSubReqOrigen"]), row["nombreSubRequerimiento"].ToString());
                         }
                         solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                         solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUnidEspacial"].ToString());

                         resp.Add(solicitudAux);

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
        //23
         public List<SolicitudConcesion> ListarSolicitud_PublicacionRadialOk(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbPublicacionRadialOk";

                 if (idSolConcesion > 0)
                 {
                     cnn.parametros.Add("@idSolicitud", idSolConcesion);
                 }

                 //DataTable dt = cnn.Execute2();
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         if (!row.IsNull("idTipoTramite"))
                         {
                             solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                         }
                         resp.Add(solicitudAux);

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

         //06
         //COMPLEMENTAR CON ExisteAvisoEnviado EN AVISODA
         public List<SolicitudConcesion> ListarSectoresRel_Rechazado(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSectoresRel_Rechazado";

                 if (idSolConcesion > 0)
                 {
                     cnn.parametros.Add("@idSolicitud", idSolConcesion);
                 }

                 DataTable dt = cnn.Execute();
                 //DataTable dt = cnn.Execute2();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         solicitudAux.sectorRelocalizacion = new DetalleSector();
                         solicitudAux.sectorRelocalizacion.numSector = Convert.ToInt32(row["numSector"]);
                         solicitudAux.titularesCad = row["titulares"].ToString();

                         resp.Add(solicitudAux);
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

         public string ObtieneSolicitudReqPendientes(int idSolConcesion)
         {
             try
             {
                 string reqPendientes=null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudReqPendientes";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         reqPendientes = row["reqPendientes"].ToString();
                     }
                 }
                 return reqPendientes;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public List<SolicitudConcesion> ListarSolicitudesSinMov(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudSinMov";

                 if (idSolConcesion > 0)
                 {
                     cnn.parametros.Add("@idSolicitud", idSolConcesion);
                 }

                 DataTable dt = cnn.Execute();
                 //DataTable dt = cnn.Execute2();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                         //solicitudAux.titularesCad = row["titulares"].ToString();
                         solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                         solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUnidEspacial"].ToString());

                         resp.Add(solicitudAux);
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

         public List<SolicitudConcesion> ListarSolicitudesSinAsignacionUsuario(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudesSinAsignacionUsuario";

                 if (idSolConcesion > 0)
                 {
                     cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 }

                 DataTable dt = cnn.Execute();
                 //DataTable dt = cnn.Execute2();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                         solicitudAux.numPert = row["numPert"].ToString();
                         solicitudAux.titularesCad = row["titulares"].ToString();

                         resp.Add(solicitudAux);
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

         public DataTable ListarHistEstadosSolicConces(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbHistEstadosSolicConces";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 
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

         public void ObtieneSolicitudRecargaEstado(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbRecargaEstado";
                 cnn.parametros.Add("@idSolicitud", idSolConcesion);
                 

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         idSolConcesion = Convert.ToInt32(row["idSolicitud"]);
                         
                     }
                 }
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
             }
         }

         public bool TramiteRecalculaEstados(int idSolicitud)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbRecalculaEstados";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 
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

         public List<int> ListarCheckMarcados(int idTipoTramite, int idUsuario, string pert)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelTramitesSolicitudes";
                 cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                 cnn.parametros.Add("@idUsuario", idUsuario);
                 if (pert != null && !pert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", pert);
                 }
                 cnn.parametros.Add("@permiso", 1);

                 DataTable dt = cnn.Execute();

                 if (dt != null && dt.Rows.Count > 0)
                 {

                     List<int> ids = new List<int>();

                     foreach (DataRow row in dt.Rows)
                     {
                        ids.Add(Convert.ToInt32(row["id"]));
                     }

                     return ids;
                 }


                 return null;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudExperimentalesAmerbAdmin(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudExAmerbAdmin";

                 if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                 }
                 if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                 {
                     cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                 }
                 if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                 {
                     cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                 }
                 if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                 {
                     cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                 }

                 if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", solicitudFiltro.fechaRangoFiltro1);
                 }
                 if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", solicitudFiltro.fechaRangoFiltro2);
                 }
                 if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                 {
                     cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                 }
                 if (solicitudFiltro.datosSolicitudUE != null && solicitudFiltro.datosSolicitudUE.amerbVista != null && solicitudFiltro.datosSolicitudUE.amerbVista.id > 0)
                 {
                     cnn.parametros.Add("@codAmerbSNP", solicitudFiltro.datosSolicitudUE.amerbVista.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         if (!row.IsNull("codAmerbSNP"))
                         {
                                solicitudAux.datosSolicitudUE.codAmerb = row["codAmerbSNP"].ToString();
                         }

                         idSolicitudAux = idSolicitud;

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

         public List<SolicitudConcesion> ListarSolicitudExperimentalesConcesionAdmin(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudExConcesionAdmin";

                 if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                 }
                 if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                 {
                     cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                 }
                 if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                 {
                     cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                 }
                 if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                 {
                     cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                 }

                 if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", solicitudFiltro.fechaRangoFiltro1);
                 }
                 if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", solicitudFiltro.fechaRangoFiltro2);
                 }
                 if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                 {
                     cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         idSolicitudAux = idSolicitud;

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
         public List<SolicitudConcesion> ListarSolicitudECMPOAdmin(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 Persona pers = null;
                 ParametroGenerico comuna = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 int idComuna = 0;
                 int rutTitular = 0;
                 HashSet<int> clavesComuna = new HashSet<int>();
                 HashSet<int> clavesTitular = new HashSet<int>();


                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudECMPOAdmin";

                 if (solicitudFiltro.numPert != null && !solicitudFiltro.numPert.Equals(""))
                 {
                     cnn.parametros.Add("@numPert", solicitudFiltro.numPert);
                 }
                 if (solicitudFiltro.region != null && solicitudFiltro.region.id > 0)
                 {
                     cnn.parametros.Add("@idRegion", solicitudFiltro.region.id);
                 }
                 if (solicitudFiltro.provincia != null && solicitudFiltro.provincia.id > 0)
                 {
                     cnn.parametros.Add("@idProvincia", solicitudFiltro.provincia.id);
                 }
                 if (solicitudFiltro.comunaFiltro != null && solicitudFiltro.comunaFiltro.id > 0)
                 {
                     cnn.parametros.Add("@idComuna", solicitudFiltro.comunaFiltro.id);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", solicitudFiltro.titularFiltro.rutPersona);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.dvPersona > 0)
                 {
                     cnn.parametros.Add("@digitoVerificador", solicitudFiltro.titularFiltro.dvPersona);
                 }
                 if (solicitudFiltro.titularFiltro != null && solicitudFiltro.titularFiltro.nombreSolicitante != null && !solicitudFiltro.titularFiltro.nombreSolicitante.Equals(""))
                 {
                     cnn.parametros.Add("@nombre", solicitudFiltro.titularFiltro.nombreSolicitante);
                 }

                 if (solicitudFiltro.fechaRangoFiltro1 != null && solicitudFiltro.fechaRangoFiltro1 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango1", solicitudFiltro.fechaRangoFiltro1);
                 }
                 if (solicitudFiltro.fechaRangoFiltro2 != null && solicitudFiltro.fechaRangoFiltro2 != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRango2", solicitudFiltro.fechaRangoFiltro2);
                 }
                 if (solicitudFiltro.estadoActual != null && solicitudFiltro.estadoActual.id > 0)
                 {
                     cnn.parametros.Add("@idEstado", solicitudFiltro.estadoActual.id);
                 }
                 if (solicitudFiltro.datosSolicitudUE != null && solicitudFiltro.datosSolicitudUE.ecmpoSSP != null && solicitudFiltro.datosSolicitudUE.ecmpoSSP.codigo > 0)
                 {
                     cnn.parametros.Add("@idEcmpoSNP", solicitudFiltro.datosSolicitudUE.ecmpoSSP.codigo);
                 }

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudAux.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }

                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("idEcmpo"))
                             {
                                 solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                                 solicitudAux.datosSolicitudUE.ecmpoSSP = new DataExterna();
                                 solicitudAux.datosSolicitudUE.ecmpoSSP.codigo = Convert.ToInt32(row["idEcmpo"]);
                                 solicitudAux.datosSolicitudUE.ecmpoSSP.nombre = row["Solicitud_Emcpo"].ToString();
                             }
                             solicitudAux.comunaFronteriza = Convert.ToBoolean(row["solicComunaFront"]);

                             solicitudAux.comuna = new List<ParametroGenerico>();
                             solicitudAux.titularesSolConcesion = new List<Persona>();

                             resp.Add(solicitudAux);
                             clavesComuna = new HashSet<int>();
                             clavesTitular = new HashSet<int>();
                         }
                         if (!row.IsNull("IdComuna"))
                         {
                             idComuna = Convert.ToInt32(row["IdComuna"]);
                             if (clavesComuna.Count == 0 || !clavesComuna.Contains(idComuna))
                             {
                                 clavesComuna.Add(idComuna);

                                 comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                                 solicitudAux.comuna.Add(comuna);
                             }
                         }

                         if (!row.IsNull("rutPersona"))
                         {
                             rutTitular = Convert.ToInt32(row["rutPersona"]);
                             if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                             {

                                 clavesTitular.Add(rutTitular);

                                 pers = new Persona();
                                 pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                 pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                 pers.nombreSolicitante = row["nombre"].ToString();
                                 solicitudAux.titularesSolConcesion.Add(pers);
                             }
                         }

                         idSolicitudAux = idSolicitud;

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


         //GUARDA SOLICITUD DE ACUICULTURA EN ECMPO
         public bool GuardarSolicitudECMPO(SolicitudConcesion solicitudInicial)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                 cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                 cnn.parametros.Add("@numPert", solicitudInicial.numPert);
                 cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                 cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                 cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_ECMPO);
                 cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                 DataTable dt = cnn.Execute();
                 solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                 return true;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
         }

         public bool GuardarSolicitudExperimentalesConcesion(SolicitudConcesion solicitudInicial)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbSolicitudConcesInicial";
                 cnn.parametros.Add("@idEstadoActual", rbEstadosGenerales.SOLICITUD_INICIADA);
                 cnn.parametros.Add("@numPert", solicitudInicial.numPert);

                 if (solicitudInicial.fechaRecepcion != null && solicitudInicial.fechaRecepcion != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaRecepcion", solicitudInicial.fechaRecepcion);
                 }
                 if (solicitudInicial.fechaIngresoTramite != null && solicitudInicial.fechaIngresoTramite != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramite", solicitudInicial.fechaIngresoTramite);
                 }

                 cnn.parametros.Add("@idTipoUnidEspacial", rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION);
                 cnn.parametros.Add("@idTipoTramite", solicitudInicial.tipoTramite.id);


                 DataTable dt = cnn.Execute();
                 solicitudInicial.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolicitudResp"]);

                 return true;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModCentroFaenamientoAdmin_Tramite(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModCentroFaenamientoAdmin_Aprobada(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModCentroFaenamientoAdmin_Rechazada(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarConcesionTramitesSolicitud(string codigoCentro, int idPestania)
         {
             /*
              * idPestania=1: trámites aprobados
              * idPestania=2: trámites rechazados
              * idPestania=3: trámites 'en trámite'
              */

             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbConcesionTramitesSolicitud";

                 cnn.parametros.Add("@codigoCentro", codigoCentro);
                 cnn.parametros.Add("@idPestania", idPestania);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();
                             solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             if (!row.IsNull("idEstadoActual"))
                             {
                                 solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }
                             if (!row.IsNull("titularesCad"))
                             {
                                 solicitudAux.titularesCad = row["titularesCad"].ToString();
                             }
                             if (!row.IsNull("fechaIngresoTramite"))
                             {
                                 solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             }
                             if (!row.IsNull("idTipoUnidEspacial"))
                             {
                                 solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreUE"].ToString());
                             }
                             if (!row.IsNull("idTipoTramite"))
                             {
                                 solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTramite"].ToString());
                             }
                             solicitudAux.traspasoOk = Convert.ToBoolean(row["traspasoOk"]);
                             
                             solicitudAux.unidadEspacial = new UnidadEspacial();
                             solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                             solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                             solicitudAux.unidadEspacial.centrosDeCultivo.nombreCentro = row["codigoCentro"].ToString();
                            
                             resp.Add(solicitudAux);
                         }
                         
                         idSolicitudAux = idSolicitud;
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

         public SolicitudConcesion ObtieneAdminUnidadesEspaciales(int idSolConcesion)
         {
             try
             {
                 int idSolConcesionVar = 0;
                 int idSolConcesionAux = 0;
                 SolicitudConcesion solicitudResp = null;
                 CoordenadaGeografica coordenadaAux = null;
                 Poligono poligonoAux = null;
                 //bool seaAux;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbAdminUnidadesEspaciales";
                 cnn.parametros.Add("@idSolicitud", idSolConcesion);
                
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                         if (idSolConcesionVar != idSolConcesionAux)
                         {
                             solicitudResp = new SolicitudConcesion();
                             solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             
                             if (!row.IsNull("numPert"))
                             {
                                solicitudResp.numPert = row["numPert"].ToString();
                             }

                             if (!row.IsNull("idUnidadEspacial"))
                             {
                                 solicitudResp.unidadEspacial = new UnidadEspacial();
                                 solicitudResp.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudResp.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                                 if (!row.IsNull("nombreCentro"))
                                 {
                                     solicitudResp.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentro"].ToString();
                                 }
                                 if (!row.IsNull("numDiarioOficial"))
                                 {
                                     solicitudResp.unidadEspacial.numeroDiarioOficial = Convert.ToInt32(row["numDiarioOficial"]);
                                 }
                                 if (!row.IsNull("fechaDiarioOficial"))
                                 {
                                     solicitudResp.unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(row["fechaDiarioOficial"]);
                                 }
                                 if (!row.IsNull("numActaEntrega"))
                                 {
                                     solicitudResp.unidadEspacial.numeroActaEntrega = Convert.ToInt32(row["numActaEntrega"]);
                                 }
                                 if (!row.IsNull("fechaActaEntrega"))
                                 {
                                     solicitudResp.unidadEspacial.fechaActaEntrega = Convert.ToDateTime(row["fechaActaEntrega"]);
                                 }
                                 if (!row.IsNull("idCapitania"))
                                 {
                                     solicitudResp.unidadEspacial.capitaniaDePuerto = new CapitaniaDePuerto();
                                     solicitudResp.unidadEspacial.capitaniaDePuerto.idCapitaDePuerto = Convert.ToInt32(row["idCapitania"]);
                                     if (!row.IsNull("CapitaniaPuerto"))
                                     {
                                         solicitudResp.unidadEspacial.capitaniaDePuerto.nombreCapitaniaDePuerto = row["CapitaniaPuerto"].ToString();
                                     }
                                 }
                                 if (!row.IsNull("idTipoPlazoNominal"))
                                 {
                                     solicitudResp.unidadEspacial.tipoPlazoNominal = new ParametroGenerico(Convert.ToInt32(row["idTipoPlazoNominal"]), row["nombreTipo"].ToString());
                                 }
                                 if (!row.IsNull("plazoInicio"))
                                 {
                                     solicitudResp.unidadEspacial.plazoInicio = Convert.ToDateTime(row["plazoInicio"]);
                                 }
                                 if (!row.IsNull("plazoVencimiento"))
                                 {
                                     solicitudResp.unidadEspacial.plazoVencimiento = Convert.ToDateTime(row["plazoVencimiento"]);
                                 }
                                 if (!row.IsNull("idEstadoVigencia"))
                                 {
                                     solicitudResp.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                                 }

                             }
                             solicitudResp.titularesCad = row["titularesCad"].ToString();
                             solicitudResp.especiesCad = row["especiesCad"].ToString();
                             solicitudResp.gruposInformativosCad = row["grupoInformativoCad"].ToString();

                             if (!row.IsNull("IdProvincia"))
                             {
                                 solicitudResp.provincia = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                             }
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudResp.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             solicitudResp.comunasCad = row["comunasCad"].ToString();
                             coordenadaAux = new CoordenadaGeografica();
                             coordenadaAux.listaPoligono = new List<Poligono>();
                             solicitudResp.coordenadaGeografica = new List<CoordenadaGeografica>();
                             solicitudResp.coordenadaGeografica.Add(coordenadaAux);
                             
                         }

                         if (!row.IsNull("idCoordenadaGeo"))
                         {
                             poligonoAux = new Poligono();
                             poligonoAux.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeo"]);
                             poligonoAux.toponimio =  row["toponimio"].ToString();

                             if (!row.IsNull("areaCalculada"))
                             {
                                 poligonoAux.areaCalculada = Convert.ToSingle(row["areaCalculada"]);
                             }

                             if (!row.IsNull("idTipoUso")) {
                                 poligonoAux.tipoUso = new ParametroGenerico(Convert.ToInt32(row["idTipoUso"]), row["nombreTipoUso"].ToString());
                             }
                             

                             coordenadaAux.listaPoligono.Add(poligonoAux);
                         }

                        idSolConcesionAux = idSolConcesionVar;

                     }
                 }
                 return solicitudResp;

             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         //GUARDA CUERPO DE AGUA ASOCIADO A LA SOLICITUD
         public bool GuardarCuerpoAguaSolicitud(int idSolicitud, int idCuerpoAgua, int idUsuario)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbCuerpoAguaSolicitud";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 if(idCuerpoAgua>0){
                    cnn.parametros.Add("@idCuerpoAgua", idCuerpoAgua);
                 }
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

         public bool GuardarEstadoSolicitud(int idSolicitud)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbEstadoSolicitudConcesion_aplicativo";

                 cnn.parametros.Add("@idSolicitud", idSolicitud);
                 
                 
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

         public List<SolicitudConcesion> ListarSolicitudModAmerbAdmin_Tramite(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModAmerbAdmin_Aprobada(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModAmerbAdmin_Rechazada(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModECMPOAdmin_Tramite(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModECMPOAdmin_Aprobada(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public List<SolicitudConcesion> ListarSolicitudModECMPOAdmin_Rechazada(SolicitudConcesion solicitudFiltro)
         {
             try
             {
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 return resp;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }





         public bool ActualizaSolicitud_SometimientoCPS_INFAS(int idSolicitud, int sometimientoCPS_INFAS)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbSolicitudConcesion_sometimientoCPS_INFAS";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 if (sometimientoCPS_INFAS > 0)
                 {
                     cnn.parametros.Add("@sometimientoCPS_INFAS", sometimientoCPS_INFAS);
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

         public bool ActualizaSolicitud_VerificaCertOperacion(int idSolicitud, int verificaCertOperacion)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbSolicitudConcesion_verificaCertOperacion";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 if (verificaCertOperacion > 0)
                 {
                     cnn.parametros.Add("@verificaCertOperacion", verificaCertOperacion);
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

         public bool ActualizaSolicitud_VerificaFirmaSeguimiento(int idSolicitud, int estadoFirmaConvenio, int estadoSeguimientoDia)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbDatosSolicitudUE_verificaFirma_Seguimiento";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 if (estadoFirmaConvenio > 0)
                 {
                     cnn.parametros.Add("@estadoFirmaConvenio", estadoFirmaConvenio);
                 }
                 if (estadoSeguimientoDia > 0)
                 {
                     cnn.parametros.Add("@estadoSeguimientoDia", estadoSeguimientoDia);
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

         public bool ActualizaSolicitud_cultivoExperimental(int idSolicitud, int cultivoExperimental, int idUsuario)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbDatosSolicitudUE_cultivoExperimental";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 if (cultivoExperimental > 0)
                 {
                     cnn.parametros.Add("@cultivoExperimental", cultivoExperimental);
                 }
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

         public bool ActualizaSolicitud_Vigencia(int idSolicitud, int idEstadoVigencia, int idUsuario)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbSolicitudConcesion_Vigencia";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
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

         public bool ActualizaSolicitudConcesion_ACS_ACM(int idSolicitud, int IdBarrio, int idACM, int idUsuario)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbSolicitudConcesion_ACS_ACM";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                
                 if (IdBarrio>0)
                 {
                    cnn.parametros.Add("@IdBarrio", IdBarrio);
                 }
                 if(idACM>0){
                    cnn.parametros.Add("@idACM", idACM);
                 }
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

         //NO IMPORTA SI EL DATO ES CADENA O ENTERO, INGRESAR SIEMPRE COMO ENTERO Y EL PROCEDIMIENTO LO PROCESARÁ COMO EL TIPO DE DATO CORRESPONDIENTE.
         public bool ActualizaSolicitud_cultivoExperimental(int idDatosSolicitud,string tipoDato, int valor)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbDatosSolicitudUE_Filtro";
                 
                 cnn.parametros.Add("@idDatosSolicitud", idDatosSolicitud);
                 cnn.parametros.Add("@tipoDato", tipoDato);

                 String dato = null;

                 if(valor>0){
                     dato = valor.ToString();
                 }

                 if (tipoDato.Equals("AMERB_VISTA") && valor>0) //
                 {
                     cnn.parametros.Add("@codEntero", valor);

                 }
                 if (tipoDato.Equals("AMERB_UE") && !dato.Equals(null)) //Codigo exp amerb
                 {
                     cnn.parametros.Add("@codCadena", dato);

                 }
                 if (tipoDato.Equals("ECMPO_VISTA") && valor > 0)
                 {
                     cnn.parametros.Add("@codEntero", valor);

                 }
                 if (tipoDato.Equals("EXP_CONCESION") && !dato.Equals(null)) //Codigo exp concesion
                 {
                     cnn.parametros.Add("@codCadena", dato);

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

         public List<SolicitudConcesion> ListarSolicitud_SubReqPlazo_Vencido_Usuario()
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 ParametroGenerico param = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 int idSolAux=0;
                 int idSol=0;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSubReqPlazo_Vencido_Usuario";

                 DataTable dt = cnn.Execute();
                
                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         idSol = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.numPert = row["numPert"].ToString();
                         if (!row.IsNull("titularesCad"))
                         {
                             solicitudAux.titularesCad = row["titularesCad"].ToString();
                         }
                         if (!row.IsNull("idTipoUnidEspacial"))
                         {
                             solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreTipoUE"].ToString());
                         }
                         if (!row.IsNull("idTipoTramite"))
                         {
                             solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipoTramite"].ToString());
                         }
                         solicitudAux.temaDestinatarioDinamico = new List<ParametroGenerico>();

                         if(idSol==idSolAux){
                            param = new ParametroGenerico();
                            if (!row.IsNull("idSubRequerimiento"))
                            { 
                                param.id = Convert.ToInt32(row["idSubRequerimiento"]);
                                param.clave =  row["nombreSubRequerimiento"].ToString();
                            }
                            if (!row.IsNull("correo"))
                            {
                                param.descripcion = row["correo"].ToString();
                            }
                            solicitudAux.temaDestinatarioDinamico.Add(param);

                         }
                         resp.Add(solicitudAux);

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

         public List<SolicitudConcesion> ListarSolicitudesSinCapitania()
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudSinCapitania";

                 DataTable dt = cnn.Execute();
                 
                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         solicitudAux = new SolicitudConcesion();
                         solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTipo"].ToString());
                         solicitudAux.numPert = row["numPert"].ToString();
                         
                         resp.Add(solicitudAux);
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
         
        //true:repetido
         public bool SolicitudAmerCodRepetido(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolAmerbCodRepetido";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("idSolConcesion"))
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
                 return true;
             }
         }

         public int DiasHabilesCartaTitular_RCA(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbDiashabilesCartaTitular_RCA";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 int diasHabiles=0;

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("diasHabiles"))
                         {
                             diasHabiles = Convert.ToInt32(row["diasHabiles"]);
                         }
                     }
                 }

                 return diasHabiles;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return 0;
             }
         }

         public int DiasHabilesCarta_MO(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbDiasHabilesCartaTitular_MO";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 int diasHabiles = 0;

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("diasHabiles"))
                         {
                             diasHabiles = Convert.ToInt32(row["diasHabiles"]);
                         }
                         break;
                     }
                 }

                 return diasHabiles;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return 0;
             }
         }

         public bool ActualizaDatosSolicitudUE_DatosAmerb(int idDatosSolicitud, float superficieSectorAmerb, float porcentSectorAmerb)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbDatosSolicitudUE_DatosAmerb";

                 cnn.parametros.Add("@idDatosSolicitud", idDatosSolicitud);
                
                 if(superficieSectorAmerb>=0){
                    cnn.parametros.Add("@superficieSectorAmerb", superficieSectorAmerb);
                 }
                 if (porcentSectorAmerb>=0)
                 {
                    cnn.parametros.Add("@porcentSectorAmerb", porcentSectorAmerb);
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

         public bool ActualizaDatosSolicitudUE_DatosColector(int idDatosSolicitud, string periodoOperacionCol, string observaciones)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbDatosSolicitudUE_DatosColector";

                 cnn.parametros.Add("@idDatosSolicitud", idDatosSolicitud);

                 if (periodoOperacionCol != null && !periodoOperacionCol.Equals(""))
                 {
                     cnn.parametros.Add("@periodoOperacionCol", periodoOperacionCol);
                 }
                 if (observaciones != null && !observaciones.Equals(""))
                 {
                     cnn.parametros.Add("@observaciones", observaciones);
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

         public bool EliminarSolicitudConcesion(int idSolConcesion, int idTipoTramite, int idUsuario)
         {
             try
             {
                 int resultado = 0;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbSolicitudConcesion";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                 cnn.parametros.Add("@idUsuario", idUsuario);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("idSolConcesion"))
                         {
                             resultado = Convert.ToInt32(row["idSolConcesion"]);
                         }
                     }
                 }

                 if(resultado<0){
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

         public List<SolicitudConcesion> ListarTramitesSolicitud(string codigoCentro, int idPestania, string nombreUE, int idUnidadEspacial)
         {
             /*
              * idPestania=1: trámites aprobados
              * idPestania=2: trámites rechazados
              * idPestania=3: trámites 'en trámite'
              */

             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
                 string procedimiento="";

                 if(nombreUE.Equals("Acopio")){
                    procedimiento ="paSelRbAcopioTramitesSolicitud";
                 }
                 if (nombreUE.Equals("Amerb")){
                     procedimiento = "paSelRbAmerbTramitesSolicitud";
                 }
                 if (nombreUE.Equals("Colector")){
                     procedimiento = "paSelRbColectorTramitesSolicitud";
                 }
                 if (nombreUE.Equals("Ecmpo")){
                     procedimiento = "paSelRbEcmpoTramitesSolicitud";
                 }
                 if (nombreUE.Equals("Faenamiento"))
                 {
                     procedimiento = "paSelRbFaenamientoTramitesSolicitud";
                 }
                 if (nombreUE.Equals("ExperimentalesAmerb"))
                 {
                     procedimiento = "paSelRbExperimentalAmerbTramitesSolicitud";
                 }
                 if (nombreUE.Equals("ExperimentalesConcesion"))
                 {
                     procedimiento = "paSelRbExperimentalConcesionTramitesSolicitud";
                 }


                 if (!procedimiento.Equals("")){
                    Conexion cnn = new Conexion();
                     cnn.procedimiento = procedimiento;

                     if (codigoCentro != null) { 
                        cnn.parametros.Add("@codigoCentro", codigoCentro);
                     }

                     cnn.parametros.Add("@idPestania", idPestania);

                     if (nombreUE.Equals("Colector") || nombreUE.Equals("ExperimentalesAmerb") || nombreUE.Equals("ExperimentalesConcesion")) //tal vez no tengan codigo de centro por lo que se busca a traves de la unidad espacial
                     {
                         cnn.parametros.Add("@idUnidadEspacial", idUnidadEspacial);
                     }

                     DataTable dt = cnn.Execute();

                     if (dt != null)
                     {

                         foreach (DataRow row in dt.Rows)
                         {
                             idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                             if (idSolicitud != idSolicitudAux)
                             {
                                 solicitudAux = new SolicitudConcesion();
                                 solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                                 if (!row.IsNull("idEstadoActual"))
                                 {
                                     solicitudAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                                 }
                                 if (!row.IsNull("numPert"))
                                 {
                                     solicitudAux.numPert = row["numPert"].ToString();
                                 }
                                 if (!row.IsNull("titularesCad"))
                                 {
                                     solicitudAux.titularesCad = row["titularesCad"].ToString();
                                 }
                                 if (!row.IsNull("fechaIngresoTramite"))
                                 {
                                     solicitudAux.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                                 }
                                 if (!row.IsNull("idTipoUnidEspacial"))
                                 {
                                     solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["nombreUE"].ToString());
                                 }
                                 if (!row.IsNull("idTipoTramite"))
                                 {
                                     solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["nombreTramite"].ToString());
                                 }
                                 solicitudAux.traspasoOk = Convert.ToBoolean(row["traspasoOk"]);

                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoCentro"].ToString();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.nombreCentro = row["codigoCentro"].ToString();

                                 resp.Add(solicitudAux);
                             }

                             idSolicitudAux = idSolicitud;
                         }

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

         /*
            Obtiene datos de la solicitud/unidad espacial dependiendo del identificador,numPert o codigoCentro que se pasa como parámetro a buscar.
         */
         public List<SolicitudConcesion> ListarSolicitudFiltroIdentificador(string identificador)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 int idSolicitud = 0;
                 int idSolicitudAux = 0;
               
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudFiltroIdentificador";

                 if (identificador!= null)
                 {
                     cnn.parametros.Add("@identificador", identificador);
                 }
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {
                         idSolicitud = Convert.ToInt32(row["idSolConcesion"]);

                         if (idSolicitud != idSolicitudAux)
                         {
                             solicitudAux = new SolicitudConcesion();

                             if (!row.IsNull("idSolConcesion"))
                             {
                                 solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             }
                             if (!row.IsNull("numPert"))
                             {
                                 solicitudAux.numPert = row["numPert"].ToString();
                             }
                             if (!row.IsNull("IdRegion"))
                             {
                                 solicitudAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                             }
                             if (!row.IsNull("comunasCad"))
                             {
                                 solicitudAux.comunasCad = row["comunasCad"].ToString();
                             }
                             if (!row.IsNull("codigoSiep"))
                             {
                                 solicitudAux.unidadEspacial = new UnidadEspacial();
                                 solicitudAux.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 solicitudAux.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoSiep"].ToString();
                             }
                             if (!row.IsNull("idTipoUnidEspacial"))
                             {
                                 solicitudAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacial"]), row["tipoUE"].ToString());
                             }
                             if (!row.IsNull("idTipoTramite"))
                             {
                                 solicitudAux.tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipoTramite"]), row["tipoTramite"].ToString());
                             }
                             if (!row.IsNull("tipoModSolicitud"))
                             {
                                 solicitudAux.tipoModSolicitud = row["tipoModSolicitud"].ToString();
                             }

                             if (!row.IsNull("titularesCad"))
                             {
                                 solicitudAux.titularesCad = row["titularesCad"].ToString();
                             }
                             if (!row.IsNull("tipo"))
                             {
                                 solicitudAux.tipoSolicitudCad = row["tipo"].ToString();
                             }
                             resp.Add(solicitudAux);
                         }
                         idSolicitudAux = idSolicitud;
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

         public int obtenerIdSolicitudFiltro(String identificador, bool centro)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbUnidadesEspaciales_Filtro";
                 cnn.parametros.Add("@identificador", identificador);
                 cnn.parametros.Add("@centro", centro);

                 int idSolicitud = 0;
                 
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row.IsNull("idSolConcesion"))
                        {
                             idSolicitud = Convert.ToInt32(row["idSolConcesion"]);
                        }
                     }
                 }

                 return idSolicitud;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return 0;
             }
         }


         public bool ActualizarVigenciaBarrio(int idBarrio, int idVigencia)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbBarrio_Vigencia";
                 cnn.parametros.Add("@IdBarrio", idBarrio);
                 cnn.parametros.Add("@idEstadoVigencia", idVigencia);

                 cnn.Execute();

                 return true;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return false;
             }
         }




         public bool ActualizaSolicitud_DecisionUsuario(int idSolicitud, string columna, int valor)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paInsRbDecisionUsuario";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 cnn.parametros.Add("@columna", columna);
                 if (valor > 0)
                 {
                     cnn.parametros.Add("@valor", valor);
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

         public SolicitudConcesion obtenerDecisionUsuario(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbDecisionUsuario";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 SolicitudConcesion solConcesion = new SolicitudConcesion(); 

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {
                        
                         solConcesion.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                         if (!row.IsNull("idDevPesca"))
                         {
                             solConcesion.devolucionJuridica = new ParametroGenerico(Convert.ToInt32(row["idDevPesca"]));
                         }
                         if (!row.IsNull("idDevMarina"))
                         {
                             solConcesion.devolucionMarina = new ParametroGenerico(Convert.ToInt32(row["idDevMarina"]));
                         }
                         if (!row.IsNull("requiereITC_MO"))
                         {
                             solConcesion.nuevoITCMOUOT = new ParametroGenerico(Convert.ToInt32(row["requiereITC_MO"]));
                         }
                         if (!row.IsNull("evaluaUOT"))
                         {
                             solConcesion.evaluaUOT = new ParametroGenerico(Convert.ToInt32(row["evaluaUOT"]));
                         }
                         if (!row.IsNull("informarSMA"))
                         {
                             solConcesion.pertinenciaSMA = new ParametroGenerico(Convert.ToInt32(row["informarSMA"]));
                         }
                         if (!row.IsNull("evalRamaNoSomete"))
                         {
                             solConcesion.tipoEvaluacionAmbiental = new ParametroGenerico(Convert.ToInt32(row["evalRamaNoSomete"]));
                         }
                         if (!row.IsNull("requiereITC_CPS"))
                         {
                             solConcesion.nuevoITCCPSUOT = new ParametroGenerico(Convert.ToInt32(row["requiereITC_CPS"]));
                         }
                         if (!row.IsNull("idTipoSolCertDistancia"))
                         {
                             solConcesion.tipoCertificadoDistancia = new ParametroGenerico(Convert.ToInt32(row["idTipoSolCertDistancia"]));
                         }
                         if (!row.IsNull("esperaRespuestaSMA"))
                         {
                             solConcesion.avanzaSMA = new ParametroGenerico(Convert.ToInt32(row["esperaRespuestaSMA"]));
                         }
                         if (!row.IsNull("envioSSPCentral_zonal"))
                         {
                             solConcesion.decisionZonal = new ParametroGenerico(Convert.ToInt32(row["envioSSPCentral_zonal"]));
                         }
                         if (!row.IsNull("confZonalNotInsuficiencia"))
                         {
                             solConcesion.decisionNotificacionInsuficiencia = new ParametroGenerico(Convert.ToInt32(row["confZonalNotInsuficiencia"]));
                         }
                         if (!row.IsNull("sspCentralDevCarta"))
                         {
                             solConcesion.decisionCentral = new ParametroGenerico(Convert.ToInt32(row["sspCentralDevCarta"]));
                         }
                         if (!row.IsNull("confCentralNotInsuficiencia"))
                         {
                             solConcesion.decisionNotificacionInsuficienciaCentral = new ParametroGenerico(Convert.ToInt32(row["confCentralNotInsuficiencia"]));
                         }
                         if (!row.IsNull("tramitaUGP"))
                         {
                             solConcesion.decisionUGP = new ParametroGenerico(Convert.ToInt32(row["tramitaUGP"]));
                         }
                         if (!row.IsNull("estadoAplica"))
                         {
                             solConcesion.decisionUGPMultiple = new ParametroGenerico(Convert.ToInt32(row["estadoAplica"]));
                         }
                         if (!row.IsNull("verificaAntecedentes"))
                         {
                             solConcesion.verificaAntecedentes = new ParametroGenerico(Convert.ToInt32(row["verificaAntecedentes"]));
                         }
                         if (!row.IsNull("idTipoRechazoSol"))
                         {
                             solConcesion.tipoRechazoSol = new ParametroGenerico(Convert.ToInt32(row["idTipoRechazoSol"]));
                         }
                         if (!row.IsNull("tramitaUTS"))
                         {
                             solConcesion.tramitaUTS = new ParametroGenerico(Convert.ToInt32(row["tramitaUTS"]));
                         }
                         if (!row.IsNull("requiereIT_UOT_Plano"))
                         {
                             solConcesion.requiereITUOT_Plano = new ParametroGenerico(Convert.ToInt32(row["requiereIT_UOT_Plano"]));
                         }
                         if (!row.IsNull("evaluaUOT_Cartografia"))
                         {
                             solConcesion.evaluaUOT_Cartografia = new ParametroGenerico(Convert.ToInt32(row["evaluaUOT_Cartografia"]));
                         }
                         if (!row.IsNull("requiereNuevoPT"))
                         {
                             solConcesion.requiereNuevoPT = new ParametroGenerico(Convert.ToInt32(row["requiereNuevoPT"]));
                         }
                         if (!row.IsNull("supeditaAvanzaAprueba"))
                         {
                             solConcesion.supeditaAvanzaAprueba = new ParametroGenerico(Convert.ToInt32(row["supeditaAvanzaAprueba"]));
                         }
                         if (!row.IsNull("suspendeAvanzaEstado"))
                         {
                             solConcesion.suspendeAvanzaEstado = new ParametroGenerico(Convert.ToInt32(row["suspendeAvanzaEstado"]));
                         }
                         if (!row.IsNull("omiteSSFFAA"))
                         {
                             solConcesion.omiteSSFFAA = new ParametroGenerico(Convert.ToInt32(row["omiteSSFFAA"]));
                         }
                         if (!row.IsNull("omiteInspTerreno"))
                         {
                             solConcesion.omiteInspTerreno = new ParametroGenerico(Convert.ToInt32(row["omiteInspTerreno"]));
                         }
                         if (!row.IsNull("omiteBanco"))
                         {
                             solConcesion.omiteBanco = new ParametroGenerico(Convert.ToInt32(row["omiteBanco"]));
                         }
                         if (!row.IsNull("omiteDifRadial"))
                         {
                             solConcesion.omiteDifRadial = new ParametroGenerico(Convert.ToInt32(row["omiteDifRadial"]));
                         }
                         if (!row.IsNull("omiteEvAmbiental"))
                         {
                             solConcesion.omiteEvAmbiental = new ParametroGenerico(Convert.ToInt32(row["omiteEvAmbiental"]));
                         }

                         break;
                     }
                 }

                 return solConcesion;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             }
         }

         public int obtieneSolicitudIdPert(string numPert)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitudConcesionPert";
                 cnn.parametros.Add("@numPert", numPert);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {

                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("idSolConcesion") && !row.IsNull("numPert"))
                         {
                             return Convert.ToInt32(row["idSolConcesion"]);
                         }
                     }
                 }

                 return 0;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return 0;
             }
        }

        //ELIMINA LA UNIDAD ESPACIAL Y SU TRAMITE HISTÓRICO COMPLETO
         public bool EliminarUE_Total(string numPert, int idSolConcesion,int idUsuario) 
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paDelRbConcesionPert";
                 if(numPert!=null && !numPert.Equals("")){
                    cnn.parametros.Add("@numPert", numPert);
                 }
                 if (idSolConcesion >0)
                 {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 }
                 if (idUsuario > 0)
                 {
                     cnn.parametros.Add("@idUsuario", idUsuario);
                 }
                 
              
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("idSolConcesion") && Convert.ToInt32(row["idSolConcesion"])>0)
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

         //PASA UNA UE A UNA SOLICITUD
         public ParametroGenerico PasoUE_TramiteSolicitud(int idSolConcesion, int idUsuario)
         {
             try
             {

                 String mesage = null;

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbUnidadEspacial_Solicitud";
                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 if (idUsuario > 0)
                 {
                     cnn.parametros.Add("@idUsuario", idUsuario);
                 }


                 ParametroGenerico resultado = null;
                 
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {
                         resultado = new ParametroGenerico();
                         if (!row.IsNull("idSolConcesionTramite") && Convert.ToInt32(row["idSolConcesionTramite"]) > 0)
                         {
                             resultado = new ParametroGenerico(1); //modificacion correcta
                             return resultado;
                         }
                         else {

                             if (!row.IsNull("msg")) {
                                 mesage = row["msg"].ToString();
                                 resultado = new ParametroGenerico(0, mesage);
                                return resultado;
                             } 
                         }
                         break;
                     }
                 }

                 return resultado;
             }
             catch (Exception ex)
             {
                 logger.PrintError(ex);
                 logger.SendMailError(ex);
                 return null;
             };
         }

         public bool ActualizaSolicitudConcesionEstado(int idSolicitud, int idEstadoActual)
         {
             try
             {

                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paUpdRbSolicitudConcesionEstado";
                 cnn.parametros.Add("@idSolConcesion", idSolicitud);
                 cnn.parametros.Add("@idEstadoActual", idEstadoActual);
                 
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



         public bool tieneEspeciesExperimentalesSolicitud(int idSolConcesion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelEspecieCultivoExperimental";

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
             };
         }
         /*
           El siguiente procedimiento elimina solicitud de UE, y relocalizaciones. Si el trámite a eliminar corresponde a una solicitud de modificación ya finalizada no debe permitir la eliminación,
           ya que generaría una incongruencia debido a que la información de la modificación ya fué traspasada a la unidad espacial,
           lo mismo para relocalizaciones finalizadas.
          */
         public bool EliminarSolicitudUE(int idSolConcesion, int idUsuario)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 int aux;
                 cnn.procedimiento = "paDelRbSolicitudUnidadEspacial";
                 cnn.parametros.Add("@idSolicitud", idSolConcesion);
                 if(idUsuario>0){
                     cnn.parametros.Add("@idUsuario", idUsuario);
                 }
                
                 DataTable dt = cnn.Execute();
                
                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {

                         if (!row.IsNull("resp"))
                         {
                             aux = Convert.ToInt32(row["resp"]);
                             if (aux<0) {
                                 return false;
                             }
                         }
                     }
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

         //Listado de colectores que registrando estado vigente su fecha de vigencia ya expiró
         public List<SolicitudConcesion> ListarSolicitud_ColectorVencido(int idSolConcesion)
         {
             try
             {
                 SolicitudConcesion solicitudAux = null;
                 List<SolicitudConcesion> resp = new List<SolicitudConcesion>();
                 
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbSolicitud_ColectorVencido";

                 if(idSolConcesion>0){
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                 }
                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {
                        solicitudAux = new SolicitudConcesion();
                        solicitudAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        solicitudAux.numPert = row["numPert"].ToString();
                        solicitudAux.titularesCad = row["titularesCad"].ToString();
                        solicitudAux.datosSolicitudUE = new DatosSolicitudUE();
                        solicitudAux.datosSolicitudUE.vigenciaColector = Convert.ToDateTime(row["vigenciaColector"]);

                        resp.Add(solicitudAux);
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
        
        //Validar que tenga el documento anterior y si los tiene que sean estado aprueba al momento de ingresar ITDAC Aprueba
        public bool cumpleValidacionIngresoITDAC_Aprueba(int idSolConcesion)
        {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbValidaIngresoITDAC_Aprueba";

                 cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                 DataTable dt = cnn.Execute();

                 if (dt != null)
                 {
                     foreach (DataRow row in dt.Rows)
                     {
                         return Convert.ToBoolean(row["permiteITDAC"]); ;
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
        //Validar que tenga el documento ITDAC aprueba al momento de ingresar resolucion SSP aprueba
        public bool cumpleValidacionIngresoSSP_Aprueba(int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidaIngresoSSP_Aprueba";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return Convert.ToBoolean(row["permiteSSP"]); ;
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

        //Validar que tenga el documento SSP al momento de ingresar resolucion SSFFAA aprueba
        public bool cumpleValidacionIngresoSSFFAA_Aprueba(int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidaIngresoSSFFAA_Aprueba";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return Convert.ToBoolean(row["permiteSSFFAA"]); ;
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

        //Validar que tenga el documento ITDAC al momento de ingresar resolucion SSP rechaza
        public bool cumpleValidacionIngresoSSP_Rechaza(int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidaIngresoSSP_Rechazo";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return Convert.ToBoolean(row["permiteSSP"]); ;
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

        //Validar que tenga el documento SSP al momento de ingresar resolucion SSFFAA rechaza
        public bool cumpleValidacionIngresoSSFFAA_Rechaza(int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidaIngresoSSFFAA_Rechazo";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return Convert.ToBoolean(row["permiteSSFFAA"]); ;
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

        /**
       * VALIDA QUE PERT/IDENTIFICADOR ASOCIADO COINCIDA CON EL TIPO DE TRAMITE PASADO COMO PARÁMETRO 
         * DEVUELVE VERDADERO SÓLO SI TIPO TRAMITE E IDENTIFICADOR COINCIDEN
         * PROCEDIMIENTO SOLO APLICABLE PARA SUPEDITADO
       **/
        public bool ValidaPertTramite(int idTipoTram, string identificador)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudValidacionSupeditado";

                cnn.parametros.Add("@idTipoSupeditado", idTipoTram);
                cnn.parametros.Add("@numPert", identificador);

               
                DataTable dt = cnn.Execute();

                if (dt != null && dt.Rows.Count > 0)
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
            }
        }



        
       
        public bool SolicitudEstaSuspendidaPendientSupeditada(int idSolConcesion)
        {
            try
            {
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelVerificaSupeditadaPendienteSuspendida";
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

        public string AdminSolicitudConcesionInfo(string pert)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionInfo";
                cnn.parametros.Add("@pert", pert);

                string msg = null;

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        msg = Convert.ToString(row["msg"]); 
                    }
                }
                return msg;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public string ObtieneReqPendiente_Solicitud(int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRequerimientoPendiente";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                string reqPend="";

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (reqPend.Equals(""))
                        {
                            reqPend = reqPend + Convert.ToString(row["pendiente"]);
                        }
                        else {
                            reqPend = reqPend +","+ Convert.ToString(row["pendiente"]);
                        }
                        
                        
                    }
                }
                return reqPend;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool SolicitudEstaSuspendidaPendiente(int idSolConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelVerificaSupeditadaPendiente";
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

        public bool SolicitudEstaSuspendida(int idSolConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelVerificaSuspendida";
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

        public HeaderSolicitudConcesion ObtieneSolicitudConcesionHeader(int idSolConcesion)
        {
            try
            {
                int idSolConcesionVar = 0;
                int idSolConcesionAux = 0;
                HeaderSolicitudConcesion solicitudResp = null;
                EstadoIsla islaAux = null;
                HashSet<int> clavesIsla = new HashSet<int>();
                //bool seaAux;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSolicitudConcesionHeader";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idSolConcesionVar = Convert.ToInt32(row["idSolConcesion"]);
                        if (idSolConcesionVar != idSolConcesionAux)
                        {
                            solicitudResp = new HeaderSolicitudConcesion();
                            solicitudResp.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            if (!row.IsNull("numPert"))
                            {
                                solicitudResp.numPert = row["numPert"].ToString();
                            }
                            if (!row.IsNull("idEstadoActual"))
                            {
                                solicitudResp.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstadoActual"].ToString());
                                if (!row.IsNull("nodoCantidad"))
                                {
                                    solicitudResp.estadoActual.clave = row["nodoCantidad"].ToString();
                                }

                            }
                            if (!row.IsNull("docReq"))
                            {
                                solicitudResp.docRequerido = row["docReq"].ToString();
                            }

                            if (!row.IsNull("idEstadoAnterior"))
                            {
                                solicitudResp.estadoAnterior = new ParametroGenerico(Convert.ToInt32(row["idEstadoAnterior"]), row["nombreEstadoAnterior"].ToString());
                            }
                            if (!row.IsNull("idEstadoPosterior"))
                            {
                                solicitudResp.estadoPosterior = new ParametroGenerico(Convert.ToInt32(row["idEstadoPosterior"]), row["nombreEstadoPosterior"].ToString());
                            }
                            if (!row.IsNull("requiereNuevoPT"))
                            {
                                solicitudResp.requiereNuevoPT = new ParametroGenerico(Convert.ToInt32(row["requiereNuevoPT"]), "");
                            }
                            if (!row.IsNull("supeditaAvanzaAprueba"))
                            {
                                solicitudResp.supeditaAvanzaAprueba = new ParametroGenerico(Convert.ToInt32(row["supeditaAvanzaAprueba"]), "");
                            }
                            if (!row.IsNull("suspendeAvanzaEstado"))
                            {
                                solicitudResp.suspendeAvanzaEstado = new ParametroGenerico(Convert.ToInt32(row["suspendeAvanzaEstado"]), "");
                            }
                            if (!row.IsNull("idConcesion"))
                            {
                                solicitudResp.idConcesion = Convert.ToInt32(row["idConcesion"]);
                            }
                            solicitudResp.especiePerteneceCultExp = Convert.ToBoolean(row["especiesCultExp"]);
                            solicitudResp.tiene_ITC_pend_sup = Convert.ToBoolean(row["tiene_ITC_pend_sup"]);
                            solicitudResp.perteneceGrupoSuspendido = Convert.ToBoolean(row["perteneceGrupoSuspendido"]);

                            solicitudResp.islas = new List<EstadoIsla>();
                            clavesIsla = new HashSet<int>();
                        }

                       
                        if (!row.IsNull("idEstadoActualIsla"))
                        {
                            if (clavesIsla.Count == 0 || !clavesIsla.Contains(Convert.ToInt32(row["idEstadoActualIsla"])))
                            {
                                clavesIsla.Add(Convert.ToInt32(row["idEstadoActualIsla"]));
                                //--
                                islaAux = new EstadoIsla();
                                islaAux.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActualIsla"]), row["nombreEstadoIsla"].ToString());
                                islaAux.tipoIsla = new ParametroGenerico(Convert.ToInt32(row["idTipoIsla"]), row["nombreTipoIsla"].ToString());
                                solicitudResp.islas.Add(islaAux);
                            }
                        }

                        idSolConcesionAux = idSolConcesionVar;

                    }
                }
                return solicitudResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarTipoModificacionSolicitud(int idSolConcesion, int idTipoModificacion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbTipoModificacionSolicitud";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                if (idTipoModificacion>0)
                {
                    cnn.parametros.Add("@idTipoModificacion", idTipoModificacion);
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

    }
}
