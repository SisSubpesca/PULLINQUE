using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class DocumentoAmbito
    {

        public bool seleccione { get; set; }
        public ParametroGenerico ambito { get; set; }
        public ParametroGenerico tipo { get; set; } //TEMA/SUBREQUERIMIENTO
        public int index { get; set; }
        public int accion { get; set; }
        public int idDocPestana { get; set; }
        public int idRequerimiento { get; set; } //idDocGeneral
        public int idDocGeneralResp { get; set; }
        public ParametroGenerico estadoVigencia { get; set; } //idEstadoVigencia
        public ParametroGenerico estadoResultadoResp { get; set; }
        public ParametroGenerico tipoResultadoSupeditado { get; set; }
        public ParametroGenerico seccion { get; set; }
        public int idSolicitud { get; set; }
        public SolicitudConcesion solicitudConcesion_sub { get; set; }


        //PARA SABER SI SE HA MODIFICACION
        public ParametroGenerico ambitoAntiguo { get; set; }
        public ParametroGenerico tipoAntiguo { get; set; }

     
           // MÉTODOS (Constructores)
        public DocumentoAmbito()
        { }

        public string resultadoString {
            get {

                return estadoResultadoResp.descripcion;
            }
        }

        public int idResultado
        {
            get
            {

                return estadoResultadoResp.id;
            }
        }

        public string tipoString {
            get {
                return tipo.descripcion;
            }
        }

        public int idTipo
        {
            get
            {
                return tipo.id;
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public bool seleccione { get; set; }
            public ParametroGenerico ambito { get; set; }
            public ParametroGenerico tipo { get; set; }
            public int index { get; set; }
            public int accion { get; set; }

            public int idDocPestana { get; set; }
            public int idRequerimiento { get; set; }

            public int idDocGeneralResp { get; set; }
            public ParametroGenerico estadoVigencia { get; set; } //idEstadoVigencia
            public ParametroGenerico estadoResultadoResp { get; set; }
            public ParametroGenerico tipoResultadoSupeditado { get; set; }
            public ParametroGenerico seccion { get; set; }

            public ParametroGenerico ambitoAntiguo { get; set; }
            public ParametroGenerico tipoAntiguo { get; set; }
            public int idSolicitud { get; set; }
            public SolicitudConcesion solicitudConcesion_sub { get; set; }
            
        }

    }
}
