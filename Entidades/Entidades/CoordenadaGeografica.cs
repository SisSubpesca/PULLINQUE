using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class CoordenadaGeografica
    {
        public Usuario usuario { get; set; }
        public int idCoordenadaGeo { get; set; }
        public int idSolConcesion { get; set; }
        public ArchivoBinario archivoBinario { get; set; }
        public List<ArchivoBinario> listaArchivoBinario { get; set; }
        public ParametroGenerico estado { get; set; }
        public ParametroGenerico tipoHuso { get; set; }
        public ParametroGenerico datum { get; set; }
        public Carta carta { get; set; }
        public float areaTotalCalculada { get; set; }
        public float areaTotalSolicitada { get; set; }
        public float areaTotalRegularizacion { get; set; }
        public ParametroGenerico tipoCoordgeografica { get; set; }
        public List<ArchivoCoordenadaGeo> listaArchivoCoordGeo { get; set; }
        public bool aplicaBanco { get; set; }
        public bool aplicaVisualizadorDeMapas { get; set; }

        public List<Poligono> listaPoligono { get; set; }

        // MÉTODOS (Constructores)
        public CoordenadaGeografica()
        { }


        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int idCoordenadaGeo { get; set; }
            public int idSolConcesion { get; set; }
            public ArchivoBinario archivoBinario { get; set; }
            public List<ArchivoBinario> listaArchivoBinario { get; set; }
            public ParametroGenerico estado { get; set; }
            public ParametroGenerico tipoHuso { get; set; }
            public ParametroGenerico datum { get; set; }
            public Carta carta { get; set; }
            public float areaTotalCalculada { get; set; }
            public float areaTotalSolicitada { get; set; }
            public float areaTotalRegularizacion { get; set; }
            public ParametroGenerico tipoCoordgeografica { get; set; }
            public List<ArchivoCoordenadaGeo> listaArchivoCoordGeo { get; set; }
            public bool aplicaBanco { get; set; }
            public bool aplicaVisualizadorDeMapas { get; set; }

            public List<Poligono> listaPoligono { get; set; }

        }

        
    }
}
