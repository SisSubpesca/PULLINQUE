using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Contantes;

namespace Datos.Entidades
{
    [Serializable()]
    public class UnidadDependenciaMod
    {
        public int idUnidadDependencia { get; set; }
        public int idSolConcesion { get; set; }
        public ParametroGenerico estadoSolicitud { get; set; }
        public ParametroGenerico estadoSolicitudDep { get; set; }//estado de la dependencia
        public ParametroGenerico aplicaDependencia { get; set; }
        public ParametroGenerico tipoUnidDependencia { get; set; }
        public string observaciones { get; set; }
        public string claveUnidDependencia { get; set; }
        public int index { get; set; }
        public int accion { get; set; }
        public SolicitudConcesion solicitudDepende { get; set; }
        public SolicitudConcesion solicitudPadre { get; set; }
        public string descripcionTipoUnidDependencia { get { return tipoUnidadDependenciaString(); } }

        public UnidadDependenciaMod()
        {

        }

        public string tipoUnidadDependenciaString()
        {
            if (tipoUnidDependencia != null && tipoUnidDependencia.id > 0)
            {
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_ACOPIO)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_ACOPIO;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_SOLICITUD)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_SOLICITUD;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_AMPLIACION_AREA)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_AMPLIACION_AREA;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_ESPECIE)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_ESPECIE;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_RELOCALIZACION)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_RELOCALIZACION;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_AMERB)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_AMERB;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_COLECTORES)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_COLECTORES;
                }
                if (tipoUnidDependencia.id == rbTipo.TIPO_UNID_DEPENDENCIA_FAENAMIENTO)
                {
                    return cadenas.TIPO_UNID_DEPENDENCIA_FAENAMIENTO;
                }
            }

            return null;
        }


        public string unidadDependenciaString()
        {
            string unidadDependenciaString = solicitudDepende.numPert + "-" + solicitudDepende.tipoTramite + "-" + solicitudDepende.tipoUnidadEspacial;
            return unidadDependenciaString;
        }



        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idUnidadDependencia { get; set; }
            public int idSolConcesion { get; set; }
            public ParametroGenerico aplicaDependencia { get; set; }
            public ParametroGenerico tipoUnidDependencia { get; set; }
            public string observaciones { get; set; }
            public string claveUnidDependencia { get; set; }
            public int index { get; set; }
            public int accion { get; set; }
            public ParametroGenerico estadoSolicitud { get; set; }
            public ParametroGenerico estadoSolicitudDep { get; set; }
            public SolicitudConcesion solicitudDepende { get; set; }
            public SolicitudConcesion solicitudPadre { get; set; }
        }
    }
}
