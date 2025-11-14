using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.modificacion;
using System.Collections;

namespace LogicaNegocio.cl.subpesca.rb.servicios.solicitudes
{
    public class AntecedentesSectorService
    {
        Logger logger = new Logger();
        SolicitudDA solicitudDA = new SolicitudDA();
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        PoligonoDA poligonoDA = new PoligonoDA();
        VerticeDA verticeDA = new VerticeDA();
        ArchivoBinarioSolicitudDA archivoBinarioSolicitudDA = new ArchivoBinarioSolicitudDA();
        ObsPestaniaInformeDA obsPestaniaInformeDA = new ObsPestaniaInformeDA();
        EspecieProyTecnicoDA especieProyTecnicoDA = new EspecieProyTecnicoDA();
        AlertaTramiteModConcesionDA alertaTramiteModConcesionDA = new AlertaTramiteModConcesionDA();

        /**
         * Método que guarda la Ubicación Geografica de una Solicitud de Concesión de Acuicultura.
         * 
         */
        public bool guardarUbicacionGeografica(Datos.Entidades.SolicitudConcesion solicitudConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    SolicitudConcesion solicitudConcesionAux = solicitudDA.ObtieneSolicitudConcesion(solicitudConcesion.idSolConcesion, 0);

                    
                    if (solicitudConcesionAux != null)
                    {
                        /* Se eliminan las relaciones de la comuna con la solicitud si existen */
                        foreach (ParametroGenerico comuna in solicitudConcesionAux.comuna)
                        {
                            if (!solicitudDA.EliminarComunaSolicitud(solicitudConcesionAux.idSolConcesion, comuna.id))
                            {
                                return false;
                            }
                        }

                        /* Eliminar la ACS y la ACM */
                        if (!solicitudDA.ActualizaSolicitudConcesion_ACS_ACM(solicitudConcesionAux.idSolConcesion, 0, 0, idUsuario))
                        {
                            return false;
                        }
                    }

                    /* Guarda la Solicitud de Concesión, si ya existe la actualiza */
                    if (!solicitudDA.GuardarSolicitud(solicitudConcesion, idUsuario))
                    {
                        return false;
                    }

                    /* Guarda el listado de comunas relacionadas a la Ubicación Geográfica */
                    foreach (ParametroGenerico comuna in solicitudConcesion.comuna)
                    {
                        if (!solicitudDA.GuardarComunaSolicitud(solicitudConcesion.idSolConcesion, comuna.id))
                        {
                            return false;
                        }
                    }

                    /* Guardar el Cuerpo de Agua */

                    if (solicitudConcesion.cuerpoAgua != null && solicitudConcesion.cuerpoAgua.idCuerpoDeAgua > 0)
                    {
                        if (!solicitudDA.GuardarCuerpoAguaSolicitud(solicitudConcesion.idSolConcesion, solicitudConcesion.cuerpoAgua.idCuerpoDeAgua, idUsuario))
                        {
                            return false;
                        }
                    }
                    else {

                        if (!solicitudDA.GuardarCuerpoAguaSolicitud(solicitudConcesion.idSolConcesion, 0, idUsuario))
                        {
                            return false;
                        }
                    }

                    
                    
                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    if (!solicitudDA.TramiteRecalculaEstados(solicitudConcesion.idSolConcesion))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que guarda el Barrio de una Solicitud de Concesión de Acuicultura.
         * 
         */
        public bool guardarBarrio(Datos.Entidades.SolicitudConcesion solicitudConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Se comenta debido a que la sección de despliegue de las pestañas de Ant. de Terreno y Regularización
                     ahora son controladas en una nueva sección llamada "Administración de Planos" */

                    /* Si las pestañas Ant de Terreno y Regularización no se requieren debe eliminarse sus referencias existentes
                    if (solicitudConcesion != null && (!solicitudConcesion.reqAntecTerreno || !solicitudConcesion.reqRegularizacion))
                    {

                        List<CoordenadaGeografica> listaCoordenadaGeo = coordenadaGeograficaDA.ListarCoordenadaGeografica(solicitudConcesion.idSolConcesion,0);
                        foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadaGeo)
                        {
                            if (coordenadaGeografica.tipoCoordgeografica != null && (coordenadaGeografica.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO || coordenadaGeografica.tipoCoordgeografica.id == rbTipo.REGULARIZACION))
                            {

                                coordenadaGeografica.listaPoligono = poligonoDA.ListarPoligono(coordenadaGeografica.idCoordenadaGeo, 0);
                                
                                foreach (Poligono poligono in coordenadaGeografica.listaPoligono)
                                {

                                    poligono.lista_vertices = verticeDA.ListarVertice(poligono.idPoligono, 0);

                                    /* Eliminar Vértices
                                    foreach (Vertice vertice in poligono.lista_vertices)
                                    {
                                        if (!verticeDA.EliminarVertice(vertice.idVertice, vertice.idPoligono))
                                        {
                                            return false;
                                        }
                                    }

                                    /*Eliminar Tipo Concesion Poligono
                                    if (poligono.tipoConcesion != null && poligono.tipoConcesion.Count > 0)
                                    {
                                         foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                                         {
                                              if (!poligonoDA.EliminarTipoConcesPoligono(poligono.idPoligono, tipoConcesion.id))
                                              {
                                                return false;
                                              }
                                         }
                                    }

                                    /*Eliminar Poligonos
                                    if (!poligonoDA.EliminarPoligono(coordenadaGeografica.idCoordenadaGeo, poligono.idPoligono))
                                    {
                                        return false;
                                    }

                                }

                                /*Eliminar CoordenadasAntesSector
                                if (!coordenadaGeograficaDA.EliminarCoordenadasAntecSector(solicitudConcesion.idSolConcesion, coordenadaGeografica.idCoordenadaGeo))
                                {
                                    return false;
                                }

                                coordenadaGeografica.listaArchivoCoordGeo = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                                /* Eliminar los Archivos de la Coordenada
                                foreach (ArchivoCoordenadaGeo archivoCoordenadaGeo in coordenadaGeografica.listaArchivoCoordGeo)
                                {
                                    if (!coordenadaGeograficaDA.EliminarArchivoCoordenadaGeo(coordenadaGeografica.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo))
                                    {
                                        return false;
                                    }
                                }

                                /*Eliminar CoordenadaGeo
                                if (!coordenadaGeograficaDA.EliminarCoordenadaGeografica(coordenadaGeografica.idCoordenadaGeo))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    */

                    /* Eliminar la Macrozona, ACS, ACM */
                    if (!solicitudDA.ActualizaSolicitudConcesion_ACS_ACM(solicitudConcesion.idSolConcesion, 0, 0, idUsuario))
                    {
                        return false;
                    }

                    /* Se guarda el barrio */
                    if (!solicitudDA.GuardarSolicitud(solicitudConcesion, idUsuario))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }


        /*
        * Método que elimina el Barrio (ACS y ACM) de una Solicitud de Concesión de Acuicultura.
        */
        public bool borrarBarrio(int idSolConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                   
                    /* Eliminar la Macrozona, ACS, ACM */
                    if (!solicitudDA.ActualizaSolicitudConcesion_ACS_ACM(idSolConcesion, 0, 0, idUsuario))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que guarda las Coordenadas Geograficas de una solicitud de concesión.
         * (Antecedentes Espaciales, Antecedentes de Terreno, Regularización)
         */
        public bool guardarCoordenadaGeografica(Datos.Entidades.SolicitudConcesion solicitudConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    foreach (CoordenadaGeografica coordenadaGeografica in solicitudConcesion.coordenadaGeografica)
                    {


                        CoordenadaGeografica coordenadaGeograficaAux = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(solicitudConcesion.idSolConcesion, coordenadaGeografica.idCoordenadaGeo);

                        if (coordenadaGeograficaAux == null)
                        {
                            /* Se guarda la coordenada geografica */
                            if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                            {
                                return false;
                            }

                            /* Se guarda la relacion entre la coordenada geografica y la solicitud */
                            if (!coordenadaGeograficaDA.GuardarCoordenadasAntecSector(solicitudConcesion.idSolConcesion, coordenadaGeografica.idCoordenadaGeo, coordenadaGeografica.tipoCoordgeografica.id, coordenadaGeografica.aplicaBanco, coordenadaGeografica.aplicaVisualizadorDeMapas))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            /* Se guarda la coordenada geografica */
                            if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                            {
                                return false;
                            }

                            /* Se elimina la relacion entre la coordenada geografica y la solicitud */
                            if (!coordenadaGeograficaDA.EliminarCoordenadasAntecSector(solicitudConcesion.idSolConcesion, coordenadaGeografica.idCoordenadaGeo))
                            {
                                return false;
                            }

                            /* Se guarda la relacion entre la coordenada geografica y la solicitud */
                            if (!coordenadaGeograficaDA.GuardarCoordenadasAntecSector(solicitudConcesion.idSolConcesion, coordenadaGeografica.idCoordenadaGeo, coordenadaGeografica.tipoCoordgeografica.id, coordenadaGeografica.aplicaBanco, coordenadaGeografica.aplicaVisualizadorDeMapas))
                            {
                                return false;
                            }
                        }
                    }
                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que guarda el Polígono de una Coordenada Geográfica
         */
        public bool guardarPoligono(Datos.Entidades.Poligono poligono, int idUsuario)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {

                    /* Guardar Poligono */
                    if (!poligonoDA.GuardarPoligono(poligono, idUsuario))
                    {
                        return false;
                    }

                    if (!poligono.cambiaEstado)
                    {
                        /* Guardar Relación con Tipo de Conseciones */
                        if (!poligono.existePoligono || poligono.idPoligono <= 0)
                        {
                            if (poligono.tipoConcesion != null && poligono.tipoConcesion.Count > 0)
                            {
                                foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                                {
                                    if (!poligonoDA.GuardarTipoConcesPoligono(poligono.idPoligono, tipoConcesion.id))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {

                            if (poligono.tipoConcesion != null && poligono.tipoConcesion.Count > 0)
                            {
                                if (!poligonoDA.EliminarTipoConcesPoligono(poligono.idPoligono, 0))
                                {
                                    return false;
                                }

                                foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                                {
                                    if (!poligonoDA.GuardarTipoConcesPoligono(poligono.idPoligono, tipoConcesion.id))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }

                        /* Guardar Vértices (Si existe los modifica) */

                        if (poligono.lista_vertices != null && poligono.lista_vertices.Count > 0)
                        {
                            foreach (Vertice vertice in poligono.lista_vertices)
                            {
                                vertice.idPoligono = poligono.idPoligono;
                                vertice.idSolicitud = poligono.idSolicitud;
                                if (!verticeDA.GuardarVertice(vertice, idUsuario))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    
                    /* Actualiza Areas Totales de la Coordenada Geográfica */
                    if (!actualizaAreasTotalesCoordenadaGeo(poligono, idUsuario))
                    {
                        return false;
                    }

                    SolicitudConcesion solicitudModificacion = solicitudDA.ObtieneSolicitudConcesion(poligono.idSolicitud, 0);
                    if (solicitudModificacion != null && solicitudModificacion.tipoUnidadEspacial != null && solicitudModificacion.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                    {
                        AlertaTramiteModConcesionDA alertaTramiteModConcesionDA = new AlertaTramiteModConcesionDA();

                        if (!alertaTramiteModConcesionDA.EliminarAlertaTramiteModConcesion(0, solicitudModificacion.idSolConcesion))
                        {
                            return false;
                        }

                        //Vertifica Alertas si estas son generadas.
                        List<AlertaTramiteModConcesion> alertas = this.verificarAlertasModificacionConcesionAcuicultura(solicitudModificacion);

                        //Guarda Alertas si estas son generadas.
                        if (alertas != null)
                        {
                            if (!this.GuardarAlertasModificacion(alertas))
                            {
                                return false;
                            }
                        }
                    }

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    if (!solicitudDA.TramiteRecalculaEstados(poligono.idSolicitud))
                    {
                        return false;
                    }


                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        private List<AlertaTramiteModConcesion> verificarAlertasModificacionConcesionAcuicultura(SolicitudConcesion solicitudModificacion)
        {
            List<AlertaTramiteModConcesion> alertaTramiteModConcesionList = new List<AlertaTramiteModConcesion>();
            AlertaTramiteModConcesion alertaTramiteModConcesion = null;

            try
            {
                /* El código de centro debe existir en el sistema */
                if (solicitudModificacion.tramiteModConcesion != null && solicitudModificacion.tramiteModConcesion.centro != null && solicitudModificacion.tramiteModConcesion.centro.id > 0)
                {

                    SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CONCESION, Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), 0);

                    if (solicitudConcesion == null)
                    {
                        alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                        alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                        alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.CENTRO_NO_EXISTE);

                        alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                    }
                }

                /* El rut del titular debe estar asociado al centro */
                if (solicitudModificacion.tramiteModConcesion != null && solicitudModificacion.tramiteModConcesion.titular != null && solicitudModificacion.tramiteModConcesion.titular.rutPersona > 0)
                {
                    SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CONCESION, Convert.ToString(solicitudModificacion.tramiteModConcesion.centro.id), Convert.ToInt32(solicitudModificacion.tramiteModConcesion.titular.rutPersona));

                    if (solicitudConcesion == null)
                    {
                        alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                        alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                        alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.MANEJO_DE_TITULARES);

                        alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                    }
                }

                /* La superficie total final debe ser igual a la superficie total */
                solicitudModificacion = solicitudDA.ObtieneSolicitudConcesionMod(solicitudModificacion.idSolConcesion, 0);

                if (Convert.ToSingle(solicitudModificacion.superficieCalculada) != Convert.ToSingle(solicitudModificacion.superficieTramFinal))
                {
                    alertaTramiteModConcesion = new AlertaTramiteModConcesion();
                    alertaTramiteModConcesion.idSolConcesion = solicitudModificacion.idSolConcesion;
                    alertaTramiteModConcesion.tipoWarning = new ParametroGenerico(rbTipo.DIFERENCIA_DE_HECTAREAS);

                    alertaTramiteModConcesionList.Add(alertaTramiteModConcesion);
                }


                return alertaTramiteModConcesionList;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        private bool GuardarAlertasModificacion(List<AlertaTramiteModConcesion> alertas)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (alertas != null)
                    {
                        foreach (AlertaTramiteModConcesion alertaTramiteModConcesion in alertas)
                        {
                            if (!alertaTramiteModConcesionDA.GuardarAlertaTramiteModConcesion(alertaTramiteModConcesion))
                            {
                                return false;
                            }
                        }
                    }


                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }

            }
        }

        /**
         * Método que actualiza las Areas Totales de una Coordenada Geográfica.
         */
        public bool actualizaAreasTotalesCoordenadaGeo(Poligono poligono, int idUsuario)
        {

            /* Se actualizan las áreas totales de la Coordenada Geográfica que corresponda */
            CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(poligono.idSolicitud, poligono.idCoordenadaGeo);

            /* Si se ingresa un poligono nuevo */
            if (!poligono.existePoligono)
            {
                float areaTotalCalculada = Convert.ToSingle(poligono.areaCalculada) + coordenadaGeografica.areaTotalCalculada;
                float areaTotalSolicitada = Convert.ToSingle(poligono.areaSolicitada) + coordenadaGeografica.areaTotalSolicitada;
                float areaTotalRegularizacion = Convert.ToSingle(poligono.areaRegularizacion) + coordenadaGeografica.areaTotalRegularizacion;

                coordenadaGeografica.areaTotalCalculada = Convert.ToSingle(areaTotalCalculada);
                coordenadaGeografica.areaTotalSolicitada = Convert.ToSingle(areaTotalSolicitada);
                coordenadaGeografica.areaTotalRegularizacion = Convert.ToSingle(areaTotalRegularizacion);

                if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                {
                    return false;
                }

            }
            else
            {

                /* Si Cambia el estado del Poligono */
                if (poligono.cambiaEstado)
                {
                    if (poligono.estado != null && poligono.estado.id == rbEstadosGenerales.VIGENTE)
                    {
                        float areaTotalCalculada = Convert.ToSingle(poligono.areaCalculada) + coordenadaGeografica.areaTotalCalculada;
                        float areaTotalSolicitada = Convert.ToSingle(poligono.areaSolicitada) + coordenadaGeografica.areaTotalSolicitada;
                        float areaTotalRegularizacion = Convert.ToSingle(poligono.areaRegularizacion) + coordenadaGeografica.areaTotalRegularizacion;

                        coordenadaGeografica.areaTotalCalculada = Convert.ToSingle(areaTotalCalculada);
                        coordenadaGeografica.areaTotalSolicitada = Convert.ToSingle(areaTotalSolicitada);
                        coordenadaGeografica.areaTotalRegularizacion = Convert.ToSingle(areaTotalRegularizacion);

                        if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        float areaTotalCalculada = coordenadaGeografica.areaTotalCalculada - Convert.ToSingle(poligono.areaCalculada);
                        float areaTotalSolicitada = coordenadaGeografica.areaTotalSolicitada - Convert.ToSingle(poligono.areaSolicitada);
                        float areaTotalRegularizacion = coordenadaGeografica.areaTotalRegularizacion - Convert.ToSingle(poligono.areaRegularizacion);

                        coordenadaGeografica.areaTotalCalculada = Convert.ToSingle(areaTotalCalculada);
                        coordenadaGeografica.areaTotalSolicitada = Convert.ToSingle(areaTotalSolicitada);
                        coordenadaGeografica.areaTotalRegularizacion = Convert.ToSingle(areaTotalRegularizacion);

                        if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                        {
                            return false;
                        }

                    }
                }

                /* Si se realiza una modificación al Poligono */
                else
                {
                    
                        float areaTotalCalculada = (coordenadaGeografica.areaTotalCalculada - poligono.poligonoOriginal.areaCalculada) + poligono.areaCalculada;
                        float areaTotalSolicitada = (coordenadaGeografica.areaTotalSolicitada - poligono.poligonoOriginal.areaSolicitada) + poligono.areaSolicitada;
                        float areaTotalRegularizacion = (coordenadaGeografica.areaTotalRegularizacion - poligono.poligonoOriginal.areaRegularizacion) + poligono.areaRegularizacion;

                        coordenadaGeografica.areaTotalCalculada = Convert.ToSingle(areaTotalCalculada);
                        coordenadaGeografica.areaTotalSolicitada = Convert.ToSingle(areaTotalSolicitada);
                        coordenadaGeografica.areaTotalRegularizacion = Convert.ToSingle(areaTotalRegularizacion);

                        if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                        {
                            return false;
                        }
                   
                }

            }
            return true;
        }


        /**
         * Método que calcula la Latitud Decimal desde la Latitud en Grados 
         */
        public double calculaLatitudDecimal(int latitudHora, int latitudMinuto, double latitudSegundo)
        {
            try
            {

                double latitudDecimal = latitudHora + latitudMinuto / 60 + latitudSegundo / 3600;

                return Convert.ToDouble(latitudDecimal);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            }
        }

        public double calculaLongitudDecimal(int longitudHora, int longitudMinuto, double longitudSegundo)
        {
            try
            {
                double longitudDecimal = longitudHora + longitudMinuto / 60 + longitudSegundo / 3600;

                return Convert.ToDouble(longitudDecimal);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return 0;
            }
        }

        public List<Poligono> ListarPoligono(int idCoordenadaGeo, int idPoligono)
        {
            try
            {

                return poligonoDA.ListarPoligono(idCoordenadaGeo, idPoligono);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<Poligono> ListarPoligonoInspeccionTerreno(int idCoordenadaGeo, int idPoligono)
        {
            try
            {

                CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(0, idCoordenadaGeo);

                List<Poligono> lista_poligonos = poligonoDA.ListarPoligono(idCoordenadaGeo, idPoligono);

                return poligonoDA.ListarPoligono(idCoordenadaGeo, idPoligono);

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
         * Método que guarda el Archivo Adjunto y su relación con la solicitud en la 
         * pestaña de Ant. Espaciales.
         */
        public bool guardarArchivoAdjuntoCoordenadaGeo(CoordenadaGeografica coordenadaGeografica)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    //GUARDAR EL DOCUMENTO ADJUNTO
                    if (coordenadaGeografica.listaArchivoCoordGeo != null && coordenadaGeografica.listaArchivoCoordGeo.Count > 0)
                    {

                        foreach (ArchivoCoordenadaGeo archivoCoordenadaGeo in coordenadaGeografica.listaArchivoCoordGeo)
                        {
                            if (!archivoCoordenadaGeo.modificaEstado)
                            {
                                if (!archivoBinarioSolicitudDA.GuardarArchivoBinarioSolicitud(archivoCoordenadaGeo.archivoBinario))
                                {
                                    return false;
                                }
                            }
                        }

                        foreach (ArchivoCoordenadaGeo archivoCoordenadaGeo in coordenadaGeografica.listaArchivoCoordGeo)
                        {
                            if (!archivoCoordenadaGeo.modificaEstado)
                            {
                                if (!coordenadaGeograficaDA.GuardarArchivoCoordenadaGeografica(coordenadaGeografica.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo, archivoCoordenadaGeo.tipoDocumento.id, archivoCoordenadaGeo.estado.id))
                                {
                                    return false;
                                }
                            }
                            else
                            {

                                /* Se elimina el registro desde ArchivoCoordenadaGeo */
                                if (!coordenadaGeograficaDA.EliminarArchivoCoordenadaGeo(coordenadaGeografica.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo))
                                {
                                    return false;
                                }

                                /* Se inserta nuevamente en ArchivoCoordenadaGeo */
                                if (!coordenadaGeograficaDA.GuardarArchivoCoordenadaGeografica(coordenadaGeografica.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo, archivoCoordenadaGeo.tipoDocumento.id, archivoCoordenadaGeo.estado.id))
                                {
                                    return false;
                                }

                            }
                        }
                    }
                    transactionScope.Complete();
                    return true;


                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool eliminarArchivoAdjuntoCoordenadaGeo(CoordenadaGeografica coordenadaGeografica)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (coordenadaGeografica.listaArchivoCoordGeo != null && coordenadaGeografica.listaArchivoCoordGeo.Count > 0)
                    {

                        foreach (ArchivoCoordenadaGeo archivoCoordenadaGeo in coordenadaGeografica.listaArchivoCoordGeo)
                        {
                            /* Se debe eliminar la relación de la solicitud con el archivo adjunto */
                            if (!coordenadaGeograficaDA.EliminarArchivoCoordenadaGeo(coordenadaGeografica.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo))
                            {
                                return false;
                            }
                        }
                    }
                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que guarda las observaciones de Antecedentes del Sector.
         */
        public bool guardarObservacionesAntSector(ObsPestaniaInforme obsPestaniaInforme)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    if (!obsPestaniaInformeDA.GuardarObsPestaniaInforme(obsPestaniaInforme))
                    {
                        return false;
                    }


                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que elimina Poligono desde una coordenada geográfica.
         * Se eliminan los vertices del Poligono también.
         * 
         */
        public bool eliminarPoligonoCoordenadaGeo(int idSolicitud, int idCoordenadaGeo, int idPoligono, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Se eliminan los vertices del Poligono */
                    List<Vertice> listaVertice = verticeDA.ListarVertice(idPoligono, 0);
                    foreach (Vertice vertice in listaVertice)
                    {
                        if (!verticeDA.EliminarVertice(vertice.idVertice, idPoligono, idUsuario))
                        {
                            return false;
                        }
                    }

                    Poligono poligono = poligonoDA.ObtienePoligono(idCoordenadaGeo, idPoligono);

                    /* Se elimina la relación con tipo de conseción */
                    foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                    {
                        if (!poligonoDA.EliminarTipoConcesPoligono(idPoligono, tipoConcesion.id))
                        {
                            return false;
                        }
                    }

                    /* Se restan las areas totales del poligono de su referencia geográfica (Solo si el poligono esta vigente) */
                    CoordenadaGeografica coordenadaGeografica = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(idSolicitud, idCoordenadaGeo);
                    if (coordenadaGeografica != null)
                    {
                        coordenadaGeografica.areaTotalCalculada = coordenadaGeografica.areaTotalCalculada - poligono.areaCalculada;
                        coordenadaGeografica.areaTotalSolicitada = coordenadaGeografica.areaTotalSolicitada - poligono.areaSolicitada;
                        coordenadaGeografica.areaTotalRegularizacion = coordenadaGeografica.areaTotalRegularizacion - poligono.areaRegularizacion;

                        if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                        {
                            return false;
                        }
                    }

                    /*Si es Coordenada Geografica Inspeccion de terreno debe eliminarse la relación de su comparación */
                    ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(idSolicitud,idPoligono,0);
                    if (comparacionPoligono != null) {

                        if (!poligonoDA.EliminarComparacionPoligono(idPoligono))
                        {
                            return false;
                        }
                    }

                    /* Se elimina el Poligono de la Coordenada */
                    if (!poligonoDA.EliminarPoligono(idCoordenadaGeo, idPoligono, idSolicitud, idUsuario))
                    {
                        return false;
                    }

                    //RECALCULAR EL ESTADO DE LA SOLICITUD
                    if (!solicitudDA.TramiteRecalculaEstados(idSolicitud))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que guarda la sección del formulario Ant. del Sector que realiza
         * la administración de las Pestañas Ant. de Terreno y Regularización.
         */
        public bool guardarOtraDefinicionGeografica(SolicitudConcesion solicitudConcesion, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    /* Si las pestañas Ant de Terreno y Regularización no se requieren debe eliminarse sus referencias existentes */
                    if (solicitudConcesion != null && (!solicitudConcesion.reqAntecTerreno || !solicitudConcesion.reqRegularizacion))
                    {

                        List<CoordenadaGeografica> listaCoordenadaGeo = coordenadaGeograficaDA.ListarCoordenadaGeografica(solicitudConcesion.idSolConcesion, 0);
                        foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadaGeo)
                        {
                            if (coordenadaGeografica.tipoCoordgeografica != null && ((coordenadaGeografica.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO && !solicitudConcesion.reqAntecTerreno) || (coordenadaGeografica.tipoCoordgeografica.id == rbTipo.REGULARIZACION && !solicitudConcesion.reqRegularizacion)))
                            {

                                coordenadaGeografica.listaPoligono = poligonoDA.ListarPoligono(coordenadaGeografica.idCoordenadaGeo, 0);

                                foreach (Poligono poligono in coordenadaGeografica.listaPoligono)
                                {

                                    poligono.lista_vertices = verticeDA.ListarVertice(poligono.idPoligono, 0);

                                    /* Eliminar Vértices */
                                    foreach (Vertice vertice in poligono.lista_vertices)
                                    {
                                        if (!verticeDA.EliminarVertice(vertice.idVertice, vertice.idPoligono, idUsuario))
                                        {
                                            return false;
                                        }
                                    }

                                    /*Eliminar Tipo Concesion Poligono */
                                    if (poligono.tipoConcesion != null && poligono.tipoConcesion.Count > 0)
                                    {
                                        foreach (ParametroGenerico tipoConcesion in poligono.tipoConcesion)
                                        {
                                            if (!poligonoDA.EliminarTipoConcesPoligono(poligono.idPoligono, tipoConcesion.id))
                                            {
                                                return false;
                                            }
                                        }
                                    }

                                    /*Eliminar Poligonos */
                                    if (!poligonoDA.EliminarPoligono(coordenadaGeografica.idCoordenadaGeo, poligono.idPoligono, solicitudConcesion.idSolConcesion, idUsuario))
                                    {
                                        return false;
                                    }

                                }

                                /*Eliminar CoordenadasAntesSector */
                                if (!coordenadaGeograficaDA.EliminarCoordenadasAntecSector(solicitudConcesion.idSolConcesion, coordenadaGeografica.idCoordenadaGeo))
                                {
                                    return false;
                                }

                                coordenadaGeografica.listaArchivoCoordGeo = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(coordenadaGeografica.idCoordenadaGeo, 0);

                                /* Eliminar los Archivos de la Coordenada */
                                foreach (ArchivoCoordenadaGeo archivoCoordenadaGeo in coordenadaGeografica.listaArchivoCoordGeo)
                                {
                                    if (!coordenadaGeograficaDA.EliminarArchivoCoordenadaGeo(coordenadaGeografica.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo))
                                    {
                                        return false;
                                    }
                                }

                                /*Eliminar CoordenadaGeo*/
                                if (!coordenadaGeograficaDA.EliminarCoordenadaGeografica(coordenadaGeografica.idCoordenadaGeo, idUsuario))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    /* Se guarda el barrio */
                    if (!solicitudDA.GuardarSolicitud(solicitudConcesion, idUsuario))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        public bool guardarCoordGeograficaInspeccionTerreno(CoordenadaGeografica coordenadaGeografica, int idUsuario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    CoordenadaGeografica coordenadaGeograficaAux =  coordenadaGeograficaDA.ObtieneCoordenadaGeografica(coordenadaGeografica.idSolConcesion, coordenadaGeografica.idCoordenadaGeo);
                    
                    /* Guardar la Coordenada Geografica */
                    if (!coordenadaGeograficaDA.GuardarCoordenadaGeografica(coordenadaGeografica, idUsuario))
                    {
                        return false;
                    }

                    /* Guardar el Poligono */
                    foreach (Poligono poligono in coordenadaGeografica.listaPoligono)
                    {
                        poligono.idCoordenadaGeo = coordenadaGeografica.idCoordenadaGeo;
                        poligono.idSolicitud = coordenadaGeografica.idSolConcesion;
                        if (!poligonoDA.GuardarPoligono(poligono, idUsuario))
                        {
                            return false;
                        }

                        /* Guardar Vértices */
                        foreach (Vertice vertice in poligono.lista_vertices)
                        {
                            vertice.idPoligono = poligono.idPoligono;
                            vertice.idSolicitud = poligono.idSolicitud;
                            if (!verticeDA.GuardarVertice(vertice, idUsuario))
                            {
                                return false;
                            }

                            /*Si es Coordenada Geografica Inspeccion de terreno existe debe eliminarse la relación de su comparación*/
                            ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(coordenadaGeografica.idSolConcesion, poligono.idPoligono, 0);
                            if (comparacionPoligono != null)
                            {

                                if (!poligonoDA.EliminarComparacionPoligono(poligono.idPoligono))
                                {
                                    return false;
                                }
                            }

                            /* Se guarda relación con el poligono de antecedentes del sector para la comparación */
                            if (!poligonoDA.GuardarComparacionPoligono(coordenadaGeografica.idSolConcesion,poligono.idPoligAntecSector, poligono.idPoligono, vertice.vertice.id))
                            {
                                return false;
                            }
                        }
                    }

                    if (coordenadaGeograficaAux != null)
                    {
                        /*Si existe debe eliminarse */
                        if (!coordenadaGeograficaDA.EliminarCoordenadasAntecSector(coordenadaGeografica.idSolConcesion, coordenadaGeografica.idCoordenadaGeo))
                        {
                            return false;
                        }
                    }

                    /* Se guarda la relacion entre la coordenada geografica y la solicitud */
                    if (!coordenadaGeograficaDA.GuardarCoordenadasAntecSector(coordenadaGeografica.idSolConcesion, coordenadaGeografica.idCoordenadaGeo, coordenadaGeografica.tipoCoordgeografica.id, coordenadaGeografica.aplicaBanco, coordenadaGeografica.aplicaVisualizadorDeMapas))
                    {
                        return false;
                    }

                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }

            }
        }



        public bool eliminarVertice(int idVertice, int idPoligono, int idUsuario)
        {
            try {

                /* Se elimina el vértice indicado por idVertice y idPoligono */
                if (!verticeDA.EliminarVertice(idVertice, idPoligono, idUsuario))
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
            }
        }

        public bool cambiarEstadpPoligono(Poligono poligono)
        {
            throw new NotImplementedException();
        }

        public bool tieneEspecieSolicitudConcesion(SolicitudConcesion solicitudConcesion, int especie1, int especie2)
        {
            try
            {

                if (especieProyTecnicoDA.GrupoEspecie_enProyTecnico(solicitudConcesion.idSolConcesion, rbTipo.SALMONIDOS) || especieProyTecnicoDA.GrupoEspecie_enProyTecnico(solicitudConcesion.idSolConcesion, rbTipo.MITILIDOS))
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

        public bool tieneEspecieACM(SolicitudConcesion solicitudConcesion, int especie1)
        {
            try
            {

                if (especieProyTecnicoDA.GrupoEspecie_enProyTecnico(solicitudConcesion.idSolConcesion, especie1))
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



        public bool existeInconsistenciaBarrioRegion(int idSolicitud, int regionId)
        {
            try
            {

                SolicitudConcesion solicitudOriginal = solicitudDA.ObtieneSolicitudConcesion(idSolicitud, 0);
                if (solicitudOriginal != null && solicitudOriginal.region != null && solicitudOriginal.region.id != regionId)
                {
                    if ((solicitudOriginal.barrio != null && solicitudOriginal.barrio.id_barrio > 0) || (solicitudOriginal.acm != null && solicitudOriginal.acm.id_barrio > 0))
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

        public bool GuardarPlanilla_Datos(Hashtable Hash, int id, ArchivoBinario _archivoBinario, int idUsuario)
        {
            bool confirmacion = false;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                //realizar el guardado en BBD si algo sale mal no se guardara
                coordenadaGeograficaDA.GuardarArchivoPlanilla(_archivoBinario);

                foreach (SolicitudConcesion Solicitud in Hash.Values)
                {
                    //SolicitudConcesion 
                    //se llama una lista con la coordenadas ya existentes por la solicitud
                    List<CoordenadaGeografica> listaCoordenadaGeo = coordenadaGeograficaDA.ListarCoordenadaGeografica(Solicitud.idSolConcesion, 0);

                    foreach (CoordenadaGeografica _coor in Solicitud.coordenadaGeografica)
                    {
                        //se debe borrar primero antes de almacenar los datos.
                        foreach (CoordenadaGeografica _coordenadageo in listaCoordenadaGeo)
                        {

                            //se borran las coordenadas existentes en caso de que se esten ingresado unas del mismo tipo a través de la plantilla
                            if (_coor.tipoCoordgeografica.id == _coordenadageo.tipoCoordgeografica.id)
                            //if (_coor.tipoCoordgeografica.id == _coordenadageo.tipoCoordgeografica.id && (Solicitud.reqAntecTerreno == true) || _coor.tipoCoordgeografica.id == _coordenadageo.tipoCoordgeografica.id && (Solicitud.reqRegularizacion == true))
                            //if (_coordenadageo.tipoCoordgeografica != null && ((_coordenadageo.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO && !solicitudConcesion.reqAntecTerreno) || (coordenadaGeografica.tipoCoordgeografica.id == rbTipo.REGULARIZACION && !solicitudConcesion.reqRegularizacion)))
                            {
                                _coordenadageo.listaPoligono = poligonoDA.ListarPoligono(_coordenadageo.idCoordenadaGeo, 0);

                                foreach (Poligono _poligono in _coordenadageo.listaPoligono)
                                {
                                    _poligono.lista_vertices = verticeDA.ListarVertice(_poligono.idPoligono, 0);

                                    foreach (Vertice vertice in _poligono.lista_vertices)
                                    {
                                        verticeDA.EliminarVertice(vertice.idVertice, vertice.idPoligono, idUsuario);
                                    }

                                    foreach (ParametroGenerico tipoConcesion in _poligono.tipoConcesion)
                                    {
                                        poligonoDA.EliminarTipoConcesPoligono(_poligono.idPoligono, tipoConcesion.id);
                                    }

                                    poligonoDA.EliminarPoligono(_coordenadageo.idCoordenadaGeo, _poligono.idPoligono, Solicitud.idSolConcesion, idUsuario);
                                }
                                coordenadaGeograficaDA.EliminarCoordenadasAntecSector(Solicitud.idSolConcesion, _coordenadageo.idCoordenadaGeo);

                                _coordenadageo.listaArchivoCoordGeo = coordenadaGeograficaDA.ListarArchivoBinarioCoordenada(_coordenadageo.idCoordenadaGeo, 0);

                                /* Eliminar los Archivos de la Coordenada */
                                foreach (ArchivoCoordenadaGeo archivoCoordenadaGeo in _coordenadageo.listaArchivoCoordGeo)
                                {
                                    if (!coordenadaGeograficaDA.EliminarArchivoCoordenadaGeo(_coordenadageo.idCoordenadaGeo, archivoCoordenadaGeo.archivoBinario.idArchivo))
                                    {
                                        return false;
                                    }
                                }

                                /*Eliminar CoordenadaGeo*/
                                if (!coordenadaGeograficaDA.EliminarCoordenadaGeografica(_coordenadageo.idCoordenadaGeo, idUsuario))
                                {
                                    return false;
                                }
                            }
                        }

                    }
                    //guardamos idPlanilla/idSolicitud
                    coordenadaGeograficaDA.GuardarPlanillaSolicitud(Solicitud.idSolConcesion, _archivoBinario.idArchivo);
                   
                    //revisamos el contenido anterior del tipo de coordenadas existentes dentro de la solicitud
                    SolicitudConcesion _SolicitudAux = solicitudDA.ObtieneSolicitudConcesion(Solicitud.idSolConcesion, idUsuario);

                    //verificamos que los subido por el excel contenga los mismos tipos de coordenadas
                    // si no es asi agregar las faltantes para no borrarlas
                    if (Solicitud.reqAntecTerreno == false && _SolicitudAux.reqAntecTerreno == true)
                    {
                        Solicitud.reqAntecTerreno = true;
                    }
                    if (Solicitud.reqRegularizacion == false && _SolicitudAux.reqRegularizacion == true)
                    {
                        Solicitud.reqRegularizacion = true;
                    }
                    //se guarda los cambios de los tipos de coordenadas de la solicitud
                    if (!solicitudDA.GuardarSolicitud(Solicitud, idUsuario))
                    {
                        return false;
                    }
                    
                    confirmacion = this.guardarCoordenadaGeografica(Solicitud, idUsuario);
                    if (confirmacion)
                    {
                        List<CoordenadaGeografica> Cor = new List<CoordenadaGeografica>();
                        Cor = Solicitud.coordenadaGeografica;
                        foreach (CoordenadaGeografica CoordenadaGeo in Cor)
                        {
                            List<Poligono> ListPol = new List<Poligono>();
                            ListPol = CoordenadaGeo.listaPoligono;
                            foreach (Poligono Pol in ListPol)
                            {
                                Pol.idCoordenadaGeo = CoordenadaGeo.idCoordenadaGeo;
                                confirmacion = this.guardarPoligono(Pol, idUsuario);
                                if (!confirmacion)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                if (!confirmacion)
                {
                    return false;
                }
                    transactionScope.Complete();
                    return true;
            }
        }

    }
}
