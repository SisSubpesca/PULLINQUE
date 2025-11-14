using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    public class HeaderSolicitudConcesion
    {
        public int idSolConcesion { get; set; }
        public int idConcesion { get; set; }
        public string numPert { get; set; }
        public string docRequerido { get; set; }
        public ParametroGenerico estadoActual { get; set; }
        public ParametroGenerico estadoAnterior { get; set; }
        public ParametroGenerico estadoPosterior { get; set; }
        public List<EstadoIsla> islas { get; set; }
        public ParametroGenerico requiereNuevoPT { get; set; }
        public ParametroGenerico supeditaAvanzaAprueba { get; set; }
        public ParametroGenerico suspendeAvanzaEstado { get; set; }
        public bool especiePerteneceCultExp { get; set; }
        public bool tiene_ITC_pend_sup { get; set; }
        public bool perteneceGrupoSuspendido { get; set; }

        public string islasString()
        {

            string islasString = "";
            int i = 0;

            if (islas != null && islas.Count > 0)
            {

                foreach (EstadoIsla estadoIsla in this.islas)
                {
                    string tipoEstadoAux = "";

                    if (estadoIsla.tipoIsla != null)
                    {
                        if (i == 0)
                        {
                            islasString = "(" + estadoIsla.tipoIsla.descripcion + "):" + estadoIsla.estadoActual.descripcion;

                        }
                        else
                        {

                            if (estadoIsla.tipoIsla != null && !estadoIsla.tipoIsla.descripcion.Equals(tipoEstadoAux))
                            {
                                islasString = islasString + "<br>";
                                islasString = islasString + " (" + estadoIsla.tipoIsla.descripcion + "):" + estadoIsla.estadoActual.descripcion;

                            }
                            else
                            {
                                islasString = islasString + "<br>";
                                islasString = islasString + "-" + estadoIsla.estadoActual.descripcion;

                            }
                        }
                        tipoEstadoAux = estadoIsla.tipoIsla.descripcion;
                        i++;
                    }
                }
            }

            return islasString;

        }

        //SUBCLASE
        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idSolConcesion { get; set; }
            public int idConcesion { get; set; }
            public string numPert { get; set; }
            public string docRequerido { get; set; }
            public ParametroGenerico estadoActual { get; set; }
            public ParametroGenerico estadoAnterior { get; set; }
            public ParametroGenerico estadoPosterior { get; set; }
            public List<EstadoIsla> islas { get; set; }
            public bool especiePerteneceCultExp { get; set; }
            public bool tiene_ITC_pend_sup { get; set; }
            public bool perteneceGrupoSuspendido { get; set; }
        }
    }
}
