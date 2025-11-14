using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Resolucion
{


    [Serializable()]
    public class ResolucionValidacion
    {

        public int idEquivalencia { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public ParametroGenerico origen { get; set; }
        public ParametroGenerico materia { get; set; }
        public ParametroGenerico subRequerimiento { get; set; }
        public ParametroGenerico estadoMateria { get; set; }
        public ParametroGenerico estadoSubRequerimiento { get; set; }

        public int numero { get; set; }
        public int fecha { get; set; }
        public int numeroCI { get; set; }
        public int fechaCI { get; set; }
        public int vincular { get; set; }

        public string tipoDocumentoString {
            get {
                if (tipoDocumento != null)
                {
                    return tipoDocumento.descripcion;
                }
                return "";
            }
        }

        public string origenString
        {
            get
            {
                if (origen != null)
                {
                    return origen.descripcion;
                }
                return "";
            }
        }

        public string materiaString
        {
            get
            {
                if (materia != null)
                {
                    return materia.descripcion;
                }
                return "";
            }
        }

        public string subRequerimientoString
        {
            get
            {
                if (subRequerimiento != null)
                {
                    return subRequerimiento.descripcion;
                }
                return "";
            }
        }

        public string estadoMateriaString
        {
            get
            {
                if (estadoMateria != null)
                {
                    return estadoMateria.descripcion;
                }
                return "";
            }
        }

        public string estadoSubRequerimientoString
        {
            get
            {
                if (estadoSubRequerimiento != null)
                {
                    return estadoSubRequerimiento.descripcion;
                }
                return "";
            }
        }


        public string numeroString
        {

            get
            {
                if (numero > 0)
                {
                    return "Campo Aplica";
                }
                else
                {
                    return "Campo No Aplica";
                }
            }
        }

        public string fechaString
        {

            get
            {
                if (fecha > 0)
                {
                    return "Campo Aplica";
                }
                else
                {
                    return "Campo No Aplica";
                }
            }
        }

        public string numeroCIString
        {

            get
            {
                if (numeroCI > 0)
                {
                    return "Campo Aplica";
                }
                else
                {
                    return "Campo No Aplica";
                }
            }
        }


        public string fechaCIString
        {

            get
            {
                if (fechaCI > 0)
                {
                    return "Campo Aplica";
                }
                else
                {
                    return "Campo No Aplica";
                }
            }
        }

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idEquivalencia { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public ParametroGenerico origen { get; set; }
            public ParametroGenerico materia { get; set; }
            public ParametroGenerico subRequerimiento { get; set; }
            public ParametroGenerico estadoMateria { get; set; }
            public ParametroGenerico estadoSubRequerimiento { get; set; }

            public int numero { get; set; }
            public int fecha { get; set; }
            public int numeroCI { get; set; }
            public int fechaCI { get; set; }
            public int vincular { get; set; }


        }

    }
}
