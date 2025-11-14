using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Diagnostics;
using System.IO;


namespace Datos.AccesoDatos
{
    public class Conexion
    {
        public string    procedimiento;
        public Hashtable parametros;

        public Conexion()
        {
            this.procedimiento = "";
            this.parametros = new Hashtable();
        }


        public DataTable Execute() 
        {
            /// <summary>Función genérica que llama a un procedimiento</summary>
            /// <param name="nombreSP">string del nombre del procedimiento</param>
            /// <param name="parametros">Hashtable con los parametros del procedimiento</param>
            /// <returns>Dataset con el resultado del procedimiento</returns>

            DataSet dataset = new DataSet();
            DataTable dt = null;
            SqlCommand cmd = new SqlCommand();
            
            try
            {
                cmd.Connection = ObtenerConexion();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedimiento;
                cmd.CommandTimeout = 400;
                if (parametros.Count > 0)
                {
                    ICollection keyColl = this.parametros.Keys;
                    foreach (string s in keyColl)
                    {

                        if (parametros[s].GetType().Equals(typeof(DateTime)))
                        {
                            cmd.Parameters.Add(s, SqlDbType.DateTime).Value = parametros[s];
                        }
                        else if (parametros[s].GetType().Equals(typeof(byte[])))
                        {
                            cmd.Parameters.Add(s, SqlDbType.VarBinary).Value = parametros[s];
                        }
                        else if (parametros[s].GetType().Equals(typeof(float)))
                        {
                            cmd.Parameters.Add(s, SqlDbType.Float).Value = parametros[s];
                        }
                        else if (parametros[s].GetType().Equals(typeof(double)))
                        {
                            cmd.Parameters.Add(s, SqlDbType.Float).Value = parametros[s];
                        }
                        else if (parametros[s].GetType().Equals(typeof(Int32)))
                        {
                            cmd.Parameters.Add(s, SqlDbType.Int).Value = parametros[s];
                        }
                        else 
                        {
                            cmd.Parameters.Add(s, SqlDbType.VarChar).Value = parametros[s];
                        }

                        
                    };
                };
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();



                //SIN  REGISTRO 
                da.Fill(ds);
                //FIN SIN REGISTRO


                
                //CON REGISTRO
                /*
                DateTime inicioQuery = DateTime.Now;
                da.Fill(ds);
                DateTime finQuery = DateTime.Now;



                System.Diagnostics.Debug.WriteLine(procedimiento);

                string nombreArchivo = "C:\\procedimientos\\procedimientos.txt";

                using (FileStream flujoArchivo = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter escritor = new StreamWriter(flujoArchivo))
                    {
                        escritor.WriteLine(procedimiento);
                    }

                }


                    
                TimeSpan ts = finQuery - inicioQuery;
                if (ts.Milliseconds > 200) //si son mas de 200 milisegundos guardamos
                {

                    if (!EventLog.SourceExists("Pullinque 4"))
                    {
                        EventLog.CreateEventSource("Pullinque 4", "Application");
                    }

                    //System.Diagnostics.Debug.WriteLine("Pullinque 4", "Duracion Query: " + procedimiento + " -> " + ts.Milliseconds.ToString());

                    String text = "";
                    IDictionaryEnumerator param = parametros.GetEnumerator();
                    while (param.MoveNext())
                    {
                       text += param.Key + "= ";
                       text += param.Value + ",\n";
                    }

                    EventLog.WriteEntry("Pullinque 4", "Duracion Query: " + ts.Milliseconds.ToString() + " -> " + procedimiento + " "  + text);
                }
                 **/
                //FIN CON REGISTRO

                


                dt = (DataTable)ds.Tables[0];
                //HttpContext.Current.Session["error"] = "";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.GetType().FullName);
                //Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
                throw ex;
            }
            finally
            {
                DescargarConexion(cmd.Connection);
            };

            return dt;

        }
                
        public SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["BDKeyAmbiental"].ConnectionString);
            try
            {
                conexion.Open();

                return conexion;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        private bool DescargarConexion(SqlConnection conexion)
        {
            try
            {
                conexion.Dispose();
            }
            catch {};

            return true;
        }

    
    }
}
