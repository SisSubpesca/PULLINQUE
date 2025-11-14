using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Contantes;
using Datos.Utilidades;

namespace Datos.Entidades.Resolucion
{
    [Serializable()]
    public class Referencia
    {
        public int index { get; set; }
        public int accion { get; set; }
        public int idReferencia { get; set; }
        public int idResolucion { get; set; }
       

        public int idTipoReferencia { get; set; } // Unidad Espacial, Documentos, Ubicación, Titular 
   

        // Unidad Espacial
        public ParametroGenerico tipo { get; set; }
        public ParametroGenerico tipoUnidadEspacial { get; set; }
        public ParametroGenerico tipoSolicitud { get; set; }
        public ParametroGenerico tipoModificacionUE { get; set; }//indica el tipo de modificación, si es PT, Especie, etc.
        public int idRefUEDocGeneral { get; set; }// Sólo estará seteado si la referencia genera un cambio automático en la Solicitud.
        public int idRefUESolConcesion { get; set; }//Sólo estará seteado si la referencia genera un cambio automático en la Solicitud.
        public String codigoCentro { get; set; }
        public String numeroPert { get; set; }
        public String codigoCentroRegularizado { get; set; }
        public String numeroIdentificador { get; set; }
        public Int32 numSector { get; set; }
        public DateTime fechaNuevoVencimiento { get; set; }
        public string fechaNuevoVencimientoString { get { return fechaNuevoVencimientotringMetodo(); } }


        //Documentos
        public ParametroGenerico origenReferencia { get; set; }
        public string numeroReferencia { get; set; }
        public DateTime fechaReferencia { get; set; }
        public ParametroGenerico tipoIngresoResol { get; set; }
        public ParametroGenerico tipoDocResol { get; set; }
        
        //Ubicacion
        public ParametroGenerico region { get; set; }
        public ParametroGenerico comuna { get; set; }
        public String sector { get; set; }

        //Titular
        public Solicitante titular { get; set; }

        //Especie
        public ParametroGenerico especie { get; set; }

        //Estado vigencia
        public ParametroGenerico estadoVigencia { get; set; }

        //SOLICITUD O UNIDAD ESPACIAL
        public SolicitudConcesion solicitudUnidadEspacial { get; set; }



        public string DescripcionNumSector { get { return numSectorToString(); } }


     
        public string numSectorToString()
        {

            string numSector = "";

            if (this.idTipoReferencia == rbTipo.RESOLUCION_REFERENCIA_UC && this.tipoSolicitud != null && (this.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION || this.tipoSolicitud.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA))
            {
                numSector = Convert.ToString(this.numSector);
            }

            return numSector;
        }


        public string obtenerVigencia { get { return obtenerVigenciaToString(); } }

        public string obtenerVigenciaToString()
        {


            string vigencia = "";

            if (this.estadoVigencia != null)
            {
                vigencia = Convert.ToString(this.estadoVigencia.id);
            }

            return vigencia;

        }



        public string fechaNuevoVencimientotringMetodo()
        {
            if (this.fechaNuevoVencimiento != null && this.fechaNuevoVencimiento != default(DateTime))
            {
                return FechaUtils.formatearFechaSinHora(this.fechaNuevoVencimiento);
            }
            else
            {
                return "";
            }
        }


        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int index { get; set; }
            public int accion { get; set; }
            public int idReferencia { get; set; }
            public int idResolucion { get; set; }
            
            
            public ParametroGenerico origenReferencia { get; set; }
            public string numeroReferencia { get; set; }
            public DateTime fechaReferencia { get; set; }
            public ParametroGenerico tipoIngresoResol { get; set; }
            public ParametroGenerico tipoDocResol { get; set; }

            public ParametroGenerico tipo { get; set; }
            public ParametroGenerico tipoUnidadEspacial { get; set; }
            public ParametroGenerico tipoSolicitud { get; set; }
            public ParametroGenerico tipoModificacionUE { get; set; }
            public int idRefUEDocGeneral { get; set; }
            public String codigoCentro { get; set; }
            public String numeroPert { get; set; }
            public String codigoCentroRegularizado { get; set; }
            public String numeroIdentificador { get; set; }
            public Int32 numSector { get; set; }
            public DateTime fechaNuevoVencimiento { get; set; }

            public ParametroGenerico region { get; set; }
            public ParametroGenerico comuna { get; set; }
            public String sector { get; set; }

            public ParametroGenerico especie { get; set; }

            public ParametroGenerico estadoVigencia { get; set; }

            public Solicitante titular { get; set; }

            public SolicitudConcesion solicitudUnidadEspacial { get; set; }


        }
    }
}
