using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Contantes
{

    /*
     * Posibles acciones a hacer el sistema asociados a una sección
     */ 
    public static class rbAccion
    {
        public static readonly int NINGUNA = 0;
        public static readonly int VER = 1;
        public static readonly int EDITAR = 2; //INGRESAR, CARGAR Y MODIFICAR
        public static readonly int ADMINISTRAR_REQUERIMIENTO = 3;
        public static readonly int VIGENTE = 4;
        public static readonly int NO_VIGENTE = 5;
        public static readonly int ELIMINAR = 6;
        public static readonly int EVALUAR = 7;
        public static readonly int DESASOCIAR = 8;
        public static readonly int ASOCIAR = 9;
        public static readonly int DESCARGAR = 10;
        
        public static readonly int ACCESO = 11;
        public static readonly int MODIFICAR_TRAMITE = 12;
        public static readonly int REDEFINIR_TRAMITE = 13;
        public static readonly int VER_ERRORES_TRAMITE = 14;
        public static readonly int VER_ALERTAS_SOLICITUD = 15;

        public static readonly int PERMISOS_DE_ACCESO = 16;
        public static readonly int PERMISOS_COMUNALES = 17;
        public static readonly int ASIGNACION_DE_PERT = 18;


        public static readonly int HISTORIAL = 19;
        public static readonly int AMPLIAR_VIGENCIA = 20;

        public static readonly int ELIMINAR_UE = 0;
        public static readonly int TRANSFORMAR_UE = 0;

        public static readonly int TERMINAR_AMERB = 30;
        public static readonly int TRANSFORMAR_AMERB = 31;

        public static readonly int PlanillaPoligonos = 21;

    }
}
