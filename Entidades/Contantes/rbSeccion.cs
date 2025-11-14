using System;
using System.Linq;
using System.Text;
using System.Collections;


namespace Datos.Contantes
{
    public static class rbSeccion
    {
        public static readonly int CERO = 0;

        //TITULARES
        public static readonly int SOLICITANTES = 83;
        public static readonly int SOLCIITUDES_PENDIENTES_ASOCIADOS_AL_TRAMITE = 84;
        

        //REFERENCIA GLOBAL
        public static readonly int REFERENCIA_GLOBAL_SERNAPESCA = 20;


        //ANTECEDENTES DEL SECTOR
        public static readonly int UBICACION_GEOGRAFICA = 28;
        public static readonly int BARRIO = 29;
        public static readonly int OTRA_DEFINICION_GEOGRAFICA = 30;
        public static readonly int ANTECEDENTES_CAPITANIA_PUERTO = 21;
        public static readonly int REFERENCIAS_GEOGRAFICAS_COORD_ORGINAL = 32;
        public static readonly int REFERENCIAS_GEOGRAFICAS_COORD_ENTREGA_MATERIAL = 33;
        public static readonly int REFERENCIAS_GEOGRAFICAS_COORD_REGULARIZACION = 34;
        public static readonly int ARCHIVOS_ADJUNTOS_COORD_ORGINAL = 35;
        public static readonly int ARCHIVOS_ADJUNTOS_COORD_ENTREGA_MATERIAL = 36;
        public static readonly int ARCHIVOS_ADJUNTOS_COORD_REGULARIZACION = 37;
        public static readonly int POLIGONO_COORD_ORIGINAL = 38;
        public static readonly int POLIGONO_COORD_ENTREGA_MATERIAL = 39;
        public static readonly int POLIGONO_COORD_REGULARIZACION = 40;
        public static readonly int VERTICE_COORD_ORIGINAL = 41;
        public static readonly int VERTICE_COORD_ENTREGA_MATERIAL = 42;
        public static readonly int VERTICE_COORD_REGULARIZACION = 43;
        public static readonly int OBSERVACIONES_ANTECEDENTES_DEL_SECTOR = 72;


        //PROYECTO TECNICO
        public static readonly int FORMA_CULIVO = 44;
        public static readonly int ESPECIES_AUTORIZADAS = 45;
        public static readonly int ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO = 46;
        public static readonly int CULTIVO_DE_ALGAS = 73;
        public static readonly int PROGRAMA_DE_PRODUCCION = 47;
        public static readonly int OBSERVACIONES_PROYECTO_TECNICO = 48;


        //INFORMES Y RESOLUCIONES
        public static readonly int ANTECEDENTES_URB = 102;
        public static readonly int EXAMEN_PRELIMINAR = 101;
                
        public static readonly int ITC_UOT = 7;
        public static readonly int UNIDADES_DE_DEPENDENCIA = 51;
        public static readonly int OBSERVACIONES_ITC_UOT = 50;
        
        public static readonly int INSPECCION_TERRENO = 10;
        public static readonly int COORDENADAS_GEOGRAFICAS_INSPECCION_TERRENO = 52;
        public static readonly int VERTICE_INSPECCION_TERRENO = 53;
        public static readonly int OBSERVACIONES_INSPECCION_TERRENO = 54;

        public static readonly int BANCO_NATURAL = 13;
        public static readonly int OBSERVACIONES_BCO_NATURAL = 55;
        
        public static readonly int DIFUSION_BANCO_NATURAL = 14;
        public static readonly int OBSERVACIONES_DIFUSION_BANCO_NATURAL = 69;
                     
        public static readonly int DIFROL = 15;
        public static readonly int OBSERVACIONES_DIFROL = 70;

        public static readonly int APLICA_SEA = 56;
        public static readonly int NOTIFICACION_SMA = 4;
        public static readonly int ANTECEDENTES_AMBIENTALES_MO = 3;
        public static readonly int INFORME_AMBIENTALES_MO = 8;
        public static readonly int ANTECEDENTES_AMBIENTALES = 2;
        public static readonly int RESOLUCION_CALIFICACION_AMBIENTAL = 12;
        public static readonly int OBSERVACIONES_INFORME_AMBIENTAL = 57;

        public static readonly int ANTECEDENTES_COMPLEMENTARIOS = 16;
        public static readonly int OBSERVACIONES_ANTECEDENTES_COMPLEMENTARIOS = 58;
        public static readonly int CERTIFICADO_REGISTRO_OPERACION = 100;

        public static readonly int ANTECEDENTES_PLANOS = 11; 
        public static readonly int INFORME_TECNICO_UOT = 9;
        public static readonly int OBSERVACIONES_PLANOS = 60;

        public static readonly int INFORME_DAC = 6;
        public static readonly int DEVOLUCION_JURIDICA = 17;
        public static readonly int OBSERVACIONES_INFORME_DAC = 61;

        public static readonly int RESOLUCION_SSP = 18;
        public static readonly int DEVOLUCION_SSFFAA = 5;
        public static readonly int OBSERVACIONES_RESOLUCION_SSP = 63;

        public static readonly int RESOLUCION_SSFFAA = 19;
        public static readonly int OBSERVACIONES_RESOLUCION_SSFFAA = 62;

