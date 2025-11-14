using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ArchivoCoordenadaGeo
    {

        public int idCoordGeo { get; set; }
        public ArchivoBinario archivoBinario { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public ParametroGenerico estado { get; set; }
        public bool modificaEstado { get; set; }

        public int _idArchivoBinario;

        public ArchivoCoordenadaGeo() { 
        }

        public int idArchivoBinario
        {

            get
            {
                int idArchivo = archivoBinario.idArchivo;
                
                _idArchivoBinario = idArchivo;
                return _idArchivoBinario;
            }
        }

         //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idCoordGeo { get; set; }
            public ArchivoBinario archivoBinario { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public ParametroGenerico estado { get; set; }
        }
    }
}
