using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Contantes;

namespace Validaciones.cl.subpesca.rb.modificacion
{
    public class AntecedDelSectorValidacion
    {
        CoordenadaGeograficaDA coordenadaGeograficaDA = new CoordenadaGeograficaDA();
        VerticeDA verticeDA = new VerticeDA();
        PoligonoDA poligonoDA = new PoligonoDA();

        /**
         *  Método que valida la Sección  Ubicación Geográfica.
         */
        public List<String> validaUbicacionGeografica(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresUbicacionGeografica = new List<String>();
            if (solicitudConcesion != null)
            {


            }
            return listaErrroresUbicacionGeografica;
        }

        /**
         * Método que valida la Sección Barrio.
         */
        public List<String> validaBarrio(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresBarrio = new List<String>();
            if (solicitudConcesion != null)
            {


            }
            return listaErrroresBarrio;
        }

        /**
         * Método que valida la Sección Archivo Adjunto en Ant. Espaciales.
         */
        public List<String> validaArchivoAdjuntoAntEspaciales(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            return null;
        }

        /**
         * Método que valida la Sección Polígono en Ant. Espaciales.
         */
        public List<String> validaPoligonoAntEspaciales(Datos.Entidades.Poligono poligono)
        {
            List<String> listaErrroresPoligono = new List<String>();
            if (poligono != null)
            {

                /* Si es una eliminación de un Poligono, no puede ser eliminado si está siendo utilizado en la comparación con coordendas de inspeccion de terreno */
                if (poligono.accion == accion.ELIMINAR || poligono.cambiaEstado || poligono.idPoligono > 0)
                {
                    ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(poligono.idSolicitud, 0, poligono.idPoligono);

                    if (comparacionPoligono != null)
                    {
                        if (poligono.accion == accion.ELIMINAR)
                        {
                            listaErrroresPoligono.Add("El Polígono no puede ser eliminado si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");
                        }
                        else if (poligono.cambiaEstado)
                        {
                            listaErrroresPoligono.Add("El Polígono no puede cambiar de estado si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");

                        }
                        else
                        {
                            listaErrroresPoligono.Add("El Polígono no se puede modificar si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");
                        }
                    }
                }

                else
                {

                    /* Los poligonos deben ser guardados solo si se creó previamente la Referencia Geográfica */
                    if (poligono.idCoordenadaGeo <= 0)
                    {
                        listaErrroresPoligono.Add("Debe guardar la Referencia Geográfica antes de guardar el Polígono");
                    }

                    /* Los polígonos deben ser guardados solo si se guardó al menos 3 vértice */
                    if (poligono.lista_vertices == null || (poligono.lista_vertices != null && poligono.lista_vertices.Count < 3))
                    {
                        listaErrroresPoligono.Add("El Polígono debe tener al menos 3 vértices");
                    }
                }
            }

            return listaErrroresPoligono;
        }

        public List<string> validaPoligonoRegularizacion(Poligono poligono)
        {
            List<String> listaErrroresPoligono = new List<String>();
            if (poligono != null)
            {
                /* Si es una eliminación de un Poligono, no puede ser eliminado si está siendo utilizado en la comparación con coordendas de inspeccion de terreno */
                if (poligono.accion == accion.ELIMINAR || poligono.cambiaEstado)
                {
                    ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(poligono.idSolicitud, 0, poligono.idPoligono);

                    if (comparacionPoligono != null)
                    {
                        if (poligono.accion == accion.ELIMINAR)
                        {
                            listaErrroresPoligono.Add("El Polígono no puede ser eliminado si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");
                        }
                        else if (poligono.cambiaEstado)
                        {
                            listaErrroresPoligono.Add("El Polígono no puede cambiar de estado si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");

                        }
                        else
                        {
                            listaErrroresPoligono.Add("El Polígono no se puede modificar si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");
                        }
                    }
                }

                else
                {
                    /* Los poligonos deben ser guardados solo si se creó previamente la Referencia Geográfica */
                    if (poligono.idCoordenadaGeo <= 0)
                    {
                        listaErrroresPoligono.Add("Debe guardar la Referencia Geográfica antes de guardar el Polígono");
                    }

                    /* Los polígonos deben ser guardados solo si se guardó al menos 3 vértice */
                    if (poligono.lista_vertices == null || (poligono.lista_vertices != null && poligono.lista_vertices.Count < 3))
                    {
                        listaErrroresPoligono.Add("El Polígono debe tener al menos 3 vértices");
                    }
                }
            }

            return listaErrroresPoligono;
        }

