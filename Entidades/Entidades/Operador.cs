using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class Operador
    {

        public int idOperador { get; set; }
        public Solicitante titular { get; set; }
        public Solicitante operador { get; set; }
        public DateTime fechaInicioRel { get; set; }
        public DateTime fechaFinRel { get; set; }
        //public List<ArchivoBinario> archivosAdjBin { get; set; }
        //public List<ArchivoBinarioEspecial> archivoBinarioEspecial { get; set; }
        public ArchivoBinarioEspecial archivoAsoc { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        

        public int _rutOperador;
        public char _dvOperador;
        public String _nombreOperador;

        public int index { get; set; }
        public int accion { get; set; }
        public int pagina { get; set; }

        // MÉTODOS (Constructores)
        public Operador()
        {
        }


        public int _idArchivoBinario;

        public int idArchivoBinario
        {

            get
            {
                int idArchivo = archivoAsoc.idArchivo;

                _idArchivoBinario = idArchivo;
                return _idArchivoBinario;
            }
        }

        public int rutOperador
        {

            get
            {
                int rutOperador = operador.rut;

                _rutOperador = rutOperador;
                return _rutOperador;
            }
        }

        public char dvOperador
        {

            get
            {
                char dvOperador = operador.dv;

                _dvOperador = dvOperador;
                return _dvOperador;
            }
        }

        public String nombreOperador
        {

            get
            {
                String nombreOperador = operador.nombreSolicitante;

                _nombreOperador = nombreOperador;
                return _nombreOperador;
            }
        }
        

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idOperador { get; set; }
            public Solicitante titular { get; set; }
            public Solicitante operador { get; set; }
            public DateTime fechaInicioRel { get; set; }
            public DateTime fechaFinRel { get; set; }
            //public List<ArchivoBinario> archivosAdjBin { get; set; }
            //public List<ArchivoBinarioEspecial> archivoBinarioEspecial { get; set; }
            public int pagina { get; set; }
            public ArchivoBinarioEspecial archivoAsoc { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            
        }

    }
}
