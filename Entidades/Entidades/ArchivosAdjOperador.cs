using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ArchivosAdjOperador
    {
        public int rutOperador { get; set; }
        public ArchivoBinarioEspecial archivoBinario { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }

        public int numCI { get; set; }
        public DateTime fechaCI { get; set; }

        public int index { get; set; }
        public int accion { get; set; }
        public int _idArchivo { get; set; }

        
        public int idArchivo
        {

            get
            {
                _idArchivo = archivoBinario.idArchivo;

                return _idArchivo;
            }
        }

        public string fechaCIString
        {
            get
            {
                if (fechaCI != null && !fechaCI.Equals("")) {
                    return fechaCI.ToShortDateString();
                }
                return "";
            }
        }

        // MÉTODOS (Constructores)
        public ArchivosAdjOperador()
        {
        }

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int rutOperador { get; set; }
            public ArchivoBinarioEspecial archivoBinario { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }

            public int numCI { get; set; }
            public DateTime fechaCI { get; set; }

            public int index { get; set; }
            public int accion { get; set; }

            
        }
    }
}
