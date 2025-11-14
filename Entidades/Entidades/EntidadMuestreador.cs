using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades

//CLASE APLICA PARA ENTIDAD ANALISIS Y MUESTREADOR AMBIENTAL
{
    [Serializable()]
    public class EntidadMuestreador
    {
        public int numEntidad { get; set; }
        public String razonSocial { get; set; }
        public String repLegal { get; set; }
        public String domicilio { get; set; }
        public String categoria { get; set; }
        public int numero { get; set; }
        public DateTime fecha  { get; set; }
        public DateTime  inicioVigencia { get; set; }
        public DateTime finVigencia  { get; set; }
        public String condVigencia  { get; set; }
        public String correo { get; set; }
        public String fono { get; set; }


        public string fechaString
        {
            get
            {
                if (fecha != null && !fecha.Equals(""))
                {
                    return fecha.ToShortDateString();
                }
                return "";
            }

        }

        public string inicioVigenciaString
        {
            get
            {
                if (inicioVigencia != null && !inicioVigencia.Equals(""))
                {
                    return inicioVigencia.ToShortDateString();
                }
                return "";
            }

        }

        public string finVigenciaString
        {
            get
            {
                if (finVigencia != null && !finVigencia.Equals("") && finVigencia != default(DateTime))
                {
                    return finVigencia.ToShortDateString();
                }
                return "";
            }

        }

        // MÉTODOS (Constructores)
        public EntidadMuestreador()
        {
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int numEntidad { get; set; }
            public String razonSocial { get; set; }
            public String repLegal { get; set; }
            public String domicilio { get; set; }
            public String categoria { get; set; }
            public int numero { get; set; }
            public DateTime fecha { get; set; }
            public DateTime inicioVigencia { get; set; }
            public DateTime finVigencia { get; set; }
            public String condVigencia { get; set; }
            public String correo { get; set; }
            public String fono { get; set; }
        
        }

    }
}
