using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Collections;
using Datos.Entidades.Resolucion;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;
using Datos.Contantes;
using Datos.Entidades.Resolucion.ResolucionSolicitud;

namespace LogicaNegocio.cl.subpesca.rb.resolucion
{
    public class ResolucionDA
    {

        Logger logger = new Logger();



        public Hashtable ListarResolucionValidacion(ResolucionValidacion filtro)
        {

            try
            {
                ResolucionValidacion valDocResp = null;
                Hashtable ht = new Hashtable();
                Hashtable htAux = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateriaSubRequerimiento";

                if (filtro.idEquivalencia > 0)
                {
                    cnn.parametros.Add("@idEquivalencia", filtro.idEquivalencia);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.origen != null && filtro.origen.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.origen.id);
                }
                if (filtro.materia != null && filtro.materia.id > 0)
                {
                    cnn.parametros.Add("@idMateria", filtro.materia.id);
                }
                if (filtro.estadoMateria != null && filtro.estadoMateria.id > 0)
                {
                    cnn.parametros.Add("@idEstadoMateria", filtro.materia.id);
                }

                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ResolucionValidacion();
                        valDocResp.idEquivalencia = Convert.ToInt32(row["idEquivalencia"]);
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipo"].ToString());
                        valDocResp.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());

                        if (!row.IsNull("idEstadoMateria"))
                        {
                            valDocResp.estadoMateria = new ParametroGenerico(Convert.ToInt32(row["idEstadoMateria"]), row["nombreEstado"].ToString());
                        }

