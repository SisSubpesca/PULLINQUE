using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class DataExterna
    {
        public DataExterna()
        {
        }
        public int codigo { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String region { get; set; }
        public String comuna { get; set; }
        public String comunaIndigena { get; set; }
        public String estado { get; set; }
        public String cdu01 { get; set; }
        public String cdu02 { get; set; }
        public String cdu03 { get; set; }
        public String fcdu01 { get; set; }
        public String fcdu02 { get; set; }
        public String fcdu03 { get; set; }
        public DateTime ultimoPlazo { get; set; }
        public String superficie { get; set; }
        public String informe { get; set; }
        //--
       
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int codigo { get; set; }
            public String nombre { get; set; }
            public String descripcion { get; set; }
            public String region { get; set; }
            public String comuna { get; set; }
            public String comunaIndigena { get; set; }
            public String estado { get; set; }
            public String cdu01 { get; set; }
            public String cdu02 { get; set; }
            public String cdu03 { get; set; }
            public String fcdu01 { get; set; }
            public String fcdu02 { get; set; }
            public String fcdu03 { get; set; }
            public DateTime ultimoPlazo { get; set; }
            public String superficie { get; set; }
            public String informe { get; set; }
        
        }

    }
}