        public static readonly int ADMINISTRADOR_DE_DOCUMENTOS = 65;
        


        //CHECKS
        public static readonly int CHECK_INTERVIENE_UOT_AMBIENTAL = 104;
        public static readonly int CHECK_TRAMITA_UTS = 105;
        public static readonly int CHECK_REFERENCIA_GLOBAL = 106;
        public static readonly int CHECK_EVALUACION_AMBIENTAL = 107; //transformado en pullinque 4
        public static readonly int CHECK_FIRMA_Y_SEGUIMIENTO = 108;
        public static readonly int CHECK_CULTIVO_EXPERIMENTAL = 109;
        public static readonly int CHECK_CERTIFICADO_OPERACION = 110;

        public static readonly int CHECK_DEVOLUCION_JURIDICA = 111; //PULLINQUE 4
        public static readonly int CHECK_PERTINENCIA_SMA = 112; //PULLINQUE 4
        public static readonly int CHECK_AVANZA_SMA = 113; //PULLINQUE 4
        //public static readonly int CHECK_TIPO_EVALUACION_AMBIENTAL = 114; //PULLINQUE 4
        public static readonly int CHECK_DEVOLUCION_MARINA = 115; //PULLINQUE 4 
        public static readonly int CHECK_TIPO_CERTIFICADO_DISTANCIA = 116; //PULLINQUE 4 (solo colectores)
        public static readonly int CHECK_NUEVO_ITC_MO_UOT = 114; //PULLINQUE 4
        public static readonly int CHECK_NUEVO_ITC_CPS_UOT = 117; //PULLINQUE 4
        public static readonly int CHECK_DECISION_ZONAL = 118; //PULLINQUE 4
        public static readonly int CHECK_DECISION_CENTRAL = 119; //PULLINQUE 4
        public static readonly int CHECK_DECISION_ZONAL_NOTIFICACION_INSUFICIENCIA = 120; //PULLINQUE 4
        public static readonly int CHECK_DECISION_UGP= 121; //PULLINQUE 4
        public static readonly int CHECK_DECISION_UGP_MULTIPLE = 122; //PULLINQUE 4
        public static readonly int CHECK_VERIFICA_ANTECEDENTES = 123; //PULLINQUE 4
        public static readonly int CHECK_GENERAR_IT_UOT = 124; //PULLINQUE 4
        public static readonly int CHECK_RECHAZO_EXP_CONCESION = 125; //PULLINQUE 4
        public static readonly int CHECK_DECISION_CENTRAL_NOTIFICACION_INSUFICIENCIA = 126; //PULLINQUE 4
        public static readonly int CHECK_PLANOS = 130; //PULLINQUE 4

        public static readonly int CHECK_PROYECTO_TECNICO = 131; //PULLINQUE 4
        public static readonly int CHECK_SUPEDITA_AVANZA_APRUEBA = 132; //PULLINQUE 4 - MEJORAS
        public static readonly int CHECK_SUSPENDIDA_AVANZA = 133; //PULLINQUE 4 - MEJORAS

        //UNIDADES ESPACIALES
        public static readonly int UNIDAD_ESPACIAL = 64;






        /* Solicitud de Modificación */
        public static readonly int DATOS_DE_LA_CONCESION = 23;
        public static readonly int DATOS_DE_LA_SOLICITUD = 24;
        public static readonly int DATOS_DEL_SOLICITANTE = 25;
        public static readonly int DATOS_SOLICITUD_ACUICULTURA = 26;
        
        public static readonly int DATOS_GENERALES = 66;
        public static readonly int TRAMITES_MODIFICACION_PENDIENTES = 67;
        public static readonly int REQUERIMIENTOS_PENDIENTES_TITULAR = 68;

        public static readonly int CENTRO_DE_ORIGEN = 81;
        public static readonly int CENTRO_DE_DESTINO = 82;
        

        


        /* Unidades Espaciales */
        public static readonly int RESOLUCIONES = 103;



        public static Hashtable hashSeccionesSI(){

            Hashtable hashSeccionesSI = new Hashtable();
            hashSeccionesSI.Add(rbSeccion.ANTECEDENTES_AMBIENTALES, null);
            hashSeccionesSI.Add(rbSeccion.RESOLUCION_CALIFICACION_AMBIENTAL, null);
            return hashSeccionesSI;
        
        }

        public static Hashtable hashSeccionesNO(){

            Hashtable hashSeccionesNO = new Hashtable();
            hashSeccionesNO.Add(rbSeccion.NOTIFICACION_SMA, null);
            hashSeccionesNO.Add(rbSeccion.ANTECEDENTES_AMBIENTALES_MO, null);
            hashSeccionesNO.Add(rbSeccion.INFORME_AMBIENTALES_MO, null);
            return hashSeccionesNO;
        }

        public static readonly int FECHA_SOLICITADA_TITULAR_COLECTOR = 128;
        public static readonly int ARCHIVO_ADJUNTO_PROYECTO_TECNICO = 127;
        public static readonly int ESTRUCTURAS_TECNICAS_A_INSTALAR_CADA_ANIO_COLECTOR = 129;
        
    }
}


