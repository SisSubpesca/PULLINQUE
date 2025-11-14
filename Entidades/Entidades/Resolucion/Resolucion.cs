using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Utilidades;

namespace Datos.Entidades.Resolucion
{

    [Serializable()]
    public class Resolucion
    {


        public Usuario usuario { get; set; }
        public int idResolucion { get; set; }
        public ParametroGenerico tipoIngreso { get; set; }
        public int pagina { get; set; }
        public ParametroGenerico tipoDocumento { get; set; }
        public ParametroGenerico tipoRelacionDocumento { get; set; }
        public ParametroGenerico origen { get; set; }
        public ParametroGenerico origenRegistro { get; set; }
        public string numero { get; set; }
        public DateTime fecha { get; set; }
        public int numeroCI { get; set; }
        public DateTime fechaCI { get; set; }
        public ParametroGenerico materia { get; set; }
        public ParametroGenerico resultado { get; set; }
        
        
        public int tieneReferencia { get; set; }


        public string numeroDiarioOficial { get; set; }
        public DateTime fechaDiarioOficial { get; set; }
        public ParametroGenerico vigencia { get; set; }
        public DateTime fechaInicioPlazo { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public DateTime nuevaFecha { get; set; }
        public ArchivoBinario archivoAdjunto { get; set; }
        public Resolucion resolucionPrincipal { get; set; }
        public String observaciones { get; set; }
        public DateTime fechaIngresoSistema { get; set; }
        public string cadena { get; set; }

        public List<Referencia> referenciasUnidadEspacial { get; set; }
        public List<Referencia> referenciasDocumento { get; set; }
        public List<Referencia> referenciasUbicacion { get; set; }
        public List<Referencia> referenciasEspecie { get; set; }
        public List<Referencia> referenciasTitular { get; set; }

        //ES LA COMBINATORIA EXISTENTE EN LA DB
        public ResolucionValidacion resolucionValidacion { get; set; }


        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }

        public List<ParametroGenerico> datoReferencia { get; set; }
        public string descripcionDatosRef { get { return concatenaReferencia(); } }

        public string fechaString { get { return fechaStringMetodo(); } }
        public string fechaCIString { get { return fechaCIStringMetodo(); } }
        public string fechaDiarioOficialString { get { return fechaDiarioOficialStringMetodo(); } }
        public string fechaInicioPlazoString { get { return fechaInicioPlazoStringMetodo(); } }
        public string fechaVencimientoString { get { return fechaVencimientoStringMetodo(); } }
        public string nuevaFechaString { get { return nuevaFechatringMetodo(); } }


        public string fechaStringMetodo()
        {
            if (this.fecha != null && this.fecha != default(DateTime))
            {
                return FechaUtils.formatearFechaSinHora(this.fecha);
            }
            else
            {
                return "";
            }
        }


        public string fechaCIStringMetodo()
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


        public string fechaDiarioOficialStringMetodo()
        {
            if (this.fechaDiarioOficial != null && this.fechaDiarioOficial != default(DateTime))
            {
                return FechaUtils.formatearFechaSinHora(this.fechaDiarioOficial);
            }
            else
            {
                return "";
            }
        }


        public string fechaInicioPlazoStringMetodo()
        {
            if (this.fechaInicioPlazo != null && this.fechaInicioPlazo != default(DateTime))
            {
                return FechaUtils.formatearFechaSinHora(this.fechaInicioPlazo);
            }
            else
            {
                return "";
            }
        }


        public string fechaVencimientoStringMetodo()
        {
            if (this.fechaVencimiento != null && this.fechaVencimiento != default(DateTime))
            {
                return FechaUtils.formatearFechaSinHora(this.fechaVencimiento);
            }
            else
            {
                return "";
            }
        }

        public string nuevaFechatringMetodo()
        {
            if (this.nuevaFecha != null && this.nuevaFecha != default(DateTime))
            {
                return FechaUtils.formatearFechaSinHora(this.nuevaFecha);
            }
            else
            {
                return "";
            }
        }

        public string concatenaReferencia()
        {

            string cadenaReferencia = "";

            int i = 0;
            if (datoReferencia != null)
            {
                foreach (ParametroGenerico param in datoReferencia)
                {

                   if (i == 0){
                      cadenaReferencia = param.descripcion;
                   }else{
                      cadenaReferencia = cadenaReferencia + ", " + param.descripcion;
                   }
                   i++;
                }
            }

            return cadenaReferencia;
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int idResolucion { get; set; }
            public ParametroGenerico tipoIngreso { get; set; }
            public int pagina { get; set; }
            public ParametroGenerico tipoDocumento { get; set; }
            public ParametroGenerico tipoRelacionDocumento { get; set; }
            public ParametroGenerico origen { get; set; }
            public ParametroGenerico origenRegistro { get; set; }
            public string numero { get; set; }
            public DateTime fecha { get; set; }
            public int numeroCI { get; set; }
            public DateTime fechaCI { get; set; }
            public ParametroGenerico materia { get; set; }
            public ParametroGenerico resultado { get; set; }
            

            public int tieneReferencia { get; set; }

            public string numeroDiarioOficial { get; set; }
            public DateTime fechaDiarioOficial { get; set; }
            public ParametroGenerico vigencia { get; set; }
            public DateTime fechaInicioPlazo { get; set; }
            public DateTime fechaVencimiento { get; set; }
            public DateTime nuevaFecha { get; set; }
            public ArchivoBinario archivoAdjunto { get; set; }
            public Resolucion resolucionPrincipal { get; set; }
            public String observaciones { get; set; }
            public DateTime fechaIngresoSistema { get; set; }
            public string cadena { get; set; }

            public List<Referencia> referenciasUnidadEspacial { get; set; }
            public List<Referencia> referenciasDocumento { get; set; }
            public List<Referencia> referenciasUbicacion { get; set; }
            public List<Referencia> referenciasEspecie { get; set; }
            public List<Referencia> referenciasTitular { get; set; }


            public ResolucionValidacion resolucionValidacion { get; set; }

            public DateTime fechaDesde { get; set; }
            public DateTime fechaHasta { get; set; }
        }

     

    }
}

