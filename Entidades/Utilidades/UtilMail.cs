using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Datos.Utilidades
{

    /*
     * Clase para los reemplazos de textos en los correos a enviar
     */ 
    public static class UtilMail
    {
        /// <summary>
        /// Realiza los reemplazos en un texto a enviar por correo
        /// </summary>
        /// <param name="mensaje">Texto del correo sin reemplazos</param>
        /// <param name="reemplazos">Hash con la lista de reemplazos</param>
        /// <returns>Texto del correo con reemplazos</returns>
        public static String parseMensaje(String mensaje, Hashtable reemplazos)
        {

            try
            {

                if (mensaje != null && mensaje.Trim().Length > 0 && reemplazos != null && reemplazos.Count > 0)
                {

                    foreach (string reemplazo in reemplazos.Keys)
                    {

                        for (int i = 1; i <= reemplazos.Count; i++)
                        {
                            if (reemplazo != null && reemplazo.Equals("[REEM_" + i + "]"))
                            {
                                mensaje = mensaje.Replace("[REEM_" + i + "]", reemplazos[reemplazo].ToString());

                            }
                        }
                    }   
                }
            }
            catch (Exception)
            {
                return mensaje;
            }
            return mensaje;

        }

    }
}
