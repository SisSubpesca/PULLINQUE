using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades.Relocalizacion
{

    [Serializable()]
    public class TramiteRelocalizacion
    {

        public int idTramiteRel { get; set; }
        public ParametroGenerico estadoTramite { get; set; }
        public String numPert { get; set; }
        public DateTime fechaRecepcion { get; set; }
        public DateTime fechaIngresoTramite { get; set; }
        public DateTime fechaTramiteFiltroIni { get; set; }
        public DateTime fechaTramiteFiltroFin { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public List<DetalleSector> sectores { get; set; }
        public Persona titularFiltro { get; set; }
        public ParametroGenerico centroOrigenFiltro { get; set; }
        public ParametroGenerico centroDestinoFiltro { get; set; }
        public Region regionOrigen { get; set; }
        public Region regionDestino { get; set; }
        public int pagina { get; set; }
        public int columnas { get; set; }
        public bool despliegaErrores { get; set; }
        public bool despliegaAlertas { get; set; }
        public bool despliegaModificacion { get; set; }
        public bool despliegaRedefinicion { get; set; }
        public int puedeRedefinir { get; set; }
        public bool enTram { get; set; }
        public int cantSSP { get; set; }
        public ParametroGenerico tipoRelocalizacion { get; set; }
        public ParametroGenerico estadoFiltro { get; set; }
        public String descripcion { get; set; }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idTramiteRel { get; set; }
            public ParametroGenerico estadoTramite { get; set; }
            public String numPert { get; set; }
            public DateTime fechaRecepcion { get; set; }
            public DateTime fechaIngresoTramite { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public List<DetalleSector> sectores { get; set; }
            public DateTime fechaTramiteFiltroIni { get; set; }
            public DateTime fechaTramiteFiltroFin { get; set; }
            public Persona titularFiltro { get; set; }
            public ParametroGenerico centroOrigenFiltro { get; set; }
            public ParametroGenerico centroDestinoFiltro { get; set; }
            public Region regionOrigen { get; set; }
            public Region regionDestino { get; set; }
            public int pagina { get; set; }
            public int columnas { get; set; }
            public bool despliegaErrores { get; set; }
            public bool despliegaAlertas { get; set; }
            public bool despliegaModificacion { get; set; }
            public bool despliegaRedefinicion { get; set; }
            public int puedeRedefinir { get; set; }
            public bool enTram { get; set; }
            public int cantSSP { get; set; }
            public ParametroGenerico tipoRelocalizacion { get; set; }
            public ParametroGenerico estadoFiltro { get; set; }
            public String descripcion { get; set; }
        }

    }
}
