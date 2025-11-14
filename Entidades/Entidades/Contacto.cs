using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Contacto
    {
        public int idContacto { get; set; }
        public int rut { get; set; }//puede ser rut titular, operador o representante, sin guión
        public ParametroGenerico tipoContacto { get; set; }
        public Region region { get; set; }
        public string valorContacto { get; set; }
        public bool accesoPublico { get; set; }
        public string detalle { get; set; }
        public bool matriz { get; set; }
        //public List<ArchivoBinario> archivosContacto { get; set; }
        public List<ArchivoBinarioEspecial> archivosContacto { get; set; }

        public int index { get; set; }
        public int accion { get; set; }

        // MÉTODOS (Constructores)
        public Contacto()
        {
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idContacto { get; set; }
            public int rut { get; set; }//puede ser rut titular, operador o representante, sin guión
            public ParametroGenerico tipoContacto { get; set; }
            public Region region { get; set; }
            public string valorContacto { get; set; }
            public bool accesoPublico { get; set; }
            public string detalle { get; set; }
            public bool matriz { get; set; }
            //public List<ArchivoBinario> archivosContacto { get; set; }
            public List<ArchivoBinarioEspecial> archivosContacto { get; set; }
            public int index { get; set; }
            public int accion { get; set; }
        }
    }
}
