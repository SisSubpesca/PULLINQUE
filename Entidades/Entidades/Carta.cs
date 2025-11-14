using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class Carta
    {
        public int idCarta { get; set; }
        public ParametroGenerico region { get; set; }
        public ParametroGenerico tipoCarta { get; set; }
        public ParametroGenerico estadoVigencia { get; set; }
        public ParametroGenerico datum { get; set; }
        public ParametroGenerico huso { get; set; }
        public string numeroCarta { get; set; }
        public int numeroEdicion { get; set; }
        public int anioEdicion { get; set; }
        public string escala { get; set; }
        public bool a_a_a { get; set; }
        public int a_a_a_Filtro { get; set; }
        public string reemplazoCarta { get; set; }
        public DateTime fechaReemplazo { get; set;}
        public string observaciones { get; set; }
        public string descripcionCarta { get; set; }

        public Carta() { 
        }


        public string fechaReemplazoString
        {
            get
            {

                if (fechaReemplazo == default(DateTime))
                {
                    return "";
                }
                return Convert.ToString(fechaReemplazo);
            }
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int idCarta { get; set; }
            public ParametroGenerico region { get; set; }
            public ParametroGenerico tipoCarta { get; set; }
            public ParametroGenerico estadoVigencia { get; set; }
            public ParametroGenerico datum { get; set; }
            public ParametroGenerico huso { get; set; }
            public string numeroCarta { get; set; }
            public int numeroEdicion { get; set; }
            public int anioEdicion { get; set; }
            public string escala { get; set; }
            public bool a_a_a { get; set; }
            public string reemplazoCarta { get; set; }
            public DateTime fechaReemplazo { get; set; }
            public string observaciones { get; set; }
            public string descripcionCarta { get; set; }
            public int a_a_a_Filtro { get; set; }

        }


        public string aaaString {
            get {

                if (a_a_a)
                {
                    return "Si";
                }
                else {
                    return "No";
                }
            }
        }

        public string regionString {
            get {

                if (region != null)
                {
                    return region.descripcion;
                }
                else {
                    return "";
                }
            }
        }

        public string tipoCartaString {
            get {
                if (tipoCarta != null)
                {
                    return tipoCarta.descripcion;
                }
                else {
                    return "";
                }
            }
        }

        public string tipoEstadoString {
            get {
                if (estadoVigencia != null)
                {
                    return estadoVigencia.descripcion;
                }
                else {

                    return "";
                }
            }
        }

        public string datumString {
            get {

                if (datum != null)
                {
                    return datum.descripcion;
                }
                else {
                    return "";
                }
            }
        }

        public string tipoHusoString {
            get {

                if (huso != null)
                {
                    return huso.descripcion;
                }
                else {
                    return "";
                }
            }
        }

        public string estadoString
        {
            get
            {

                if (estadoVigencia != null)
                {
                    return estadoVigencia.descripcion;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
