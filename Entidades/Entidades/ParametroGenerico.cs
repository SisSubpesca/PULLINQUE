using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class ParametroGenerico : IComparable<ParametroGenerico>
    {

        public int id  { get; set; }
        public string descripcion { get; set; }
        public string clave { get; set; }


        public ParametroGenerico(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }

        public ParametroGenerico(string clave)
        {
            this.clave = clave;
        }

        public ParametroGenerico(int id)
        {
            this.id = id;
        }

        public ParametroGenerico(int id, string descripcion, string clave)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.clave = clave;
        }

        public ParametroGenerico(string clave, string descripcion)
        {
            this.clave = clave;
            this.descripcion = descripcion;
            
        }


        public int CompareTo(ParametroGenerico obj)
        {
            int comp = this.descripcion.CompareTo(obj.descripcion);
            if (comp == 0)
                return this.descripcion.CompareTo(obj.descripcion);
            else
                return comp;
        }

          // MÉTODOS (Constructores)
        public ParametroGenerico()
        { }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int id { get; set; }
            public String descripcion { get; set; }
            public string clave { get; set; }

        }
    }


    
}
