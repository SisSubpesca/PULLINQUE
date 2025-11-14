using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    public class ProyectoTecnico
    {
        public Usuario usuario { get; set; }
        public int IdProyectoTecnico { get; set; }
        public int idSolicitud { get; set; }
        public ParametroGenerico tipoCultivo { get; set; }
        public ParametroGenerico estadoProy { get; set; }
        public bool mangasPlasticas { get; set; }
        public float densidadSiembra { get; set; }
        public string observaciones { get; set; }
        public Boolean controlaProduccion { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public DateTime fechaInicio { get; set; }//Fecha de Inicio solicitada por el titular, aplica sólo para colectores.
        public DateTime fechaTermino { get; set; }//Fecha de Termino solicitada por el titular, aplica sólo para colectores.
        public ParametroGenerico especieAutorizada { get; set; }
        
        public List<EspecieAutorizadaPT> especieAutProyTecnico { get; set; }
        public List<GrupoPT> grupoAutProyTecnico { get; set; }

        public List<EstructuraTecnicaPT> estructTecnicaProyTecnico { get; set; }
        public List<EstructuraTecnicaPT> estructTecnicaProyTecnicoDirectoSustrato { get; set; }
        public EstructuraTecnicaPT estructTecnicaColector { get; set; }

        public List<ProgrProduccionPT> progrProduccionProyTecnico { get; set; }
        public List<TipoAlimentoProyecto> tipoAlimento { get; set; }
        public List<TipoAlimentoProyecto> metodoCultivoAlgas { get; set; }
        public List<TipoAlimentoProyecto> tipoFondo { get; set; }
        public int mangasPlasticasVal { get; set; }

        public List<ArchivosAdjPT> archivoBinarioList { get; set; }
        public ParametroGenerico tipoCultivoAlgas { get; set; }

        public int numeroColectores { get; set; }
        public int numeroLineas { get; set; }
       
        // MÉTODOS (Constructores)
        public ProyectoTecnico(){
        }

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int IdProyectoTecnico {get;set;}
            public int idSolicitud { get; set; }
            public ParametroGenerico tipoCultivo { get; set; }
            public ParametroGenerico estadoProy { get; set; }
            public Boolean mangasPlasticas { get; set; }
            public float densidadSiembra { get; set; }
            public string observaciones { get; set; }
            public bool controlaProduccion { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public ParametroGenerico especieAutorizada { get; set; }
            
            public List<EspecieAutorizadaPT> grupoAutProyTecnico { get; set; }
            public List<GrupoPT> especieAutProyTecnico { get; set; }
            
            public List<EstructuraTecnicaPT> estructTecnicaProyTecnico { get; set; }
            public List<EstructuraTecnicaPT> estructTecnicaProyTecnicoDirectoSustrato { get; set; }
            public EstructuraTecnicaPT estructTecnicaColector { get; set; }

            public List<ProgrProduccionPT> progrProduccionProyTecnico { get; set; }
            public List<TipoAlimentoProyecto> tipoAlimento { get; set; }
            public List<TipoAlimentoProyecto> metodoCultivoAlgas { get; set; }
            public List<TipoAlimentoProyecto> tipoFondo { get; set; }
            public int mangasPlasticasVal { get; set; }

            public List<ArchivosAdjPT> archivoBinarioList { get; set; }
            public ParametroGenerico tipoCultivoAlgas { get; set; }

            public DateTime fechaInicio { get; set; }//Fecha de Inicio solicitada por el titular, aplica sólo para colectores.
            public DateTime fechaTermino { get; set; }//Fecha de Termino solicitada por el titular, aplica sólo para colectores.

            public int numeroColectores { get; set; }
            public int numeroLineas { get; set; }
            
        }


        
    }



}
