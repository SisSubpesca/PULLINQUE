using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{

    [Serializable()]
    public class EspecieAutorizadaPT
    {
        public Usuario usuario { get; set; }
        public int idEspeciePT { get; set; }
        public int idProyectoTecnico { get; set; }
        public ParametroGenerico tipoCultivo { get; set; }
        public ParametroGenerico tipoAlimento { get; set; }
        public string nombreOtroTipoAlimento { get; set; }

        public ParametroGenerico especie { get; set; }
        public string especieGrupoAutorizString { get; set; }

        public ParametroGenerico etapaCultivo { get; set; }
        public List<EtapaCultivo> etapaCultivoList { get; set; }

        public ParametroGenerico grupoEspecie { get; set; }
        public ParametroGenerico grupoEspecieAutoriz { get; set; }
        public bool autorizada { get; set; }
        public bool incorporar { get; set; }
        public bool especieCheck { get; set; }
        public bool grupoCheck { get; set; }
        public string detalle { get; set; }
        public ParametroGenerico grupoAutorizAuto { get; set; }

        public int index { get; set; }
        public int accion { get; set; }

        public List<ProgrProduccionPT> list_ProgrProd { get; set; }

        public EspecieAutorizadaPT() { 
        }

        public string DescripcionEspecie{ get {return especie.descripcion;}}
        public string IDEspecie { 

            get {
                if (especie != null && especie.id > 0)
                {
                    return (especie.id).ToString();
                }
                else {
                    return null;
                }
            } 
        }


        public string etapaCultivoString {

            get {

                string etapaCultivoString = "";

                int i = 0;

                if (etapaCultivoList != null && etapaCultivoList.Count > 0)
                {
                    foreach (EtapaCultivo etapaCultivo in etapaCultivoList)
                    {
                        if (i == 0)
                        {
                            etapaCultivoString = etapaCultivo.nombreEtapaDesarrollo;
                        }
                        else
                        {

                            etapaCultivoString = etapaCultivoString + "," + etapaCultivo.nombreEtapaDesarrollo;
                        }
                        i++;
                    }
                }
                return etapaCultivoString;
            }
        }




        [Serializable]
        public class Serializable  //(Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public Usuario usuario { get; set; }
            public int idEspeciePT { get; set; }
            public int idProyectoTecnico { get; set; }
            
            public ParametroGenerico especie { get; set; }
            public string especieGrupoAutorizString { get; set; }

            public ParametroGenerico etapaCultivo { get; set; }
            public List<EtapaCultivo> etapaCultivoList { get; set; }
            
            public bool autorizada { get; set; }
            public bool incorporar { get; set; }
            public int index { get; set; }
            public int accion { get; set; }
            public bool especieCheck { get; set; }
            public bool grupoCheck { get; set; }
            public ParametroGenerico grupoEspecie { get; set; }
            public ParametroGenerico grupoEspecieAutoriz { get; set; }
            public List<ProgrProduccionPT> list_ProgrProd { get; set; }
            public ParametroGenerico tipoCultivo { get; set; }
            public ParametroGenerico tipoAlimento { get; set; }
            public string nombreOtroTipoAlimento { get; set; }
            public string detalle { get; set; }
            
        }

    }
}

