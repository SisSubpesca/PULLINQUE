using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class Requerimiento
    {
        public int idRequerimiento { get; set; }
        public int idReqPrincipal { get; set; }
        public SolicitudConcesion solicitud { get; set; }
        public ParametroGenerico flujoDocumental { get; set; }
        public ParametroGenerico tipoSalida { get; set; }
        public ParametroGenerico tipoEntrada { get; set; }
        public List<DocumentoAmbito> ambitoTipo { get; set; } 
        public ParametroGenerico tipoDocumento { get; set; }
        public string numero { get; set; }
        public DateTime fecha { get; set; }
        public DateTime nuevaFecha { get; set; }
        public int numeroCI { get; set; }
        public DateTime fechaCI { get; set; }
        public ParametroGenerico destinatario { get; set; }
        public ParametroGenerico origen { get; set; }
        public ArchivoBinario archivoAdjunto { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public ParametroGenerico estadoFinal { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public bool esReposicion { get; set; }
        public ParametroGenerico tipoUnidEspacial { get; set; }
        public ParametroGenerico tipoTramite { get; set; }
        public String titularesCad { get; set; }

        //Datos de a quien respondo
        public int idReqSalida { get; set; }
        public String numeroReqSalida  { get; set; }

        /* Asociación con Grupos Suspendidos y Tipo Supeditado */
        public List<DependenciaSupeditados> dependenciaSupeditadosList { get; set; }
        public List<AsocGrupoSolicitud> asocGrupoSolicitudList { get; set; }
       
        //Lista de Personas asociadas cuando se solicita un requerimiento.
        public List<Persona> personas { get; set; }

        public String _personasString;
        public String _idRequerimientoString;
        public String _idArchivoString;


        //SOLICITUDES CERRADAS FORZADAMENTE
        public List<DocSolicitudCierre> solCierreForzado  { get; set; }

        public ParametroGenerico tipoIOAux { get; set; }

        // MÉTODOS (Constructores)
        public Requerimiento()
        { }

        public String idArchivo
        {
            get
            {
                ArchivoBinario archivoBinario = this.archivoAdjunto;

                if (archivoBinario != null)
                {
                    _idArchivoString = Convert.ToString(archivoBinario.idArchivo);
                }
                return _idArchivoString;
            }
        }

        public String idRequerimientoString
        {
            get
            {
                foreach (DocumentoAmbito documentoAmbito in this.ambitoTipo)
                {
                    if (documentoAmbito != null)
                    {
                        _idRequerimientoString = Convert.ToString(documentoAmbito.idRequerimiento);
                        break;
                    }
                }
                return _idRequerimientoString;
            }
        }

        public String personasString
        {

            get
            {
                String personasString = "";
                int i = 0;
                foreach (Persona persona in this.personas)
                {
                    if (i == 0)
                    {
                        personasString = persona.rutPersona + "-" + persona.dvPersona + " " + persona.nombreSolicitante;
                    }
                    else
                    {
                        personasString = personasString + "," + persona.rutPersona + "-" + persona.dvPersona + " " + persona.nombreSolicitante;
                    }
                    i++;

                }

                _personasString = personasString;

                return _personasString;
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idRequerimiento { get; set; }
            public SolicitudConcesion solicitud { get; set; }
            public ParametroGenerico flujoDocumental { get; set; }
            public ParametroGenerico tipoSalida { get; set; }
            public ParametroGenerico tipoEntrada { get; set; }
            public List<DocumentoAmbito> ambitoTipo { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public string numero { get; set; }
            public DateTime fecha { get; set; }
            public DateTime nuevaFecha { get; set; }

            public int numeroCI { get; set; }
            public DateTime fechaCI { get; set; }

            public ParametroGenerico destinatario { get; set; }
            public ParametroGenerico origen { get; set; }
            public ArchivoBinario archivoAdjunto { get; set; }

            public int idReqSalida { get; set; }
            public String numeroReqSalida { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public List<Persona> personas { get; set; }
            public ParametroGenerico estadoFinal { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public int idReqPrincipal { get; set; }
            public bool esReposicion { get; set; }

            //SOLICITUDES CERRADAS FORZADAMENTE
            public List<DocSolicitudCierre> solCierreForzado { get; set; }

            public ParametroGenerico tipoUnidEspacial { get; set; }
            public ParametroGenerico tipoTramite { get; set; }
            public String titularesCad { get; set; }
            public ParametroGenerico tipoIOAux { get; set; }

            public List<DependenciaSupeditados> dependenciaSupeditadosList { get; set; }
            public List<AsocGrupoSolicitud> asocGrupoSolicitudList { get; set; }
        }

    }
}
