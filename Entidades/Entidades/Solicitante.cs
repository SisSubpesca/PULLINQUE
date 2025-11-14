using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Solicitante
    {
        public Usuario usuario { get; set; }
        public int accion;
        public int idPersonasLeg;
        public SolicitudConcesion solicitud;
        public ParametroGenerico tipoPersona;
        public ParametroGenerico estadoAPE;
        public ParametroGenerico subtipoPersona;
        public int rut { get; set; }
        public char dv { get; set; }
        public String nombreSolicitante { get; set; }
        public Boolean genero { get; set; }
        public int idEstadoAsociacion;
        public DateTime fechaInicioVigencia;
        public DateTime fechaFinVigencia;
        public int numeroRegistroSubpesca;
        public DateTime fechaRegistroSubpesca;
        

        public Boolean regCentralizado { get; set; }
        public ParametroGenerico estadoPersona { get; set; }
        public ParametroGenerico holding { get; set; }
        public bool esRPA { get; set; }

        //Mantenedores
        public int numeroControlIngreso { get; set; }
        public DateTime fechaControlIngreso { get; set; }
        public List<Contacto> listaContacto { get; set; }
        public List<RepLegal> listaRepresentanteLegal { get; set; }
        public List<Operador> listaOperador { get; set; }
        public List<MatrizSucursal> matrizSucursales { get; set; }
        //public List<ArchivoBinario> listaArchivoAdjunto { get; set; }
        //public List<ArchivoBinarioEspecial> listaArchivoBinarioEspecial { get; set; }
        public List<ArchivosAdjTitular> listaArchivosAdjTitular { get; set; }
        public List<ArchivosAdjRepLegal> listaArchivosAdjRep { get; set; }
        public List<ArchivosAdjOperador> listaArchivosAdjOperador { get; set; }
        public List<NombrePersona> listaNombrePersona { get; set; }
        public int pagina { get; set; }

        public String nombreTitular { get; set; }
        
        // MÉTODOS (Constructores)
        public Solicitante()
        { }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int accion { get; set; }
            public int idPersonasLeg { get; set; }
            public SolicitudConcesion solicitud { get; set; }
            public ParametroGenerico tipoPersona { get; set; }
            public ParametroGenerico estadoAPE;
            public ParametroGenerico subtipoPersona { get; set; }
            public int rut { get; set; }
            public char dv { get; set; }
            public String nombreSolicitante { get; set; }
            public Boolean genero { get; set; }
            public int idEstadoAsociacion { get; set; }
            public DateTime fechaInicioVigencia { get; set; }
            public DateTime fechaFinVigencia { get; set; }
            public int numeroRegistroSubpesca { get; set; }
            public DateTime fechaRegistroSubpesca { get; set; }
            public int numeroControlIngreso { get; set; }
            public DateTime fechaControlIngreso { get; set; }
            public ParametroGenerico holding { get; set; }
            public List<Contacto> listaContacto { get; set; }
            public List<RepLegal> listaRepresentanteLegal { get; set; }
            public List<Operador> listaOperador { get; set; }
            public List<MatrizSucursal> matrizSucursales { get; set; }
            //public List<ArchivoBinario> listaArchivoAdjunto { get; set; }
            public List<ArchivoBinarioEspecial> listaArchivoBinarioEspecial { get; set; }
            public List<NombrePersona> listaNombrePersona { get; set; }
            public int pagina { get; set; }
            public Boolean regCentralizado { get; set; }
            public ParametroGenerico estadoPersona { get; set; }
            public List<ArchivosAdjTitular> listaArchivosAdjTitular { get; set; }
            public List<ArchivosAdjRepLegal> listaArchivosAdjRep { get; set; }
            public List<ArchivosAdjOperador> listaArchivosAdjOperador { get; set; }
            public bool esRPA { get; set; }

            public String nombreTitular { get; set; }
        }
       
    }
}
