using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ArchivoBinarioEspecial
    {
        public int idArchivo { get; set; }
        public byte[] bytes { get; set; }
        public String nombreFisico { get; set; }
        public String nombreArchivo { get; set; }
        public double tamano { get; set; }
        public String formato { get; set; }
        public String observaciones { get; set; }
        public ParametroGenerico tipoArchivo { get; set; }


        private int _index;
        private int _accion;

        // MÉTODOS (Constructores)
        public ArchivoBinarioEspecial()
        { }
        
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }

        public int accion
        {
            get { return _accion; }
            set { _accion = value; }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idArchivo { get; set; }
            public byte[] bytes { get; set; }
            public String nombreFisico { get; set; }
            public String nombreArchivo { get; set; }
            public double tamano { get; set; }
            public String formato { get; set; }
            public String observaciones { get; set; }
            public ParametroGenerico tipoArchivo { get; set; }

        }
    }
}
