using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class VisacionMasiva
    {

        public int idSolConcesion { get; set; }
        public int idDocGeneral { get; set; }
        public int idDocPestana { get; set; }
        public bool corrige { get; set; } //indica si el usuario mando a corregir
        public ParametroGenerico requerimiento { get; set; } //Es el requerimiento que esta abierto y se desea cerrar (ya sea con el requerimiento siguiente o con la respuesta en caso de ser una firma)
        public Usuario.Serializable usuario { get; set; }

        public int pagina { get; set; }
        public ParametroGenerico tipoTramite { get; set; }

        public String subTipoTramite { get; set; }

        public ParametroGenerico tipoVisacion { get; set; }
        public ParametroGenerico resultado { get; set; }
        public ParametroGenerico region { get; set; }
        public ParametroGenerico provincia { get; set; }
        public ParametroGenerico estado { get; set; }
        public String pert { get; set; }
        public String comunas { get; set; }

        public ParametroGenerico comunaFiltro { get; set; }
        public String pertFiltro { get; set; }

        public int buscaVisaOFirma { get; set; } // indica si se debe buscar visaciones-> 1  o firmas ->2

        public bool esFirmaJefatura { get; set; }

    }
}
