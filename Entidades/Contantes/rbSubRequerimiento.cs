using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{
    public static class rbSubRequerimiento
    {

        public static readonly int RESOLUCION_PERTINENCIA_SEA = 128;

        public static readonly int NOTIFICACION_SMA = 312;
        public static readonly int REITERA_NOTIFICACION_SMA = 313;

        /* Envio de Correo Electrónico Carta Ambiental */
        public static readonly int VISACION_DE_CARTA = 46;
        public static readonly int CORRECION_CARTA_MO = 93;
        public static readonly int CARTA_FIRMADA_POR_JEFATURA = 94;
        
        /* Envio de Correo Electrónico Carta Titular */
        public static readonly int CORRECCION_CARTA_SEIA = 137;
        
        /* Envio de Correo Electrónico Informe DAC */
        public static readonly int VISACION_IT_DAC = 122;
        public static readonly int CORRECCION_IT_DAC = 102;
        public static readonly int IT_DAC_FIRMADO_POR_JEFATURA = 104;

        /* Envio de Correo Electrónico Informe Cartografía */
        public static readonly int CORRECION_IT_UOT = 91;
        public static readonly int ACTUALIZACION_IT_OUT = 92;

        /* Envio de Correo Electrónico Planos */
        public static readonly int VISACION_CARTA_PLANO = 119;
        public static readonly int VISACION_CARTA_CORRIGE_PLANO = 118;
        public static readonly int CARTA_PLANO_FIRMADA_POR_JEFATURA = 97;
        public static readonly int CARTA_CORRIGE_PLANO_FIRMADA_POR_JEFATURA = 95;
        public static readonly int CORRECCION_DE_CARTA_PLANO = 100;
        public static readonly int CORRECCION_DE_CARTA_CORRIGE_PLANO = 99;

        /**/
        public static readonly int RESOLUCION_SSP = 132;

        /**/
        public static readonly int RESOLUCION_SSFFAA = 130;

        
        /* Difusión de BN */
        public static readonly int PUBLICACION_RADIAL = 8;
        public static readonly int REITERA_PUBLICACION_RADIAL = 42;
        public static readonly int CORRECCION_PUBLICACION_RADIAL = 125;
        public static readonly int REITERA_CORRECCION_PUBLICACION_RADIAL = 51;

        public static readonly int PUBLICACION_WEB = 55;
        public static readonly int REITERA_PUBLICACION_WEB = 56;

        /**/
        public static readonly int RECURSO_DE_REPOSICION = 129;

        /**/
        public static readonly int INFORME_TECNICO = 85;

        /**/
        public static readonly int INFORME_AMBIENTAL_DE_MO = 103;
        public static readonly int ITC_CPS_UOT = 106;
        public static readonly int ITC_MO_UOT = 107;

        public static readonly int EVALUACION_CPS_E_INFAS = 201;
        public static readonly int INFORMATIVO = 65;

        public static readonly int CORRECCION_CARTA = 214;


        public static readonly int CARTA_SOMETIMIENTO_SEA_RCA = 30;



        /* Correos electrónicos Pullinque 4 */
        public static int CORRECCION_CARTA_CERTIFICADO_DISTANCIA = 144;
        public static int CORRECCION_OFICIO_INSPECCION_TERRENO = 304;
        public static int INSPECCION_TERRENO_PARA_FIRMA_JEFATURA = 305;
        public static int VISACION_CARTA_DIFROL = 306;
        public static int CORRECCION_DIFROL = 307;
        public static int DIFROL_PARA_FIRMA_JEFATURA = 308;
        public static int VISACION_CARTA_ANTECEDENTES_COMPLEMENTARIOS = 300;
        public static int CORRECCION_CARTA_ANTECEDENTES_COMPLEMENTARIOS = 301;
        public static int CARTA_ANTECEDENTES_COMPLEMENTARIOS_FIRMA_JEFATURA = 302;
        public static int VISACION_CARTA_NOTIFICACION_REFORMULACION = 371;
        public static int CORRECCION_CARTA_NOTIFICACION_REFORMULACION = 372;
        public static int CARTA_NOTIFICACION_REFORMULACION_FIRMA_JEFATURA = 373;
        public static int VISACION_CARTA_NOTIFICACION_POR_INSUFICIENCIA = 376;
        public static int CORRECCION_CARTA_NOTIFICACION_POR_SUFICIENCIA = 377;
        public static int CARTA_NOTIFICACION_POR_INSUFICIENCIA_FIRMA_JEFATURA = 378;
        public static int VISACION_CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA = 380;
        public static int CORRECCION_CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA = 381;
        public static int CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA_FIRMA_JEFATURA = 382;
        public static int CORRECION_CARTA_PLANOS_UOT = 354;
        public static int VISACION_CARTA_PLANOS_UTS = 352;
        public static int VISACION_CARTA_CORRIGE_PLANOS = 353;
        public static int CORRECCION_CARTA_PLANOS_UTS = 356;
        public static int CORRECCION_CARTA_CORRIGE_PLANOS_UTS = 357;
        public static int CARTA_PLANOS_FIRMA_JEFATURA = 358;
        public static int CARTA_CORRIGE_PLANOS_FIRMA_JEFATURA = 359;
        public static int ESPERA_REVISION_PLANOS = 360;
        public static int VISACION_OFICIO_VISACION_PLANOS = 361;
        public static int CORRECCION_OFICIO_VISACION_PLANOS = 362;
        public static int OFICIO_VISACION_PLANOS_FIRMA_JEFATURA = 363;
        public static int CORRECCION_CARTA_CORRIGE_PLANOS_UOT = 355;
        public static int ANTECEDENTES_AMERB = 389;
        public static int EVALUACION_AMERB = 390;
        public static int ANTECEDENTES_DIRECCION_ZONAL = 397;
        public static int VISACION_CARTA_UTS = 0;
        public static int CORRECCION_CARTA_MO_UTS= 320;
        public static int VISACION_CARTA_RECOPILACION_CPS_E_INFAS_UOT = 322;
        public static int CORRECCION_CARTA_RECOPILACION_CPS_E_INFAS_UOT = 323;
        public static int VISACION_CARTA_RECOPILACION_CPS_E_INFAS_UTS = 324;
        public static int CORRECCION_CARTA_RECOPILACION_CPS_E_INFAS_UTS = 325;
        public static int CARTA_RECOPILACION_CPS_E_INFAS_FIRMA_JEFATURA= 326;
        public static int VISACION_CARTA_AMBIENTAL_UTS = 366;
        public static int CORRECCION_CARTA_SEIA_UTS = 327;
        public static int VISACION_CARTA_TITULAR_UTS = 366;
        public static int VISACION_CERTIFICADO_REGISTRO_OPERACION = 330;
        public static int CORRECCION_CERTIFICADO_REGISTRO_OPERACION = 331;
        public static int CERTIFICADO_REGISTRO_OPERACION_FIRMA_JEFATURA = 332;
        public static int VISACION_IT_DAC_UTS = 365;
        public static int CORRECCION_IT_DAC_UTS = 319;
        public static int EVALUACION_CARTOGRAFICA = 399;
        public static int CORRECCION_CARTA_DAC_REFORM_CARTOGRAFICA = 392;
        public static int CARTA_DAC_REFORM_CARTOGRAFICA_PARA_FIRMA_JEFATURA = 393;
        public static int INFORME_AMBIENTAL_RECOPILACION_CPS_E_INFAS = 347;
        public static int VISACION_OFICIO_NOTIFICACION_SMA = 309;
        public static int CORRECCION_OFICIO_NOTIFICACION_SMA = 310;
        public static int OFICIO_NOTIFICACION_SMA_PARA_FIRMA_JEFATURA = 311;



        public static int CARTA_TITULAR_MO = 31;
        public static int INGRESO_SOLICITUD = 127;
        public static int ASIGNADOR_UA = 67;

        public static int IT_UOT = 109;


        public static int DECRETO_SSFFAA = 404;
        public static int RESUELVE_APELACION = 135;

        
        public static readonly int RESOLUCION_RECURSO = 133; //recurso de reposición

        public static readonly int RESOLUCION_AMPLIACION_CARTA_MO = 192; //Resolución Ampliación de Plazo por Informe MO (192)
        public static readonly int RESOLUCION_AMPLIACION_CARTA_SOMETIMIENTO = 194; //Resolución Ampliación de Plazo por Sometimiento SEIA (194)
        public static readonly int RESOLUCION_AMPLIACION_CARTA_CPS_INFAS = 532; //Resolución Ampliación de Plazo Recopilación de CPS e INFAS


    }
}
