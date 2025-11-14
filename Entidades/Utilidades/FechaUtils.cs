using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Datos.Utilidades
{
    public static  class FechaUtils
    {

        public static String formatearFecha(DateTime date) {

            try
            {

                if (date == null)
                {
                    return "";
                }
                else
                {

                    //date.ToString("dd/MM/yyyy g", CultureInfo.CreateSpecificCulture("es-CL"));
                    //date.ToString("dd'/'MM'/'yyyy hh:mm", CultureInfo.CreateSpecificCulture("es-CL"));
                    //Convert.ToDateTime(date).ToString("dd'/'MM'/'yyyy hh:mm");
                    //return Convert.ToDateTime(date).ToString("dd-MM-yyyy hh:mm");

                    return Convert.ToDateTime(date).ToString("dd'/'MM'/'yyyy HH:mm");
                    
                }

            }catch (Exception) { 
                return "";
            }

        }

        public static String formatearFechaSinHora(DateTime date)
        {

            try
            {

                if (date == null)
                {
                    return "";
                }
                else
                {

                    //date.ToString("dd/MM/yyyy g", CultureInfo.CreateSpecificCulture("es-CL"));
                    //date.ToString("dd'/'MM'/'yyyy hh:mm", CultureInfo.CreateSpecificCulture("es-CL"));
                    //Convert.ToDateTime(date).ToString("dd'/'MM'/'yyyy hh:mm");
                    //return Convert.ToDateTime(date).ToString("dd-MM-yyyy hh:mm");

                    return Convert.ToDateTime(date).ToString("dd'/'MM'/'yyyy");

                }

            }
            catch (Exception)
            {
                return "";
            }

        }

        public static DateTime fechaMinimaSistema()
        {
            return  new DateTime(1930, 1, 1, 0, 0, 0); //AAAA,DD,MM,HH,MM,SS
        }

    }
}
