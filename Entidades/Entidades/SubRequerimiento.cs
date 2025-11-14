using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class SubRequerimiento
    {
        public int idSubRequerimiento { get; set; }
        public String nombreSubRequerimiento { get; set; }
        public bool aplicaReitera { get; set; }
        public bool aplicaComplementario { get; set; }
        public bool aplicaVisacionMasiva { get; set; }

        public int aplicaReiteraFiltro { get; set; }
        public int aplicaComplementarioFiltro { get; set; }
        public int aplicaVisacionMasivaFiltro { get; set; }

            // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idSubRequerimiento { get; set; }
            public String nombreSubRequerimiento { get; set; }
            public bool aplicaReitera { get; set; }
            public bool aplicaComplementario { get; set; }
            public bool aplicaVisacionMasiva { get; set; }

            public int aplicaReiteraFiltro { get; set; }
            public int aplicaComplementarioFiltro { get; set; }
            public int aplicaVisacionMasivaFiltro { get; set; }
        }

        public string aplicaReiteraString
        {
            get
            {

                if (aplicaReitera)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaComplementarioString
        {
            get
            {

                if (aplicaComplementario)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }

        public string aplicaVisacionMasivaString
        {
            get
            {

                if (aplicaVisacionMasiva)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }
    }
}
