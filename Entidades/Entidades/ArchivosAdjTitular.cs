using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Utilidades;

namespace Datos.Entidades
{
    [Serializable()]
    public class ArchivosAdjTitular
    {
        public int rutPersona { get; set; }
        public ArchivoBinarioEspecial archivoBinario { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }


        public int numCI { get; set; }
        public DateTime fechaCI { get; set; }

        public int index { get; set; }
        public int accion { get; set; }
        public int _idArchivo { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }

        public ArchivosAdjTitular()
        { }

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

        public int idArchivo
        {

            get
            {
                _idArchivo = archivoBinario.idArchivo;

                return _idArchivo;
            }
        }


        public string numCIStringMetodo
        {
            get
            {
                if (this.numCI > 0)
                {
                    return this.numCI.ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        public string fechaCIStringMetodo
        {
            get
            {
               if (this.fechaCI != null && this.fechaCI != default(DateTime))
                {
                    return FechaUtils.formatearFechaSinHora(this.fechaCI);
                }
                else
                {
                    return "";
                }
            }
        }

       

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int rutPersona { get; set; }
            public ArchivoBinarioEspecial archivoBinario { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }

            public int numCI { get; set; }
            public DateTime fechaCI { get; set; }

            public int index { get; set; }
            public int accion { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
        }
    }
}