                        if (!row.IsNull("idEstadoSubReq"))
                        {
                            valDocResp.estadoSubRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idEstadoSubReq"]), row["nombreEstadoSuReq"].ToString());
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.vincular = Convert.ToInt32(row["vincular"]);


                        if (!ht.Contains(valDocResp.tipoDocumento.id))
                        {
                            ht.Add(valDocResp.tipoDocumento.id, new Combobox(valDocResp.tipoDocumento.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(ht[valDocResp.tipoDocumento.id])).hash;

                        if (!htAux.Contains(valDocResp.origen.id))
                        {
                            htAux.Add(valDocResp.origen.id, new Combobox(valDocResp.origen.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(htAux[valDocResp.origen.id])).hash;

                        if (!htAux.Contains(valDocResp.materia.id))
                        {
                            htAux.Add(valDocResp.materia.id, new Combobox(valDocResp.materia.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(htAux[valDocResp.materia.id])).hash;

                        //EXISTEN REGISTROS QUE NO TIENEN RESULTADO (ESTADO)
                        if (valDocResp.estadoMateria != null && valDocResp.estadoMateria.id > 0)
                        {
                            if (!htAux.Contains(valDocResp.estadoMateria.id))
                            {
                                htAux.Add(valDocResp.estadoMateria.id, valDocResp);
                            }
                        }


                    }
                }

                return ht;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public List<ParametroGenerico> ListarPosiblesOrigenes()
        {

            try
            {
                List<ParametroGenerico> result = new List<ParametroGenerico>();
                ParametroGenerico origenAux = null;
                Hashtable ht = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateriaSubRequerimiento";

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        origenAux = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());

                        if (!ht.Contains(origenAux.id))
                        {
                            ht.Add(origenAux.id, origenAux.descripcion);
                        }

                    }
                }


                if (ht != null) {

                    foreach (DictionaryEntry item in ht)
                    {

                      result.Add(new ParametroGenerico((int)item.Key, item.Value.ToString()));
                        
                    }
                
                
                }


                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }



        public List<ParametroGenerico> ListarPosiblesMaterias()
        {

            try
            {
                List<ParametroGenerico> result = new List<ParametroGenerico>();
                ParametroGenerico materiaAux = null;
                Hashtable ht = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateriaSubRequerimiento";

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        materiaAux = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());

                        if (!ht.Contains(materiaAux.id))
                        {
                            ht.Add(materiaAux.id, materiaAux.descripcion);
                        }

                    }
                }


                if (ht != null)
                {

                    foreach (DictionaryEntry item in ht)
                    {

                        result.Add(new ParametroGenerico((int)item.Key, item.Value.ToString()));

                    }


                }


                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public ResolucionValidacion obtenerCombinatoriaResolucion(int idTipoDocumento, int idOrigen, int idMateria, int idResultado)
        {
            try{


                ResolucionValidacion valDocResp = null;
                Hashtable ht = new Hashtable();
                Hashtable htAux = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateriaSubRequerimiento";
                
                cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                cnn.parametros.Add("@idTipoDestinatario", idOrigen);
                cnn.parametros.Add("@idMateria", idMateria);

                if (idResultado > 0)
                {
                    cnn.parametros.Add("@idEstadoMateria", idResultado);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ResolucionValidacion();
                        valDocResp.idEquivalencia = Convert.ToInt32(row["idEquivalencia"]);
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipo"].ToString());
                        valDocResp.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());

                        if (!row.IsNull("idEstadoMateria"))
                        {
                            valDocResp.estadoMateria = new ParametroGenerico(Convert.ToInt32(row["idEstadoMateria"]), row["nombreEstado"].ToString());
                        }

                        if (!row.IsNull("idEstadoSubReq"))
                        {
                            valDocResp.estadoSubRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idEstadoSubReq"]), row["nombreEstadoSuReq"].ToString());
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.vincular = Convert.ToInt32(row["vincular"]);


                    }
                }

                return valDocResp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public bool MateriaTieneResultados(int idTipoDocumento, int idOrigen, int idMateria)
        {
            try
            {

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateriaSubRequerimiento";

                cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                cnn.parametros.Add("@idTipoDestinatario", idOrigen);
                cnn.parametros.Add("@idMateria", idMateria);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idEstadoMateria"))
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
            }
        }


        public SolicitudConcesion VerificaExistenciaReferencia(int idTipo, int idTipoUnidadEspacial, int idTipoSolicitud, string clave, int numSector)
        {
            try
            {

                SolicitudConcesion solicitud = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidarReferencia";


                cnn.parametros.Add("@idTipo", idTipo);

                if (idTipoUnidadEspacial > 0)
                {
                    cnn.parametros.Add("@idTipoUnidadEspacial", idTipoUnidadEspacial);
                }

                if (idTipoSolicitud > 0)
                {
                    cnn.parametros.Add("@idTipoSolicitud", idTipoSolicitud);
                }

                if (clave != null && !clave.Trim().Equals(""))
                {
                    cnn.parametros.Add("@clave", clave);
                }

                if (numSector >= 0)  //Puede ser un sector 0
                {
                    cnn.parametros.Add("@numSector", numSector);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        solicitud = new SolicitudConcesion();

                        solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);

                        if (!row.IsNull("titularesCad")) {
                            solicitud.titularesCad = row["titularesCad"].ToString();
                        }

                        if (!row.IsNull("comunasCad"))
                        {
                            solicitud.comunasCad = row["comunasCad"].ToString();
                        }

                        if (!row.IsNull("toponimioCad"))
                        {
                            solicitud.toponimiosCad = row["toponimioCad"].ToString();
                        }

                        if (!row.IsNull("IdRegion"))
                        {
                            solicitud.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());
                        }
                        

                    }
                }

                return solicitud;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public bool GuardarResolucion(Resolucion resolucion, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbResolucion";
                if(resolucion.idResolucion>0){
                    cnn.parametros.Add("@idResolucion",resolucion.idResolucion);
                }
                if(resolucion.resultado!=null && resolucion.resultado.id>0){
                    cnn.parametros.Add("@idResultado",resolucion.resultado.id);
                }
                
                if(resolucion.resolucionPrincipal!=null && resolucion.resolucionPrincipal.idResolucion>0){
                    cnn.parametros.Add("@idResolucionPrinc",resolucion.resolucionPrincipal.idResolucion);
                }
                if(resolucion.tipoDocumento!=null && resolucion.tipoDocumento.id>0){
                    cnn.parametros.Add("@idTipoDocumento",resolucion.tipoDocumento.id);
                }
                if(resolucion.tipoRelacionDocumento!=null && resolucion.tipoRelacionDocumento.id>0){
                    cnn.parametros.Add("@idTipoRelDoc",resolucion.tipoRelacionDocumento.id);
                }
                if(resolucion.origen!=null && resolucion.origen.id>0){
                    cnn.parametros.Add("@idTipoDestinatario",resolucion.origen.id);
                }
                if(resolucion.materia!=null && resolucion.materia.id>0){
                    cnn.parametros.Add("@idMateria",resolucion.materia.id);
                }
                if(resolucion.archivoAdjunto!=null && resolucion.archivoAdjunto.idArchivo>0){
                    cnn.parametros.Add("@idArchivoBinario",resolucion.archivoAdjunto.idArchivo);
                }
                if(resolucion.vigencia!=null && resolucion.vigencia.id>0){
                    cnn.parametros.Add("@idEstadoVigencia",resolucion.vigencia.id);
                }
                if(resolucion.numero!=null){
                    cnn.parametros.Add("@numero",resolucion.numero);
                }
                if(resolucion.fecha!=null && resolucion.fecha != default(DateTime)){
                    cnn.parametros.Add("@fecha",resolucion.fecha);
                }
                if(resolucion.numeroCI!=null){
                    cnn.parametros.Add("@numeroCI",resolucion.numeroCI);
                }
                if(resolucion.fechaCI!=null && resolucion.fechaCI != default(DateTime)){
                    cnn.parametros.Add("@fechaCI",resolucion.fechaCI);
                }
                if(resolucion.numeroDiarioOficial!=null){
                    cnn.parametros.Add("@numDiarioOficial",resolucion.numeroDiarioOficial);
                }
                if(resolucion.fechaDiarioOficial!=null && resolucion.fechaDiarioOficial != default(DateTime)){
                    cnn.parametros.Add("@fechaDiarioOficial",resolucion.fechaDiarioOficial);
                }
                if (resolucion.nuevaFecha != null && resolucion.nuevaFecha != default(DateTime))
                {
                    cnn.parametros.Add("@fechaProrroga", resolucion.nuevaFecha);
                }
                if(resolucion.tieneReferencia!=null && resolucion.tieneReferencia>0){
                    cnn.parametros.Add("@tieneReferencia",resolucion.tieneReferencia);
                }
                if(resolucion.fechaInicioPlazo!=null && resolucion.fechaInicioPlazo != default(DateTime)){
                    cnn.parametros.Add("@fechaInicioPlazo",resolucion.fechaInicioPlazo);
                }
                if(resolucion.fechaVencimiento!=null && resolucion.fechaVencimiento != default(DateTime)){
                    cnn.parametros.Add("@fechaFinPlazo",resolucion.fechaVencimiento);
                }
                if(resolucion.observaciones!=null){
                    cnn.parametros.Add("@observaciones", resolucion.observaciones);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                if (resolucion.tipoIngreso != null && resolucion.tipoIngreso.id > 0)
                {
                    cnn.parametros.Add("@idTipoIngreso", resolucion.tipoIngreso.id);
                }
                
                DataTable dt = cnn.Execute();
                resolucion.idResolucion = Convert.ToInt32(dt.Rows[0]["idResolucion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        public bool GuardarReferenciaResolucion(Referencia referencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbReferenciaResolucion";
                if (referencia.idResolucion > 0)
                {
                    cnn.parametros.Add("@idResolucion", referencia.idResolucion);
                }
                if (referencia.idTipoReferencia > 0)
                {
                    cnn.parametros.Add("@idTipoReferencia", referencia.idTipoReferencia);
                }
                if (referencia.tipo != null && referencia.tipo.id > 0)
                {
                    cnn.parametros.Add("@idRefUETipoSolUE", referencia.tipo.id);
                }
                if (referencia.tipoSolicitud != null && referencia.tipoSolicitud.id > 0)
                {
                    cnn.parametros.Add("@idRefUETipoTramite", referencia.tipoSolicitud.id);
                }
                if (referencia.tipoUnidadEspacial != null && referencia.tipoUnidadEspacial.id > 0)
                {
                    cnn.parametros.Add("@idRefUETipoUE", referencia.tipoUnidadEspacial.id);
                }
                if (referencia.tipoModificacionUE != null && referencia.tipoModificacionUE.id > 0)
                {
                    cnn.parametros.Add("@idRefUETipoMod", referencia.tipoModificacionUE.id);
                }
                if (referencia.idRefUEDocGeneral > 0)
                {
                    cnn.parametros.Add("@idRefUEDocGeneral", referencia.idRefUEDocGeneral);
                }
                if (referencia.idRefUESolConcesion > 0)
                {
                    cnn.parametros.Add("@idRefUESolConcesion", referencia.idRefUESolConcesion);
                }
                if (referencia.region != null && referencia.region.id > 0)
                {
                    cnn.parametros.Add("@idRefUbicRegion", referencia.region.id);
                }
                if (referencia.comuna != null && referencia.comuna.id > 0)
                {
                    cnn.parametros.Add("@idRefUbicComuna", referencia.comuna.id);
                }
                if (referencia.especie != null && referencia.especie.id > 0)
                {
                    cnn.parametros.Add("@idRefEspecieId", referencia.especie.id);
                }
                if (referencia.titular != null && referencia.titular.rut>0)
                {
                    cnn.parametros.Add("@idRefTitularRut", referencia.titular.rut);
                }
                if (referencia.numeroReferencia != null && !referencia.numeroReferencia.Equals(""))
                {
                    cnn.parametros.Add("@refDocNumero", referencia.numeroReferencia);
                }
                if (referencia.fechaReferencia != null && referencia.fechaReferencia != default(DateTime))
                {
                    cnn.parametros.Add("@refDocFecha", referencia.fechaReferencia);
                }
                if (referencia.origenReferencia != null && referencia.origenReferencia.id>0)
                {
                    cnn.parametros.Add("@idRefDocOrigen", referencia.origenReferencia.id);
                }
                if (referencia.codigoCentro != null && !referencia.codigoCentro.Equals(""))
                {
                    cnn.parametros.Add("@refUECodCentro", referencia.codigoCentro);
                }
                if (referencia.numeroPert != null && !referencia.numeroPert.Equals(""))
                {
                    cnn.parametros.Add("@refUENumPert", referencia.numeroPert);
                }
                if (referencia.numeroIdentificador != null && !referencia.numeroIdentificador.Equals(""))
                {
                    cnn.parametros.Add("@refUENumIdentificador", referencia.numeroIdentificador);
                }
                if (referencia.numSector>0)
                {
                    cnn.parametros.Add("@refUENumSector", referencia.numSector);
                }
                if (referencia.sector != null && !referencia.sector.Equals(""))
                {
                    cnn.parametros.Add("@refUbicSector", referencia.sector);
                }
                
                //SIEMPRE UNA REFERENCIA INGRESA COMO VIGENTE
                if (referencia.estadoVigencia != null && referencia.estadoVigencia.id>0)
                {
                    cnn.parametros.Add("@idEstadoVigencia", referencia.estadoVigencia.id);

                }
                if (referencia.fechaNuevoVencimiento != null && referencia.fechaNuevoVencimiento != default(DateTime))
                {
                    cnn.parametros.Add("@fechaAmpliaPlazo", referencia.fechaNuevoVencimiento);
                }
                if (referencia.tipoIngresoResol != null && referencia.tipoIngresoResol.id > 0)
                {
                    cnn.parametros.Add("@idRefDocTipoIngreso", referencia.tipoIngresoResol.id);
                }
                if (referencia.tipoDocResol != null && referencia.tipoDocResol.id > 0)
                {
                    cnn.parametros.Add("@idRefDocTipoDoc", referencia.tipoDocResol.id);
                }
                
                DataTable dt = cnn.Execute();
                referencia.idReferencia = Convert.ToInt32(dt.Rows[0]["idRefResolucion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarReferenciaResolucion(int idRefResolucion, int idRefUEDocGeneral, int idRefUESolConcesion, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbReferenciaResolucion";


                cnn.parametros.Add("@idRefResolucion", idRefResolucion);
                if (idRefUEDocGeneral > 0)
                {
                    cnn.parametros.Add("@idRefUEDocGeneral", idRefUEDocGeneral);
                }
                if (idRefUESolConcesion > 0)
                {
                    cnn.parametros.Add("@idRefUESolConcesion", idRefUESolConcesion);
                }
                if (idEstadoVigencia > 0)
                {
                    cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
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


        public Resolucion ObtenerResolucion(Resolucion filtro)
        {
            try
            {
                Resolucion resolucionAux = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucion";

                if (filtro.idResolucion > 0)
                {
                    cnn.parametros.Add("@idResolucion", filtro.idResolucion);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.origen != null && filtro.origen.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.origen.id);
                }
                if (filtro.materia != null && filtro.materia.id > 0)
                {
                    cnn.parametros.Add("@idMateria", filtro.materia.id);
                }
                if (filtro.numero != null && !filtro.numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", filtro.numero);
                }
                if (filtro.fechaDesde != null && filtro.fechaDesde != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIni", filtro.fechaDesde);
                }
                if (filtro.fechaHasta != null && filtro.fechaHasta != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFin", filtro.fechaHasta);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        resolucionAux = new Resolucion();
                        resolucionAux.idResolucion = Convert.ToInt32(row["idResolucion"]);

                        if (!row.IsNull("idResultado"))
                        {
                            resolucionAux.resultado = new ParametroGenerico(Convert.ToInt32(row["idResultado"]), row["nombreEstado"].ToString());
                        }
                        if (!row.IsNull("idTipoIngreso"))
                        {
                            resolucionAux.tipoIngreso = new ParametroGenerico(Convert.ToInt32(row["idTipoIngreso"]), row["nombreTipoIngreso"].ToString());
                        }
                        if (!row.IsNull("idResolucionPrinc"))
                        {
                            resolucionAux.resolucionPrincipal = new Resolucion();
                            resolucionAux.resolucionPrincipal.idResolucion = (Convert.ToInt32(row["idResolucionPrinc"]));
                        }
                        if (!row.IsNull("idTipoDocumento"))
                        {
                            resolucionAux.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        }

                        if (!row.IsNull("idTipoRelDoc"))
                        {
                            resolucionAux.tipoRelacionDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoRelDoc"]), row["nombreTipoRel"].ToString());
                        }

                        if (!row.IsNull("idTipoDestinatario"))
                        {
                            resolucionAux.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        }

                        if (!row.IsNull("idMateria"))
                        {
                            resolucionAux.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        }
                        if (!row.IsNull("idArchivoBinario"))
                        {
                            resolucionAux.archivoAdjunto = new ArchivoBinario();
                            resolucionAux.archivoAdjunto.idArchivo = Convert.ToInt32(row["idArchivoBinario"]);
                        }
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            resolucionAux.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                        }
                        if (!row.IsNull("numero"))
                        {
                            resolucionAux.numero = row["numero"].ToString();
                        }
                        if (!row.IsNull("fecha"))
                        {
                            resolucionAux.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("numeroCI"))
                        {
                            resolucionAux.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if (!row.IsNull("fechaCI"))
                        {
                            resolucionAux.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        if (!row.IsNull("numDiarioOficial"))
                        {
                            resolucionAux.numeroDiarioOficial = row["numDiarioOficial"].ToString();
                        }
                        if (!row.IsNull("fechaDiarioOficial"))
                        {
                            resolucionAux.fechaDiarioOficial = Convert.ToDateTime(row["fechaDiarioOficial"]);
                        }
                        if (!row.IsNull("fechaProrroga"))
                        {
                            resolucionAux.nuevaFecha = Convert.ToDateTime(row["fechaProrroga"]);
                        }
                        if (!row.IsNull("tieneReferencia"))
                        {
                            resolucionAux.tieneReferencia = Convert.ToInt32(row["tieneReferencia"]);
                        }
                        if (!row.IsNull("fechaInicioPlazo"))
                        {
                            resolucionAux.fechaInicioPlazo = Convert.ToDateTime(row["fechaInicioPlazo"]);
                        }
                        if (!row.IsNull("fechaFinPlazo"))
                        {
                            resolucionAux.fechaVencimiento = Convert.ToDateTime(row["fechaFinPlazo"]);
                        }
                        if (!row.IsNull("fechaIngresoSistema"))
                        {
                            resolucionAux.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            resolucionAux.observaciones = row["observaciones"].ToString();
                        }

                        
                    }
                }
                return resolucionAux;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<Resolucion> ListarResolucion(Resolucion filtro)
        {
            try
            {
                Resolucion resolucionAux = null;
                List<Resolucion> resp = new List<Resolucion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucion";

                if (filtro.idResolucion>0)
                {
                    cnn.parametros.Add("@idResolucion", filtro.idResolucion);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.origen != null && filtro.origen.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.origen.id);
                }
                if (filtro.materia != null && filtro.materia.id > 0)
                {
                    cnn.parametros.Add("@idMateria", filtro.materia.id);
                }
                if (filtro.numero!=null && !filtro.numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", filtro.numero);
                }
                
                if (filtro.fechaDesde != null && filtro.fechaDesde != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIni", filtro.fechaDesde);
                }
                if (filtro.fechaHasta != null && filtro.fechaHasta != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFin", filtro.fechaHasta);
                }

                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        
                            resolucionAux = new Resolucion();
                            resolucionAux.idResolucion = Convert.ToInt32(row["idResolucion"]);
                            if (!row.IsNull("idResultado"))
                            {
                                resolucionAux.resultado = new ParametroGenerico(Convert.ToInt32(row["idResultado"]), row["nombreEstado"].ToString());
                            }
                            if (!row.IsNull("idTipoIngreso"))
                            {
                                resolucionAux.tipoIngreso = new ParametroGenerico(Convert.ToInt32(row["idTipoIngreso"]), row["nombreTipoIngreso"].ToString());
                            }
                            if (!row.IsNull("idResolucionPrinc"))
                            {
                                resolucionAux.resolucionPrincipal = new Resolucion();
                                resolucionAux.resolucionPrincipal.idResolucion =(Convert.ToInt32(row["idResolucionPrinc"]));
                            }
                            if (!row.IsNull("idTipoDocumento"))
                            {
                                resolucionAux.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                            }

                            if (!row.IsNull("idTipoRelDoc"))
                            {
                                resolucionAux.tipoRelacionDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoRelDoc"]), row["nombreTipoRel"].ToString());
                            }

                            if (!row.IsNull("idTipoDestinatario"))
                            {
                                resolucionAux.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                            }

                            if (!row.IsNull("idMateria"))
                            {
                                resolucionAux.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                            }
                            if (!row.IsNull("idArchivoBinario"))
                            {
                                resolucionAux.archivoAdjunto = new ArchivoBinario();
                                resolucionAux.archivoAdjunto.idArchivo=Convert.ToInt32(row["idArchivoBinario"]);
                            }
                            if(!row.IsNull("idEstadoVigencia")){
                                resolucionAux.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                            }
                            if(!row.IsNull("numero")){
                                resolucionAux.numero = row["numero"].ToString();
                            }
                            if(!row.IsNull("fecha")){
                                resolucionAux.fecha = Convert.ToDateTime(row["fecha"]);
                            }
                            if(!row.IsNull("numeroCI")){
                                resolucionAux.numeroCI = Convert.ToInt32(row["numeroCI"]);
                            }
                            if(!row.IsNull("fechaCI")){
                                resolucionAux.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                            }
                            if(!row.IsNull("numDiarioOficial")){
                                resolucionAux.numeroDiarioOficial = row["numDiarioOficial"].ToString();
                            }
                            if(!row.IsNull("fechaDiarioOficial")){
                                resolucionAux.fechaDiarioOficial = Convert.ToDateTime(row["fechaDiarioOficial"]);
                            }
                            if (!row.IsNull("fechaProrroga"))
                            {
                                resolucionAux.nuevaFecha = Convert.ToDateTime(row["fechaProrroga"]);
                            }
                            if(!row.IsNull("tieneReferencia")){
                                resolucionAux.tieneReferencia = Convert.ToInt32(row["tieneReferencia"]);
                            }
                            if(!row.IsNull("fechaInicioPlazo")){
                                resolucionAux.fechaInicioPlazo = Convert.ToDateTime(row["fechaInicioPlazo"]);
                            }
                            if(!row.IsNull("fechaFinPlazo")){
                                resolucionAux.fechaVencimiento = Convert.ToDateTime(row["fechaFinPlazo"]);
                            }
                            if(!row.IsNull("fechaIngresoSistema")){
                                resolucionAux.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                            }
                             if(!row.IsNull("observaciones")){
                                resolucionAux.observaciones = row["observaciones"].ToString();
                            }

                            resp.Add(resolucionAux);
                            
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

        public List<Referencia> ListarReferencia(Referencia filtro)
        {
            try
            {
                Referencia referenciaAux = null;
                List<Referencia> resp = new List<Referencia>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbReferenciaResolucion";

                if (filtro.idReferencia > 0)
                {
                    cnn.parametros.Add("@idRefResolucion", filtro.idReferencia);
                }
                if (filtro.idResolucion > 0)
                {
                    cnn.parametros.Add("@idResolucion", filtro.idResolucion);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        referenciaAux = new Referencia();
                        referenciaAux.accion = accion.LISTADO;
                        referenciaAux.idReferencia = Convert.ToInt32(row["idRefResolucion"]);
                        referenciaAux.idResolucion = Convert.ToInt32(row["idResolucion"]);
                        if (!row.IsNull("idTipoReferencia"))
                        {
                            referenciaAux.idTipoReferencia = Convert.ToInt32(row["idTipoReferencia"]);
                        }
                        if (!row.IsNull("idRefUETipoSolUE"))
                        {
                            referenciaAux.tipo = new ParametroGenerico(Convert.ToInt32(row["idRefUETipoSolUE"]), row["nomRefUETipoSolUE"].ToString());
                        }
                        if (!row.IsNull("idRefUETipoTramite"))
                        {
                            referenciaAux.tipoSolicitud = new ParametroGenerico(Convert.ToInt32(row["idRefUETipoTramite"]), row["nomRefUETipoTramite"].ToString());
                        }

                        if (!row.IsNull("idRefUETipoUE"))
                        {
                            referenciaAux.tipoUnidadEspacial = new ParametroGenerico(Convert.ToInt32(row["idRefUETipoUE"]), row["nomRefUETipoUE"].ToString());
                        }

                        if (!row.IsNull("idRefUETipoMod"))
                        {
                            referenciaAux.tipoModificacionUE = new ParametroGenerico(Convert.ToInt32(row["idRefUETipoMod"]), row["nomRefUETipoMod"].ToString());
                        }

                        if (!row.IsNull("idRefUEDocGeneral"))
                        {
                            referenciaAux.idRefUEDocGeneral = Convert.ToInt32(row["idRefUEDocGeneral"]);
                        }
                        if (!row.IsNull("idRefUESolConcesion"))
                        {
                            referenciaAux.idRefUESolConcesion = Convert.ToInt32(row["idRefUESolConcesion"]);
                            referenciaAux.solicitudUnidadEspacial = new SolicitudConcesion();
                            referenciaAux.solicitudUnidadEspacial.idSolConcesion = referenciaAux.idRefUESolConcesion;
                        }
                        if (!row.IsNull("idRefUbicRegion"))
                        {
                            referenciaAux.region = new ParametroGenerico(Convert.ToInt32(row["idRefUbicRegion"]), row["Region"].ToString());
                        }
                        if (!row.IsNull("idRefUbicComuna"))
                        {
                            referenciaAux.comuna = new ParametroGenerico(Convert.ToInt32(row["idRefUbicComuna"]), row["Comuna"].ToString());
                        }
                        if (!row.IsNull("idRefEspecieId"))
                        {
                            referenciaAux.especie = new ParametroGenerico(Convert.ToInt32(row["idRefEspecieId"]), row["EspecieCultivo"].ToString());
                        }
                        if (!row.IsNull("idRefTitularRut"))
                        {
                            referenciaAux.titular = new Solicitante();
                            referenciaAux.titular.rut = Convert.ToInt32(row["idRefTitularRut"]);
                            referenciaAux.titular.dv = Convert.ToChar(row["digitoVerificador"]);
                            referenciaAux.titular.nombreSolicitante = row["nombrePersona"].ToString();
                        }

                        if (!row.IsNull("idRefDocTipoIngreso"))
                        {
                            referenciaAux.tipoIngresoResol = new ParametroGenerico(Convert.ToInt32(row["idRefDocTipoIngreso"]), row["nomRefDocTipoIngreso"].ToString());
                        }
                        if (!row.IsNull("idRefDocTipoDoc"))
                        {
                            referenciaAux.tipoDocResol = new ParametroGenerico(Convert.ToInt32(row["idRefDocTipoDoc"]), row["nomRefDocTipoDoc"].ToString());
                        }

                        if (!row.IsNull("refDocNumero"))
                        {
                            referenciaAux.numeroReferencia = row["refDocNumero"].ToString();
                        }
                        if (!row.IsNull("refDocFecha"))
                        {
                            referenciaAux.fechaReferencia = Convert.ToDateTime(row["refDocFecha"]);
                        }
                        if (!row.IsNull("refUECodCentro"))
                        {
                            referenciaAux.codigoCentro = row["refUECodCentro"].ToString();
                        }
                        if (!row.IsNull("refUENumPert"))
                        {
                            referenciaAux.numeroPert = row["refUENumPert"].ToString();
                        }
                        if (!row.IsNull("refUENumIdentificador"))
                        {
                            referenciaAux.numeroIdentificador = row["refUENumIdentificador"].ToString();
                        }
                        if (!row.IsNull("refUENumSector"))
                        {
                            referenciaAux.numSector = Convert.ToInt32(row["refUENumSector"]);
                        }
                        if (!row.IsNull("refUbicSector"))
                        {
                            referenciaAux.sector = row["refUbicSector"].ToString();
                        }
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            referenciaAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }
                        if (!row.IsNull("idRefDocOrigen"))
                        {
                            referenciaAux.origenReferencia = new ParametroGenerico(Convert.ToInt32(row["idRefDocOrigen"]), row["nombreTipoDestinatario"].ToString());
                        }
                        if (!row.IsNull("codigoSiep"))
                        {
                            referenciaAux.codigoCentroRegularizado = row["codigoSiep"].ToString();
                        }
                        if (!row.IsNull("fechaAmpliaPlazo"))
                        {
                            referenciaAux.fechaNuevoVencimiento = Convert.ToDateTime(row["fechaAmpliaPlazo"]);
                        }

                        resp.Add(referenciaAux);

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

        public bool EliminarReferenciaResolucion(int idResolucion, int idRefResolucion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbReferenciaResolucion";

                cnn.parametros.Add("@idResolucion", idResolucion);
                if (idRefResolucion > 0)
                {
                    cnn.parametros.Add("@idRefResolucion", idRefResolucion);
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

        public bool EliminarResolucion(int idResolucion, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbResolucion";
                cnn.parametros.Add("@idResolucion", idResolucion);
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

        public bool GuardarResolucionSolicitud(ResolucionSolicitud resolucionSolicitud)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbResolucionSolicitud";

                if (resolucionSolicitud.resolucion != null && resolucionSolicitud.resolucion.idResolucion > 0)
                {
                    cnn.parametros.Add("@idResolucion", resolucionSolicitud.resolucion.idResolucion);
                }
                if (resolucionSolicitud.solicitud != null && resolucionSolicitud.solicitud.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", resolucionSolicitud.solicitud.idSolConcesion);
                }
                if (resolucionSolicitud.estadoVigencia != null && resolucionSolicitud.estadoVigencia.id > 0)
                {
                    cnn.parametros.Add("@idEstadoVigencia", resolucionSolicitud.estadoVigencia.id);
                }
                if (resolucionSolicitud.tipoIngreso != null && resolucionSolicitud.tipoIngreso.id > 0)
                {
                    cnn.parametros.Add("@idTipoIngreso", resolucionSolicitud.tipoIngreso.id);
                }
                if (resolucionSolicitud.observaciones != null && !resolucionSolicitud.observaciones.Equals(""))
                {
                    cnn.parametros.Add("@observaciones", resolucionSolicitud.observaciones);
                }
                if (resolucionSolicitud.docConcesion != null && resolucionSolicitud.docConcesion.idDocConcesion > 0)
                {
                    cnn.parametros.Add("@idDocConcesion", resolucionSolicitud.docConcesion.idDocConcesion);
                }

                DataTable dt = cnn.Execute();

                resolucionSolicitud.idHistUE = Convert.ToInt32(dt.Rows[0]["idHistUE"]);
                
                //resolucion.idResolucion = Convert.ToInt32(dt.Rows[0]["idResolucion"]);
                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarResolucionSolicitud(ResolucionSolicitud resolucionSolicitud)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbResolucionSolicitud";

                cnn.parametros.Add("@idResolucion", resolucionSolicitud.resolucion.idResolucion);

                if (resolucionSolicitud.solicitud != null && resolucionSolicitud.solicitud.idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", resolucionSolicitud.solicitud.idSolConcesion);
                }
                if (resolucionSolicitud.estadoVigencia != null && resolucionSolicitud.estadoVigencia.id > 0)
                {
                    cnn.parametros.Add("@idEstadoVigencia", resolucionSolicitud.estadoVigencia.id);
                }


                DataTable dt = cnn.Execute();
                resolucionSolicitud.resolucion.idResolucion = Convert.ToInt32(dt.Rows[0]["idResolucion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarResolucionSolicitud(int idResolucion,int idSolConcesion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbResolucionSolicitud";

                cnn.parametros.Add("@idResolucion", idResolucion);
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
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

        //public List<ResolucionSolicitud> ListarResolucionSolicitud(int idSolicitud)
        //{
        //    try
        //    {
        //        ResolucionSolicitud resolucionSolAux = null;
        //        List<ResolucionSolicitud> resp = new List<ResolucionSolicitud>();

        //        Conexion cnn = new Conexion();
        //        cnn.procedimiento = "paSelRbResolucionSolicitud";

        //        cnn.parametros.Add("@idSolConcesion", idSolicitud);
                
        //        DataTable dt = cnn.Execute();

        //        if (dt != null)
        //        {

        //            foreach (DataRow row in dt.Rows)
        //            {

        //                resolucionSolAux = new ResolucionSolicitud();
        //                resolucionSolAux.resolucion = new Resolucion();
        //                resolucionSolAux.resolucion.idResolucion = Convert.ToInt32(row["idResolucion"]);
        //                resolucionSolAux.cadena = row["cadInfo"].ToString();
        //                resolucionSolAux.solicitud = new SolicitudConcesion();
        //                resolucionSolAux.solicitud.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
        //                if (!row.IsNull("idEstadoVigencia"))
        //                {
        //                    resolucionSolAux.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
        //                }
        //                if (!row.IsNull("fechaInsercion"))
        //                {
        //                    resolucionSolAux.fechaIngresoSistema = Convert.ToDateTime(row["fechaInsercion"]);
        //                }

        //                resp.Add(resolucionSolAux);
        //            }
        //        }
        //        return resp;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.PrintError(ex);
        //        logger.SendMailError(ex);
        //        return null;
        //    }
        //}

        //public List<Resolucion> ListarResolucionesManuales(int idSolicitud)
        //{
        //    try
        //    {
        //        Resolucion resolucion = null;
        //        List<Resolucion> resp = new List<Resolucion>();

        //        Conexion cnn = new Conexion();
        //        cnn.procedimiento = "paSelRbResolucionesManual";

        //        cnn.parametros.Add("@idSolicitud", idSolicitud);

        //        DataTable dt = cnn.Execute();

        //        if (dt != null)
        //        {

        //            foreach (DataRow row in dt.Rows)
        //            {

        //                resolucion = new Resolucion();
        //                resolucion.idResolucion = Convert.ToInt32(row["idResolucion"]);
        //                if(!row.IsNull("idResultado")){
        //                    resolucion.resultado = new ParametroGenerico(Convert.ToInt32(row["idResultado"]), "");
        //                }
                        
        //                resolucion.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
        //                if (!row.IsNull("numero"))
        //                {
        //                resolucion.numero = row["numero"].ToString();
        //                }
        //                if (!row.IsNull("fecha"))
        //                {
        //                resolucion.fecha = Convert.ToDateTime(row["fecha"]);
        //                }
        //                if (!row.IsNull("numeroCI"))
        //                {
        //                resolucion.numeroCI = Convert.ToInt32(row["numeroCI"]);
        //                }
        //                if (!row.IsNull("fechaCI"))
        //                {
        //                resolucion.fechaCI = Convert.ToDateTime(row["fechaCI"]);
        //                }
        //                if (!row.IsNull("cadInfo"))
        //                {
        //                    resolucion.cadena = row["cadInfo"].ToString();
        //                }

        //                resp.Add(resolucion);
        //            }
        //        }
        //        return resp;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.PrintError(ex);
        //        logger.SendMailError(ex);
        //        return null;
        //    }
        //}

        public List<Resolucion> ListarResolucionesManuales_AdminUE(int idSolicitud)
        {
            try
            {
                Resolucion resolucion = null;
                List<Resolucion> resp = new List<Resolucion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionesManual_UE";

                cnn.parametros.Add("@idSolicitud", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        resolucion = new Resolucion();
                        resolucion.idResolucion = Convert.ToInt32(row["idResolucion"]);
                        if (!row.IsNull("idResultado"))
                        {
                            resolucion.resultado = new ParametroGenerico(Convert.ToInt32(row["idResultado"]), "");
                        }

                        resolucion.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        if (!row.IsNull("numero"))
                        {
                            resolucion.numero = row["numero"].ToString();
                        }
                        if (!row.IsNull("fecha"))
                        {
                            resolucion.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("numeroCI"))
                        {
                            resolucion.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if (!row.IsNull("fechaCI"))
                        {
                            resolucion.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        if (!row.IsNull("cadInfo"))
                        {
                            resolucion.cadena = row["cadInfo"].ToString();
                        }

                        resp.Add(resolucion);
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

        public List<HistUnidEspacialVigencia> ListarbHistUnidEspacialVigencia(int idSolicitud)
        {
            try
            {
                HistUnidEspacialVigencia histUE = null;
                List<HistUnidEspacialVigencia> resp = new List<HistUnidEspacialVigencia>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbHistUnidEspacialVigencia";

                cnn.parametros.Add("@idSolConcesion", idSolicitud);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        histUE = new HistUnidEspacialVigencia();
                        histUE.idHistUE = Convert.ToInt32(row["idHistUE"]);
                        histUE.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                        if (!row.IsNull("idResolucion"))
                        {
                            histUE.idResolucion = Convert.ToInt32(row["idResolucion"]);
                        }
                        if (!row.IsNull("cadInfo"))
                        {
                            histUE.cadena = row["cadInfo"].ToString();
                        }
                        histUE.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        if (!row.IsNull("observaciones"))
                        {
                            histUE.observaciones = row["observaciones"].ToString();
                        }
                        histUE.fechaIngresoSist = Convert.ToDateTime(row["fechaIngresoSist"]);
                        if (!row.IsNull("idDocConcesion"))
                        {
                            histUE.idDocConcesion = Convert.ToInt32(row["idDocConcesion"]);
                        }
                        if (!row.IsNull("cadDocConcesion"))
                        {
                            histUE.cadenaDocConcesion = row["cadDocConcesion"].ToString();
                        }


                        resp.Add(histUE);
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


        public bool validarExistenciaResolucion(int idResolucion, int idTipoDocumento, int idOrigen, int idTipoIngreso, string numero, int anio)
        {

            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSeRbResolucion_Validacion";

                cnn.parametros.Add("@idResolucion", idResolucion);
                cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                cnn.parametros.Add("@idOrigen", idOrigen);
                cnn.parametros.Add("@idTipoIngreso", idTipoIngreso);
                cnn.parametros.Add("@numero", numero);
                cnn.parametros.Add("@anio", anio);

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

        public Resolucion validarExistenciaResolucionPrincipal(int idTipoDocumento, int idOrigen, string numero, DateTime fecha)
        {
            try
            {

                Resolucion resolucion = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionValidacion";

                cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                cnn.parametros.Add("@idTipoDestinatario", idOrigen);
                cnn.parametros.Add("@numero", numero);
                cnn.parametros.Add("@fecha", fecha);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        resolucion = new Resolucion();
                        
                        resolucion.idResolucion = Convert.ToInt32(row["idResolucion"]);

                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            resolucion.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]));
                        }

                        if (!row.IsNull("idTipoDocumento"))
                        {
                            resolucion.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]));
                        }

                        if (!row.IsNull("idTipoDestinatario"))
                        {
                            resolucion.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]));
                        }

                        if (!row.IsNull("numero"))
                        {
                            resolucion.numero = row["numero"].ToString();
                        }

                        if (!row.IsNull("fecha"))
                        {
                            resolucion.fecha = Convert.ToDateTime(row["fecha"]);
                        }

                        break;

                    }
                }

                return resolucion;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarResolucionTramite(int idDocPestana, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDocResolucionAdmin";


                cnn.parametros.Add("@idDocPestana", idDocPestana);
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

        public DataTable ListarResolucionComplementarias(int idResolucion)
        {
            try
            {
                List<Resolucion> resp = new List<Resolucion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionComplementarias";

                cnn.parametros.Add("@idResolucion", idResolucion);
                
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

        public List<Resolucion> ListarResolucionReferenciada(int idResolucion)
        {
            try
            {
                Resolucion resolucionAux = null;
                List<Resolucion> resp = new List<Resolucion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionReferenciada";

                cnn.parametros.Add("@idResolucion", idResolucion);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        resolucionAux = new Resolucion();
                        resolucionAux.idResolucion = Convert.ToInt32(row["idResolucion"]);
                        
                        if (!row.IsNull("idTipoIngreso"))
                        {
                            resolucionAux.tipoIngreso = new ParametroGenerico(Convert.ToInt32(row["idTipoIngreso"]), row["nombreTipoIngreso"].ToString());
                        }
                        
                        if (!row.IsNull("idTipoDocumento"))
                        {
                            resolucionAux.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        }

                        if (!row.IsNull("numero"))
                        {
                            resolucionAux.numero = row["numero"].ToString();
                        }
                        if (!row.IsNull("fecha"))
                        {
                            resolucionAux.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                       
                        resp.Add(resolucionAux);

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

        public List<Resolucion> BusquedaResolucion(Resolucion filtro)
        {
            try
            {
                Resolucion resolucionAux = null;
                List<Resolucion> resp = new List<Resolucion>();
                ParametroGenerico dato = null;
                int idResol = 0;
                int idResolAux = 0;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbBusquedaResolucion";

                if (filtro.origen != null && filtro.origen.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.origen.id);
                }
                if (filtro.numero != null && !filtro.numero.Equals(""))
                {
                    cnn.parametros.Add("@numero", filtro.numero);
                }
                if (filtro.fechaDesde != null && filtro.fechaDesde != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIni", filtro.fechaDesde);
                }
                if (filtro.fechaHasta != null && filtro.fechaHasta != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFin", filtro.fechaHasta);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        
                        idResol = Convert.ToInt32(row["idClave"]);// puede ser idresolucion o idSolicitud ue o tramite

                        if (idResol!=idResolAux)
                        {
                            resolucionAux = new Resolucion();
                            resolucionAux.idResolucion = idResol;
                            resolucionAux.origenRegistro = new ParametroGenerico(Convert.ToInt32(row["claveOrigen"]), row["origen"].ToString());
                            if (!row.IsNull("idTipoDocumento"))
                            {
                                resolucionAux.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                            }
                            if (!row.IsNull("idResultado"))
                            {
                                resolucionAux.resultado = new ParametroGenerico(Convert.ToInt32(row["idResultado"]), row["nombreResultado"].ToString());
                            }
                            if (!row.IsNull("idTipoDestinatario"))
                            {
                                resolucionAux.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                            }
                            if (!row.IsNull("idTema"))
                            {
                                resolucionAux.materia = new ParametroGenerico(Convert.ToInt32(row["idTema"]), row["nombreTema"].ToString());
                            }
                            if (!row.IsNull("idEstadoVigencia"))
                            {
                                resolucionAux.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVigencia"].ToString());
                            }
                            if (!row.IsNull("numero"))
                            {
                                resolucionAux.numero = row["numero"].ToString();
                            }
                            if (!row.IsNull("fecha"))
                            {
                                resolucionAux.fecha = Convert.ToDateTime(row["fecha"]);
                            }
                            if (!row.IsNull("tieneReferencia"))
                            {
                                resolucionAux.tieneReferencia = Convert.ToInt32(row["tieneReferencia"]);
                            }
                            resolucionAux.datoReferencia = new List<ParametroGenerico>();
                            resp.Add(resolucionAux);
                        }

                        if (!row.IsNull("dato"))
                        {
                            dato = new ParametroGenerico(0, row["dato"].ToString());
                            resolucionAux.datoReferencia.Add(dato);
                            //despues invocar el string descripcionDatosRef que concatena la lista 
                        }

                        idResolAux = idResol;

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

        public bool AcogeRecursoRepResolucion(int idResolucion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbAcogeRecursoRep";
                
                cnn.parametros.Add("@idResolucion", idResolucion);
                
                DataTable dt = cnn.Execute();
                idResolucion = Convert.ToInt32(dt.Rows[0]["idResolucion"]);

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
