using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class MatrizSucursal
    {
        public int idMatrizSuc { get; set; }
        public Region region { get; set; }
        public Comuna comunaCasilla { get; set; }
        //public ArchivoBinario archivoBin { get; set; }
        public ArchivoBinarioEspecial archivoBinarioEspecial { get; set; }
        public string direccion { get; set; }
        public string casilla { get; set; }
        public int numeroCI { get; set; }
        public DateTime fechaCI { get; set; }
        public List<Contacto> contactosMatrizSuc { get; set; }
        public bool matriz { get; set; }
        public Persona persona { get; set; }

        public int index { get; set; }
        public int accion { get; set; }
        public int bp { get; set; }


        // MÉTODOS (Constructores)
        public MatrizSucursal()
        {
        }

        
        public int _idArchivoBinario;

        public int idArchivoBinario
        {

            get
            {
                if (archivoBinarioEspecial != null)
                {
                    int idArchivo = archivoBinarioEspecial.idArchivo;

                    _idArchivoBinario = idArchivo;
                    return _idArchivoBinario;
                }
                return 0;

            }
        }
        

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idMatrizSuc { get; set; }
            public Region region { get; set; }
            public Comuna comunaCasilla { get; set; }
            //public ArchivoBinario archivoBin { get; set; }
            public ArchivoBinarioEspecial archivoBinarioEspecial { get; set; }
            public string direccion { get; set; }
            public string casilla { get; set; }
            public int numeroCI { get; set; }
            public DateTime fechaCI { get; set; }
            public List<Contacto> contactosMatrizSuc { get; set; }
            public bool matriz { get; set; }
            public Persona persona { get; set; }
            public int index { get; set; }
            public int bp { get; set; }
            public int accion { get; set; }

        }

    }
}
