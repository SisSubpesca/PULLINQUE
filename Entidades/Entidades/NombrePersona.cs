using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class NombrePersona
    {
        public int idNomPersona { get; set; }
        public int rut { get; set; }
        public ArchivoBinarioEspecial archivo { get; set; }
        public ParametroGenerico estadoActual { get; set; }
        public string nombre { get; set; }
        public int numeroCI { get; set; }
        public DateTime fechaCI { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        
        public int index { get; set; }

        
        public NombrePersona() { }
        
        public int _idArchivoBinario;

        public int idArchivoBinario
        {

            get
            {
                if (archivo != null)
                {
                    int idArchivo = archivo.idArchivo;

                    _idArchivoBinario = idArchivo;
                    return _idArchivoBinario;
                }
                return 0;
            }
        }


        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idNomPersona { get; set; }
            public int rut { get; set; }
            public ArchivoBinarioEspecial archivo { get; set; }
            public ParametroGenerico estadoActual { get; set; }
            public string nombre { get; set; }
            public int numeroCI { get; set; }
            public DateTime fechaCI { get; set; }
            public int index { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
         
        }
    }
}
