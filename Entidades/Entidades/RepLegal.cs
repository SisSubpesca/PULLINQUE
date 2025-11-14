using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class RepLegal
    {
        public int idRepLegal { get; set; }
        public Solicitante titular { get; set; }
        public Solicitante representanteLegal { get; set; }
        public DateTime fechaInicioRel { get; set; }
        public DateTime fechaFinRel { get; set; }
       
        public ArchivoBinarioEspecial archivoAsoc { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        
        public int index { get; set; }
        public int accion { get; set; }
        public int pagina { get; set; }


        public int _rutRepresentante { get; set; }
        public char _dvRepresentante { get; set; }
        public String _tipoRepresentante { get; set; }
        public String _nombreRepresentante { get; set; }


        public int _idArchivoBinario;

        public int idArchivoBinario
        {

            get
            {
                if (archivoAsoc != null)
                {
                    int idArchivo = archivoAsoc.idArchivo;

                    _idArchivoBinario = idArchivo;
                    return _idArchivoBinario;
                }

                return 0;

            }
        }

        public int rutPersonaRepLegal
        {
            get
            {
                _rutRepresentante = representanteLegal.rut;

                return _rutRepresentante;
            }
        }

        public char dvRepresentante
        {

            get
            {
                char dvRep = representanteLegal.dv;

                _dvRepresentante = dvRep;
                return _dvRepresentante;
            }
        }

        public String nombreRepresentante
        {

            get
            {
                String nombreRep = representanteLegal.nombreSolicitante;

                _nombreRepresentante = nombreRep;
                return _nombreRepresentante;
            }
        }

        public String tipoRepresentante
        {

            get
            {
                String tipoRep = representanteLegal.tipoPersona.descripcion;

                _tipoRepresentante = tipoRep;
                return _tipoRepresentante;
            }
        }

        // MÉTODOS (Constructores)
        public RepLegal()
        {
        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idRepLegal { get; set; }
            public Solicitante titular { get; set; }
            public Solicitante representanteLegal { get; set; }
            public DateTime fechaInicioRel { get; set; }
            public DateTime fechaFinRel { get; set; }
            public ArchivoBinarioEspecial archivoAsoc { get; set; }
            public int pagina { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public int accion { get; set; }
        }

    }
}
