using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class EquivalenciaUOT
    {
        public ParametroGenerico tipoUE{ get; set; }
        public ParametroGenerico estadoSolicitud{ get; set; }
        public ParametroGenerico estadoUOT{ get; set; }
        

        public EquivalenciaUOT()
        { }

         // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public ParametroGenerico tipoUE{ get; set; }
            public ParametroGenerico estadoSolicitud{ get; set; }
            public ParametroGenerico estadoUOT{ get; set; }
        }


        public int idTipoUE
        {

            get
            {

                if (tipoUE != null)
                {
                    return tipoUE.id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public String nombreTipoUE
        {

            get
            {

                if (tipoUE != null)
                {
                    return tipoUE.descripcion;
                }
                else
                {
                    return "";
                }
            }
        }


        public int idEstadoSolicitud
        {

            get
            {

                if (estadoSolicitud != null)
                {
                    return estadoSolicitud.id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public String nombreEstadoSolicitud
        {

            get
            {

                if (estadoSolicitud != null)
                {
                    return estadoSolicitud.descripcion;
                }
                else
                {
                    return "";
                }
            }
        }

        public int idEstadoSolicitudUOT
        {

            get
            {

                if (estadoUOT != null)
                {
                    return estadoUOT.id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public String nombreEstadoSolicitudUOT
        {

            get
            {

                if (estadoUOT != null)
                {
                    return estadoUOT.descripcion;
                }
                else
                {
                    return "";
                }
            }
        }


    }
}
