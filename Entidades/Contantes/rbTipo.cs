using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{
    public static class rbTipo
    {

        public static readonly int ENTRADA = 1;
        public static readonly int SALIDA = 2;
        public static readonly int REQUERIMIENTO_CON_RESPUESTA = 3;
        public static readonly int INFORMATIVO = 4;
        public static readonly int RESPUESTA_A_UN_REQUERIMIENTO = 5;
        public static readonly int INGRESO_SIN_REQUERIMIENTO = 6;

        public static readonly int RESOLUCION_EXENTA = 579;

        public static readonly int DECRETO = 580;
        public static readonly int DECRETO_EXENTO = 581;

        public static readonly int DECRETO_PRINCIPAL = 729;
        public static readonly int DECRETO_COMPLEMENTARIO = 730;
        

        public static readonly int PERSONA_NATURAL = 12;
        public static readonly int PERSONA_JURIDICA = 13;

        public static readonly int TIPO_CARTA_IGM = 14;
        public static readonly int TIPO_CARTA_SHOA = 15;

        public static readonly int CONCESION_DE_ACUICULTURA = 36;
        public static readonly int CENTRO_DE_FAENAMIENTO = 37;
        //public static readonly int VIVERO = 38;
        public static readonly int CENTRO_DE_ACOPIO = 118;
        public static readonly int ACUICULTURA_EN_AMERB = 119;
        public static readonly int COLECTORES_DE_SEMILLA = 120;

        public static readonly int UNID_ESPACIAL_CONCESION = 36;
        public static readonly int UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO = 37;
        //public static readonly int UNID_ESPACIAL_VIVERO = 38;
        public static readonly int UNID_ESPACIAL_CENTRO_DE_ACOPIO = 118;
        public static readonly int UNID_ESPACIAL_ACUICULTURA_EN_AMERB = 119;
        public static readonly int UNID_ESPACIAL_COLECTORES_DE_SEMILLA = 120;



        public static readonly int ANTECEDENTES_ESPACIALES = 39;
        public static readonly int ANTECEDENTES_TERRENO = 40;
        public static readonly int REGULARIZACION = 41;
        public static readonly int INSPECCION_TERRENO = 75;

        public static readonly int ESTRUCTURAS_ANIO = 47;
        public static readonly int ESTRUCTURAS_ANIO_MAXIMO = 48;

        public static readonly int PESO_PROM_RANGO = 51;
        public static readonly int PESO_PROM_SIN_RANGO = 52;

        public static readonly int ANTEC_SECTOR = 53;
        public static readonly int PROY_TECNICO = 54;
        public static readonly int INFO_CARTOGRAFIA = 55;
        public static readonly int INSP_TERRENO = 56;
        public static readonly int BCO_NATURAL = 57;
        public static readonly int DIFUSION_BCO_NAT = 58;
        public static readonly int DIFROL = 59;
        public static readonly int INFO_SEA = 60;
        public static readonly int ANTEC_COMPLEMENTARIOS = 61;
        public static readonly int PLANOS = 62;
        public static readonly int INFO_DAC = 63;
        public static readonly int RESOLUCION_SSP = 64;
        public static readonly int RESOLUCION_SSFFAA = 65;
        

        public static readonly int TIPO_CULT_EXTENSIVO = 42;
        public static readonly int TIPO_CULT_INTENSIVO = 43;
        public static readonly int TIPO_CULT_AMBOS = 44;
        public static readonly int TIPO_ALIMENTO_PT_PELLET = 73;
        public static readonly int TIPO_ALIMENTO_PT_ALGA = 72;
        public static readonly int TIPO_ALIMENTO_PT_OTRO = 74;
        public static readonly int MET_ALGAS_DIR_SUSTRATO = 76;
        public static readonly int MET_ALGAS_INDIR_SUSTRATO = 77;
        public static readonly int MET_ALGAS_SUSPENDIDO = 78;
        public static readonly int MET_ALGAS_ESTANQUE = 79;
        public static readonly int MET_ALGAS_OTRO = 80;
        public static readonly int T_FONDO_DURO = 81;
        public static readonly int T_FONDO_SEMIDURO = 82;
        public static readonly int T_FONDO_BLANDO = 83;
        public static readonly int T_FONDO_OTRO = 84;
     
        public static readonly int INFORME_PRINCIPAL = 66;
        public static readonly int INFORME_COMPLEMENTARIO = 67;
        public static readonly int RESOLUCION_PRINCIPAL = 69;
        public static readonly int RESOLUCION_COMPLEMENTARIA = 70;

        public static readonly int ESTRUCT_MEDIDA_ALTO = 1;
        public static readonly int ESTRUCT_MEDIDA_ANCHO = 2;
        public static readonly int ESTRUCT_MEDIDA_DIAMETRO = 3;
        public static readonly int ESTRUCT_MEDIDA_LARGO = 4;
        public static readonly int ESTRUCT_MEDIDA_VOLUMEN = 5;

        public static readonly int TIPO_FORMA_CIRCULAR = 1;
        public static readonly int TIPO_FORMA_RECTANGULAR = 2;
        public static readonly int TIPO_FORMA_CUADRADA = 3;
        public static readonly int TIPO_FORMA_LINEAL = 4;


        public static readonly int SALMONIDOS = 2;
        public static readonly int MITILIDOS = 7;
        public static readonly int OSTREIDOS = 3;

        public static readonly int RELOCALIZACION_SECTOR_CERO = 85;
        public static readonly int RELOCALIZACION_CREA = 86;
        public static readonly int RELOCALIZACION_FUSIONA = 87;

        

        public static readonly int TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA = 88;
        public static readonly int TIPO_TRAMITE_MODIFICACION = 89;
        public static readonly int TIPO_TRAMITE_SECTOR_RELOCALIZACION = 95;
        public static readonly int TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO = 121;
        public static readonly int TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB = 122;
        public static readonly int TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA = 123;
        public static readonly int TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO = 124;
        //public static readonly int TIPO_TRAMITE_RELOCALIZACION = 139;


        public static readonly int MOD_CONCESION_ESPECIE = 90;
        public static readonly int MOD_CONCESION_PT = 91;
        public static readonly int MOD_CONCESION_AMPLIA_SUPERFICIE = 92;
        public static readonly int MOD_CONCESION_REDUCE_SUPERFICIE = 93;
        public static readonly int MOD_CONCESION_REGULARIZACION = 94;

        public static readonly int SOMETIMIENTO_SEA_SOMETE = 98;
        public static readonly int SOMETIMIENTO_SEA_NO_SOMETE = 99;

        public static readonly int CENTRO_NO_EXISTE = 105;
        public static readonly int DIFERENCIA_DE_HECTAREAS = 100;
        public static readonly int PREFERENCIA_DE_RELOCALIZACION = 101;
        public static readonly int MANEJO_DE_TITULARES = 102;
        public static readonly int DIFERENCIA_REGION = 116;
        public static readonly int CREA_CONCESION_SIN_PREFERENCIA = 117;
        public static readonly int CANTIDAD_SECTORES_RELOCALIZACION = 638;
        public static readonly int ORIGEN_SIN_INFORME = 639;
        
        


        public static readonly int UNIDAD_DEPENDENCIA_SI = 106;
        public static readonly int UNIDAD_DEPENDENCIA_NO = 107;

        public static readonly int TIPO_UNID_DEPENDENCIA_SOLICITUD = 108;
        public static readonly int TIPO_UNID_DEPENDENCIA_AMPLIACION_AREA = 109;
        public static readonly int TIPO_UNID_DEPENDENCIA_ESPECIE = 110;
        public static readonly int TIPO_UNID_DEPENDENCIA_RELOCALIZACION = 111;
        public static readonly int TIPO_UNID_DEPENDENCIA_AMERB = 112;
        public static readonly int TIPO_UNID_DEPENDENCIA_COLECTORES = 113;
        public static readonly int TIPO_UNID_DEPENDENCIA_FAENAMIENTO = 114;
        public static readonly int TIPO_UNID_DEPENDENCIA_ACOPIO = 115;


        public static readonly int NINGUNO_RANGO_PESO_EJEMPLAR = 159;


        public static readonly int SECTORIALISTA = 407;



        //Experimentales Amerb
        public static readonly int EXPERIMENTALES_AMERB = 532; //esta valor debe ser igual a UNID_ESPACIAL_EXPERIMENTALES_AMERB
        public static readonly int UNID_ESPACIAL_EXPERIMENTALES_AMERB = 532; //esta valor debe ser igual a EXPERIMENTALES_AMERB
        public static readonly int TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB = 535;
        public static readonly int TIPO_UNID_DEPENDENCIA_EXPERIMENTALES_AMERB = 538;

        //Acuicultura en ECMPO
        public static readonly int ECMPO = 534; //esta valor debe ser igual a UNID_ESPACIAL_ECMPO
        public static readonly int UNID_ESPACIAL_ECMPO = 534; //esta valor debe ser igual a ECMPO
        public static readonly int TIPO_TRAMITE_SOLICITUD_ECMPO = 537;
        public static readonly int TIPO_UNID_DEPENDENCIA_ECMPO = 540;

        //Experimentales Concesión
        public static readonly int UNID_ESPACIAL_EXPERIMENTALES_CONCESION = 533;
        public static readonly int TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION = 536;
        public static readonly int TIPO_UNID_DEPENDENCIA_EXPERIMENTALES_CONCESION = 539;


        //Modificación Centro de Faenamiento
        public static readonly int UNID_ESPACIAL_MOD_CENTRO_FAENAMIENTO = 37; //Es el mismo id que su unidad espacial centro de faenamiento.
        public static readonly int TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO = 546;
        
        public static readonly int MOD_CENTRO_FAENAMIENTO_RENOVACION = 541;
        public static readonly int MOD_CENTRO_FAENAMIENTO_PT_ESPECIE = 542;
        public static readonly int MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE = 543;
        public static readonly int MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE = 544;
        public static readonly int MOD_CENTRO_FAENAMIENTO_REGULARIZACION = 545;
         

        //Modificación Centro de Acopio
        public static readonly int UNID_ESPACIAL_MOD_CENTRO_ACOPIO = 118; //Es el mismo id que su unidad espacial centro de acopio.
        public static readonly int TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO = 552;
        
        public static readonly int MOD_CENTRO_ACOPIO_RENOVACION = 547;
        public static readonly int MOD_CENTRO_ACOPIO_PT_ESPECIE = 548;
        public static readonly int MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE = 549;
        public static readonly int MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE = 550;
        public static readonly int MOD_CENTRO_ACOPIO_REGULARIZACION = 551;

        

        //Solicitud de Modificación Amerb
        public static readonly int UNID_ESPACIAL_MOD_AMERB = 119; //Es el mismo id que su unidad espacial acuicultura en amerb.
        public static readonly int TIPO_TRAMITE_SOLICITUD_MOD_AMERB = 558;
        
        public static readonly int MOD_AMERB_ESPECIE = 553;
        public static readonly int MOD_AMERB_PT = 554;
        public static readonly int MOD_AMERB_AMPLIA_SUPERFICIE = 555;
        public static readonly int MOD_AMERB_REDUCE_SUPERFICIE = 556;
        public static readonly int MOD_AMERB_REGULARIZACION = 557;

        


        //Modificación ECMPO
        public static readonly int UNID_ESPACIAL_MOD_ECMPO = 37; //Es el mismo id que su unidad espacial.
        public static readonly int TIPO_TRAMITE_SOLICITUD_MOD_ECMPO = 564;
        //public static readonly int TIPO_UNID_DEPENDENCIA_MOD_ECMPO = 

        public static readonly int MOD_ECMPO_ESPECIE = 559;
        public static readonly int MOD_ECMPO_PT = 560;
        public static readonly int MOD_ECMPO_AMPLIA_SUPERFICIE = 561;
        public static readonly int MOD_ECMPO_REDUCE_SUPERFICIE = 562;
        public static readonly int MOD_ECMPO_REGULARIZACION = 563;




        //TIPOS DE REFERENCIAS
        public static readonly int RESOLUCION_REFERENCIA_UC = 594;
        public static readonly int RESOLUCION_REFERENCIA_DOCUMENTO = 595;
        public static readonly int RESOLUCION_REFERENCIA_UBICACION = 596;
        public static readonly int RESOLUCION_REFERENCIA_ESPECIE = 597;
        public static readonly int RESOLUCION_REFERENCIA_TITULAR = 598;

        //TIPO PLAZO
        public static readonly int TIPO_PLAZO_DIAS = 578;
        public static readonly int TIPO_PLAZO_MESES = 577;
        public static readonly int TIPO_PLAZO_ANIOS = 576;
        public static readonly int RESOLUCION_SUB_REFERENCIA_SOLICITUD = 599;
        public static readonly int RESOLUCION_SUB_REFERENCIA_UE = 600;





        public static readonly int DESCANSO = 609;
        public static readonly int PRODUCCION = 608;

        public static readonly int TIPO_INTERFAZ_RESOLUCION_ADMIN_UE = 610;
        public static readonly int TIPO_INTERFAZ_RESOLUCION_ADMIN_RESOLUCIONES = 611;

        public static readonly int TIPO_BARRIO_ACS = 23;
        public static readonly int TIPO_BARRIO_ACM = 24;




        //RELOCALIZACION

        public static readonly int RELOCALIZACION_SECTOR_CERO_RESA = 612;
        public static readonly int RELOCALIZACION_CREA_RESA = 613;
        public static readonly int RELOCALIZACION_FUSIONA_RESA = 614;


        public static readonly int RELOCALIZACION_RESA = 622;
        public static readonly int RELOCALIZACION_LEY = 623;

        public static readonly int TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA = 624;


        public static readonly int SUPEDITADO_SOLICITUD_CONCESION = 625;
        public static readonly int SUPEDITADO_PLANOS_ORIGINALES = 628;
        public static readonly int SUPEDITADO_FALTA_DE_ANTECEDENTES = 634;
        public static readonly int SUPEDITADO_ENVIO_CERTIF_CAPITANIA_PUERTO = 632;
        public static readonly int SUPEDITADO_AMERB = 627;

        public static readonly int SUPEDITADO_SOLICITUD_RELOCALIZACION_LEY_RESA = 637;
        public static readonly int SUPEDITADO_SOLICITUD_AMERB = 641;
        public static readonly int SUPEDITADO_SOLICITUD_ECMPO = 626;

        //TIPO_DEVOLUCION_PESCA
        public static readonly int DEV_JURIDICA_EXPEDIENTE = 700;
        public static readonly int DEV_JURIDICA_NUEVO_ITDAC = 701;
        public static readonly int DEV_JURIDICA_MOD_RESOLUCION = 702;

        //TIPO_DEVOLUCION_MARINA
        public static readonly int DEV_MARINA_EXPEDIENTE = 703;
        public static readonly int DEV_MARINA_OFICIO = 704;
        public static readonly int DEV_MARINA_NUEVO_ITDAC = 705;
        public static readonly int DEV_MARINA_MOD_RESOLUCION = 706;



        //TIPO_INGRESO_CERT_DISTANCIA
        public static readonly int CERT_DISTANCIA_CORREO = 710;
        public static readonly int CERT_DISTANCIA_CARTA = 711;


        //TIPO_RECHAZO_SOLICITUD
        public static readonly int EXP_CON_RECHAZO_CARTA = 717;
        public static readonly int EXP_CON_RECHAZO_RESOLUCION = 718;



        public static readonly int EXISTE_CENTRO_ORIGEN_PERT_DISTINTO = 125;

        public static int FECHA_EXACTA = 733;

        //ERRORES DE MODIFICACION
        public static readonly int DIFERENCIA_DE_HECTAREAS_MODIFICACION = 161;
        public static readonly int MANEJO_DE_TITULARES_MODIFICACION = 162;
        public static readonly int CENTRO_NO_EXISTE_MODIFICACION = 163;


        //TIPO DOCUMENTOS
        public static readonly int OFICIO = 8;
        public static readonly int RESOLUCION = 10;
    }
}

