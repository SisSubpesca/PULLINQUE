using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class OperadorDA
    {

        public Logger Log { get; set; }
        
        public OperadorDA()
        { 
            this.Log = new Logger();
        }

        public Operador ObtenerOperadorTitular(int rutTitular, int rutOperador)
        {
            try
            {
                Operador operador = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadores";
                if (rutTitular > 0)
                {
                    cnn.parametros.Add("@rutTitular", rutTitular);
                }
                if (rutOperador > 0)
                {
                    cnn.parametros.Add("@rutOperador", rutOperador);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        operador = new Operador();
                        operador.idOperador = Convert.ToInt32(row["idOperador"]);
                        operador.titular = new Solicitante();
                        operador.titular.rut = operador.idOperador = Convert.ToInt32(row["rutTitular"]);
                        operador.titular.dv = Convert.ToChar(row["dvTitular"]);
                        operador.titular.nombreSolicitante = row["nombreTitu"].ToString();
                        operador.operador = new Solicitante();
                        operador.operador.rut = operador.idOperador = Convert.ToInt32(row["rutOperador"]);
                        operador.operador.dv = Convert.ToChar(row["dvOp"]);
                        operador.operador.nombreSolicitante = row["nombreOp"].ToString();
                        operador.operador.fechaInicioVigencia = Convert.ToDateTime(row["fechaInicioRel"]);
                        if (row.IsNull("fechaFinRel"))
                        {
                            operador.operador.fechaFinVigencia = Convert.ToDateTime(row["fechaFinRel"]);
                        }
                        operador.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                        
                    }
                }

                return operador;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<Operador> ListarOperadorTitular(int rutTitular, int rutOperador)
        {
            try
            {
                List<Operador> resp = new List<Operador>();
                Operador operador = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadores";
                if(rutTitular>0){
                    cnn.parametros.Add("@rutTitular", rutTitular);
                }
                if(rutOperador>0){
                    cnn.parametros.Add("@rutOperador", rutOperador);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        operador = new Operador();
                        operador.idOperador =  Convert.ToInt32(row["idOperador"]);
                        operador.titular = new Solicitante();
                        operador.titular.rut = operador.idOperador =  Convert.ToInt32(row["rutTitular"]);
                        operador.titular.dv = Convert.ToChar(row["dvTitular"]);
                        operador.titular.nombreSolicitante = row["nombreTitu"].ToString();
                        operador.operador = new Solicitante();
                        operador.operador.rut = operador.idOperador =  Convert.ToInt32(row["rutOperador"]);
                        operador.operador.dv = Convert.ToChar(row["dvOp"]);
                        operador.operador.nombreSolicitante = row["nombreOp"].ToString();
                        operador.operador.fechaInicioVigencia = Convert.ToDateTime(row["fechaInicioRel"]);
                        if (!row.IsNull("fechaFinRel"))
                        {
                            operador.operador.fechaFinVigencia = Convert.ToDateTime(row["fechaFinRel"]);
                        }
                        operador.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());

                        //operador.archivoAsoc = new ArchivoBinarioEspecial();
                        //operador.archivoAsoc.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //operador.archivoAsoc.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        //operador.archivoAsoc.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        //operador.archivoAsoc.formato = Convert.ToString(row["formato"]);
                        //operador.archivoAsoc.bytes = (byte[])row["contenido"];

                        operador.accion = accion.LISTADO;
                        operador.index = i;

                        resp.Add(operador);

                        i++;
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

        public Operador ObtenerOperador(int rutTitular, string nombre)
        {
            try
            {
                Operador operador = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperador";
                if (rutTitular > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutTitular);
                }
                if (nombre!=null && !nombre.Equals(""))
                {
                    cnn.parametros.Add("@nombre", nombre);
                }
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        operador = new Operador();
                        operador.operador = new Solicitante();
                        operador.operador.rut = operador.idOperador = Convert.ToInt32(row["rutOperador"]);
                        operador.operador.dv = Convert.ToChar(row["digitoVerificador"]);
                        operador.operador.nombreSolicitante = row["nombre"].ToString();
                        operador.operador.estadoPersona = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                    }
                }

                return operador;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<Operador> ListarOperador(int rutTitular, string nombre)
        {
            try
            {
                List<Operador> resp = new List<Operador>();
                Operador operador = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperador";
                if (rutTitular > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutTitular);
                }
                if (nombre != null && !nombre.Equals(""))
                {
                    cnn.parametros.Add("@nombre", nombre);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        operador = new Operador();
                        operador.operador = new Solicitante();
                        operador.operador.rut = operador.idOperador = Convert.ToInt32(row["rutOperador"]);
                        operador.operador.dv = Convert.ToChar(row["digitoVerificador"]);
                        operador.operador.nombreSolicitante = row["nombre"].ToString();
                        //if (!row.IsNull("numeroCI"))
                        //{
                        //    operador.operador.numeroControlIngreso = Convert.ToInt32(row["numeroCI"]);
                        //}
                        //if (!row.IsNull("fechaCI"))
                        //{
                        //    operador.operador.fechaControlIngreso = Convert.ToDateTime(row["fechaCI"]);
                        //}
                        operador.operador.estadoPersona = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());

                        resp.Add(operador);
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

        public bool GuardarOperador(Solicitante operador)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbOperador";
                cnn.parametros.Add("@rutOperador", operador.rut);
                cnn.parametros.Add("@idEstadoVigencia", operador.idEstadoAsociacion);
                cnn.parametros.Add("@digitoVerificador", operador.dv);
                
                DataTable dt = cnn.Execute();
                operador.rut = Convert.ToInt32(dt.Rows[0]["rutOperador"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarOperadorTitular(Operador operador)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbOperadores";
                cnn.parametros.Add("@idOperador", operador.idOperador);
                cnn.parametros.Add("@rutTitular", operador.titular.rut);
                cnn.parametros.Add("@rutOperador", operador.rutOperador);
                if (operador.fechaFinRel != null && operador.fechaFinRel!=default(DateTime))
                {
                    cnn.parametros.Add("@fechaFinRel", operador.fechaFinRel);
                }
                                
                DataTable dt = cnn.Execute();
                operador.idOperador = Convert.ToInt32(dt.Rows[0]["idOperador"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarOperadorArchivo(int rutOperador, int idArchivo, int idTipoDoc, int numCI, DateTime fechaCI)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbOperadorArchivo";
                cnn.parametros.Add("@rutOperador", rutOperador);
                cnn.parametros.Add("@idArchivoBinSC", idArchivo);
                cnn.parametros.Add("@idTipo", idTipoDoc);
                if(numCI>0){
                    cnn.parametros.Add("@numeroCI", numCI);
                }
                if (fechaCI != null && fechaCI != default(DateTime))
                {
                    cnn.parametros.Add("@fechaCI", fechaCI);
                }

                DataTable dt = cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarOperadorArchivo(int idOperador, int idArchivo)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbOperadorArchivo";
                cnn.parametros.Add("@rutOperador", idOperador);
                if (idArchivo > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivo);
                }

                DataTable dt = cnn.Execute();

                return true;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public List<ArchivosAdjOperador> ListarArchivosAdjuntoPersona(int rutPersona, int idArchivoBinSC)
        {
            try
            {
                List<ArchivosAdjOperador> resp = new List<ArchivosAdjOperador>();
                ArchivosAdjOperador archivoOpLegal = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadorArchivo";
                cnn.parametros.Add("@rutPersona", rutPersona);

                if (idArchivoBinSC > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        archivoOpLegal = new ArchivosAdjOperador();
                        archivoOpLegal.rutOperador = Convert.ToInt32(row["rutOperador"]);
                        archivoOpLegal.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());
                        archivoOpLegal.archivoBinario = new ArchivoBinarioEspecial();
                        archivoOpLegal.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        archivoOpLegal.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoOpLegal.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoOpLegal.archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoOpLegal.archivoBinario.bytes = (byte[])row["contenido"];
                        if (!row.IsNull("numeroCI"))
                        {
                            archivoOpLegal.numCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if (!row.IsNull("fechaCI"))
                        {
                            archivoOpLegal.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        archivoOpLegal.accion = accion.LISTADO;
                        archivoOpLegal.index = i;

                        resp.Add(archivoOpLegal);
                        i++;
                    }
                }

                return resp;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            };
        }

        public ArchivosAdjOperador ObtenerArchivosAdjuntoPersona(int rutPersona, int idArchivoAdjunto)
        {
            try
            {

                ArchivosAdjOperador archivoOp = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadorArchivo";
                cnn.parametros.Add("@rutPersona", rutPersona);
                if (idArchivoAdjunto>0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoAdjunto);
                }
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        archivoOp = new ArchivosAdjOperador();
                        archivoOp.rutOperador = Convert.ToInt32(row["rutOperador"]);
                        archivoOp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());

                        archivoOp.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        archivoOp.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoOp.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoOp.archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoOp.archivoBinario.bytes = (byte[])row["contenido"];

                        if (!row.IsNull("numeroCI"))
                        {
                            archivoOp.numCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if (!row.IsNull("fechaCI"))
                        {
                            archivoOp.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }

                    }
                }
                return archivoOp;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            };
        }

        public bool GuardarNombreOperador(NombrePersona nomPersona)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbNombreOperador";
                cnn.parametros.Add("@rut", nomPersona.rut);
                if (nomPersona.idNomPersona > 0)
                {
                    cnn.parametros.Add("@idNombreOperador", nomPersona.idNomPersona);
                }
                cnn.parametros.Add("@idEstadoActual", nomPersona.estadoActual.id);
                cnn.parametros.Add("@nombre", nomPersona.nombre);
                
                DataTable dt = cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarContactoOperador(int rutOperador, int idContacto)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbContactoOperador";
                cnn.parametros.Add("@idContacto", idContacto);
                cnn.parametros.Add("@rutOperador", rutOperador);
                
                DataTable dt = cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public List<Contacto> ListarContactoOperador(int rutPersona, int idContacto)
        {
            try
            {
                Contacto claseResp = null;
                List<Contacto> resp = new List<Contacto>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbContactoOperador";
                if (rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutPersona);
                }
                if (idContacto > 0)
                {
                    cnn.parametros.Add("@idContacto", idContacto);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        claseResp = new Contacto();
                        claseResp.idContacto = Convert.ToInt32(row["idContacto"]);
                        claseResp.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
                        claseResp.valorContacto = row["valorContacto"].ToString();
                        if (!row.IsNull("detalle"))
                        {
                            claseResp.detalle = row["detalle"].ToString();
                        }
                        //if (!row.IsNull("idRegion"))
                        //{
                        //    claseResp.region = new Region();
                        //    claseResp.region.id_region = Convert.ToInt32(row["idRegion"]);
                        //    claseResp.region.region = row["region"].ToString();    
                        //}

                        claseResp.accion = accion.LISTADO;
                        claseResp.index = i;

                        resp.Add(claseResp);
                        i++;
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

        public bool GuardarOperadorMatrizSuc(int rutOperador, int idMatrizSuc)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbOperadorMatrizSuc";
                cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                cnn.parametros.Add("@rutOperador", rutOperador);

                DataTable dt = cnn.Execute();

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public List<MatrizSucursal> ListarOperadorMatrizSuc(int rutOperador)
        {
            try
            {
                MatrizSucursal claseResp = null;
                List<MatrizSucursal> resp = new List<MatrizSucursal>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadorMatrizSuc";
                cnn.parametros.Add("@rutOperador", rutOperador);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        claseResp = new MatrizSucursal();

                        claseResp.idMatrizSuc = Convert.ToInt32(row["idMatrizSuc"]);

                        if (!row.IsNull("idRegion"))
                        {
                            claseResp.region = new Region();
                            claseResp.region.id_region = Convert.ToInt32(row["idRegion"]);
                            claseResp.region.region = row["Region"].ToString();
                        }

                        if (!row.IsNull("idComuna"))
                        {
                            claseResp.region.comuna = new Comuna();
                            claseResp.region.comuna.id_comuna = Convert.ToInt32(row["idComuna"]);
                            claseResp.region.comuna.comuna = row["Comuna"].ToString();
                        }

                        claseResp.direccion = row["direccion"].ToString();
                        
                        claseResp.accion = accion.LISTADO;
                        claseResp.index = i;

                        resp.Add(claseResp);

                        i++;

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

        public bool ActualizarOperadorEstado(int rutOperador, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbOperadorEstado";
                cnn.parametros.Add("@rutOperador", rutOperador);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

                DataTable dt = cnn.Execute();
                rutOperador = Convert.ToInt32(dt.Rows[0]["rutOperador"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarOperadorTitularEstado(int rutPersonaTitular, int rutPersonaOperador, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRepLegalTitularEstado";
                cnn.parametros.Add("@rutPersonaTitular", rutPersonaTitular);
                cnn.parametros.Add("@rutPersonaOperador", rutPersonaOperador);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

                DataTable dt = cnn.Execute();
                rutPersonaOperador = Convert.ToInt32(dt.Rows[0]["@rutPersonaOperador"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarOperador(int rutOperador)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbOperador";
                cnn.parametros.Add("@rutOperador", rutOperador);
                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarOperadorTitular(int rutTitular, int rutOperador)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbOperadorTitular";
                cnn.parametros.Add("@rutTitular", rutTitular);
                cnn.parametros.Add("@rutOperador", rutOperador);
                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public NombrePersona ObtenerNombreOperador(int rut, int idEstadoActual)
        {
            try
            {
                NombrePersona persona = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbNombreOperador";
                if (rut > 0)
                {
                    cnn.parametros.Add("@rut", rut);
                }
                if (idEstadoActual > 0)
                {
                    cnn.parametros.Add("@idEstadoActual", idEstadoActual);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        persona = new NombrePersona();
                        persona.idNomPersona = Convert.ToInt32(row["idNomOperador"]);
                        persona.rut = Convert.ToInt32(row["rutOperador"]);

                        //if (row.IsNull("idArchivoBinSC"))
                        //{
                        //    persona.archivo = new ArchivoBinarioEspecial();
                        //    persona.archivo.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //}
                        persona.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                        persona.nombre = row["nombre"].ToString();
                        //if(!row.IsNull("numeroCI")){
                        //    persona.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        //}
                        //if (!row.IsNull("fechaCI"))
                        //{
                        //    persona.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        //}
                        
                        persona.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);

                    }
                }

                return persona;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<NombrePersona> ListarNombrePersona(int rut, int idEstadoActual)
        {
            try
            {
                List<NombrePersona> resp = new List<NombrePersona>();
                NombrePersona persona = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbNombreOperador";
                if (rut > 0)
                {
                    cnn.parametros.Add("@rut", rut);
                }
                if (idEstadoActual > 0)
                {
                    cnn.parametros.Add("@idEstadoActual", idEstadoActual);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        persona = new NombrePersona();
                        persona.idNomPersona = Convert.ToInt32(row["idNomOperador"]);
                        persona.rut = Convert.ToInt32(row["rutOperador"]);

                        //if (!row.IsNull("idArchivoBinSC"))
                        //{
                        //    persona.archivo = new ArchivoBinarioEspecial();
                        //    persona.archivo.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //}
                        persona.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                        persona.nombre = row["nombre"].ToString();
                        //if (!row.IsNull("numeroCI"))
                        //{
                        //    persona.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        //}
                        //if (!row.IsNull("fechaCI"))
                        //{
                        //    persona.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        //}
                        persona.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);
                        resp.Add(persona);
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

        public bool EliminarContactoOperador(int rutPersona, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbContactoOperador";
                cnn.parametros.Add("@rutOperador", rutPersona);
                if (idContacto > 0)
                {
                    cnn.parametros.Add("@idContacto", idContacto);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarOperadorMatrizSuc(int idMatrizSuc, int rutOperador)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbOperadorMatrizSuc";
                cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                if (rutOperador > 0)
                {
                    cnn.parametros.Add("@rutOperador", rutOperador);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarNombreOperador(int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbNombreOperador";
                cnn.parametros.Add("@rutOperador", rutPersona);
                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public MatrizSucursal ObtenerOperadorMatrizSuc_filtros(int rutOperador, int idMatrizSuc)
        {
            try
            {
                MatrizSucursal claseResp = null;
                List<MatrizSucursal> resp = new List<MatrizSucursal>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadorMatrizSuc_filtros";
                if (rutOperador>0)
                {
                    cnn.parametros.Add("@rutOperador", rutOperador);
                }
                if (idMatrizSuc>0)
                {
                    cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                }
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        claseResp = new MatrizSucursal();

                        claseResp.idMatrizSuc = Convert.ToInt32(row["idMatrizSuc"]);
                        claseResp.region = new Region();
                        claseResp.region.id_region = Convert.ToInt32(row["idRegion"]);
                        claseResp.region.region = row["Region"].ToString();
                        claseResp.region.comuna = new Comuna();
                        claseResp.region.comuna.id_comuna = Convert.ToInt32(row["idComuna"]);
                        claseResp.region.comuna.comuna = row["Comuna"].ToString();
                        claseResp.direccion = row["direccion"].ToString();
                        claseResp.persona.rutPersona = Convert.ToInt32(row["rutOperador"]);
                        claseResp.persona.dvPersona = Convert.ToChar(row["digitoVerificador"]);
                        claseResp.persona.nombreSolicitante = row["nombre"].ToString();

                    }
                }

                return claseResp;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public Operador ObtenerOperadorTitularId(int idOperador)
        {
            try
            {
                Operador operador = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbOperadoresId";

                cnn.parametros.Add("@idOperador", idOperador);


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        operador = new Operador();
                        operador.idOperador = Convert.ToInt32(row["idOperador"]);
                        operador.titular = new Solicitante();
                        operador.titular.rut = Convert.ToInt32(row["rutTitular"]);
                        operador.titular.dv = Convert.ToChar(row["dvTitular"]);
                        operador.titular.nombreSolicitante = row["nombreTitu"].ToString();
                        operador.operador = new Solicitante();
                        operador.operador.rut = Convert.ToInt32(row["rutOperador"]);
                        operador.operador.dv = Convert.ToChar(row["dvOp"]);
                        operador.operador.nombreSolicitante = row["nombreOp"].ToString();
                        if (!row.IsNull("fechaInicioRel"))
                        {
                            operador.operador.fechaInicioVigencia = Convert.ToDateTime(row["fechaInicioRel"]);
                        }
                        if (!row.IsNull("fechaFinRel"))
                        {
                            operador.operador.fechaFinVigencia = Convert.ToDateTime(row["fechaFinRel"]);
                        }
                        operador.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());

                    }
                }

                return operador;
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
