using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades.Relocalizacion;
using Datos.Entidades;
using System.Data;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.relocalizacion
{
    public class DetalleSectorDA
    {

        public Logger Log { get; set; }

        public DetalleSectorDA()
        {
            this.Log = new Logger();
        }

        public bool GuardarDetalleSector(DetalleSector detSector)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDetalleSector";
                cnn.parametros.Add("@idDetalleSector", detSector.idDetalleSector);
                cnn.parametros.Add("@idTramiteRel", detSector.tramiteRel.idTramiteRel);
                if (detSector.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", detSector.idSolConcesion);
                }
                cnn.parametros.Add("@idTipoRelocalizacion", detSector.tipoRelocalizacion.id);
                cnn.parametros.Add("@idEstadoSector", detSector.estadoSector.id);
                cnn.parametros.Add("@numSector", detSector.numSector);
                cnn.parametros.Add("@esSectorCero", detSector.esSectorCero);
                if (detSector.concesionDestino != null && detSector.concesionDestino.unidadEspacial != null && detSector.concesionDestino.unidadEspacial.centrosDeCultivo!=null && !detSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@codSiepDestino", detSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro);
                }
                
                cnn.parametros.Add("@superficieSector", detSector.superficieSector);
                cnn.parametros.Add("@versionDS", detSector.version);

                
                DataTable dt = cnn.Execute();
                detSector.idDetalleSector = Convert.ToInt32(dt.Rows[0]["idDetalleSector"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public DetalleSector obtenerDetalleSector(int idTramiteRel, int idDetalleSector)
        {
            try
            {
                DetalleSector detalleSec = null;
                OrigenSector origenSec = null;
                ParametroGenerico preferencia = null;

                int idDetSector = 0;
                int idDetSectorAux = 0;

                HashSet<int> origenes = new HashSet<int>();
                HashSet<int> preferencias = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDetalleSector";
                if (idTramiteRel > 0)
                {
                    cnn.parametros.Add("@idTramiteRel", idTramiteRel);
                }
                if (idDetalleSector > 0)
                {
                    cnn.parametros.Add("@idDetalleSector", idDetalleSector);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        idDetSector = Convert.ToInt32(row["idDetalleSector"]);
                        if (idDetSector != idDetSectorAux)
                        {
                            detalleSec = new DetalleSector();
                            detalleSec.idDetalleSector = Convert.ToInt32(row["idDetalleSector"]);
                            detalleSec.tramiteRel = new TramiteRelocalizacion();
                            detalleSec.tramiteRel.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);

                            if (!row.IsNull("idSolConcesion"))
                            {
                                detalleSec.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            }
                            detalleSec.contieneSSp = Convert.ToBoolean(row["contieneSSp"]);

                            if (!row.IsNull("idEstadoResultadoResp"))
                            {
                                detalleSec.estadoSSp = new ParametroGenerico(Convert.ToInt32(row["idEstadoResultadoResp"]));
                            }

                            detalleSec.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipoRel"].ToString());
                            if (!row.IsNull("numPert"))
                            {
                                detalleSec.numPert = row["numPert"].ToString();
                            }
                            detalleSec.estadoSector = new ParametroGenerico(Convert.ToInt32(row["idEstadoSector"]), row["nombreEstado"].ToString());
                            detalleSec.numSector = Convert.ToInt32(row["numSector"]);
                            detalleSec.esSectorCero = Convert.ToBoolean(row["esSectorCero"]);
                            if (!row.IsNull("codSiepDestino"))
                            {
                                detalleSec.concesionDestino = new SolicitudConcesion();
                                detalleSec.concesionDestino.unidadEspacial = new UnidadEspacial();
                                detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = row["codSiepDestino"].ToString();
                                if (!row.IsNull("nombreCentroDestino"))
                                {
                                    detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroDestino"].ToString();
                                }
                                if (!row.IsNull("idSolConcesionDestino"))
                                {
                                    detalleSec.concesionDestino.unidadEspacial.idSolicitud = Convert.ToInt32(row["idSolConcesionDestino"]);
                                }
                                
                            }

                            detalleSec.superficieSector = Convert.ToSingle(row["superficieSector"]);
                            detalleSec.origenes = new List<OrigenSector>();
                            origenes = new HashSet<int>();

                            
                        }
                        if (!row.IsNull("idOrigenSector"))
                        {
                            origenSec = new OrigenSector();
                            origenSec.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);
                            if (origenes.Count == 0 || !origenes.Contains(origenSec.idOrigenSector))
                            {
                                origenes.Add(origenSec.idOrigenSector);

                                origenSec.concesionOrigen = new SolicitudConcesion();
                                origenSec.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoSiep"].ToString();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroOrigen"].ToString();
                                if (!row.IsNull("idSolConcesionOrigen"))
                                {
                                    origenSec.concesionOrigen.unidadEspacial.idSolicitud = Convert.ToInt32(row["idSolConcesionOrigen"]);
                                }
                                
                                origenSec.superficieRelocalizada = Convert.ToSingle(row["superficieRelocalizacion"]);

                                detalleSec.origenes.Add(origenSec);
                                origenSec.preferencias = new List<ParametroGenerico>();
                                preferencias = new HashSet<int>();
                            }

                        }

                        if (!row.IsNull("idPreferenciaRel"))
                        {
                            preferencia = new ParametroGenerico(Convert.ToInt32(row["idPreferenciaRel"]), row["nombrePreferenciaRel"].ToString());
                            if (preferencias.Count == 0 || !preferencias.Contains(preferencia.id))
                            {
                                preferencias.Add(preferencia.id);
                                origenSec.preferencias.Add(preferencia);
                            }
                        }

                        idDetSectorAux = idDetSector;
                    }



                }

                return detalleSec;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<DetalleSector> ListarDetalleSector(int idTramiteRel, int idDetalleSector, int contieneSSp)
        {
            try
            {
                DetalleSector detalleSec = null;
                List<DetalleSector> resp = new List<DetalleSector>();
                OrigenSector origenSec = null;
                ParametroGenerico preferencia = null;

                int idDetSector = 0;
                int idDetSectorAux = 0;
                
                HashSet<int> origenes = new HashSet<int>();
                HashSet<int> preferencias = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComunaO = new HashSet<int>();
                HashSet<int> clavesPersonaO = new HashSet<int>();

                //
                CoordenadaGeografica coordAux = null;
                Poligono poligonoAux = null;
                ParametroGenerico comunaResp = null;
                Persona persona = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDetalleSector";
                if(idTramiteRel>0){
                    cnn.parametros.Add("@idTramiteRel", idTramiteRel);
                }
                if(idDetalleSector>0){
                    cnn.parametros.Add("@idDetalleSector", idDetalleSector);
                }
                if (contieneSSp > 0)
                {
                    cnn.parametros.Add("@contieneSSp", contieneSSp);
                }
                
                DataTable dt = cnn.Execute();

                int indexSector = 0;
                int indexOrigen = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        idDetSector = Convert.ToInt32(row["idDetalleSector"]);
                        if(idDetSector!=idDetSectorAux){

                            detalleSec = new DetalleSector();
                            detalleSec.index = indexSector;
                            indexSector++;


                            detalleSec.accion = accion.LISTADO;

                            detalleSec.idDetalleSector = Convert.ToInt32(row["idDetalleSector"]);
                            detalleSec.tramiteRel = new TramiteRelocalizacion();
                            detalleSec.tramiteRel.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);

                            if (!row.IsNull("idSolConcesion"))
                            {
                                detalleSec.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            }
                            detalleSec.contieneSSp = Convert.ToBoolean(row["contieneSSp"]);

                            if (!row.IsNull("idEstadoResultadoResp"))
                            {
                                detalleSec.estadoSSp = new ParametroGenerico(Convert.ToInt32(row["idEstadoResultadoResp"]));
                            }

                            detalleSec.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipoRel"].ToString());
                            if (!row.IsNull("numPert"))
                            {
                                detalleSec.tramiteRel.numPert = row["numPert"].ToString();
                            }
                            detalleSec.estadoSector = new ParametroGenerico(Convert.ToInt32(row["idEstadoSector"]), row["nombreEstado"].ToString());
                            detalleSec.numSector = Convert.ToInt32(row["numSector"]);
                            detalleSec.esSectorCero = Convert.ToBoolean(row["esSectorCero"]);
                            detalleSec.superficieSector = Convert.ToSingle(row["superficieSector"]);
                            if (!row.IsNull("codSiepDestino"))
                            {
                                detalleSec.concesionDestino = new SolicitudConcesion();
                                detalleSec.concesionDestino.unidadEspacial = new UnidadEspacial();
                                detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = row["codSiepDestino"].ToString();
                                if (!row.IsNull("idSolConcesionDestino"))
                                {
                                    detalleSec.concesionDestino.unidadEspacial.idSolicitud = Convert.ToInt32(row["idSolConcesionDestino"]);
                                }
                                detalleSec.concesionDestino.coordenadaGeografica = new List<CoordenadaGeografica>();
                                detalleSec.concesionDestino.barrio = new Barrio();
                                detalleSec.concesionDestino.comuna = new List<ParametroGenerico>();
                                detalleSec.concesionDestino.titularesSolConcesion = new List<Persona>();

                                coordAux = new CoordenadaGeografica();
                                coordAux.listaPoligono = new List<Poligono>();
                                poligonoAux = new Poligono();

                                detalleSec.concesionDestino.coordenadaGeografica.Add(coordAux);
                                coordAux.listaPoligono.Add(poligonoAux);

                                if (!row.IsNull("nombreCentroDestino"))
                                {
                                    detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroDestino"].ToString();
                                }
                                detalleSec.concesionDestino.numPert = row["numPertDestino"].ToString();
                                poligonoAux.toponimio = row["toponimioDestino"].ToString();
                                poligonoAux.areaCalculada = Convert.ToSingle(row["superficieTotalCalculadaDestino"]);

                                if (!row.IsNull("idRegDestino"))
                                {
                                    detalleSec.concesionDestino.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestino"]), row["nombreRegDestino"].ToString());
                                }

                                if (!row.IsNull("IdBarrioDestino"))
                                {
                                    detalleSec.concesionDestino.barrio.id_barrio = Convert.ToInt32(row["IdBarrioDestino"]);
                                    detalleSec.concesionDestino.barrio.barrio = row["BarrioDestino"].ToString();
                                }

                                if (!row.IsNull("idTipoUnidEspacialDestino"))
                                {
                                    detalleSec.concesionDestino.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacialDestino"]), row["nombreTipoUnidEspDestino"].ToString());
                                }
                                
                            }

                            detalleSec.origenes = new List<OrigenSector>();
                            //detalleSec.concesionDestino.comuna = new List<ParametroGenerico>();
                            //detalleSec.concesionDestino.titularesSolConcesion = new List<Persona>();
                            origenes = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                           
                            resp.Add(detalleSec);
                        }

                        if (!row.IsNull("idComunaDestino"))
                        {
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(Convert.ToInt32(row["idComunaDestino"])))
                            {
                                clavesComuna.Add(Convert.ToInt32(row["idComunaDestino"]));
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["idComunaDestino"]), row["nomComunaDestino"].ToString());
                                detalleSec.concesionDestino.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersonaDestino"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersonaDestino"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersonaDestino"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersonaDestino"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificadorDestino"]);
                                persona.nombreSolicitante = row["nombrePersDestino"].ToString();
                                detalleSec.concesionDestino.titularesSolConcesion.Add(persona);
                            }
                        }

                        if (!row.IsNull("idOrigenSector"))
                        {
                            if (origenes.Count == 0 || !origenes.Contains(Convert.ToInt32(row["idOrigenSector"])))
                            {
                                origenSec = new OrigenSector();
                                origenSec.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);


                                origenSec.index = indexOrigen;
                                indexOrigen++;

                                origenes.Add(origenSec.idOrigenSector);

                                preferencias = new HashSet<int>(); //SE LIMPIA EL HASH DE PREFERENCIAS


                                coordAux = new CoordenadaGeografica();
                                coordAux.listaPoligono = new List<Poligono>();
                                poligonoAux = new Poligono();
                                origenSec.concesionOrigen = new SolicitudConcesion();
                                origenSec.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                origenSec.concesionOrigen.coordenadaGeografica = new List<CoordenadaGeografica>();
                                origenSec.concesionOrigen.comuna = new List<ParametroGenerico>();
                                origenSec.concesionOrigen.barrio = new Barrio();

                                coordAux.listaPoligono.Add(poligonoAux);
                                origenSec.concesionOrigen.coordenadaGeografica.Add(coordAux);
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoSiepOrigen"].ToString();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroOrigen"].ToString();
                                if (!row.IsNull("idSolConcesionOrigen"))
                                {
                                    origenSec.concesionOrigen.unidadEspacial.idSolicitud = Convert.ToInt32(row["idSolConcesionOrigen"]);
                                }
                                origenSec.superficieRelocalizada = Convert.ToSingle(row["superficieRelocalizacion"]);
                                origenSec.concesionOrigen.numPert = row["numPertOrigen"].ToString();
                                poligonoAux.toponimio = row["toponimioOrigen"].ToString();
                                poligonoAux.areaCalculada = Convert.ToSingle(row["superficieTotalCalculadaOrigen"]);

                                if (!row.IsNull("idRegDestinoOrigen"))
                                {
                                    origenSec.concesionOrigen.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestinoOrigen"]), row["nombreRegOrigen"].ToString());
                                }
                                
                                if (!row.IsNull("IdBarrioOrigen"))
                                {
                                    origenSec.concesionOrigen.barrio.id_barrio = Convert.ToInt32(row["IdBarrioOrigen"]);
                                    origenSec.concesionOrigen.barrio.barrio = row["BarrioOrigen"].ToString();
                                }
                                if (!row.IsNull("idTipoUnidEspacialOrigen"))
                                {
                                    origenSec.concesionOrigen.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacialOrigen"]), row["nombreTipoUnidEspOrigen"].ToString());
                                }
                                
                                origenSec.concesionOrigen.titularesSolConcesion = new List<Persona>();

                                detalleSec.origenes.Add(origenSec);
                                origenSec.preferencias = new List<ParametroGenerico>();
                                origenSec.concesionOrigen.comuna = new List<ParametroGenerico>();
                                clavesComunaO = new HashSet<int>();
                                clavesPersonaO = new HashSet<int>();
                            }
                        }

                        if (!row.IsNull("idPreferenciaRel"))
                        {
                            preferencia = new ParametroGenerico(Convert.ToInt32(row["idPreferenciaRel"]), row["nombrePreferenciaRel"].ToString());
                            if (preferencias.Count == 0 || !preferencias.Contains(preferencia.id))
                            {
                                preferencias.Add(preferencia.id);
                                origenSec.preferencias.Add(preferencia);
                            }
                        }
                        //---------------------------------
                        if (!row.IsNull("idComunaOrigen"))
                        {
                            if (clavesComunaO.Count == 0 || !clavesComunaO.Contains(Convert.ToInt32(row["idComunaOrigen"])))
                            {
                                clavesComunaO.Add(Convert.ToInt32(row["idComunaOrigen"]));
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["idComunaOrigen"]), row["nomComunaOrigen"].ToString());
                                origenSec.concesionOrigen.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersonaOrigen"))
                        {
                            if (clavesPersonaO.Count == 0 || !clavesPersonaO.Contains(Convert.ToInt32(row["rutPersonaOrigen"])))
                            {
                                clavesPersonaO.Add(Convert.ToInt32(row["rutPersonaOrigen"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersonaOrigen"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificadorOrigen"]);
                                persona.nombreSolicitante = row["nombrePersOrigen"].ToString();
                                origenSec.concesionOrigen.titularesSolConcesion.Add(persona);
                            }
                        }

                        idDetSectorAux = idDetSector;
                    }

                }

                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarDetalleSector(int idDetalleSector, int idOrigenSector, int idUser, int opc)
        {
            try
            {
                Conexion cnn = new Conexion();
                int result = 0;
                cnn.procedimiento = "paDelRbDetalleSector";
                cnn.parametros.Add("@idDetalleSector", idDetalleSector);
                if (idOrigenSector > 0)
                {
                    cnn.parametros.Add("@idOrigenSector", idOrigenSector);
                }
                cnn.parametros.Add("@idUser", idUser);
                cnn.parametros.Add("@opc", opc);

                DataTable dt = cnn.Execute();
                result = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (result == 0 || result == -1)
                {
                    return true;
                }
                return false;
    

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public DetalleSector obtenerDetalleSector_Solicitud(int idSolConcesion)
        {
            try
            {
                DetalleSector detalleSec = null;
                OrigenSector origenSec = null;
                
                int idDetSector = 0;
                int idDetSectorAux = 0;

                HashSet<int> origenes = new HashSet<int>();
                HashSet<int> preferencias = new HashSet<int>();
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesPersona = new HashSet<int>();
                HashSet<int> clavesComunaO = new HashSet<int>();
                HashSet<int> clavesPersonaO = new HashSet<int>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDetalleSector_Solicitud";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                //
                CoordenadaGeografica coordAux = null;
                Poligono poligonoAux = null;
                ParametroGenerico comunaResp = null;
                Persona persona = null;

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        idDetSector = Convert.ToInt32(row["idDetalleSector"]);
                        if (idDetSector != idDetSectorAux)
                        {
                            detalleSec = new DetalleSector();
                            detalleSec.idDetalleSector = Convert.ToInt32(row["idDetalleSector"]);
                            detalleSec.tramiteRel = new TramiteRelocalizacion();
                            detalleSec.tramiteRel.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                            detalleSec.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                            detalleSec.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipoRel"].ToString());
                            if (!row.IsNull("numPert"))
                            {
                                detalleSec.numPert = row["numPert"].ToString();
                            }
                            detalleSec.estadoSector = new ParametroGenerico(Convert.ToInt32(row["idEstadoSector"]), row["nombreEstado"].ToString());
                            detalleSec.numSector = Convert.ToInt32(row["numSector"]);
                            detalleSec.esSectorCero = Convert.ToBoolean(row["esSectorCero"]);
                            detalleSec.superficieSector = Convert.ToSingle(row["superficieSector"]);

                            if (!row.IsNull("idUnidEspacialRel"))
                            {
                                detalleSec.tramiteEnCurso = new SolicitudConcesion();
                                detalleSec.tramiteEnCurso.unidadEspacial = new UnidadEspacial();
                                detalleSec.tramiteEnCurso.unidadEspacial.idUnidadEspacial = Convert.ToInt32(row["idUnidEspacialRel"]);

                                if (!row.IsNull("ueIdSolConcesion"))
                                {
                                    detalleSec.tramiteEnCurso.unidadEspacial.idSolicitud = Convert.ToInt32(row["ueIdSolConcesion"]);
                                }

                                if (!row.IsNull("ueCodigoCentro"))
                                {
                                    detalleSec.tramiteEnCurso.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                    detalleSec.tramiteEnCurso.unidadEspacial.centrosDeCultivo.codigoCentro = row["ueCodigoCentro"].ToString();
                                }

                                if (!row.IsNull("ueNumDiarioOficial"))
                                {
                                    detalleSec.tramiteEnCurso.unidadEspacial.numeroDiarioOficial = Convert.ToInt32(row["ueNumDiarioOficial"]);
                                }

                                if (!row.IsNull("ueFechaDiarioOficial"))
                                {
                                    detalleSec.tramiteEnCurso.unidadEspacial.fechaDiarioOficial = Convert.ToDateTime(row["ueFechaDiarioOficial"]);
                                }
                            }

                            
                            if (!row.IsNull("codSiepDestino"))
                            {
                                coordAux = new CoordenadaGeografica();
                                coordAux.listaPoligono = new List<Poligono>();
                                poligonoAux = new Poligono();
                                detalleSec.concesionDestino = new SolicitudConcesion();
                                detalleSec.concesionDestino.unidadEspacial = new UnidadEspacial();
                                detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = row["codSiepDestino"].ToString();
                                detalleSec.concesionDestino.coordenadaGeografica = new List<CoordenadaGeografica>();
                                detalleSec.concesionDestino.barrio = new Barrio();
                                detalleSec.concesionDestino.comuna = new List<ParametroGenerico>();
                                detalleSec.concesionDestino.titularesSolConcesion = new List<Persona>();
                                
                                detalleSec.concesionDestino.coordenadaGeografica.Add(coordAux);
                                coordAux.listaPoligono.Add(poligonoAux);

                                if (!row.IsNull("nombreCentroDestino"))
                                {
                                    detalleSec.concesionDestino.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroDestino"].ToString();
                                }
                                
                                detalleSec.concesionDestino.numPert = row["numPertDestino"].ToString();
                                poligonoAux.toponimio = row["toponimioDestino"].ToString();
                                poligonoAux.areaCalculada = Convert.ToSingle(row["superficieTotalCalculadaDestino"]);


                                if (!row.IsNull("idRegDestinoDestino"))
                                {
                                    detalleSec.concesionDestino.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestinoDestino"]), row["nombreRegDestino"].ToString());
                                }

                                if (!row.IsNull("IdBarrioDestino"))
                                {
                                    detalleSec.concesionDestino.barrio.id_barrio = Convert.ToInt32(row["IdBarrioDestino"]);
                                    detalleSec.concesionDestino.barrio.barrio = row["BarrioDestino"].ToString();
                                }


                                if (!row.IsNull("idTipoUnidEspacialDestino"))
                                {
                                    detalleSec.concesionDestino.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacialDestino"]), row["nombreTipoUnidEspDestino"].ToString());
                                }
                                

                            }

                            detalleSec.origenes = new List<OrigenSector>();
                            origenes = new HashSet<int>();
                            clavesComuna = new HashSet<int>();
                            clavesPersona = new HashSet<int>();
                        }

                        //---
                       
                        if (!row.IsNull("idComunaDestino"))
                        {
                            if (clavesComuna.Count == 0 || !clavesComuna.Contains(Convert.ToInt32(row["idComunaDestino"])))
                            {
                                clavesComuna.Add(Convert.ToInt32(row["idComunaDestino"]));
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["idComunaDestino"]), row["nomComunaDestino"].ToString());
                                detalleSec.concesionDestino.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersonaDestino"))
                        {
                            if (clavesPersona.Count == 0 || !clavesPersona.Contains(Convert.ToInt32(row["rutPersonaDestino"])))
                            {
                                clavesPersona.Add(Convert.ToInt32(row["rutPersonaDestino"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersonaDestino"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificadorDestino"]);
                                persona.nombreSolicitante = row["nombrePersDestino"].ToString();
                                detalleSec.concesionDestino.titularesSolConcesion.Add(persona);
                            }
                        }

                        if (!row.IsNull("idOrigenSector"))
                        {
                            if (origenes.Count == 0 || !origenes.Contains(Convert.ToInt32(row["idOrigenSector"])))
                            {

                                origenSec = new OrigenSector();
                                origenSec.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);

                                origenes.Add(origenSec.idOrigenSector);


                                coordAux = new CoordenadaGeografica();
                                coordAux.listaPoligono = new List<Poligono>();
                                poligonoAux = new Poligono();
                                origenSec.concesionOrigen = new SolicitudConcesion();
                                origenSec.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                origenSec.concesionOrigen.coordenadaGeografica = new List<CoordenadaGeografica>();
                                origenSec.concesionOrigen.comuna = new List<ParametroGenerico>();
                                origenSec.concesionOrigen.barrio = new Barrio();

                                coordAux.listaPoligono.Add(poligonoAux);
                                origenSec.concesionOrigen.coordenadaGeografica.Add(coordAux);
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoSiepOrigen"].ToString();
                                origenSec.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroOrigen"].ToString();
                                if (!row.IsNull("superficieRelocalizacion"))
                                {
                                    origenSec.superficieRelocalizada = Convert.ToSingle(row["superficieRelocalizacion"]);
                                }
                                origenSec.concesionOrigen.numPert = row["numPertOrigen"].ToString();
                                poligonoAux.toponimio = row["toponimioOrigen"].ToString();
                                if (!row.IsNull("superficieTotalCalculadaOrigen"))
                                {
                                    poligonoAux.areaCalculada = Convert.ToSingle(row["superficieTotalCalculadaOrigen"]);
                                }
                                if (!row.IsNull("idRegDestinoOrigen"))
                                {
                                    origenSec.concesionOrigen.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestinoOrigen"]), row["nombreRegOrigen"].ToString());
                                }
                                if (!row.IsNull("IdBarrioOrigen"))
                                {
                                    origenSec.concesionOrigen.barrio.id_barrio = Convert.ToInt32(row["IdBarrioOrigen"]);
                                    origenSec.concesionOrigen.barrio.barrio = row["BarrioOrigen"].ToString();
                                }
                                origenSec.concesionOrigen.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idTipoUnidEspacialOrigen"]), row["nombreTipoUnidEspOrigen"].ToString());
                                origenSec.concesionOrigen.titularesSolConcesion = new List<Persona>();
                              
                                detalleSec.origenes.Add(origenSec);
                                clavesComunaO = new HashSet<int>();
                                clavesPersonaO = new HashSet<int>();
                            }
                        }
                        //---------------------------------
                        if (!row.IsNull("idComunaOrigen"))
                        {
                            if (clavesComunaO.Count == 0 || !clavesComunaO.Contains(Convert.ToInt32(row["idComunaOrigen"])))
                            {
                                clavesComunaO.Add(Convert.ToInt32(row["idComunaOrigen"]));
                                comunaResp = new ParametroGenerico(Convert.ToInt32(row["idComunaOrigen"]), row["nomComunaOrigen"].ToString());
                                origenSec.concesionOrigen.comuna.Add(comunaResp);
                            }
                        }
                        if (!row.IsNull("rutPersonaOrigen"))
                        {
                            if (clavesPersonaO.Count == 0 || !clavesPersonaO.Contains(Convert.ToInt32(row["rutPersonaOrigen"])))
                            {
                                clavesPersonaO.Add(Convert.ToInt32(row["rutPersonaOrigen"]));
                                persona = new Persona();
                                persona.rutPersona = Convert.ToInt32(row["rutPersonaOrigen"]);
                                persona.dvPersona = Convert.ToChar(row["digitoVerificadorOrigen"]);
                                persona.nombreSolicitante = row["nombrePersOrigen"].ToString();
                                origenSec.concesionOrigen.titularesSolConcesion.Add(persona);
                            }
                        }


                        idDetSectorAux = idDetSector;
                    }



                }

                return detalleSec;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }


    }
}
