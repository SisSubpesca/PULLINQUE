using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DatosSolicitudUE
    {
        public Usuario usuario { get; set; }
        public int idDatosSolicitud { get; set; }
        public int idSolConcesion { get; set; }
        public Persona personaVerificacion { get; set; }
        public int numIdentSolicitud { get; set; }
        public int numeroCI { get; set; }
        public DateTime fechaCI{ get; set; }
        public ParametroGenerico cultivoExperimental { get; set; }
        public ParametroGenerico estadoFirmaConvenio { get; set; }
        public ParametroGenerico estadoSeguimientoDia { get; set; }
        public ParametroGenerico tipoNumeroZonal { get; set; }
        public ParametroGenerico ecmpoVista { get; set; }
        public ParametroGenerico amerbVista { get; set; }
        public String codAmerb { get; set; }
        public String codCentro { get; set; }
        public DataExterna amerbSSP { get; set; }
        public DataExterna ecmpoSSP { get; set; }
        public int idDetDato { get; set; }
        public ParametroGenerico tipoCentro { get; set; }
        public ParametroGenerico tipoEvaluacion { get; set; }
        public float superficieSectorAmerb { get; set; }
        public float porcentSectorAmerb { get; set; }
        public float profundidadMin { get; set; }
        public string periodoOperacionCol { get; set; }
        public string observaciones { get; set; }
        public DateTime vigenciaColector { get; set; }
        public int codigo { get; set; }
        public ParametroGenerico oficina { get; set; } //la direccion zonal o central rbDireccionZonal

       // MÉTODOS (Constructores)
        public DatosSolicitudUE()
        { }


        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idDatosSolicitud { get; set; }
            public int idSolConcesion { get; set; }
            public Persona personaVerificacion { get; set; }
            public int numIdentSolicitud { get; set; }
            public int numeroCI { get; set; }
            public DateTime fechaCI { get; set; }
            public ParametroGenerico cultivoExperimental { get; set; }
            public ParametroGenerico estadoFirmaConvenio { get; set; }
            public ParametroGenerico estadoSeguimientoDia { get; set; }
            public ParametroGenerico tipoNumeroZonal { get; set; }
            public ParametroGenerico ecmpoVista { get; set; }
            public ParametroGenerico amerbVista { get; set; }
            public String codAmerb { get; set; }
            public String codCentro { get; set; }
            public DataExterna amerbSSP { get; set; }
            public DataExterna ecmpoSSP { get; set; }
            public int idDetDato { get; set; }
            public ParametroGenerico tipoCentro { get; set; }
            public ParametroGenerico tipoEvaluacion { get; set; }
            public float superficieSectorAmerb { get; set; }
            public float porcentSectorAmerb { get; set; }
            public float profundidadMin { get; set; }
            public string periodoOperacionCol { get; set; }
            public string observaciones { get; set; }
            public DateTime vigenciaColector { get; set; }
            public int codigo { get; set; }
            public ParametroGenerico oficina { get; set; } //la direccion zonal o central rbDireccionZonal
            public Usuario usuario { get; set; }
        }

    }
}
