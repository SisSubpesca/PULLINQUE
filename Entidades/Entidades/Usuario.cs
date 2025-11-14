using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;
using Utilidades;


namespace Datos.Entidades
{
    [Serializable()]
    public class Usuario
    {
        public int    id_usuario { get; set; }
        public string clave { get; set; }
        public string correo { get; set; }
        public int estado { get; set; }
        public int id_grupo { get; set; }
        public string nombre { get; set; }
        public int RUT { get; set; }
        public string usuario { get; set; }
        public string grupo { get; set; }
        public char dvUsuario { get; set; }
        public Region regionUsuario { get; set; }
        //public ParametroGenerico tipoUsuario { get; set; }
        public string apellidos { get; set; }

        public string _estado { get; set; }

        public string nombApelli { get; set; }

        // MÉTODOS (Constructores)
        public Usuario()
        {

            this.id_usuario = 0 ;
            this.clave      = "";
            this.correo     = "";
            this.estado     = 6;
            this.id_grupo   = -1;
            this.nombre     = "";
            this.RUT           = 0;
            this.usuario    = "";
            this.grupo      = "";
            this.dvUsuario = new char();
            this.regionUsuario = null;
            //this.tipoUsuario = null;
            this.apellidos = "";
        
        }


        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_usuario { get; set; }
            public string clave { get; set; }
            public string correo { get; set; }
            public int estado { get; set; }
            public int id_grupo { get; set; }
            public string nombre { get; set; }
            public Hashtable permisos { get; set; }
            public Hashtable accesosRB { get; set; } //Acceso a las secciones y acciones de Rio Blanco 2
            public int RUT { get; set; }
            public char dvUsuario { get; set; }
            public string usuario { get; set; }
            public Region regionUsuario { get; set; }
            //public ParametroGenerico tipoUsuario { get; set; }
            public string apellidos { get; set; }
            public string _estado { get; set; }
        }

    }
}
