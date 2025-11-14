using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades.Relocalizacion;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.relocalizacion
{
    public class TramiteRelocalizacionDA
    {

        public Logger Log { get; set; }

        public TramiteRelocalizacionDA()
        {
            this.Log = new Logger();
        }

        public bool GuardarTramiteRelocalizacion(TramiteRelocalizacion tramiteRel)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTramiteRelocalizacion";
                
                cnn.parametros.Add("@idTramiteRel", tramiteRel.idTramiteRel);
                cnn.parametros.Add("@idEstadoTramite", tramiteRel.estadoTramite.id);
                cnn.parametros.Add("@numPert", tramiteRel.numPert);
                cnn.parametros.Add("@fechaRecepcion", tramiteRel.fechaRecepcion);
                cnn.parametros.Add("@fechaIngresoTramite", tramiteRel.fechaIngresoTramite);
                cnn.parametros.Add("@idTipoRel", tramiteRel.tipoRelocalizacion.id);
                
                DataTable dt = cnn.Execute();
                tramiteRel.idTramiteRel = Convert.ToInt32(dt.Rows[0]["idTramiteRel"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

         public TramiteRelocalizacion ObtieneTramiteRelocalizacion(int idTramiteRel, int numPert)
        {
            try
            {
                TramiteRelocalizacion tramRelocalizacion = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTramiteRelocalizacion";
                if (idTramiteRel>0){
                    cnn.parametros.Add("@idTramiteRel", idTramiteRel);
                }
                if(numPert>0){
                    cnn.parametros.Add("@numPert", numPert);
                }
                
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       tramRelocalizacion = new TramiteRelocalizacion();
                       tramRelocalizacion.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                       tramRelocalizacion.estadoTramite = new ParametroGenerico(Convert.ToInt32(row["idEstadoTramite"]), row["nombreEstado"].ToString());
                       tramRelocalizacion.numPert =  Convert.ToString(row["numPert"]);
                       if (!row.IsNull("fechaRecepcion"))
                       {
                           tramRelocalizacion.fechaRecepcion = Convert.ToDateTime(row["fechaRecepcion"]);
                       }
                       if (!row.IsNull("fechaIngresoTramite"))
                       {
                           tramRelocalizacion.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                       }
                       if (!row.IsNull("fechaIngresoSistema"))
                       {
                           tramRelocalizacion.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                       }
                       if (!row.IsNull("idTipoRelocalizacion"))
                       {
                           tramRelocalizacion.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipo"].ToString());
                       }
                       tramRelocalizacion.enTram = Convert.ToBoolean(row["enTram"]);
                       tramRelocalizacion.despliegaAlertas = Convert.ToBoolean(row["alertas"]);
                    }
                }

                return tramRelocalizacion;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

         public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_Tramite(TramiteRelocalizacion tramiteFiltro, int cantidadPaginacion, int tipoRelocalizacion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbTramiteRelocalizacionAdmin_EnTramite";

                 cnn.parametros.Add("@idEstadoTramite", tramiteFiltro.estadoTramite.id);
                
                 if (tramiteFiltro.numPert != null && !tramiteFiltro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", tramiteFiltro.numPert);
                 }
                 if (tramiteFiltro.fechaTramiteFiltroIni != null && tramiteFiltro.fechaTramiteFiltroIni != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramiteIni", tramiteFiltro.fechaTramiteFiltroIni);
                 }
                 if (tramiteFiltro.fechaTramiteFiltroFin != null && tramiteFiltro.fechaTramiteFiltroFin != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramiteFin", tramiteFiltro.fechaTramiteFiltroFin);
                 }
                 if (tramiteFiltro.titularFiltro != null && tramiteFiltro.titularFiltro.rutPersona>0)
                 {
                     cnn.parametros.Add("@rutPersona", tramiteFiltro.titularFiltro.rutPersona);
                 }
                 if (tramiteFiltro.centroDestinoFiltro != null && tramiteFiltro.centroDestinoFiltro.id>0)
                 {
                     cnn.parametros.Add("@codSiepDestino", tramiteFiltro.centroDestinoFiltro.id);
                 }
                 if (tramiteFiltro.centroOrigenFiltro != null && tramiteFiltro.centroOrigenFiltro.id>0)
                 {
                     cnn.parametros.Add("@codigoSiepOrigen", tramiteFiltro.centroOrigenFiltro.id);
                 }
                 if (tramiteFiltro.regionDestino != null && tramiteFiltro.regionDestino.id_region>0)
                 {
                     cnn.parametros.Add("@idRegionDest", tramiteFiltro.regionDestino.id_region);
                 }
                 if (tramiteFiltro.regionOrigen != null && tramiteFiltro.regionOrigen.id_region > 0)
                 {
                     cnn.parametros.Add("@idRegionOrigen", tramiteFiltro.regionOrigen.id_region);
                 }
                 if (tramiteFiltro.regionDestino != null && tramiteFiltro.regionDestino.comuna != null && tramiteFiltro.regionDestino.comuna.id_comuna>0)
                 {
                     cnn.parametros.Add("@idComunaDestino", tramiteFiltro.regionDestino.comuna.id_comuna);
                 }
                 if (tramiteFiltro.regionOrigen != null && tramiteFiltro.regionOrigen.comuna != null && tramiteFiltro.regionOrigen.comuna.id_comuna > 0)
                 {
                     cnn.parametros.Add("@idComunaOrigen", tramiteFiltro.regionOrigen.comuna.id_comuna);
                 }
                 cnn.parametros.Add("@idTipoRel", tipoRelocalizacion);
                 if (tramiteFiltro.estadoFiltro != null && tramiteFiltro.estadoFiltro.id > 0)
                 {
                    cnn.parametros.Add("@idEstado", tramiteFiltro.estadoFiltro.id);
                 }
                 if (tramiteFiltro.tipoRelocalizacion!= null && tramiteFiltro.tipoRelocalizacion.id > 0)
                 {
                     cnn.parametros.Add("@tipoFlujoRel", tramiteFiltro.tipoRelocalizacion.id);
                 }
                


                 DataTable dt = cnn.Execute();
                 int idTramiteRel = 0;
                 int idTramiteRelAux = 0;
                 int idDetSector = 0;
                 int idDetSectorAux = 0;
                 TramiteRelocalizacion tramiteRel = null;
                 List<TramiteRelocalizacion> resp = new List<TramiteRelocalizacion>();
                 DetalleSector detSectorAux = null;
                 ParametroGenerico comunaAux = null;
                 HashSet<int> clavesComunaDes = new HashSet<int>();
                 HashSet<int> clavesComunaOrigen = new HashSet<int>();
                 HashSet<int> clavesPoligono = new HashSet<int>();
                 HashSet<int> clavesOrigenes = new HashSet<int>();
                 CoordenadaGeografica coordAux = null;
                 Poligono poligonoAux = null;
                 OrigenSector origenSectorAux = null;



                 if (dt != null)
                 {

                     int sectoresAcumulados = 0;
                     bool setearColumnas = false;
                     bool cambioPagina = false;
                     int cantidadSectoresPagAnt = 0;
                     int cantidadSectoresPagSig = 0;

                     foreach (DataRow row in dt.Rows)
                     {


                         idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                         idDetSector = Convert.ToInt32(row["idDetalleSector"]);


                         //COMIENZA UN NUEVO TRAMITE
                         if (idTramiteRel != idTramiteRelAux)
                         {

                             if (cambioPagina)
                             {
                                 sectoresAcumulados = cantidadSectoresPagSig;
                                 cambioPagina = false;
                             }

                             setearColumnas = false;
                             cantidadSectoresPagAnt = 0;
                             cantidadSectoresPagSig = 0;


                             int cantidadSectoresTramite = Convert.ToInt32(row["columnas"]);

                             if ((cantidadSectoresTramite + sectoresAcumulados) <= cantidadPaginacion)
                             {

                                 sectoresAcumulados = sectoresAcumulados + cantidadSectoresTramite;
                             }
                             else
                             {
                                 //CALCULAR PARA EL TRAMITE QUE SE TOPA CON LA PAGINACION

                                 setearColumnas = true;

                                 cantidadSectoresPagAnt = cantidadPaginacion - sectoresAcumulados;
                                 cantidadSectoresPagSig = cantidadSectoresTramite - cantidadSectoresPagAnt;
                             }
                         }


                         if(idDetSector != idDetSectorAux){





                             //DEJAR SOLO 1 SECTOR EN CADA TRAMITE
                             tramiteRel = new TramiteRelocalizacion();
                             tramiteRel.idTramiteRel = idTramiteRel;
                             tramiteRel.estadoTramite = new ParametroGenerico(Convert.ToInt32(row["idEstadoTramite"]), "");
                             tramiteRel.numPert = Convert.ToString(row["numPert"]);
                             tramiteRel.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             tramiteRel.puedeRedefinir = Convert.ToInt32(row["puedeRedefinir"]);
                             if (tramiteRel.puedeRedefinir > 0) {
                                 tramiteRel.despliegaRedefinicion = true;
                             }


                             if (setearColumnas)
                             {

                                 if (sectoresAcumulados >= cantidadPaginacion)
                                 {
                                     tramiteRel.columnas = cantidadSectoresPagSig;
                                     cambioPagina = true;
                                 }
                                 else
                                 {
                                     tramiteRel.columnas = cantidadSectoresPagAnt;
                                 }

                                 sectoresAcumulados++;
                             }
                             else
                             {
                                 tramiteRel.columnas = Convert.ToInt32(row["columnas"]);
                             }


                             tramiteRel.despliegaErrores = Convert.ToBoolean(row["errores"]);
                             tramiteRel.despliegaAlertas = Convert.ToBoolean(row["alertas"]);
                             tramiteRel.despliegaModificacion = Convert.ToBoolean(row["modificacion"]);
                             tramiteRel.sectores = new List<DetalleSector>();
                             resp.Add(tramiteRel);
                             //FIN DEJAR SOLO 1 SECTOR EN CADA TRAMITE
                

                             detSectorAux = new DetalleSector();
                             tramiteRel.sectores.Add(detSectorAux);
                             detSectorAux.idDetalleSector = idDetSector;

                             if (!row.IsNull("idSolConcesion"))
                             {
                                 detSectorAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             }

                             if (!row.IsNull("resultadoResolucion"))
                             {
                                 detSectorAux.resultadoResolucion = new ParametroGenerico(Convert.ToInt32(row["resultadoResolucion"]));
                             }

                             detSectorAux.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]),row["nombreTipoRelocalizacion"].ToString());
                             detSectorAux.numSector = Convert.ToInt32(row["numSector"]);
                             detSectorAux.esSectorCero = Convert.ToBoolean(row["esSectorCero"]);
                             detSectorAux.concesionDestino = new SolicitudConcesion();
                             detSectorAux.concesionDestino.unidadEspacial = new UnidadEspacial();
                             detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

                             if (!row.IsNull("codSiepDestino"))
                             {
                                 detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = row["codSiepDestino"].ToString();
                             }
                             if (!row.IsNull("nombreCentroDestino"))
                             {
                                detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroDestino"].ToString();
                             }
                             if (!row.IsNull("idEstadoActual"))
                             {
                                 detSectorAux.concesionDestino.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstadoActual"].ToString());
                             }
                             
                             if (!row.IsNull("idRegDestino"))
                             {
                                 detSectorAux.concesionDestino.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestino"]), row["nombreRegDestino"].ToString());
                             }
                             
                             detSectorAux.concesionDestino.comuna = new List<ParametroGenerico>();
                             clavesComunaDes = new HashSet<int>();
                             clavesComunaOrigen = new HashSet<int>();
                             clavesPoligono = new HashSet<int>();
                             clavesOrigenes = new HashSet<int>();
                             detSectorAux.concesionDestino.coordenadaGeografica = new List<CoordenadaGeografica>();
                             coordAux = new CoordenadaGeografica();
                             coordAux.listaPoligono = new List<Poligono>();
                             detSectorAux.concesionDestino.coordenadaGeografica.Add(coordAux);
                             detSectorAux.origenes = new List<OrigenSector>();
                         }
                         if (!row.IsNull("idComunaDestino"))
                         {
                             if (clavesComunaDes.Count == 0 || !clavesComunaDes.Contains(Convert.ToInt32(row["idComunaDestino"])))
                            {
                                clavesComunaDes.Add(Convert.ToInt32(row["idComunaDestino"]));
                                comunaAux = new ParametroGenerico(Convert.ToInt32(row["idComunaDestino"]), row["nomComunaDestino"].ToString());
                                detSectorAux.concesionDestino.comuna.Add(comunaAux);
                            }
                         }
                          if (!row.IsNull("idPoligono"))
                         {
                            if (clavesPoligono.Count==0 || !clavesPoligono.Contains(Convert.ToInt32(row["idPoligono"])))
                            {
                                clavesPoligono.Add(Convert.ToInt32(row["idPoligono"]));
                                poligonoAux = new Poligono();
                                poligonoAux.idPoligono = Convert.ToInt32(row["idPoligono"]);
                                poligonoAux.toponimio = row["toponimio"].ToString();
                                coordAux.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeo"]);
                                coordAux.listaPoligono.Add(poligonoAux);
                            }
                         }
                         if (!row.IsNull("idOrigenSector"))
                         {
                              if (clavesOrigenes.Count == 0 || !clavesOrigenes.Contains(Convert.ToInt32(row["idOrigenSector"])))
                              {
                                  clavesOrigenes.Add(Convert.ToInt32(row["idOrigenSector"]));
                                  origenSectorAux = new OrigenSector();
                                  origenSectorAux.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);
                                  origenSectorAux.concesionOrigen = new SolicitudConcesion();
                                  origenSectorAux.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                  origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                  origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro =row["codigoSiep"].ToString();
                                  origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroOrigen"].ToString();
                                  if (!row.IsNull("idRegOrigen"))
                                  {
                                      origenSectorAux.concesionOrigen.region = new ParametroGenerico(Convert.ToInt32(row["idRegOrigen"]), row["nombreRegOrigen"].ToString());
                                  }
                                  
                                  origenSectorAux.concesionOrigen.comuna = new List<ParametroGenerico>();
                                  detSectorAux.origenes.Add(origenSectorAux);
                              }
                         }
                         if (!row.IsNull("idComunaOrigen"))
                         {
                             if (clavesComunaOrigen.Count == 0 || !clavesComunaOrigen.Contains(Convert.ToInt32(row["idComunaOrigen"])))
                             {
                                 clavesComunaOrigen.Add(Convert.ToInt32(row["idComunaOrigen"]));
                                 comunaAux = new ParametroGenerico(Convert.ToInt32(row["idComunaOrigen"]), row["nomComunaOrigen"].ToString());
                                 origenSectorAux.concesionOrigen.comuna.Add(comunaAux);
                             }
                         }

                         idTramiteRelAux = idTramiteRel;
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

         public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_Rechazo(TramiteRelocalizacion tramiteFiltro, int cantidadPaginacion, int tipoRelocalizacion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbTramiteRelocalizacionAdmin_Rechazado";

                 cnn.parametros.Add("@idEstadoTramite", tramiteFiltro.estadoTramite.id);

                 if (tramiteFiltro.numPert != null && !tramiteFiltro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", tramiteFiltro.numPert);
                 }
                 if (tramiteFiltro.fechaTramiteFiltroIni != null && tramiteFiltro.fechaTramiteFiltroIni != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramiteIni", tramiteFiltro.fechaTramiteFiltroIni);
                 }
                 if (tramiteFiltro.fechaTramiteFiltroFin != null && tramiteFiltro.fechaTramiteFiltroFin != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramiteFin", tramiteFiltro.fechaTramiteFiltroFin);
                 }
                 if (tramiteFiltro.titularFiltro != null && tramiteFiltro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", tramiteFiltro.titularFiltro.rutPersona);
                 }
                 if (tramiteFiltro.centroDestinoFiltro != null && tramiteFiltro.centroDestinoFiltro.id > 0)
                 {
                     cnn.parametros.Add("@codSiepDestino", tramiteFiltro.centroDestinoFiltro.id);
                 }
                 if (tramiteFiltro.centroOrigenFiltro != null && tramiteFiltro.centroOrigenFiltro.id > 0)
                 {
                     cnn.parametros.Add("@codigoSiepOrigen", tramiteFiltro.centroOrigenFiltro.id);
                 }
                 if (tramiteFiltro.regionDestino != null && tramiteFiltro.regionDestino.id_region > 0)
                 {
                     cnn.parametros.Add("@idRegionDest", tramiteFiltro.regionDestino.id_region);
                 }
                 if (tramiteFiltro.regionOrigen != null && tramiteFiltro.regionOrigen.id_region > 0)
                 {
                     cnn.parametros.Add("@idRegionOrigen", tramiteFiltro.regionOrigen.id_region);
                 }
                 if (tramiteFiltro.regionDestino != null && tramiteFiltro.regionDestino.comuna != null && tramiteFiltro.regionDestino.comuna.id_comuna > 0)
                 {
                     cnn.parametros.Add("@idComunaDestino", tramiteFiltro.regionDestino.comuna.id_comuna);
                 }
                 if (tramiteFiltro.regionOrigen != null && tramiteFiltro.regionOrigen.comuna != null && tramiteFiltro.regionOrigen.comuna.id_comuna > 0)
                 {
                     cnn.parametros.Add("@idComunaOrigen", tramiteFiltro.regionOrigen.comuna.id_comuna);
                 }
                 cnn.parametros.Add("@idTipoRel", tipoRelocalizacion);
                 
                 if (tramiteFiltro.estadoFiltro != null && tramiteFiltro.estadoFiltro.id > 0)
                 {
                     cnn.parametros.Add("@idEstado", tramiteFiltro.estadoFiltro.id);
                 }
                 if (tramiteFiltro.tipoRelocalizacion != null && tramiteFiltro.tipoRelocalizacion.id > 0)
                 {
                     cnn.parametros.Add("@tipoFlujoRel", tramiteFiltro.tipoRelocalizacion.id);
                 }


                 DataTable dt = cnn.Execute();
                 int idTramiteRel = 0;
                 int idTramiteRelAux = 0;
                 int idDetSector = 0;
                 int idDetSectorAux = 0;
                 TramiteRelocalizacion tramiteRel = null;
                 List<TramiteRelocalizacion> resp = new List<TramiteRelocalizacion>();
                 DetalleSector detSectorAux = null;
                 ParametroGenerico comunaAux = null;
                 HashSet<int> clavesComunaDes = new HashSet<int>();
                 HashSet<int> clavesComunaOrigen = new HashSet<int>();
                 HashSet<int> clavesPoligono = new HashSet<int>();
                 HashSet<int> clavesOrigenes = new HashSet<int>();
                 CoordenadaGeografica coordAux = null;
                 Poligono poligonoAux = null;
                 OrigenSector origenSectorAux = null;




                 if (dt != null)
                 {

                     int sectoresAcumulados = 0;
                     bool setearColumnas = false;
                     bool cambioPagina = false;
                     int cantidadSectoresPagAnt = 0;
                     int cantidadSectoresPagSig = 0;

                     foreach (DataRow row in dt.Rows)
                     {


                         idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                         idDetSector = Convert.ToInt32(row["idDetalleSector"]);


                         //COMIENZA UN NUEVO TRAMITE
                         if (idTramiteRel != idTramiteRelAux)
                         {

                             if (cambioPagina)
                             {
                                 sectoresAcumulados = cantidadSectoresPagSig;
                                 cambioPagina = false;
                             }

                             setearColumnas = false;
                             cantidadSectoresPagAnt = 0;
                             cantidadSectoresPagSig = 0;


                             int cantidadSectoresTramite = Convert.ToInt32(row["columnas"]);

                             if ((cantidadSectoresTramite + sectoresAcumulados) <= cantidadPaginacion)
                             {

                                 sectoresAcumulados = sectoresAcumulados + cantidadSectoresTramite;
                             }
                             else
                             {
                                 //CALCULAR PARA EL TRAMITE QUE SE TOPA CON LA PAGINACION

                                 setearColumnas = true;

                                 cantidadSectoresPagAnt = cantidadPaginacion - sectoresAcumulados;
                                 cantidadSectoresPagSig = cantidadSectoresTramite - cantidadSectoresPagAnt;
                             }
                         }


                         if (idDetSector != idDetSectorAux)
                         {




                             //DEJAR SOLO 1 SECTOR EN CADA TRAMITE
                             tramiteRel = new TramiteRelocalizacion();
                             tramiteRel.idTramiteRel = idTramiteRel;
                             tramiteRel.estadoTramite = new ParametroGenerico(Convert.ToInt32(row["idEstadoTramite"]), "");
                             tramiteRel.numPert = Convert.ToString(row["numPert"]);
                             tramiteRel.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             tramiteRel.puedeRedefinir = Convert.ToInt32(row["puedeRedefinir"]);

                             if (setearColumnas)
                             {

                                 if (sectoresAcumulados >= cantidadPaginacion)
                                 {
                                     tramiteRel.columnas = cantidadSectoresPagSig;
                                     cambioPagina = true;
                                 }
                                 else
                                 {
                                     tramiteRel.columnas = cantidadSectoresPagAnt;
                                 }

                                 sectoresAcumulados++;
                             }
                             else
                             {
                                 tramiteRel.columnas = Convert.ToInt32(row["columnas"]);
                             }


                             tramiteRel.despliegaErrores = Convert.ToBoolean(row["errores"]);
                             tramiteRel.despliegaAlertas = Convert.ToBoolean(row["alertas"]);
                             tramiteRel.despliegaModificacion = Convert.ToBoolean(row["modificacion"]);
                             tramiteRel.sectores = new List<DetalleSector>();
                             resp.Add(tramiteRel);
                             //FIN DEJAR SOLO 1 SECTOR EN CADA TRAMITE


                             detSectorAux = new DetalleSector();
                             tramiteRel.sectores.Add(detSectorAux);
                             detSectorAux.idDetalleSector = idDetSector;

                             if (!row.IsNull("idSolConcesion"))
                             {
                                 detSectorAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             }
                             if (!row.IsNull("resultadoResolucion"))
                             {
                                 detSectorAux.resultadoResolucion = new ParametroGenerico(Convert.ToInt32(row["resultadoResolucion"]));
                             }

                             detSectorAux.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipoRelocalizacion"].ToString());
                             detSectorAux.numSector = Convert.ToInt32(row["numSector"]);
                             detSectorAux.esSectorCero = Convert.ToBoolean(row["esSectorCero"]);
                             detSectorAux.concesionDestino = new SolicitudConcesion();
                             detSectorAux.concesionDestino.unidadEspacial = new UnidadEspacial();
                             detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

                             if (!row.IsNull("codSiepDestino"))
                             {
                                 detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = row["codSiepDestino"].ToString();
                             }
                             if (!row.IsNull("nombreCentroDestino"))
                             {
                                 detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroDestino"].ToString();
                             }
                             if (!row.IsNull("idEstadoActual"))
                             {
                                 detSectorAux.concesionDestino.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstadoActual"].ToString());
                             }
                             if (!row.IsNull("idRegDestino"))
                             {
                                 detSectorAux.concesionDestino.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestino"]), row["nombreRegDestino"].ToString());
                             }

                             detSectorAux.concesionDestino.comuna = new List<ParametroGenerico>();
                             clavesComunaDes = new HashSet<int>();
                             clavesComunaOrigen = new HashSet<int>();
                             clavesPoligono = new HashSet<int>();
                             clavesOrigenes = new HashSet<int>();
                             detSectorAux.concesionDestino.coordenadaGeografica = new List<CoordenadaGeografica>();
                             coordAux = new CoordenadaGeografica();
                             coordAux.listaPoligono = new List<Poligono>();
                             detSectorAux.concesionDestino.coordenadaGeografica.Add(coordAux);
                             detSectorAux.origenes = new List<OrigenSector>();
                         }
                         if (!row.IsNull("idComunaDestino"))
                         {
                             if (clavesComunaDes.Count == 0 || !clavesComunaDes.Contains(Convert.ToInt32(row["idComunaDestino"])))
                             {
                                 clavesComunaDes.Add(Convert.ToInt32(row["idComunaDestino"]));
                                 comunaAux = new ParametroGenerico(Convert.ToInt32(row["idComunaDestino"]), row["nomComunaDestino"].ToString());
                                 detSectorAux.concesionDestino.comuna.Add(comunaAux);
                             }
                         }
                         if (!row.IsNull("idPoligono"))
                         {
                             if (clavesPoligono.Count == 0 || !clavesPoligono.Contains(Convert.ToInt32(row["idPoligono"])))
                             {
                                 clavesPoligono.Add(Convert.ToInt32(row["idPoligono"]));
                                 poligonoAux = new Poligono();
                                 poligonoAux.idPoligono = Convert.ToInt32(row["idPoligono"]);
                                 poligonoAux.toponimio = row["toponimio"].ToString();
                                 coordAux.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeo"]);
                                 coordAux.listaPoligono.Add(poligonoAux);
                             }
                         }
                         if (!row.IsNull("idOrigenSector"))
                         {
                             if (clavesOrigenes.Count == 0 || !clavesOrigenes.Contains(Convert.ToInt32(row["idOrigenSector"])))
                             {
                                 clavesOrigenes.Add(Convert.ToInt32(row["idOrigenSector"]));
                                 origenSectorAux = new OrigenSector();
                                 origenSectorAux.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);
                                 origenSectorAux.concesionOrigen = new SolicitudConcesion();
                                 origenSectorAux.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                 origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoSiep"].ToString();
                                 origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroOrigen"].ToString();
                                 if (!row.IsNull("idRegOrigen"))
                                 {
                                     origenSectorAux.concesionOrigen.region = new ParametroGenerico(Convert.ToInt32(row["idRegOrigen"]), row["nombreRegOrigen"].ToString());
                                 }

                                 origenSectorAux.concesionOrigen.comuna = new List<ParametroGenerico>();
                                 detSectorAux.origenes.Add(origenSectorAux);
                             }
                         }
                         if (!row.IsNull("idComunaOrigen"))
                         {
                             if (clavesComunaOrigen.Count == 0 || !clavesComunaOrigen.Contains(Convert.ToInt32(row["idComunaOrigen"])))
                             {
                                 clavesComunaOrigen.Add(Convert.ToInt32(row["idComunaOrigen"]));
                                 comunaAux = new ParametroGenerico(Convert.ToInt32(row["idComunaOrigen"]), row["nomComunaOrigen"].ToString());
                                 origenSectorAux.concesionOrigen.comuna.Add(comunaAux);
                             }
                         }

                         idTramiteRelAux = idTramiteRel;
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

         public List<TramiteRelocalizacion> ListarTramiteRelocalizacionAdmin_Aprueba(TramiteRelocalizacion tramiteFiltro, int cantidadPaginacion, int tipoRelocalizacion)
         {
             try
             {
                 Conexion cnn = new Conexion();
                 cnn.procedimiento = "paSelRbTramiteRelocalizacionAdmin_Aprobado";

                 cnn.parametros.Add("@idEstadoTramite", tramiteFiltro.estadoTramite.id);

                 if (tramiteFiltro.numPert != null && !tramiteFiltro.numPert.Trim().Equals(""))
                 {
                     cnn.parametros.Add("@numPert", tramiteFiltro.numPert);
                 }
                 if (tramiteFiltro.fechaTramiteFiltroIni != null && tramiteFiltro.fechaTramiteFiltroIni != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramiteIni", tramiteFiltro.fechaTramiteFiltroIni);
                 }
                 if (tramiteFiltro.fechaTramiteFiltroFin != null && tramiteFiltro.fechaTramiteFiltroFin != default(DateTime))
                 {
                     cnn.parametros.Add("@fechaIngresoTramiteFin", tramiteFiltro.fechaTramiteFiltroFin);
                 }
                 if (tramiteFiltro.titularFiltro != null && tramiteFiltro.titularFiltro.rutPersona > 0)
                 {
                     cnn.parametros.Add("@rutPersona", tramiteFiltro.titularFiltro.rutPersona);
                 }
                 if (tramiteFiltro.centroDestinoFiltro != null && tramiteFiltro.centroDestinoFiltro.id > 0)
                 {
                     cnn.parametros.Add("@codSiepDestino", tramiteFiltro.centroDestinoFiltro.id);
                 }
                 if (tramiteFiltro.centroOrigenFiltro != null && tramiteFiltro.centroOrigenFiltro.id > 0)
                 {
                     cnn.parametros.Add("@codigoSiepOrigen", tramiteFiltro.centroOrigenFiltro.id);
                 }
                 if (tramiteFiltro.regionDestino != null && tramiteFiltro.regionDestino.id_region > 0)
                 {
                     cnn.parametros.Add("@idRegionDest", tramiteFiltro.regionDestino.id_region);
                 }
                 if (tramiteFiltro.regionOrigen != null && tramiteFiltro.regionOrigen.id_region > 0)
                 {
                     cnn.parametros.Add("@idRegionOrigen", tramiteFiltro.regionOrigen.id_region);
                 }
                 if (tramiteFiltro.regionDestino != null && tramiteFiltro.regionDestino.comuna != null && tramiteFiltro.regionDestino.comuna.id_comuna > 0)
                 {
                     cnn.parametros.Add("@idComunaDestino", tramiteFiltro.regionDestino.comuna.id_comuna);
                 }
                 if (tramiteFiltro.regionOrigen != null && tramiteFiltro.regionOrigen.comuna != null && tramiteFiltro.regionOrigen.comuna.id_comuna > 0)
                 {
                     cnn.parametros.Add("@idComunaOrigen", tramiteFiltro.regionOrigen.comuna.id_comuna);
                 }
                 cnn.parametros.Add("@idTipoRel", tipoRelocalizacion);
                 if (tramiteFiltro.estadoFiltro != null && tramiteFiltro.estadoFiltro.id > 0)
                 {
                     cnn.parametros.Add("@idEstado", tramiteFiltro.estadoFiltro.id);
                 }
                 if (tramiteFiltro.tipoRelocalizacion != null && tramiteFiltro.tipoRelocalizacion.id > 0)
                 {
                     cnn.parametros.Add("@tipoFlujoRel", tramiteFiltro.tipoRelocalizacion.id);
                 }

                 DataTable dt = cnn.Execute();
                 int idTramiteRel = 0;
                 int idTramiteRelAux = 0;
                 int idDetSector = 0;
                 int idDetSectorAux = 0;
                 TramiteRelocalizacion tramiteRel = null;
                 List<TramiteRelocalizacion> resp = new List<TramiteRelocalizacion>();
                 DetalleSector detSectorAux = null;
                 ParametroGenerico comunaAux = null;
                 HashSet<int> clavesComunaDes = new HashSet<int>();
                 HashSet<int> clavesComunaOrigen = new HashSet<int>();
                 HashSet<int> clavesPoligono = new HashSet<int>();
                 HashSet<int> clavesOrigenes = new HashSet<int>();
                 CoordenadaGeografica coordAux = null;
                 Poligono poligonoAux = null;
                 OrigenSector origenSectorAux = null;



                 if (dt != null)
                 {

                     int sectoresAcumulados = 0;
                     bool setearColumnas = false;
                     bool cambioPagina = false;
                     int cantidadSectoresPagAnt = 0;
                     int cantidadSectoresPagSig = 0;

                     foreach (DataRow row in dt.Rows)
                     {


                         idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                         idDetSector = Convert.ToInt32(row["idDetalleSector"]);


                         //COMIENZA UN NUEVO TRAMITE
                         if (idTramiteRel != idTramiteRelAux)
                         {

                             if (cambioPagina)
                             {
                                 sectoresAcumulados = cantidadSectoresPagSig;
                                 cambioPagina = false;
                             }

                             setearColumnas = false;
                             cantidadSectoresPagAnt = 0;
                             cantidadSectoresPagSig = 0;


                             int cantidadSectoresTramite = Convert.ToInt32(row["columnas"]);

                             if ((cantidadSectoresTramite + sectoresAcumulados) <= cantidadPaginacion)
                             {

                                 sectoresAcumulados = sectoresAcumulados + cantidadSectoresTramite;
                             }
                             else
                             {
                                 //CALCULAR PARA EL TRAMITE QUE SE TOPA CON LA PAGINACION

                                 setearColumnas = true;

                                 cantidadSectoresPagAnt = cantidadPaginacion - sectoresAcumulados;
                                 cantidadSectoresPagSig = cantidadSectoresTramite - cantidadSectoresPagAnt;
                             }
                         }


                         if (idDetSector != idDetSectorAux)
                         {


                             //DEJAR SOLO 1 SECTOR EN CADA TRAMITE
                             tramiteRel = new TramiteRelocalizacion();
                             tramiteRel.idTramiteRel = idTramiteRel;
                             tramiteRel.estadoTramite = new ParametroGenerico(Convert.ToInt32(row["idEstadoTramite"]), "");
                             tramiteRel.numPert = Convert.ToString(row["numPert"]);
                             tramiteRel.fechaIngresoTramite = Convert.ToDateTime(row["fechaIngresoTramite"]);
                             tramiteRel.puedeRedefinir = Convert.ToInt32(row["puedeRedefinir"]);

                             if (setearColumnas)
                             {

                                 if (sectoresAcumulados >= cantidadPaginacion)
                                 {
                                     tramiteRel.columnas = cantidadSectoresPagSig;
                                     cambioPagina = true;
                                 }
                                 else
                                 {
                                     tramiteRel.columnas = cantidadSectoresPagAnt;
                                 }

                                 sectoresAcumulados++;
                             }
                             else
                             {
                                 tramiteRel.columnas = Convert.ToInt32(row["columnas"]);
                             }

                             tramiteRel.despliegaErrores = Convert.ToBoolean(row["errores"]);
                             tramiteRel.despliegaAlertas = Convert.ToBoolean(row["alertas"]);
                             tramiteRel.despliegaModificacion = Convert.ToBoolean(row["modificacion"]);
                             tramiteRel.sectores = new List<DetalleSector>();
                             resp.Add(tramiteRel);
                             //FIN DEJAR SOLO 1 SECTOR EN CADA TRAMITE


                             detSectorAux = new DetalleSector();
                             tramiteRel.sectores.Add(detSectorAux);
                             detSectorAux.idDetalleSector = idDetSector;

                             if (!row.IsNull("idSolConcesion"))
                             {
                                 detSectorAux.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);
                             }
                             if (!row.IsNull("resultadoResolucion"))
                             {
                                 detSectorAux.resultadoResolucion = new ParametroGenerico(Convert.ToInt32(row["resultadoResolucion"]));
                             }


                             detSectorAux.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelocalizacion"]), row["nombreTipoRelocalizacion"].ToString());
                             detSectorAux.numSector = Convert.ToInt32(row["numSector"]);
                             detSectorAux.esSectorCero = Convert.ToBoolean(row["esSectorCero"]);
                             detSectorAux.concesionDestino = new SolicitudConcesion();
                             detSectorAux.concesionDestino.unidadEspacial = new UnidadEspacial();
                             detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();

                             if (!row.IsNull("codSiepDestino"))
                             {
                                 detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro = row["codSiepDestino"].ToString();
                             }
                             if (!row.IsNull("nombreCentroDestino"))
                             {
                                 detSectorAux.concesionDestino.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroDestino"].ToString();
                             }
                             if (!row.IsNull("idEstadoActual"))
                             {
                                 detSectorAux.concesionDestino.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstadoActual"].ToString());
                             }
                             
                             if (!row.IsNull("idRegDestino"))
                             {
                                 detSectorAux.concesionDestino.region = new ParametroGenerico(Convert.ToInt32(row["idRegDestino"]), row["nombreRegDestino"].ToString());
                             }

                             detSectorAux.concesionDestino.comuna = new List<ParametroGenerico>();
                             clavesComunaDes = new HashSet<int>();
                             clavesComunaOrigen = new HashSet<int>();
                             clavesPoligono = new HashSet<int>();
                             clavesOrigenes = new HashSet<int>();
                             detSectorAux.concesionDestino.coordenadaGeografica = new List<CoordenadaGeografica>();
                             coordAux = new CoordenadaGeografica();
                             coordAux.listaPoligono = new List<Poligono>();
                             detSectorAux.concesionDestino.coordenadaGeografica.Add(coordAux);
                             detSectorAux.origenes = new List<OrigenSector>();
                         }
                         if (!row.IsNull("idComunaDestino"))
                         {
                             if (clavesComunaDes.Count == 0 || !clavesComunaDes.Contains(Convert.ToInt32(row["idComunaDestino"])))
                             {
                                 clavesComunaDes.Add(Convert.ToInt32(row["idComunaDestino"]));
                                 comunaAux = new ParametroGenerico(Convert.ToInt32(row["idComunaDestino"]), row["nomComunaDestino"].ToString());
                                 detSectorAux.concesionDestino.comuna.Add(comunaAux);
                             }
                         }
                         if (!row.IsNull("idPoligono"))
                         {
                             if (clavesPoligono.Count == 0 || !clavesPoligono.Contains(Convert.ToInt32(row["idPoligono"])))
                             {
                                 clavesPoligono.Add(Convert.ToInt32(row["idPoligono"]));
                                 poligonoAux = new Poligono();
                                 poligonoAux.idPoligono = Convert.ToInt32(row["idPoligono"]);
                                 poligonoAux.toponimio = row["toponimio"].ToString();
                                 coordAux.idCoordenadaGeo = Convert.ToInt32(row["idCoordenadaGeo"]);
                                 coordAux.listaPoligono.Add(poligonoAux);
                             }
                         }
                         if (!row.IsNull("idOrigenSector"))
                         {
                             if (clavesOrigenes.Count == 0 || !clavesOrigenes.Contains(Convert.ToInt32(row["idOrigenSector"])))
                             {
                                 clavesOrigenes.Add(Convert.ToInt32(row["idOrigenSector"]));
                                 origenSectorAux = new OrigenSector();
                                 origenSectorAux.idOrigenSector = Convert.ToInt32(row["idOrigenSector"]);
                                 origenSectorAux.concesionOrigen = new SolicitudConcesion();
                                 origenSectorAux.concesionOrigen.unidadEspacial = new UnidadEspacial();
                                 origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo = new CentrosDeCultivo();
                                 origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro = row["codigoSiep"].ToString();
                                 origenSectorAux.concesionOrigen.unidadEspacial.centrosDeCultivo.nombreCentro = row["nombreCentroOrigen"].ToString();
                                 if (!row.IsNull("idRegOrigen"))
                                 {
                                     origenSectorAux.concesionOrigen.region = new ParametroGenerico(Convert.ToInt32(row["idRegOrigen"]), row["nombreRegOrigen"].ToString());
                                 }

                                 origenSectorAux.concesionOrigen.comuna = new List<ParametroGenerico>();
                                 detSectorAux.origenes.Add(origenSectorAux);
                             }
                         }
                         if (!row.IsNull("idComunaOrigen"))
                         {
                             if (clavesComunaOrigen.Count == 0 || !clavesComunaOrigen.Contains(Convert.ToInt32(row["idComunaOrigen"])))
                             {
                                 clavesComunaOrigen.Add(Convert.ToInt32(row["idComunaOrigen"]));
                                 comunaAux = new ParametroGenerico(Convert.ToInt32(row["idComunaOrigen"]), row["nomComunaOrigen"].ToString());
                                 origenSectorAux.concesionOrigen.comuna.Add(comunaAux);
                             }
                         }

                         idTramiteRelAux = idTramiteRel;
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

         public bool ObtieneDespliegueSeccionUnidad(int idSeccion, int idTipo)
        {
            try
            {
                // idTipoRel:
                //85  -- Sector 0
                //86  -- Crea
                //87  -- Fusiona
                //612  -- Sector 0 (RESA)
                //613  -- Crea (RESA)
                //614  -- Fusiona (RESA)
                //121 -- Tramite de solicitud Centro de Acopio
                //122 -- Tramite de solicitud Acuicultura en Amerb
                //123 -- Tramite de solicitud Colectores de Semilla
                //124 -- Tramite de solicitud Centro de Faenamiento
                //535 -- Tramite de solicitud Experimentales AMERB
                //536 -- Tramite de solicitud Experimentales Concesión
                //537 -- Tramite de solicitud Acuicultura en ECMPO

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDespliegueSeccionUnidad";
                cnn.parametros.Add("@idSeccion", idSeccion);
                cnn.parametros.Add("@idTipoRel", idTipo);

                DataTable dt = cnn.Execute();

                if (dt != null && dt.Rows.Count >0)
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
            }
        }

        
        //Indica si un tramite de relocalización tiene todos sus detalles con resolución ssp aprobados
        public bool Tramite_resolucion_SSpCompleto(int idTramiteRel)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTramite_ResolucionSSP";
                cnn.parametros.Add("@idTramiteRel", idTramiteRel);

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
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            }
        }

        public bool aplicaPertTramiteExistente(string numPert)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTramiteRelocalizacionPert";
                cnn.parametros.Add("@numPert", numPert);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("idTramiteRel") && !row.IsNull("numPert"))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return true;
            }
        }

        public TramiteRelocalizacion ObtieneResolucionSSpTramiteRel(int idSolConcesion)
        {
            try
            {
                TramiteRelocalizacion tramRelocalizacion = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionSSpTramiteRel";
                
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        tramRelocalizacion = new TramiteRelocalizacion();
                        tramRelocalizacion.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                        tramRelocalizacion.numPert = Convert.ToString(row["numPert"]);
                        tramRelocalizacion.cantSSP = Convert.ToInt32(row["sectoresSinSsp"]); 
                    }
                }

                return tramRelocalizacion;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<TramiteRelocalizacion> aplicaCentroEnOtroTramiteRel(int idTramiteRel)
        {
            try
            {
                List<TramiteRelocalizacion> resp = new List<TramiteRelocalizacion>();
                TramiteRelocalizacion tram = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRBCentroEnOtroTramiteRel";
                cnn.parametros.Add("@idTramiteRel", idTramiteRel);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        tram = new TramiteRelocalizacion();
                        tram.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                        tram.numPert = row["numPert"].ToString();
                        tram.centroOrigenFiltro = new ParametroGenerico( Convert.ToInt32(row["codigoSiep"]), row["titulares"].ToString());
                        
                        resp.Add(tram);
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

        public TramiteRelocalizacion ObtieneResolucionSSpTramiteRelSolicitud(int idSolConcesion)
        {
            try
            {
                TramiteRelocalizacion tramRelocalizacion = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResolucionSSpTramiteRelSolic";

                cnn.parametros.Add("@idSolConcesion", idSolConcesion);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        tramRelocalizacion = new TramiteRelocalizacion();
                        tramRelocalizacion.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                        tramRelocalizacion.numPert = Convert.ToString(row["numPert"]);
                        tramRelocalizacion.cantSSP = Convert.ToInt32(row["sectoresSinSsp"]);
                        tramRelocalizacion.descripcion = Convert.ToString(row["DESCR"]);
                    }
                }

                return tramRelocalizacion;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }
        //valida que el centro de origen no se encuentre en otro trámite de relocalización 
        //que se encuentre tramitando
        public List<TramiteRelocalizacion> aplicaOrigenEnOtroTramiteRel(int idTramiteRel, int codigoSiepOrigen)
        {
            try
            {
                List<TramiteRelocalizacion> resp = new List<TramiteRelocalizacion>();
                TramiteRelocalizacion tram = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidaOrigen_TramiteRel";
                
                //si no se posee tramite se pasa 0 como parámetro
                cnn.parametros.Add("@idTramiteRel", idTramiteRel);
                cnn.parametros.Add("@codigoSiepOrigen", codigoSiepOrigen);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        tram = new TramiteRelocalizacion();
                        tram.idTramiteRel = Convert.ToInt32(row["idTramiteRel"]);
                        tram.numPert = row["numPert"].ToString();
                        tram.tipoRelocalizacion = new ParametroGenerico(Convert.ToInt32(row["idTipoRelGral"]), row["nombreTipoRelGral"].ToString());
                        
                        resp.Add(tram);
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

        public bool EliminarTramiteRelocalizacion(int idTramiteRel)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbTramiteRel";
                cnn.parametros.Add("@idTramiteRel", idTramiteRel);
                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool CambiarTipoTramiteRelocalizacion(int idTramiteRel, int idTipoRelocalizacion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbTipoRelocalizacion";

                cnn.parametros.Add("@idTramiteRel", idTramiteRel);
                cnn.parametros.Add("@idTipoRelocalizacion", idTipoRelocalizacion);

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["idTramiteRel"]);
                if (resul >= 0) return true;

                return false;
             
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }


    }
}
