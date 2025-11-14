using LogicaNegocio.cl.subpesca.rb.errores;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace LogicaNegocio.cl.subpesca.rb.servicios.super
{
    public class ValidacionRutCupService
    {
        private string BaseUrl;
        private Logger logger = new Logger();

        public ValidacionRutCupService()
        {
            // Definimos TLS 1.2 manualmente si no está disponible en SecurityProtocolType
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // Valor numérico de TLS 1.2
            
            // Skip validation of SSL/TLS certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            BaseUrl = ConfigurationManager.AppSettings["superBaseUrl"];


        }

        /// <summary>
        /// Valida el CUP y el RUT de la empresa en SUPER.
        /// </summary>
        /// <param name="rut">El RUT de la empresa (sin puntos, con guion).</param>
        /// <param name="cup">El Código Único de Proyecto.</param>
        /// <returns>True si el CUP es válido y pertenece a la empresa, False en caso contrario.</returns>
        public bool ValidarCUP(string rut, string cup)
        {
            // Aquí configuramos manualmente la API key y el Chile Atiende ID
            string apiKey = ConfigurationManager.AppSettings["superApiKey"]; // Modifica esto con tu API key
            string chileAtiendeId = ConfigurationManager.AppSettings["superChileAtiendeId"]; // Modifica esto con tu Chile Atiende ID

            // Obtención del token
            string token = ObtenerToken(apiKey, chileAtiendeId);

            if (string.IsNullOrEmpty(token))
            {
                System.Diagnostics.Debug.WriteLine("No se pudo obtener el token.");
                return false;
            }

            // Configuración para realizar la validación del CUP
            using (var client = new WebClient())
            {
                client.Headers.Add("Authorization", $"Token {token}");

                string endpoint = $"{BaseUrl}companies/{rut}/projects/{cup}";

                try
                {
                    // Realizamos la solicitud GET
                    string response = client.DownloadString(endpoint);

                    // Imprimimos la respuesta completa en la consola de depuración
                    System.Diagnostics.Debug.WriteLine($"Respuesta completa desde la API SUPER: {response}");

                    // Deserializamos la respuesta JSON
                    var json = new JavaScriptSerializer();
                    dynamic jsonResponse = json.Deserialize<dynamic>(response);

                    if (jsonResponse["status"] == "OK" && jsonResponse["valid"] == true && jsonResponse["existent"] == true)
                    {
                        return true;
                    }

                    return false;
                }
                catch (WebException ex)
                {
                    // Manejo de excepciones con detalles en consola
                    System.Diagnostics.Debug.WriteLine($"Error al realizar la solicitud: {ex.Message}");

                    // Intentamos leer y mostrar la respuesta de error si existe
                    if (ex.Response != null)
                    {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new System.IO.StreamReader(stream))
                        {
                            string errorResponse = reader.ReadToEnd();
                            System.Diagnostics.Debug.WriteLine($"Respuesta de error desde la API SUPER: {errorResponse}");
                        }
                    }

                    return false;
                }
            }
        }

        /// <summary>
        /// Obtiene el token desde el servicio SUPER.
        /// </summary>
        /// <param name="apiKey">Clave API proporcionada.</param>
        /// <param name="chileAtiendeId">ID de Chile Atiende.</param>
        /// <returns>Token obtenido o null si falla.</returns>
        private string ObtenerToken(string apiKey, string chileAtiendeId)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("El apiKey no puede estar vacío.", nameof(apiKey));
            if (string.IsNullOrWhiteSpace(chileAtiendeId)) throw new ArgumentException("El chileAtiendeId no puede estar vacío.", nameof(chileAtiendeId));

            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json");

                string endpoint = $"{BaseUrl}api-token-auth/";
                var requestBody = new
                {
                    api_key = apiKey,
                    chile_atiende_id = chileAtiendeId
                };

                try
                {
                    // Serializar el cuerpo de la solicitud como JSON
                    var jsonRequest = new JavaScriptSerializer().Serialize(requestBody);
                    var response = client.UploadString(endpoint, "POST", jsonRequest);

                    // Imprimir la respuesta completa en la consola de depuración
                    System.Diagnostics.Debug.WriteLine($"Respuesta completa desde la API SUPER (Token): {response}");
                    logger.LogDB($"Respuesta completa desde la API SUPER (Token): {response}");

                    // Deserializar la respuesta para obtener el token
                    var jsonResponse = new JavaScriptSerializer().Deserialize<dynamic>(response);

                    if (jsonResponse["status"] == "OK" && jsonResponse.ContainsKey("token"))
                    {
                        return jsonResponse["token"];
                    }

                    return null;
                }
                catch (WebException ex)
                {

                  
                    // Manejo de excepciones con detalles en consola
                    System.Diagnostics.Debug.WriteLine($"Error al realizar la solicitud: {ex.Message}");

                    logger.LogDB($"Error al realizar la solicitud: {ex.Message}");

                    // Intentamos leer y mostrar la respuesta de error si existe
                    if (ex.Response != null)
                    {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new System.IO.StreamReader(stream))
                        {
                            string errorResponse = reader.ReadToEnd();
                            System.Diagnostics.Debug.WriteLine($"Respuesta de error desde la API SUPER: {errorResponse}");
                            logger.LogDB($"Respuesta de error desde la API SUPER: {errorResponse}");
                        }
                    }

                    return null;
                }
            }
        }
    }
}
