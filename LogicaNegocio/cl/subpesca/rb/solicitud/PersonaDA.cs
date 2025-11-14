using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class PersonaDA
    {

        Logger logger = new Logger();

        public List<Persona> ListarTitularesDetalleSector(int idSolicitud)
        {
            try
            {
                Persona pers = null;
                List<Persona> resp = new List<Persona>();
                
                int rutTitular = 0;
                HashSet<int> clavesComuna = new HashSet<int>();
                HashSet<int> clavesTitular = new HashSet<int>();


                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTitularesDetalleSector";
                cnn.parametros.Add("@idSolConcesion", idSolicitud);
               
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row.IsNull("rutPersona"))
                        {
                            rutTitular = Convert.ToInt32(row["rutPersona"]);
                            if (clavesTitular.Count == 0 || !clavesTitular.Contains(rutTitular))
                            {

                                clavesTitular.Add(rutTitular);

                                pers = new Persona();
                                pers.rutPersona = Convert.ToInt32(row["rutPersona"]);
                                pers.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                                pers.nombreSolicitante = row["nombre"].ToString();
                                resp.Add(pers);
                            }
                        }

                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }
    }
}
