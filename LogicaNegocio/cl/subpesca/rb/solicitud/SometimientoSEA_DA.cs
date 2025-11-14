using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class SometimientoSEA_DA
    {
        Logger logger = new Logger();

        public bool GuardarSometimientoSEA(SometimientoSEA sometimiento)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSometimientoSEA";

                if (sometimiento.idSometimientoSEA > 0){
                    cnn.parametros.Add("@idSometimientoSEA", sometimiento.idSometimientoSEA);
                }
                cnn.parametros.Add("@idSolConcesion", sometimiento.idSolConcesion);
                if (sometimiento.fechaSometimiento!=null && sometimiento.fechaSometimiento != default(DateTime)) {
                    cnn.parametros.Add("@fechaSometimiento", sometimiento.fechaSometimiento);
                }
                if (sometimiento.nombreProySEA!=null && !sometimiento.nombreProySEA.Equals("")) {
                    cnn.parametros.Add("@nombreProySEA", sometimiento.nombreProySEA);
                }
                if (sometimiento.idSEA != null && !sometimiento.idSEA.Equals(""))
                {
                    cnn.parametros.Add("@idSEA", sometimiento.idSEA);
                }
                if (sometimiento.numCarpetaSEA != null && !sometimiento.numCarpetaSEA.Equals("")){
                    cnn.parametros.Add("@numCarpetaSEA", sometimiento.numCarpetaSEA);
                }
                if (sometimiento.linkSEA!=null && !sometimiento.linkSEA.Equals("")) {
                    cnn.parametros.Add("@linkSEA", sometimiento.linkSEA);
                }
                if (sometimiento.categoriaSEA!=null && !sometimiento.categoriaSEA.Equals("")) {
                    cnn.parametros.Add("@categoriaCPS", sometimiento.categoriaSEA);
                }
                if (sometimiento.consultorAmbiental!=null && sometimiento.consultorAmbiental.numInscripcion>0) {
                    cnn.parametros.Add("@numInscripConsAmb", sometimiento.consultorAmbiental.numInscripcion);
                }
                if (sometimiento.entidadAnalisis!=null && sometimiento.entidadAnalisis.numEntidad>0) {
                    cnn.parametros.Add("@numEntidadAnalisis", sometimiento.entidadAnalisis.numEntidad);
                }
                if (sometimiento.muestreadorAmb != null && sometimiento.muestreadorAmb.numEntidad>0){
                    cnn.parametros.Add("@numMuestreadorAmb", sometimiento.muestreadorAmb.numEntidad);
                }
                
                DataTable dt = cnn.Execute();
                sometimiento.idSometimientoSEA = Convert.ToInt32(dt.Rows[0]["idSometimientoSEA"]);

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };

        }


        public SometimientoSEA ObtenerSometimientoSEA(int idSolConcesion)
        {
            try
            {

                SometimientoSEA sometimientoSEA = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelrbSometimientoSEA";
                cnn.parametros.Add("@idSolConcesion", idSolConcesion);
               
                DataTable dt = cnn.Execute();
                
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        sometimientoSEA = new SometimientoSEA();
                        sometimientoSEA.idSometimientoSEA = Convert.ToInt32(row["idSometimientoSEA"]);
                        sometimientoSEA.idSolConcesion = Convert.ToInt32(row["idSolConcesion"]);


                        if (!row.IsNull("fechaSometimiento") && Convert.ToDateTime(row["fechaSometimiento"]) != default(DateTime))
                        {
                            sometimientoSEA.fechaSometimiento = Convert.ToDateTime(row["fechaSometimiento"]);
                        }

                        if (!row.IsNull("nombreProySEA"))
                        {
                            sometimientoSEA.nombreProySEA = row["nombreProySEA"].ToString();
                        }

                        if (!row.IsNull("idSEA"))
                        {
                            sometimientoSEA.idSEA = row["idSEA"].ToString();
                        }

                        if (!row.IsNull("numCarpetaSEA"))
                        {
                            sometimientoSEA.numCarpetaSEA = row["numCarpetaSEA"].ToString();
                        }

                        if (!row.IsNull("linkSEA"))
                        {
                            sometimientoSEA.linkSEA = row["linkSEA"].ToString();
                        }

                        if (!row.IsNull("categoriaCPS"))
                        {
                            sometimientoSEA.categoriaSEA = row["categoriaCPS"].ToString();
                        }

                        if (!row.IsNull("numInscripConsAmb"))
                        {
                            ConsultorAmbiental consultorAmbiental = new ConsultorAmbiental();
                            consultorAmbiental.numInscripcion = Convert.ToInt32(row["numInscripConsAmb"]);
                            sometimientoSEA.consultorAmbiental = consultorAmbiental;
                        }

                        if (!row.IsNull("numEntidadAnalisis"))
                        {
                            EntidadMuestreador entidadAnalisis = new EntidadMuestreador();
                            entidadAnalisis.numEntidad = Convert.ToInt32(row["numEntidadAnalisis"]);
                            sometimientoSEA.entidadAnalisis = entidadAnalisis;
                        }

                        if (!row.IsNull("numMuestreadorAmb"))
                        {
                            EntidadMuestreador muestreadorAmb = new EntidadMuestreador();
                            muestreadorAmb.numEntidad = Convert.ToInt32(row["numMuestreadorAmb"]);
                            sometimientoSEA.muestreadorAmb = muestreadorAmb;
                        }
                       
                    }
                }

                return sometimientoSEA;
                

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };

        }

        
    }
}