        public List<string> validaPoligonoAntTerreno(Poligono poligono)
        {
            List<String> listaErrroresPoligono = new List<String>();
            if (poligono != null)
            {

                /* Si es una eliminación de un Poligono, no puede ser eliminado si está siendo utilizado en la comparación con coordendas de inspeccion de terreno */
                if (poligono.accion == accion.ELIMINAR || poligono.cambiaEstado)
                {
                    ComparacionPoligono comparacionPoligono = poligonoDA.obtenerComparacionPoligono(poligono.idSolicitud, 0, poligono.idPoligono);

                    if (comparacionPoligono != null)
                    {
                        if (poligono.accion == accion.ELIMINAR)
                        {
                            listaErrroresPoligono.Add("El Polígono no puede ser eliminado si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");
                        }
                        else if (poligono.cambiaEstado)
                        {
                            listaErrroresPoligono.Add("El Polígono no puede cambiar de estado si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");

                        }
                        else
                        {
                            listaErrroresPoligono.Add("El Polígono no se puede modificar si está siendo utilizado en la comparación con Coordendas de Inspeccion de Terreno");
                        }
                    }
                }

                else
                {

                    /* Los poligonos deben ser guardados solo si se creó previamente la Referencia Geográfica */
                    if (poligono.idCoordenadaGeo <= 0)
                    {
                        listaErrroresPoligono.Add("Debe guardar la Referencia Geográfica antes de guardar el Polígono");
                    }

                    /* Los polígonos deben ser guardados solo si se guardó al menos 3 vértice */
                    if (poligono.lista_vertices == null || (poligono.lista_vertices != null && poligono.lista_vertices.Count < 3))
                    {
                        listaErrroresPoligono.Add("El Polígono debe tener al menos 3 vértices");
                    }
                }
            }

            return listaErrroresPoligono;
        }


        /**
         * Método que valida la Sección Referencia Geografica en Ant. Espaciales.
         */
        public List<string> validaArchivoAdjuntoAntEspaciales(CoordenadaGeografica coordenadaGeografica)
        {
            List<String> listaErrroresArchivo = new List<String>();
            if (coordenadaGeografica != null)
            {
                if (coordenadaGeografica.idCoordenadaGeo <= 0)
                {
                    listaErrroresArchivo.Add("Debe guardar la Referencia Geográfica antes de guardar el Archivo");
                }
            }
            return listaErrroresArchivo;
        }

        /**
         * Método que valida la Sección Referencia Geografica en Ant. Terreno.
         */
        public List<string> validaArchivoAdjuntoAntTerreno(CoordenadaGeografica coordenadaGeografica)
        {
            List<String> listaErrroresArchivo = new List<String>();
            if (coordenadaGeografica != null)
            {
                if (coordenadaGeografica.idCoordenadaGeo <= 0)
                {
                    listaErrroresArchivo.Add("Debe guardar la Referencia Geográfica antes de guardar el Archivo");
                }
            }
            return listaErrroresArchivo;
        }

        /**
         * Método que valida la Sección Referencia Geografica en Regularización.
         */
        public List<string> validaArchivoAdjuntoRegularizacion(CoordenadaGeografica coordenadaGeografica)
        {
            List<String> listaErrroresArchivo = new List<String>();
            if (coordenadaGeografica != null)
            {
                if (coordenadaGeografica.idCoordenadaGeo <= 0)
                {
                    listaErrroresArchivo.Add("Debe guardar la Referencia Geográfica antes de guardar el Archivo");
                }
            }
            return listaErrroresArchivo;
        }

        /*
         * Método que valida los vertices de un poligono.
         */
        public List<string> validaVertice(Vertice vertice, List<Vertice> List_Vertices)
        {
            List<String> listaErrroresVertice = new List<String>();
            if (vertice != null)
            {
                /* No se pueden ingresar vértices con las mismas letras */

                foreach (Vertice verticeAux in List_Vertices)
                {
                    if (vertice.vertice != null && verticeAux.vertice != null && vertice.idVertice <= 0 && vertice.vertice.id == verticeAux.vertice.id)
                    {
                        listaErrroresVertice.Add("Ya existe un vértice " + vertice.vertice.descripcion + " ingresado en el sistema.");
                    }
                }
            }
            return listaErrroresVertice;
        }

