using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Datos.Contantes
{

    /*
     * Texto y Color de celdas en las grillas de documentos
     */
    public static class colorPlanilla
    {

        public static String PARCIAL    = "Parcial";
        public static String COMPLETADO = "Completado";
        public static String COMPLETADO_AUTOMATICO = "Completado (Cierre Autom&#225;tico)";
        public static String PENDIENTE  = "Pendiente";

        public static Color COLOR_PARCIAL       = Color.Khaki;
        public static Color COLOR_COMPLETADO    = Color.PaleGreen;
        public static Color COLOR_PENDIENTE     = Color.Salmon;


        public static Color SIN_SSP = Color.Salmon;
        public static Color SSP_APROBADA = Color.PaleGreen;
        



        public static Color COLOR_BLANCO        = Color.White;
        public static Color COLOR_CELESTE       = ColorTranslator.FromHtml("#EFF3FB");


        public static String RAYA_DIVISORA = "2px solid #A4A4A4";


        /* Comparación de Coordenadas de Ant. Sector y Coordenadas de Inspección de Terreno */
        public static String NEGATIVA = "Negativa";
        public static String POSITIVA = "Positiva";



    }
}
