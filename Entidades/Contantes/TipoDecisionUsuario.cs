using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{

    /*
     * STRING CON EL TIPO DE DECISION DEL USUARIO QUE PUEDE TOMAR EN LOS FLUJOS
     */ 
    public static class TipoDecisionUsuario
    {

        public static String idDevPesca = "idDevPesca";
        public static String idDevMarina = "idDevMarina";
        public static String requiereITC_MO = "requiereITC_MO"; //RAMA DE MO
        public static String evaluaUOT = "evaluaUOT";
        public static String tramitaUTS = "tramitaUTS";
        public static String informarSMA = "informarSMA";
        public static String evalRamaNoSomete = "evalRamaNoSomete";
        public static String requiereITC_CPS = "requiereITC_CPS"; //RAMA DE CPS E INFAS
        public static String idTipoSolCertDistancia = "idTipoSolCertDistancia";
        public static String esperaRespuestaSMA = "esperaRespuestaSMA";
        public static String envioSSPCentral_zonal = "envioSSPCentral_zonal";

        public static String confZonalNotInsuficiencia = "confZonalNotInsuficiencia";
        public static String sspCentralDevCarta = "sspCentralDevCarta";
        public static String confCentralNotInsuficiencia = "confCentralNotInsuficiencia";
        public static String tramitaUGP = "tramitaUGP";
        public static String estadoAplica = "estadoAplica";

        public static String verificaAntecedentes = "verificaAntecedentes"; //experimental de concesion
        public static String idTipoRechazoSol = "idTipoRechazoSol"; //experimental de concesion

        public static String evaluaUOT_Cartografia = "evaluaUOT_Cartografia"; //experimental de concesion


        public static String requiereIT_UOT_Plano = "requiereIT_UOT_Plano";

        public static String requiereNuevoPT = "requiereNuevoPT";


        public static String supeditaAvanzaAprueba = "supeditaAvanzaAprueba";

        public static String suspendeAvanzaEstado = "suspendeAvanzaEstado";


        public static String omiteSSFFAA = "omiteSSFFAA";
        public static String omiteInspTerreno = "omiteInspTerreno";
        public static String omiteBanco = "omiteBanco";
        public static String omiteDifRadial = "omiteDifRadial";
        public static String omiteEvAmbiental = "omiteEvAmbiental";


        


    }
}
