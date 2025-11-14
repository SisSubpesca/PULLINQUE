using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using Datos.Entidades;
using System.Data;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class RepLegalDA
    {
        public Logger Log { get; set; }

        public RepLegalDA() {
            this.Log = new Logger();
        }

        public bool GuardarRepresentanteLegal(Solicitante repLegal)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbRepresentanteLegal";
                cnn.parametros.Add("@rutRepLegal", repLegal.rut);
                cnn.parametros.Add("@idEstadoVigencia", repLegal.idEstadoAsociacion);
                if (repLegal.tipoPersona != null && repLegal.tipoPersona.id>0)
                {
                    cnn.parametros.Add("@idTipoPersJur", repLegal.tipoPersona.id);
                }
                
                cnn.parametros.Add("@digitoVerificador", repLegal.dv);
               
                DataTable dt = cnn.Execute();
                repLegal.rut = Convert.ToInt32(dt.Rows[0]["rutRepLegal"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarRepLegalTitular(RepLegal repLegal)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbRepLegalTitular";
                cnn.parametros.Add("@idRepTitular", repLegal.idRepLegal);
                cnn.parametros.Add("@rutPersonaTitular", repLegal.titular.rut);
                cnn.parametros.Add("@rutPersonaRepLegal", repLegal.representanteLegal.rut);
                if (repLegal.fechaFinRel != null && repLegal.fechaFinRel != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFinRel", repLegal.fechaFinRel);
                }
               
                DataTable dt = cnn.Execute();
                repLegal.idRepLegal = Convert.ToInt32(dt.Rows[0]["idRepTitular"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarRepresentanteArchivo(int rutPersona, int idArchivo, int idTipoDoc, int numCI, DateTime fechaCI)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbRepresentanteArchivo";
                cnn.parametros.Add("@rutRepLegal", rutPersona);
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

        public bool EliminarRepresentanteArchivo(int rutRepLegal, int idArchivoBinSC)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRepresentanteArchivo";
                cnn.parametros.Add("@rutRepLegal", rutRepLegal);
                if (idArchivoBinSC>0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
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

        public List<ArchivosAdjRepLegal> ListarArchivosAdjuntoPersona(int rutPersona, int idArchivoBinSC)
        {
            try
            {

                List<ArchivosAdjRepLegal> resp = new List<ArchivosAdjRepLegal>();
                ArchivosAdjRepLegal archivoRepLegal = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepresentanteArchivo";
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

                        archivoRepLegal = new ArchivosAdjRepLegal();
                        archivoRepLegal.rutRepLegal = Convert.ToInt32(row["rutRepLegal"]);
                        archivoRepLegal.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());
                        /*
                        archivoRepLegal.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        archivoRepLegal.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoRepLegal.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoRepLegal.archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoRepLegal.archivoBinario.bytes = (byte[])row["contenido"];
                        */
                        archivoRepLegal.archivoBinario = new ArchivoBinarioEspecial();
                        archivoRepLegal.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        archivoRepLegal.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoRepLegal.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoRepLegal.archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoRepLegal.archivoBinario.bytes = (byte[])row["contenido"];
                        if(!row.IsNull("numeroCI")){
                            archivoRepLegal.numCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if(!row.IsNull("fechaCI")){
                            archivoRepLegal.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }
                        archivoRepLegal.accion = accion.LISTADO;
                        archivoRepLegal.index = i;

                        resp.Add(archivoRepLegal);
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

        public ArchivosAdjRepLegal ObtenerArchivosAdjuntoPersona(int idRepTitular, int idArchivoAdjunto)
        {
            try
            {

                ArchivosAdjRepLegal archivoRepLegal = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepresentanteArchivo";
                cnn.parametros.Add("@rutPersona", idRepTitular);
                if (idArchivoAdjunto>0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoAdjunto);
                }
                
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        archivoRepLegal = new ArchivosAdjRepLegal();
                        archivoRepLegal.rutRepLegal = Convert.ToInt32(row["rutRepLegal"]);
                        archivoRepLegal.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());
                        
                        /*
                        archivoRepLegal.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        archivoRepLegal.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoRepLegal.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoRepLegal.archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoRepLegal.archivoBinario.bytes = (byte[])row["contenido"];
                        */
                        archivoRepLegal.archivoBinario.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        archivoRepLegal.archivoBinario.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        archivoRepLegal.archivoBinario.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        archivoRepLegal.archivoBinario.formato = Convert.ToString(row["formato"]);
                        archivoRepLegal.archivoBinario.bytes = (byte[])row["contenido"];
                        if (!row.IsNull("numeroCI"))
                        {
                            archivoRepLegal.numCI = Convert.ToInt32(row["numeroCI"]);
                        }
                        if (!row.IsNull("fechaCI"))
                        {
                            archivoRepLegal.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        }

                    }
                }
                return archivoRepLegal;

            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            };
        }

        public bool GuardarNombreRepresentante(NombrePersona nomPersona)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbNombreRepresentante";
                cnn.parametros.Add("@rut", nomPersona.rut);
                if (nomPersona.idNomPersona > 0)
                {
                    cnn.parametros.Add("@idNombreRepresentante", nomPersona.idNomPersona);
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

        public RepLegal ObtieneRepresentanteTitular(int rutPersonaTitular, int rutPersonaRepLegal)
        {
            try
            {
                RepLegal repLegalT = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepLegalTitular";
                if(rutPersonaTitular>0){
                    cnn.parametros.Add("@rutPersonaTitular", rutPersonaTitular);
                }
                if(rutPersonaRepLegal>0){
                    cnn.parametros.Add("@rutPersonaRepLegal", rutPersonaRepLegal);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        repLegalT = new RepLegal() ;
                        repLegalT.idRepLegal = Convert.ToInt32(row["idRepTitular"]);
                        repLegalT.representanteLegal = new Solicitante();
                        repLegalT.representanteLegal.rut = Convert.ToInt32(row["rutPersonaRepLegal"]);
                        repLegalT.representanteLegal.dv = Convert.ToChar(row["digitoVerificador"]);

                        if (!row.IsNull("idTipoPersJur"))
                        {
                            repLegalT.representanteLegal.tipoPersona = new ParametroGenerico(Convert.ToInt32(row["idTipoPersJur"]), row["nombreTipo"].ToString());
                        }

                        repLegalT.representanteLegal.nombreSolicitante = row["nombre"].ToString();
                        repLegalT.representanteLegal.fechaInicioVigencia = Convert.ToDateTime(row["fechaInicioRel"]);
                        if (!row.IsNull("fechaFinRel"))
                        {
                            repLegalT.representanteLegal.fechaFinVigencia = Convert.ToDateTime(row["fechaFinRel"]);
                        }
                        repLegalT.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                        //repLegalT.archivoAsoc = new ArchivoBinarioEspecial();
                        //repLegalT.archivoAsoc.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //repLegalT.archivoAsoc.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        //repLegalT.archivoAsoc.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        //repLegalT.archivoAsoc.formato = Convert.ToString(row["formato"]);
                        //repLegalT.archivoAsoc.bytes = (byte[])row["contenido"];
                    }
                }

                return repLegalT;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<RepLegal> ListarRepresentanteTitular(int rutPersonaTitular)
        {
            try
            {
                List<RepLegal> resp = new List<RepLegal>();
                RepLegal repLegalT = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepLegalTitular";
                cnn.parametros.Add("@rutPersonaTitular", rutPersonaTitular);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        repLegalT = new RepLegal();
                        repLegalT.idRepLegal = Convert.ToInt32(row["idRepTitular"]);
                        repLegalT.representanteLegal = new Solicitante();
                        repLegalT.representanteLegal.rut = Convert.ToInt32(row["rutPersonaRepLegal"]);
                        repLegalT.representanteLegal.dv = Convert.ToChar(row["digitoVerificador"]);

                        if (!row.IsNull("idTipoPersJur"))
                        {
                            repLegalT.representanteLegal.tipoPersona = new ParametroGenerico(Convert.ToInt32(row["idTipoPersJur"]), row["nombreTipo"].ToString());
                        }

                        repLegalT.representanteLegal.nombreSolicitante = row["nombre"].ToString();

                        if (!row.IsNull("fechaInicioRel"))
                        {
                            repLegalT.representanteLegal.fechaInicioVigencia = Convert.ToDateTime(row["fechaInicioRel"]);
                        }

                        if (!row.IsNull("fechaFinRel"))
                        {
                            repLegalT.representanteLegal.fechaFinVigencia = Convert.ToDateTime(row["fechaFinRel"]);
                        }

                        repLegalT.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());

                        //if (!row.IsNull("idArchivoBinSC"))
                        //{
                        //    repLegalT.archivoAsoc = new ArchivoBinarioEspecial();
                        //    repLegalT.archivoAsoc.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //    repLegalT.archivoAsoc.nombreFisico = Convert.ToString(row["nombreFisico"]);
                        //    repLegalT.archivoAsoc.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                        //    repLegalT.archivoAsoc.formato = Convert.ToString(row["formato"]);
                        //    repLegalT.archivoAsoc.bytes = (byte[])row["contenido"];
                        //}
                        repLegalT.accion = accion.LISTADO;
                        repLegalT.index = i;

                        resp.Add(repLegalT);

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

        public RepLegal ObtieneRepresentanteLegal(int rutRepLegal, string nombre)
        {
            try
            {
                RepLegal repLegalT = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepresentanteLegal";

                if (rutRepLegal > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutRepLegal);
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

                        repLegalT = new RepLegal();
                        repLegalT.representanteLegal = new Solicitante();
                        repLegalT.representanteLegal.rut = Convert.ToInt32(row["rutRepLegal"]);
                        repLegalT.representanteLegal.dv = Convert.ToChar(row["digitoVerificador"]);
                        repLegalT.representanteLegal.nombreSolicitante = row["nombre"].ToString();
                        if (!row.IsNull("idTipoPersJur"))
                        {
                            repLegalT.representanteLegal.tipoPersona = new ParametroGenerico(Convert.ToInt32(row["idTipoPersJur"]), row["nombreTipoPersJur"].ToString());
                        }
                        repLegalT.representanteLegal.estadoPersona = new ParametroGenerico(Convert.ToInt32(row["idEstadoPers"]), row["nombreEstado"].ToString());

                    }
                }

                return repLegalT;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return null;
            }
        }

        public List<RepLegal> ListarRepresentanteLegal(int rutRepLegal, string nombre)
        {
            try
            {
                List<RepLegal> resp = new List<RepLegal>();
                RepLegal repLegalT = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepresentanteLegal";

                if (rutRepLegal > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutRepLegal);
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

                        repLegalT = new RepLegal();
                        repLegalT.representanteLegal = new Solicitante();
                        repLegalT.representanteLegal.rut = Convert.ToInt32(row["rutRepLegal"]);
                        repLegalT.representanteLegal.dv = Convert.ToChar(row["digitoVerificador"]);
                        repLegalT.representanteLegal.nombreSolicitante = row["nombre"].ToString();
                        //if (!row.IsNull("numeroCI"))
                        //{
                        //    repLegalT.representanteLegal.numeroControlIngreso = Convert.ToInt32(row["numeroCI"]);
                        //}
                        //if (!row.IsNull("fechaCI"))
                        //{
                        //    repLegalT.representanteLegal.fechaControlIngreso = Convert.ToDateTime(row["fechaCI"]);
                        //}
                        if (!row.IsNull("idTipoPersJur"))
                        {
                            repLegalT.representanteLegal.tipoPersona = new ParametroGenerico(Convert.ToInt32(row["idTipoPersJur"]), row["nombreTipoPersJur"].ToString());
                        }
                        repLegalT.representanteLegal.estadoPersona = new ParametroGenerico(Convert.ToInt32(row["idEstadoPers"]), row["nombreEstado"].ToString());

                        resp.Add(repLegalT);
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

        public bool GuardarContactoRepresentante(int rutRepLegal, int idContacto)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbContactoRepresentante";
                cnn.parametros.Add("@rutRepLegal", rutRepLegal);
                cnn.parametros.Add("@idContacto", idContacto);
                
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

        public List<Contacto> ListarContactoRepresentante(int rutPersona, int idContacto)
        {
            try
            {
                Contacto claseResp = null;
                List<Contacto> resp = new List<Contacto>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbContactoRepresentante";
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

                        claseResp.accion = accion.LISTADO;
                        claseResp.index = i;

                        resp.Add(claseResp);
                        i++;

                        //claseResp.region = new Region();
                        //if (!row.IsNull("idComuna")){
                        //    claseResp.region.comuna = new Comuna();
                        //    claseResp.region.comuna.id_comuna = Convert.ToInt32(row["idComuna"]);
                        //    claseResp.region.comuna.comuna = row["comuna"].ToString();
                        //}
                        //if (!row.IsNull("idRegion")){
                        //    claseResp.region.id_region = Convert.ToInt32(row["idRegion"]);
                        //    claseResp.region.region = row["region"].ToString();
                        //    resp.Add(claseResp);
                        //}
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

        public bool GuardarRepresentanteMatrizSuc(int rutRepLegal, int idMatrizSuc)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbRepresentanteMatrizSuc";
                cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                cnn.parametros.Add("@rutRepLegal", rutRepLegal);

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

        public List<MatrizSucursal> ListarRepresentanteMatrizSuc(int rutRepLegal)
        {
            try
            {
                MatrizSucursal claseResp = null;
                List<MatrizSucursal> resp = new List<MatrizSucursal>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepresentanteMatrizSuc";
                cnn.parametros.Add("@rutRepLegal", rutRepLegal);

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
                        
                        //claseResp.matriz = Convert.ToBoolean(row["matriz"]);
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

        public bool ActualizarRepresentanteLegalEstado(int rutRepLegal, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbRepresentanteLegalEstado";
                cnn.parametros.Add("@rutRepLegal", rutRepLegal);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
                
                DataTable dt = cnn.Execute();
                rutRepLegal = Convert.ToInt32(dt.Rows[0]["rutRepLegal"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool ActualizarRepLegalTitularEstado(int rutPersonaTitular, int rutPersonaRepLegal, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRepLegalTitularEstado";
                cnn.parametros.Add("@rutPersonaTitular", rutPersonaTitular);
                cnn.parametros.Add("@rutPersonaRepLegal", rutPersonaRepLegal);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);
                
                DataTable dt = cnn.Execute();
                rutPersonaRepLegal = Convert.ToInt32(dt.Rows[0]["rutPersonaRepLegal"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarRepresentanteLegal(int rutRepLegal)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRepresentanteLegal";
                cnn.parametros.Add("@rutRepLegal", rutRepLegal);
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

        public bool EliminarRepLegalTitular(int rutPersonaTitular, int rutPersonaRepLegal)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRepLegalTitular";
                cnn.parametros.Add("@rutPersonaTitular", rutPersonaTitular);
                cnn.parametros.Add("@rutPersonaRepLegal", rutPersonaRepLegal);
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

        public NombrePersona ObtenerNombreRepresentante(int rut, int idEstadoActual)
        {
            try
            {
                NombrePersona persona = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbNombreRepresentante";
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
                        persona.idNomPersona = Convert.ToInt32(row["idNomRep"]);
                        persona.rut = Convert.ToInt32(row["rutRepLegal"]);

                        persona.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                        persona.nombre = row["nombre"].ToString();
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

        public List<NombrePersona> ListarNombreRepresentante(int rut, int idEstadoActual)
        {
            try
            {
                List<NombrePersona> resp = new List<NombrePersona>();
                NombrePersona persona = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbNombreRepresentante";
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
                        persona.idNomPersona = Convert.ToInt32(row["idNomRep"]);
                        persona.rut = Convert.ToInt32(row["rutRepLegal"]);

                        //if (!row.IsNull("idArchivoBinSC"))
                        //{
                        //    persona.archivo = new ArchivoBinarioEspecial();
                        //    persona.archivo.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //}
                        persona.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
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

        public bool EliminarContactoRepresentante(int rutPersona, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbContactoRepresentante";
                cnn.parametros.Add("@rutRepLegal", rutPersona);
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

        public bool EliminarTitularMatrizSuc(int idMatrizSuc, int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbRepresentanteMatrizSuc";
                cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                if (rutPersona > 0)
                {
                    cnn.parametros.Add("@rutRepLegal", rutPersona);
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

        public bool EliminarNombreRepresentante(int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbNombreRepresentante";
                cnn.parametros.Add("@rutRepLegal", rutPersona);
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

        public MatrizSucursal ObtenerRepresentanteMatrizSuc_filtros(int rutRepLegal, int idMatrizSuc)
        {
            try
            {
                MatrizSucursal claseResp = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepresentanteMatrizSuc_filtros";
                if (rutRepLegal>0)
                {
                    cnn.parametros.Add("@rutRepLegal", rutRepLegal);
                }
                if(idMatrizSuc>0){
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
                        claseResp.persona = new Persona();
                        claseResp.persona.rutPersona = Convert.ToInt32(row["rutRepLegal"]);
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

        public RepLegal ObtieneRepresentanteTitularId(int idRepTitular)
        {
            try
            {
                RepLegal repLegalT = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepLegalTitularId";

                cnn.parametros.Add("@idRepTitular", idRepTitular);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        repLegalT = new RepLegal();
                        repLegalT.idRepLegal = Convert.ToInt32(row["idRepTitular"]);
                        repLegalT.representanteLegal = new Solicitante();
                        repLegalT.representanteLegal.rut = Convert.ToInt32(row["rutPersonaRepLegal"]);
                        repLegalT.representanteLegal.dv = Convert.ToChar(row["digitoVerificador"]);
                        repLegalT.representanteLegal.tipoPersona = new ParametroGenerico(Convert.ToInt32(row["idTipoPersJur"]), row["nombreTipo"].ToString());
                        repLegalT.representanteLegal.nombreSolicitante = row["nombre"].ToString();
                        if (!row.IsNull("fechaInicioRel"))
                        {
                            repLegalT.representanteLegal.fechaInicioVigencia = Convert.ToDateTime(row["fechaInicioRel"]);
                        }
                        if (!row.IsNull("fechaFinRel"))
                        {
                            repLegalT.representanteLegal.fechaFinVigencia = Convert.ToDateTime(row["fechaFinRel"]);
                        }
                        repLegalT.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());

                    }
                }

                return repLegalT;
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
