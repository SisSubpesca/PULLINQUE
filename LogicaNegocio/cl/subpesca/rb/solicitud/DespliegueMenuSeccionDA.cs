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
    public class DespliegueMenuSeccionDA
    {

        public Logger Log { get; set; }
        public DespliegueMenuSeccionDA()
        {
            this.Log = new Logger();
        }

        public List<DespliegueMenuSeccion> ListarDespliegueMenuSeccion(int idMenu, int idSeccion)
        {
            try
            {
                DespliegueMenuSeccion paramAux = null;
                List<DespliegueMenuSeccion> resp = new List<DespliegueMenuSeccion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDespliegueMenuSeccion";

                if (idMenu > 0)
                {
                    cnn.parametros.Add("@idMenu", idMenu);
                }

                if (idSeccion > 0)
                {
                    cnn.parametros.Add("@idSeccion", idSeccion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        paramAux = new DespliegueMenuSeccion();
                        paramAux.idMenu = Convert.ToInt32(row["idMenu"]);
                        paramAux.idSeccion = Convert.ToInt32(row["idSeccion"]);

                        if (!row.IsNull("aplicaModAmpliacion"))
                        {
                            paramAux.aplicaModAmpliacion = Convert.ToBoolean(row["aplicaModAmpliacion"]);
                        }

                        if (!row.IsNull("aplicaModReduccion"))
                        {
                            paramAux.aplicaModReduccion = Convert.ToBoolean(row["aplicaModReduccion"]);
                        }

                        if (!row.IsNull("aplicaModEspecie"))
                        {
                            paramAux.aplicaModEspecie = Convert.ToBoolean(row["aplicaModEspecie"]);
                        }

                        if (!row.IsNull("aplicaModRegularizacion"))
                        {
                            paramAux.aplicaModRegularizacion = Convert.ToBoolean(row["aplicaModRegularizacion"]);
                        }

                        if (!row.IsNull("aplicaModPT"))
                        {
                            paramAux.aplicaModPT = Convert.ToBoolean(row["aplicaModPT"]);
                        }
                        
                        resp.Add(paramAux);
                    }
                }

                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<DespliegueMenuSeccion> ListarDespliegueMenuSeccion_UE(int idMenu, int idSeccion, int idTipoUnidEspacial)
        {
            try
            {
                DespliegueMenuSeccion paramAux = null;
                List<DespliegueMenuSeccion> resp = new List<DespliegueMenuSeccion>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDespliegueMenuSeccion_UE";

                cnn.parametros.Add("@idMenu", idMenu);

                if (idSeccion > 0)
                {
                    cnn.parametros.Add("@idSeccion", idSeccion);
                }
                cnn.parametros.Add("@idTipoUnidEspacial", idTipoUnidEspacial);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        paramAux = new DespliegueMenuSeccion();
                        paramAux.idMenu = Convert.ToInt32(row["idMenu"]);
                        paramAux.idSeccion = Convert.ToInt32(row["idSeccion"]);
                        paramAux.aplicaModAmpliacion = Convert.ToBoolean(row["aplicaModAmpliacion"]);
                        paramAux.aplicaModReduccion = Convert.ToBoolean(row["aplicaModReduccion"]);
                        paramAux.aplicaModEspecie = Convert.ToBoolean(row["aplicaModEspecie"]);
                        paramAux.aplicaModRegularizacion = Convert.ToBoolean(row["aplicaModRegularizacion"]);
                        paramAux.aplicaModPT = Convert.ToBoolean(row["aplicaModPT"]);

                        resp.Add(paramAux);
                    }
                }

                return resp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

    }
}
