using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class ArchivosAdjPT
    {
        public int idPT { get; set; }
        public ArchivoBinarioEspecial archivoBinario { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public int numCI { get; set; }
        public DateTime fechaCI { get; set; }
        public int accion { get; set; }
        public int index { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public bool cambiaEstado { get; set; }




        public string fechaCIString
        {
            get
            {
                if (fechaCI != null && !fechaCI.Equals(""))
                {
                    return fechaCI.ToShortDateString();
                }
                return "";
            }
        }

        public int idArchivoBin
        {
            get
            {
                if (archivoBinario != null)
                {
                    if (archivoBinario.idArchivo > 0)
                        return archivoBinario.idArchivo;
                    else
                        return 0;
                }
                return 0;

            }
        }



        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idPT { get; set; }
            public ArchivoBinarioEspecial archivoBinario { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public int numCI { get; set; }
            public DateTime fechaCI { get; set; }
            public int accion { get; set; }
            public int index { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public bool cambiaEstado { get; set; }
        
        }
    }
}
