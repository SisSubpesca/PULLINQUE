using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class UnidadEspacial
    {

        public int idUnidadEspacial { get; set; }
        public int idSolicitud { get; set; }
        public CentrosDeCultivo centrosDeCultivo { get; set; }
        public int numeroDiarioOficial { get; set; }
        public int numeroActaEntrega { get; set; }
        public DateTime fechaActaEntrega { get; set; }
        public DateTime fechaDiarioOficial { get; set; }
        public CapitaniaDePuerto capitaniaDePuerto { get; set; }
        public DateTime fechaInicioPerAut { get; set; }
        public DateTime fechaFinPerAut { get; set; }
        public int mesesAut { get; set; }
        public ParametroGenerico tipoPlazoNominal { get; set; }
        public int numPlazo { get; set; }
        public DateTime plazoInicio { get; set; }
        public DateTime plazoVencimiento { get; set; }
        public int accion { get; set; }
        public Usuario usuario { get; set; }

         // MÉTODOS (Constructores)
        public UnidadEspacial()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idUnidadEspacial { get; set; }
            public CentrosDeCultivo centrosDeCultivo { get; set; }
            public int numeroDiarioOficial { get; set; }
            public int numeroActaEntrega { get; set; }
            public DateTime fechaActaEntrega { get; set; }
            public CapitaniaDePuerto capitaniaDePuerto { get; set; }
            public DateTime fechaDiarioOficial { get; set; }
            public DateTime fechaInicioPerAut { get; set; }
            public DateTime fechaFinPerAut { get; set; }
            public int mesesAut { get; set; }
            public ParametroGenerico tipoPlazoNominal { get; set; }
            public int numPlazo { get; set; }
            public DateTime plazoInicio { get; set; }
            public DateTime plazoVencimiento { get; set; }
            public int accion { get; set; }
            public Usuario usuario { get; set; }
        }
    }
}