        /**
         * Método que valida la Referencias Geograficas de una coordenada. 
         */
        public List<string> validaReferenciasGeograficas(SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresReferenciasGeograficas = new List<String>();
            if (solicitudConcesion != null)
            {

                if (solicitudConcesion.coordenadaGeografica != null && solicitudConcesion.coordenadaGeografica[0].idCoordenadaGeo > 0)
                {
                    List<CoordenadaGeografica> listCoordenadasGeo = coordenadaGeograficaDA.ListarCoordenadaGeografica(solicitudConcesion.idSolConcesion, 0);
                    foreach (CoordenadaGeografica coordenadaGeografica in listCoordenadasGeo)
                    {
                        if (coordenadaGeografica.aplicaBanco && solicitudConcesion.coordenadaGeografica[0] != null && solicitudConcesion.coordenadaGeografica[0].aplicaBanco &&
                            coordenadaGeografica.idCoordenadaGeo != solicitudConcesion.coordenadaGeografica[0].idCoordenadaGeo)
                        {

                            listaErrroresReferenciasGeograficas.Add("Ya existe una Coordenada Geográfica que es utilizada para Banco");
                            break;
                        }

                        if (coordenadaGeografica.aplicaVisualizadorDeMapas && solicitudConcesion.coordenadaGeografica[0] != null && solicitudConcesion.coordenadaGeografica[0].aplicaVisualizadorDeMapas &&
                            coordenadaGeografica.idCoordenadaGeo != solicitudConcesion.coordenadaGeografica[0].idCoordenadaGeo)
                        {

                            listaErrroresReferenciasGeograficas.Add("Ya existe una Coordenada Geográfica que es utilizada para Visualizador de Mapas");
                            break;
                        }
                    }

                    /* No puede modificar una Referencia Geografica si esta ha sido utilizada para la comparación de poligono de inspeccion de terreno */
                    CoordenadaGeografica coordenedaGeograficaAnterior = coordenadaGeograficaDA.ObtieneCoordenadaGeografica(solicitudConcesion.idSolConcesion, solicitudConcesion.coordenadaGeografica[0].idCoordenadaGeo);
                    bool tienePoligonoComparado = poligonoDA.EsCoordenadaUsadaCompPoligono(solicitudConcesion.coordenadaGeografica[0].idCoordenadaGeo);

                    if (coordenedaGeograficaAnterior.aplicaBanco && !solicitudConcesion.coordenadaGeografica[0].aplicaBanco && tienePoligonoComparado)
                    {
                        listaErrroresReferenciasGeograficas.Add("No es posible modificar una Coordenada Geográfica cuyos Polígonos están siendo utilizados en la comparación con Inspección de Terreno");
                    }


                }
                else
                {
                    bool resp = coordenadaGeograficaDA.ObtieneCoordSectorAplicaBanco(solicitudConcesion.idSolConcesion);
                    if (resp && solicitudConcesion.coordenadaGeografica[0] != null && solicitudConcesion.coordenadaGeografica[0].aplicaBanco)
                    {
                        listaErrroresReferenciasGeograficas.Add("Ya existe una Coordenada Geográfica que es utilizada para Banco");
                    }

                    resp = coordenadaGeograficaDA.ObtieneCoordSectorAplicaVizMapa(solicitudConcesion.idSolConcesion);
                    if (resp && solicitudConcesion.coordenadaGeografica[0] != null && solicitudConcesion.coordenadaGeografica[0].aplicaVisualizadorDeMapas)
                    {
                        listaErrroresReferenciasGeograficas.Add("Ya existe una Coordenada Geográfica que es utilizada para Visualizador de Mapas");
                    }
                }


            }
            return listaErrroresReferenciasGeograficas;
        }

        public List<string> validaOtraDefinicionGeografica(SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErroresOtraDefinicionGeografica = new List<String>();
            if (solicitudConcesion != null)
            {
                List<CoordenadaGeografica> listaCoordenadas = coordenadaGeograficaDA.ListarCoordenadaGeografica(solicitudConcesion.idSolConcesion, 0);

                if (!solicitudConcesion.reqAntecTerreno)
                {
                    foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadas)
                    {
                        if (coordenadaGeografica != null && coordenadaGeografica.tipoCoordgeografica != null && coordenadaGeografica.tipoCoordgeografica.id == rbTipo.ANTECEDENTES_TERRENO)
                        {
                            bool esCoordenadaUsadaCompPoligono = poligonoDA.EsCoordenadaUsadaCompPoligono(coordenadaGeografica.idCoordenadaGeo);
                            if (esCoordenadaUsadaCompPoligono)
                            {
                                listaErroresOtraDefinicionGeografica.Add("Debe usar Coordenadas de Entrega de Material porque sus polígonos están siendo utilizados en la comparación con Inspección de Terreno");
                            }
                        }
                    }
                }

                if (!solicitudConcesion.reqRegularizacion)
                {
                    foreach (CoordenadaGeografica coordenadaGeografica in listaCoordenadas)
                    {
                        if (coordenadaGeografica != null && coordenadaGeografica.tipoCoordgeografica != null && coordenadaGeografica.tipoCoordgeografica.id == rbTipo.REGULARIZACION)
                        {
                            bool esCoordenadaUsadaCompPoligono = poligonoDA.EsCoordenadaUsadaCompPoligono(coordenadaGeografica.idCoordenadaGeo);
                            if (esCoordenadaUsadaCompPoligono)
                            {
                                listaErroresOtraDefinicionGeografica.Add("Debe usar Coordenadas de Regularización porque sus polígonos están siendo utilizados en la comparación con Inspección de Terreno");
                            }
                        }
                    }
                }
            }
            return listaErroresOtraDefinicionGeografica;
        }


        public List<string> validaPoligonoInspeccionTerreno(Poligono poligono)
        {
            List<String> listaErroresPoligonoInsTerreno = new List<String>();
            if (poligono != null)
            {
                /* Los polígonos deben ser guardados solo si se guardó al menos 3 vértice */
                if (poligono.lista_vertices == null || (poligono.lista_vertices != null && poligono.lista_vertices.Count < 3))
                {
                    listaErroresPoligonoInsTerreno.Add("El Polígono debe tener al menos 3 vértices");
                }
            }
            return listaErroresPoligonoInsTerreno;
        }
    }
}
