using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Data;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class ProgrProduccionDA
    {

        Logger logger = new Logger();

        public bool GuardarProgrProduccion(ProgrProduccionPT progrProduccion, int idUsuario, int idSolConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbProgrProduccion";
                cnn.parametros.Add("@idProgrProd", progrProduccion.idProgrProduccion);
                cnn.parametros.Add("@idProyectoTecnico", progrProduccion.idProyectoTecnico);
                if (progrProduccion.especie!=null && progrProduccion.especie.id > 0)
                {
                    cnn.parametros.Add("@idEspecie", progrProduccion.especie.id);
                }
                if (progrProduccion.grupo!=null && progrProduccion.grupo.id > 0)
                {
                    cnn.parametros.Add("@idGrupo", progrProduccion.grupo.id);
                }
                cnn.parametros.Add("@idTipoNroKilos", progrProduccion.tipoUnidProgramaProd.id);
                cnn.parametros.Add("@idTipoRango", progrProduccion.tipoPesoPromEjemplares.id);
                cnn.parametros.Add("@produccionUltimoAnio", progrProduccion.produccionUltimoAnio);
                if(progrProduccion.pesoPromSR>0){
                    cnn.parametros.Add("@pesoPromedio", progrProduccion.pesoPromSR);
                }
                if(progrProduccion.pesoPromR1>0){
                    cnn.parametros.Add("@pesoPromMin", progrProduccion.pesoPromR1);
                }
                if (progrProduccion.pesoPromR2>0)
                {
                    cnn.parametros.Add("@pesoPromMax", progrProduccion.pesoPromR2);
                }
                
                cnn.parametros.Add("@controlaProd", false);

                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                if (progrProduccion.tipoAnio!=null && progrProduccion.tipoAnio.id > 0)
                {
                    cnn.parametros.Add("@idTipoProgrPorAnio", progrProduccion.tipoAnio.id);
                }

                DataTable dt = cnn.Execute();
                progrProduccion.idProgrProduccion = Convert.ToInt32(dt.Rows[0]["idProgrProd"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarProgrProduccion(int idProgrProd, int idProyectoTecnico, int idUsuario)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbProgrProduccion";
                cnn.parametros.Add("@idProgrProd", idProgrProd);
                if (idProyectoTecnico > 0)
                {
                    cnn.parametros.Add("@idProyectoTecnico", idProyectoTecnico);
                }
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }

                DataTable dt = cnn.Execute();
                
                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }


        public bool GuardarValorProgrProduccion(ValorParametroAnioPT valorProgrProd)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbValorProgrProduccion";
                cnn.parametros.Add("@idRegistroValor", valorProgrProd.idRegistro);
                cnn.parametros.Add("@idProgrProd", valorProgrProd.idclaveParametro);
                cnn.parametros.Add("@valor", valorProgrProd.valorProgrProd);
                cnn.parametros.Add("@anio", valorProgrProd.anio);
               
                DataTable dt = cnn.Execute();
                valorProgrProd.idRegistro = Convert.ToInt32(dt.Rows[0]["idRegistroValor"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarValorProgrProduccion(int idProgrProd, int idUsuario, int idSolConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbValorProgrProduccion";
                cnn.parametros.Add("@idProgrProd", idProgrProd);
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                if (idSolConcesion > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolConcesion);
                }
                
                DataTable dt = cnn.Execute();
                
                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public List<ProgrProduccionPT> ListarProgrProduccion(int idProyTecnico, int idProgrProd)
        {
            try
            {
                
                List<ProgrProduccionPT> resp = new List<ProgrProduccionPT>();
                ProgrProduccionPT progrProd = null;

                int idProgProd = 0;
                int idProgProdAux = 0;
                int indes = 0;
                ValorParametroAnioPT valorAnio = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbProgrProduccion";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);

                if (idProgrProd>0)
                {
                    cnn.parametros.Add("@idProgrProd", idProgrProd);
                }

                DataTable dt = cnn.Execute();

                
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        idProgProd = Convert.ToInt32(row["idProgrProd"]);
                        if (idProgProd != idProgProdAux)
                        {
                            progrProd = new ProgrProduccionPT();
                            progrProd.idProgrProduccion = Convert.ToInt32(row["idProgrProd"]);
                            progrProd.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);
                            if (!row.IsNull("idEspecie"))
                            {
                                progrProd.especie = new ParametroGenerico(Convert.ToInt32(row["idEspecie"]), row["EspecieCultivo"].ToString());
                            }
                            if (!row.IsNull("idGrupo"))
                            {
                                progrProd.grupo = new ParametroGenerico(Convert.ToInt32(row["idGrupo"]), row["GrupoEspecie"].ToString());
                            }

                            progrProd.tipoUnidProgramaProd = new ParametroGenerico(Convert.ToInt32(row["idTipoNroKilos"]), row["nombreTipoNroKilos"].ToString());
                            progrProd.tipoPesoPromEjemplares = new ParametroGenerico(Convert.ToInt32(row["idTipoRango"]), row["nombreTipoRango"].ToString());
                            progrProd.produccionUltimoAnio = Convert.ToSingle(row["produccionUltimoAnio"]);
                            if (!row.IsNull("pesoPromedio"))
                            {
                                progrProd.pesoPromSR = Convert.ToSingle(row["pesoPromedio"]);
                            }
                            if (!row.IsNull("pesoPromMin"))
                            {
                                progrProd.pesoPromR1 = Convert.ToSingle(row["pesoPromMin"]);
                            }
                            if (!row.IsNull("pesoPromMax"))
                            {
                                progrProd.pesoPromR2 = Convert.ToSingle(row["pesoPromMax"]);
                            }
                            if (!row.IsNull("idTipoProgrPorAnio"))
                            {
                                progrProd.tipoAnio = new ParametroGenerico(Convert.ToInt32(row["idTipoProgrPorAnio"]), row["nombreTipoProdAnio"].ToString());
                            }

                            progrProd.aniosProgrProd = new List<ValorParametroAnioPT>();
                            progrProd.accion = accion.LISTADO;
                            progrProd.index = indes;

                            resp.Add(progrProd);
                            indes++;
                        }
                        valorAnio = new ValorParametroAnioPT();
                        valorAnio.idRegistro = Convert.ToInt32(row["idRegistroValor"]);
                        valorAnio.idclaveParametro = Convert.ToInt32(row["idProgrProd"]);
                        valorAnio.anio = Convert.ToInt32(row["anio"]);
                        valorAnio.valorProgrProd = Convert.ToSingle(row["valor"]);

                        progrProd.aniosProgrProd.Add(valorAnio);
                        idProgProdAux= idProgProd;
                       
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

        public ProgrProduccionPT ObtenerProgrProduccion(int idProyTecnico, int idProgrProd)
        {
            try
            {

                ProgrProduccionPT progrProd = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbProgrProduccion";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);

                if (idProgrProd > 0)
                {
                    cnn.parametros.Add("@idProgrProd", idProgrProd);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        progrProd = new ProgrProduccionPT();
                        progrProd.idProgrProduccion = Convert.ToInt32(row["idProgrProd"]);
                        progrProd.idProyectoTecnico = Convert.ToInt32(row["idProyectoTecnico"]);
                        if (!row.IsNull("idEspecie"))
                        {
                            progrProd.especie = new ParametroGenerico(Convert.ToInt32(row["idEspecie"]), row["EspecieCultivo"].ToString());
                        }
                        if (!row.IsNull("idGrupo"))
                        {
                            progrProd.grupo = new ParametroGenerico(Convert.ToInt32(row["idGrupo"]), row["GrupoEspecie"].ToString());
                        }

                        progrProd.tipoUnidProgramaProd = new ParametroGenerico(Convert.ToInt32(row["idTipoNroKilos"]), row["nombreTipoNroKilos"].ToString());
                        progrProd.tipoPesoPromEjemplares = new ParametroGenerico(Convert.ToInt32(row["idTipoRango"]), row["nombreTipoRango"].ToString());
                        progrProd.produccionUltimoAnio = Convert.ToSingle(row["produccionUltimoAnio"]);
                        if (!row.IsNull("pesoPromedio"))
                        {
                            progrProd.pesoPromSR = Convert.ToSingle(row["pesoPromedio"]);
                        }
                        if (!row.IsNull("pesoPromMin"))
                        {
                            progrProd.pesoPromR1 = Convert.ToSingle(row["pesoPromMin"]);
                        }
                        if (!row.IsNull("pesoPromMax"))
                        {
                            progrProd.pesoPromR2 = Convert.ToSingle(row["pesoPromMax"]);
                        }
                        if (!row.IsNull("idTipoProgrPorAnio"))
                        {
                            progrProd.tipoAnio = new ParametroGenerico(Convert.ToInt32(row["idTipoProgrPorAnio"]), row["nombreTipoProdAnio"].ToString());
                        }

                    }
                }

                return progrProd;
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
