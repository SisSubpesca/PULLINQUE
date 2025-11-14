using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;

namespace LogicaNegocio.cl.subpesca.rb.modificacion
{
    public class AntecedenteModConcesionDA
    {
        Logger logger = new Logger();

        /*
        public List<AntecedenteModConcesion> ListarAntecedenteModConcesion(string tiposModificaciones)//string con data entre paréntesis, ej. (90,94)
        {
            try
            {
                int despl = 0;
                AntecedenteModConcesion antecedenteCon = null;
                List<AntecedenteModConcesion> resp = new List<AntecedenteModConcesion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbAntecedenteModificacionConcesion";

                cnn.parametros.Add("@tipoModificaciones", tiposModificaciones);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        antecedenteCon = new AntecedenteModConcesion();
                        antecedenteCon.menu = new ParametroGenerico(Convert.ToInt32(row["idMenu"]));
                        despl = Convert.ToInt32(row["despliegue"]);
                        if (despl == 0)
                        {
                            antecedenteCon.despliegue = false;
                        }
                        else {
                            antecedenteCon.despliegue = true;
                        }

                        resp.Add(antecedenteCon);

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
         */
    }
}
