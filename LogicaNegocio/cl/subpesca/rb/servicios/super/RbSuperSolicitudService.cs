using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LogicaNegocio.cl.subpesca.rb.servicios.super
{
    public class RbSuperSolicitudService
    {
        private readonly string _connectionString;

        public RbSuperSolicitudService()
        {
            // Leer la cadena de conexión desde Web.config
            _connectionString = ConfigurationManager.ConnectionStrings["BDKeyAmbiental"].ConnectionString;

            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("La cadena de conexión no está configurada correctamente.");
            }
        }

        /// <summary>
        /// Inserta o actualiza un registro en la tabla rbSuperSolicitud usando el procedimiento almacenado.
        /// </summary>
        /// <param name="idSuperSolicitud">ID de la solicitud.</param>
        /// <param name="idEstadoSuper">Estado de la solicitud (puede ser nulo).</param>
        /// <param name="serviceApplicationId">ID del servicio (puede ser nulo).</param>
        /// <param name="msApplicationId">ID de MS (puede ser nulo).</param>
        /// <param name="idSolConcesion">ID de concesión.</param>
        /// <param name="companyRut">RUT de la empresa.</param>
        /// <param name="cup">Código único del proyecto.</param>
        /// <param name="fechaModificacion">Fecha de modificación (puede ser nulo).</param>
        /// <param name="archivoFinal">Archivo asociado (puede ser nulo).</param>
        /// <returns>El ID generado o actualizado.</returns>
        public int InsertarOActualizarRbSuperSolicitud(
            int idSuperSolicitud,
            int? idEstadoSuper,
            string serviceApplicationId,
            int? msApplicationId,
            int idSolConcesion,
            string companyRut,
            string cup,
            DateTime? fechaModificacion,
            byte[] archivoFinal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("paInsRbSuperSolicitud", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros obligatorios
                        command.Parameters.AddWithValue("@idSuperSolicitud", idSuperSolicitud);
                        command.Parameters.AddWithValue("@idSolConcesion", idSolConcesion);

                        // Parámetros opcionales con validaciones explícitas
                        command.Parameters.AddWithValue("@idEstadoSuper", idEstadoSuper ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@company_rut", string.IsNullOrEmpty(companyRut) ? (object)DBNull.Value : companyRut);
                        command.Parameters.AddWithValue("@cup", string.IsNullOrEmpty(cup) ? (object)DBNull.Value : cup);

                        // Validación específica para service_application_id
                        command.Parameters.Add("@service_application_id", SqlDbType.VarChar).Value =
                            string.IsNullOrEmpty(serviceApplicationId) ? (object)DBNull.Value : serviceApplicationId;

                        // Parámetro ms_application_id
                        command.Parameters.AddWithValue("@ms_application_id", msApplicationId ?? (object)DBNull.Value);

                        // Parámetro fechaModificacion
                        command.Parameters.AddWithValue("@fechaModificacion", fechaModificacion ?? (object)DBNull.Value);

                        // Validación específica para archivoFinal
                        if (archivoFinal != null && archivoFinal.Length > 0)
                        {
                            command.Parameters.Add("@archivoFinal", SqlDbType.VarBinary, archivoFinal.Length).Value = archivoFinal;
                        }
                        else
                        {
                            command.Parameters.Add("@archivoFinal", SqlDbType.VarBinary).Value = DBNull.Value;
                        }

                        // Imprimir los valores de los parámetros para depuración
                        System.Diagnostics.Debug.WriteLine("Parámetros enviados al procedimiento almacenado:");
                        foreach (SqlParameter parameter in command.Parameters)
                        {
                            System.Diagnostics.Debug.WriteLine($"{parameter.ParameterName}: {(parameter.Value == DBNull.Value ? "NULL" : parameter.Value)}");
                        }

                        // Ejecutar el procedimiento almacenado y capturar el ID generado o actualizado
                        object id = command.ExecuteScalar();
                        int idResult = id != null && id != DBNull.Value ? Convert.ToInt32(id) : -1;

                        // Imprimir el resultado para depuración
                        System.Diagnostics.Debug.WriteLine($"ID generado o actualizado: {idResult}");
                        return idResult;
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Capturar y mostrar los errores SQL
                    System.Diagnostics.Debug.WriteLine("Error SQL capturado:");
                    foreach (SqlError error in sqlEx.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"  - Mensaje: {error.Message}");
                        System.Diagnostics.Debug.WriteLine($"  - Número: {error.Number}");
                        System.Diagnostics.Debug.WriteLine($"  - Línea: {error.LineNumber}");
                        System.Diagnostics.Debug.WriteLine($"  - Procedimiento: {error.Procedure}");
                    }
                    throw; // Lanzar la excepción para depuración
                }
                catch (Exception ex)
                {
                    // Capturar y mostrar errores generales
                    System.Diagnostics.Debug.WriteLine($"Error general: {ex.Message}");
                    throw; // Lanzar la excepción para depuración
                }
            }
        }
    }
}
