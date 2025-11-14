using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;

namespace Datos.Entidades
{
    [Serializable()]
    public class Especies
    {

        public int id_especie { get; set; }
        public GrupoEspecie grupoEspecie { get; set; }
        public string especieNombreComun { get; set; }
        public string especieNombreCientifico { get; set; }
        public int codigoSernapesca { get; set; }
        public string codSernapesca { get; set; }
        public int esExotica { get; set; }
        public int esExperimental { get; set; }
        public GrupoEspecie grupoEspecieAutorizado { get; set; }

        public int _grupo;


         // MÉTODOS (Constructores)
        public Especies()
        {
        }

         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_especie { get; set; }
            public GrupoEspecie grupoEspecie { get; set; }
            public string especieNombreComun { get; set; }
            public string especieNombreCientifico { get; set; }
            public int codigoSernapesca { get; set; }
            public int esExotica { get; set; }
            public int esExperimental { get; set; }
            public string codSernapesca { get; set; }
            public GrupoEspecie grupoEspecieAutorizado { get; set; }
        }



        public int grupo{
            get
            {
                _grupo = grupoEspecie.id_grupoEspecie;
                return _grupo;
            }
        }

        public string nombreGrupo
        {
            get
            {
                if (grupoEspecie != null)
                {
                    return grupoEspecie.grupoEspecie;
                }
                else {
                    return "";
                }
            }
        }

        public string nombreGrupoAutorizado
        {
            get
            {
                if (grupoEspecieAutorizado != null)
                {
                    return grupoEspecieAutorizado.grupoEspecie;
                }
                else {
                    return "";
                }
            }
        }

        public string esExoticaString {
            get {
                if (esExotica == 1)
                {
                    return "Si";
                }else{
                    return "No";
                }
            }
        
        }

        public string esExperimentalString
        {
            get
            {
                if (esExperimental == 1)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }

        }

    }
}
