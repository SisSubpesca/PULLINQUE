using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Datos.AccesoDatos;


namespace Datos.Entidades
{
    [Serializable()]
    public class Barrio
    {
        public int    id_barrio    { get; set; }
        public string barrio  { get; set; }
        public int id_macrozona { get; set; }
        public string nombreMacrozona { get; set; }
        public int id_region { get; set; }
        public string nombreRegion { get; set; }
        public ParametroGenerico tipo_barrio { get; set; }
        public bool necesitaAntTerreno { get; set; }
        public bool necesitaRegul { get; set; }
        public ParametroGenerico vigencia { get; set; }

        public int _idArchivoBinario;


        // MÉTODOS (Constructores)
        public Barrio()
        {
            barrio = "";
        }


          // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {

            public int id_barrio { get; set; }
            public int id_region { get; set; }
            public string barrio { get; set; }
            public int id_macrozona { get; set; }
            public string nombreMacrozona { get; set; }
            public ParametroGenerico tipo_barrio { get; set; }
            public bool necesitaAntTerreno { get; set; }
            public bool necesitaRegul { get; set; }
            public string nombreRegion { get; set; }

        }

        public String nombreTipoBarrio
        {

            get
            {

                if (tipo_barrio != null)
                {
                    return tipo_barrio.descripcion;
                }
                else {
                    return "";
                }
            }
        }

        public int idTipoBarrio
        {

            get
            {

                if (tipo_barrio != null)
                {
                    return tipo_barrio.id;
                }
                else
                {
                    return 0;
                }
            }
        }



       
    }
}
