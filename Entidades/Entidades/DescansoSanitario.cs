using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DescansoSanitario
    {
        public DescansoSanitario()
        {
        }

        public int idDescanso { get; set; }
        public ParametroGenerico barrio { get; set; }
        public ParametroGenerico tipoOperacion { get; set; }
        public int numOperacion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaInsercion { get; set; }
        public DateTime fechaInicioProduccionCero { get; set; }//se usa cuando se está ingresando el descanso cero

        public int index { get; set; }
        public int accion { get; set; }



        public string fechaInicioString
        {
            get
            {
                if (fechaInicio != null && !fechaInicio.Equals(""))
                {
                    return fechaInicio.ToShortDateString();
                }
                return "";
            }
        
        }

        public string fechaFinString
        {
            get
            {
                if (fechaFin != null && !fechaFin.Equals(""))
                {
                    return fechaFin.ToShortDateString();
                }
                return "";
            }

        }


        public string barrioString
        {
            get
            {
                if (barrio != null && barrio.id > 0) {
                    return barrio.descripcion;
                }
                return "";
            }
        }

        public string tipoOperacionString
        {
            get
            {
                if (tipoOperacion != null && tipoOperacion.id > 0)
                {
                    return tipoOperacion.descripcion;
                }
                return "";
            }
        }




        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idDescanso { get; set; }
            public ParametroGenerico barrio { get; set; }
            public ParametroGenerico tipoOperacion { get; set; }
            public int numOperacion { get; set; }
            public DateTime fechaInicio { get; set; }
            public DateTime fechaFin { get; set; }
            public DateTime fechaInsercion { get; set; }
            public DateTime fechaInicioProduccionCero { get; set; }//se usa cuando se está ingresando el descanso cero

            public int index { get; set; }
            public int accion { get; set; }
        
        }
    }
}
