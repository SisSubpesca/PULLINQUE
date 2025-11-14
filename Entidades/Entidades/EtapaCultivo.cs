using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Contantes;

namespace Datos.Entidades
{
    [Serializable()]
    public class EtapaCultivo
    {

        public int id_etapaDesarrollo { get; set; }
        public int codigo { get; set; }
        public Especies especie { get; set; }
        public string nombreEtapaDesarrollo { get; set; }
        public ParametroGenerico estado { get; set; }
        public ParametroGenerico grupoEspecie { get; set; }

        public int _idEspecie;
        public string _nombreEspecie;


        public int idEspecie {

            get {

                if (especie != null && especie.id_especie > 0)
                {
                    _idEspecie = especie.id_especie;
                    return _idEspecie;
                }
                else {
                    return 0;
                }
            
            }
        }

        public string especieNombreComun {

            get
            {
                if (especie != null)
                {
                    _nombreEspecie = especie.especieNombreComun;
                    return _nombreEspecie;
                }
                else {
                    return "";
                }
            }
        }

        public string estadoString {
            get
            {
                if (estado != null)
                {
                    if (estado.id == 1)
                    {
                        return "Vigente";
                    }
                    else {
                        return "No Vigente";
                    }
                    
                }
                else {
                    return "";
                }

            }
        
        }

        public string grupoEspecieString{ 
        get
            {
                if(grupoEspecie != null){
                    return grupoEspecie.descripcion;
                }else{
                    return "";
                }
            }
            
        }

         // MÉTODOS (Constructores)
        public EtapaCultivo()
        {
        }

        // SUBCLASE 
        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int id_etapaDesarrollo { get; set; }
            public int codigo { get; set; }
            public Especies especie { get; set; }
            public string nombreEtapaDesarrollo { get; set; }
            public ParametroGenerico estado { get; set; }
            public ParametroGenerico grupoEspecie { get; set; }
        }
    }
}
