using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Contantes;
using Datos.Entidades;
using System.Data.SqlClient;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class SolicitanteDA
    {
        Logger logger = new Logger();

        /**
         * Método que lista los solicitantes asociados a la solicitud
         * de concesión de acuicultura.
         */
        public DataTable VerSolicitante(int IdSolicitud, int rutPersona, string KeySort, int idEstadoAsociacion)
        {
            // Inicializar la conexión para el procedimiento almacenado
            Conexion cnn = new Conexion();

            // Asignar el procedimiento almacenado
            cnn.procedimiento = "paSelRbPersonasLegalesSolicitud";

            // Añadir parámetros al procedimiento almacenado
            if (IdSolicitud > 0)
            {
                cnn.parametros.Add("@idSolConcesion", IdSolicitud);
            }
            if (rutPersona > 0)
            {
                cnn.parametros.Add("@rutPersona", rutPersona);
            }
            if (idEstadoAsociacion > 0)
            {
                cnn.parametros.Add("@idEstadoAsociacion", idEstadoAsociacion);
            }

            // Ejecutar el procedimiento almacenado
            DataTable dt = cnn.Execute();
            
            return dt;
        }

        public DataTable VerPersona(int rutPersona, int tipoPersona)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbPersona";
            cnn.parametros.Add("@rutPersona", rutPersona);

            if (tipoPersona > 0)
            {
                cnn.parametros.Add("@idTipoPersona", tipoPersona);
            }
            DataTable dt = cnn.Execute();
            return dt;
        }

        public Datos.Entidades.Solicitante ObtenerPersona(int rutPersona, int tipoPersona)
        {
            DataTable dt = this.VerPersona(rutPersona, tipoPersona);
            try
            {
                Datos.Entidades.Solicitante solicitante = new Datos.Entidades.Solicitante();
                solicitante.rut = Convert.ToInt32(dt.Rows[0]["rutPersona"]);
                solicitante.dv = Convert.ToChar(dt.Rows[0]["digitoVerificador"]);
                solicitante.nombreSolicitante = Convert.ToString(dt.Rows[0]["nombre"]);
                solicitante.tipoPersona = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idTipoPersona"]), Convert.ToString(dt.Rows[0]["nombreTipo"]));

                if (dt.Rows[0]["genero"] != null && !dt.Rows[0]["genero"].ToString().Equals(""))
                {
                    solicitante.genero = Convert.ToBoolean(dt.Rows[0]["genero"]);
                }
                if (dt.Rows[0]["numRegSubpesca"] != null && !dt.Rows[0]["numRegSubpesca"].ToString().Equals(""))
                {
                    solicitante.numeroRegistroSubpesca = Convert.ToInt32(dt.Rows[0]["numRegSubpesca"]);
                }

                if (dt.Rows[0]["fechaRegSubpesca"] != null && !dt.Rows[0]["fechaRegSubpesca"].ToString().Equals(""))
                {
                    solicitante.fechaRegistroSubpesca = Convert.ToDateTime(dt.Rows[0]["fechaRegSubpesca"]);
                }

                if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                {
                    solicitante.subtipoPersona = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idTipoPersJur"]), Convert.ToString(dt.Rows[0]["nombreTipoPersJur"]));
                }

                solicitante.estadoPersona = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idEstadoPers"]), Convert.ToString(dt.Rows[0]["nombreEstadoPers"]));

                if (dt.Rows[0]["idHolding"] != null && !dt.Rows[0]["idHolding"].ToString().Equals(""))
                {
                    solicitante.holding = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idHolding"]), Convert.ToString(dt.Rows[0]["nombreHolding"]));
                }
                if (dt.Rows[0]["idEstadoAPE"] != null && !dt.Rows[0]["idEstadoAPE"].ToString().Equals(""))
                {
                    solicitante.estadoAPE = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idEstadoAPE"]), Convert.ToString(dt.Rows[0]["nombreEstadoAPE"]));
                }
                
                return solicitante;
            }
            catch { return null; };
        }

        public Datos.Entidades.Solicitante ObtenerSolicitante(int idSolicitud, int rutPersona, string keysort, int idEstadoAsociacion)
        {
            DataTable dt = this.VerSolicitante(idSolicitud, rutPersona, keysort, idEstadoAsociacion);
            try
            {
                Datos.Entidades.Solicitante solicitante = new Datos.Entidades.Solicitante();
                Datos.Entidades.SolicitudConcesion solicitudConsecion = new Datos.Entidades.SolicitudConcesion();

                solicitante.solicitud = solicitudConsecion;
                solicitante.solicitud.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);
                solicitante.idPersonasLeg = Convert.ToInt32(dt.Rows[0]["idPersonasLeg"]);
                solicitante.idEstadoAsociacion = Convert.ToInt32(dt.Rows[0]["idEstadoAsociacion"]); 
                solicitante.rut = Convert.ToInt32(dt.Rows[0]["rutPersona"]);
                solicitante.dv = Convert.ToChar(dt.Rows[0]["digitoVerificador"]);
                solicitante.nombreSolicitante = Convert.ToString(dt.Rows[0]["nombre"]);
                solicitante.tipoPersona = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idTipoPersona"]), Convert.ToString(dt.Rows[0]["nombreTipo"]));

                if (!dt.Rows[0].IsNull("genero"))
                {
                    solicitante.genero = Convert.ToBoolean(dt.Rows[0]["genero"]);
                }
                if (dt.Rows[0]["idEstadoAPE"] != null && !dt.Rows[0]["idEstadoAPE"].ToString().Equals(""))
                {
                    solicitante.estadoAPE = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idEstadoAPE"]), Convert.ToString(dt.Rows[0]["nombreEstadoAPE"]));
                }

                return solicitante;
            }
            catch { return null; };
        }


        public List<Solicitante> listarSolicitante(int idSolicitud, int rutPersona, string keysort, int idEstadoAsociacion)
        {
            DataTable dt = this.VerSolicitante(idSolicitud, rutPersona, keysort, idEstadoAsociacion);
            try
            {
                Datos.Entidades.Solicitante solicitante = null;
                Datos.Entidades.SolicitudConcesion solicitudConsecion = null;
                List<Solicitante> solicitanteList = new List<Solicitante>();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        solicitante = new Datos.Entidades.Solicitante();
                        solicitudConsecion = new Datos.Entidades.SolicitudConcesion();
                        solicitante.solicitud = solicitudConsecion;
                        solicitante.solicitud.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);
                        solicitante.idPersonasLeg = Convert.ToInt32(dt.Rows[0]["idPersonasLeg"]);
                        solicitante.idEstadoAsociacion = Convert.ToInt32(dt.Rows[0]["idEstadoAsociacion"]);
                        solicitante.rut = Convert.ToInt32(dt.Rows[0]["rutPersona"]);
                        solicitante.dv = Convert.ToChar(dt.Rows[0]["digitoVerificador"]);
                        solicitante.nombreSolicitante = Convert.ToString(dt.Rows[0]["nombre"]);
                        solicitante.tipoPersona = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idTipoPersona"]), Convert.ToString(dt.Rows[0]["nombreTipo"]));

                        if (!dt.Rows[0].IsNull("genero"))
                        {
                            solicitante.genero = Convert.ToBoolean(dt.Rows[0]["genero"]);
                        }
                        if (dt.Rows[0]["idEstadoAPE"] != null && !dt.Rows[0]["idEstadoAPE"].ToString().Equals(""))
                        {
                            solicitante.estadoAPE = new Datos.Entidades.ParametroGenerico(Convert.ToInt32(dt.Rows[0]["idEstadoAPE"]), Convert.ToString(dt.Rows[0]["nombreEstadoAPE"]));
                        }

                        solicitanteList.Add(solicitante);
                    }
                }


                return solicitanteList;
            }
            catch { return null; };
        }

       
        /**
         * Método que guarda solicitante asociado a la solicitud
         * de concesión de acuicultura.
         */ 
        public bool GuardarSolicitante(Datos.Entidades.Solicitante solicitante, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbPersonasLegalesSolicitud";

                cnn.parametros.Add("@idPersonasLeg", solicitante.idPersonasLeg);
                cnn.parametros.Add("@idSolConcesion", solicitante.solicitud.idSolConcesion);
                cnn.parametros.Add("@idEstadoAsociacion", solicitante.idEstadoAsociacion);
                cnn.parametros.Add("@rutPersona", solicitante.rut);
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                
                DataTable dt = cnn.Execute();
                solicitante.idPersonasLeg = Convert.ToInt32(dt.Rows[0]["idPersonasLeg"]);

                return true;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };

        }

        /**
         * Método que elimina un solicitante asociado a la solicitud
         * de concesión de acuicultura.
         */ 
        public bool EliminarSolicitante(Datos.Entidades.Solicitante solicitante, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbPersonasLegalesSolicitud";
                cnn.parametros.Add("@idPersonasLeg", solicitante.idPersonasLeg);
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

        /**
         * 
         */
        public Datos.Entidades.Solicitante existeSolicitante(string idSolicitud, int rutPersona, int idEstadoAsociacion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPersonasLegalesSolExistente";

                cnn.parametros.Add("@idSolConcesion", idSolicitud);
                cnn.parametros.Add("@rutPersona", rutPersona);

                if (idEstadoAsociacion > 0)
                {
                    cnn.parametros.Add("@idEstadoAsociacion", idEstadoAsociacion);
                }

                DataTable dt = cnn.Execute();
                if (dt != null)
                {

                     //int i = 0;
                     foreach (DataRow row in dt.Rows)
                     {

                         Datos.Entidades.Solicitante solicitante = new Datos.Entidades.Solicitante();
                         Datos.Entidades.SolicitudConcesion solicitudConsecion = new Datos.Entidades.SolicitudConcesion();

                         solicitante.idPersonasLeg = Convert.ToInt32(dt.Rows[0]["idPersonasLeg"]);
                         solicitante.solicitud = solicitudConsecion;
                         solicitante.solicitud.idSolConcesion = Convert.ToInt32(dt.Rows[0]["idSolConcesion"]);
                         solicitante.idEstadoAsociacion = Convert.ToInt32(dt.Rows[0]["idEstadoAsociacion"]);
                         solicitante.rut = Convert.ToInt32(dt.Rows[0]["rutPersona"]);

                         return solicitante;
                     }
                }

                return null;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

        public DataTable ObtenerContactoMatrizSucursales(int rutSolicitante, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbContactoPersona";
                cnn.parametros.Add("@rutPersona", rutSolicitante);

                if(idContacto > 0){
                    cnn.parametros.Add("@idContacto", idContacto);
                }
                DataTable dt = cnn.Execute();

                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }
       
        public DataTable ObtenerRepresentantesLegales(int rutSolicitante, int rutRepresentanteLegal)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbRepLegalTitular";
                if (rutSolicitante > 0)
                {
                    cnn.parametros.Add("@rutPersonaTitular", rutSolicitante);
                }
                if (rutRepresentanteLegal > 0)
                {
                    cnn.parametros.Add("@rutPersonaRepLegal", rutRepresentanteLegal);
                }
                DataTable dt = cnn.Execute();

                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

        public DataTable ObtenerArchivosAdjuntoPersona(int rutPersona, int idArchivoAdjunto)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbArchivosAdjTitular";
                cnn.parametros.Add("@rutPersona", rutPersona);

                if (idArchivoAdjunto > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoAdjunto);
                }
                DataTable dt = cnn.Execute();

                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }
        //**

        public List<ArchivosAdjTitular> ListarArchivosAdjuntoTitular(int rutPersona, int idArchivoBinSC)
        {
            try
            {
                List<ArchivosAdjTitular> resp = new List<ArchivosAdjTitular>();
                ArchivosAdjTitular archivoOpLegal = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbArchivosAdjTitular";
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

                        archivoOpLegal = new ArchivosAdjTitular();
                        archivoOpLegal.rutPersona = Convert.ToInt32(row["rutPersona"]);
                        archivoOpLegal.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocAsociado"]), row["nombreTipo"].ToString());
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
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            archivoOpLegal.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVig"].ToString());
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
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

        public ArchivosAdjTitular ObtenerArchivosAdjuntoTitular(int rutPersona, int idArchivoAdjunto)
        {
            try
            {

                ArchivosAdjTitular archivoOp = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbArchivosAdjTitular";
                cnn.parametros.Add("@rutPersona", rutPersona);
                if (idArchivoAdjunto > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoAdjunto);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        archivoOp = new ArchivosAdjTitular();
                        archivoOp.rutPersona = Convert.ToInt32(row["rutPersona"]);
                        archivoOp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocAsociado"]), row["nombreTipo"].ToString());

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
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            archivoOp.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstadoVig"].ToString());
                        }
                    }
                }
                return archivoOp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }


        //**

        public DataTable ObtenerNombresPersona(int rutPersona, int estadoNombrePersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbNombrePersona";
                cnn.parametros.Add("@rut", rutPersona);

                if (estadoNombrePersona > 0)
                {
                    cnn.parametros.Add("@idEstadoActual", estadoNombrePersona);
                }
                DataTable dt = cnn.Execute();

                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }



        public List<Datos.Entidades.Solicitante> ListarTitularesDetalleSector(int idSolicitud)
        {
            try
            {
                Datos.Entidades.Solicitante solicitante = null;
                List<Datos.Entidades.Solicitante> resp = new List<Datos.Entidades.Solicitante>();

                int rutTitular = 0;
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

                                solicitante = new Datos.Entidades.Solicitante();
                                solicitante.rut = Convert.ToInt32(row["rutPersona"]);
                                solicitante.dv = Convert.ToChar(row["digitoVerificador"]);
                                solicitante.nombreSolicitante = row["nombre"].ToString();
                                resp.Add(solicitante);
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

        public bool GuardarContactoPersona(int rutPersona, int idContacto)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbContactoPersona";
                cnn.parametros.Add("@rutPersona", rutPersona);
                cnn.parametros.Add("@idContacto", idContacto);

                DataTable dt = cnn.Execute();
                rutPersona = Convert.ToInt32(dt.Rows[0]["rutPersona"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

       
        public Solicitante ObtenerVistaRepLegal_Persona(int rutPersona, char dvPersona)
        {
            Solicitante solicitante = null;
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelVT_PersJuridica_Personas";
            cnn.parametros.Add("@RUT_NUM_PER_JURIDIC", rutPersona);
            cnn.parametros.Add("@RUT_DV_PER_JURIDIC", dvPersona);

            DataTable dt = cnn.Execute();
            try
            {
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        solicitante = new Solicitante();
                        solicitante.rut = Convert.ToInt32(dt.Rows[0]["RUT_NUM_PER_JURIDIC"]);
                        solicitante.dv = Convert.ToChar(dt.Rows[0]["RUT_DV_PER_JURIDIC"]);
                        solicitante.numeroRegistroSubpesca = Convert.ToInt32(dt.Rows[0]["NUMERO_RPJ"]);
                        solicitante.fechaRegistroSubpesca = Convert.ToDateTime(dt.Rows[0]["FECHA_TRAMITE_PER_JUR"]);

                        if (solicitante.tipoPersona != null && solicitante.tipoPersona.id == rbTipo.PERSONA_JURIDICA)
                        {
                            solicitante.subtipoPersona = new Datos.Entidades.ParametroGenerico(0, Convert.ToString(dt.Rows[0]["RAZON_SOCIAL_PER_JURIDIC"]));
                        }    
                    }
                }

                return solicitante;
            }
            catch { return null; };
        }

        public bool GuardarTitular(Solicitante titular)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTitular";
                cnn.parametros.Add("@rutPersona", titular.rut);

                if (titular.tipoPersona!=null && titular.tipoPersona.id>0)
                {
                    cnn.parametros.Add("@idTipoPersona", titular.tipoPersona.id);
                }
                cnn.parametros.Add("@idEstado", titular.idEstadoAsociacion);
                cnn.parametros.Add("@digitoVerificador", titular.dv);
                if (titular.tipoPersona != null && titular.tipoPersona.id == 12)  //Persona Natural aplica genero.
                {
                    cnn.parametros.Add("@genero", titular.genero);
                }
                
                if (titular.numeroRegistroSubpesca > 0)
                {
                    cnn.parametros.Add("@numRegSubpesca", titular.numeroRegistroSubpesca);
                }
                if (titular.fechaRegistroSubpesca !=null && !titular.fechaRegistroSubpesca.Equals(default(DateTime)))
                {
                    cnn.parametros.Add("@fechaRegSubpesca", titular.fechaRegistroSubpesca);
                }
               
                cnn.parametros.Add("@registroCentralizado", titular.regCentralizado);
                
                if (titular.subtipoPersona != null && titular.subtipoPersona.id>0)
                {
                    cnn.parametros.Add("@idTipoPersJur", titular.subtipoPersona.id);
                }
                
                if (titular.holding!=null && titular.holding.id>0)
                {
                    cnn.parametros.Add("@idHolding", titular.holding.id);
                }
                if (titular.estadoAPE != null && titular.estadoAPE.id > 0)
                {
                    cnn.parametros.Add("@idEstadoAPE", titular.estadoAPE.id);
                }
                

                DataTable dt = cnn.Execute();
                titular.rut = Convert.ToInt32(dt.Rows[0]["rutPersona"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarArchivosAdjTitular(int rutPersona, int idArchivoBinSC, int idTipoDocAsociado, int numCI, DateTime fechaCI, int idEstadoVig)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbArchivosAdjTitular";
                cnn.parametros.Add("@rutPersona", rutPersona);
                cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                cnn.parametros.Add("@idTipoDocAsociado", idTipoDocAsociado);

                if (numCI > 0)
                {
                    cnn.parametros.Add("@numeroCI", numCI);
                }
                if (fechaCI != null && fechaCI != default(DateTime))
                {
                    cnn.parametros.Add("@fechaCI", fechaCI);
                }
                if (idEstadoVig > 0)
                {
                    cnn.parametros.Add("@idEstadoVig", idEstadoVig);
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

        public bool GuardarNombrePersona(NombrePersona nomPersona)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbNombrePersona";
                cnn.parametros.Add("@rut", nomPersona.rut);
                if (nomPersona.idNomPersona>0)
                {
                    cnn.parametros.Add("@idNombrePersona", nomPersona.idNomPersona);
                }
                cnn.parametros.Add("@idEstadoActual", nomPersona.estadoActual.id);
                cnn.parametros.Add("@nombre", nomPersona.nombre);
                
                
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

        public List<Solicitante> ListarTitularFiltro(int rutPersona, string nombre)
        {
            try
            {
                Solicitante solAux = null;
                List<Solicitante> resp = new List<Solicitante>();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTitularFiltro";

                if (rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutPersona);
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
                        solAux = new Solicitante();
                        solAux.rut = Convert.ToInt32(row["rutPersona"]);
                        solAux.nombreSolicitante = row["nombre"].ToString();
                        solAux.tipoPersona = new ParametroGenerico(Convert.ToInt32(row["idTipoPersona"]), row["nombreTipo"].ToString());
                        solAux.dv = Convert.ToChar(row["digitoVerificador"]);
                        if (!row.IsNull("genero"))
                        {
                            solAux.genero = Convert.ToBoolean(row["genero"]);
                        }
                        solAux.regCentralizado = Convert.ToBoolean(row["registroCentralizado"]);

                        resp.Add(solAux);

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

        public bool ActualizarTitularEstado(int rutPersona, int idEstadoVigencia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbTitularEstado";
                cnn.parametros.Add("@rutPersona", rutPersona);
                cnn.parametros.Add("@idEstadoVigencia", idEstadoVigencia);

                DataTable dt = cnn.Execute();
                rutPersona = Convert.ToInt32(dt.Rows[0]["rutPersona"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarPersona(int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbPersona";
                cnn.parametros.Add("@rutPersona", rutPersona);
                DataTable dt = cnn.Execute();
                
                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public NombrePersona ObtenerNombrePersona(int rut, int idEstadoActual)
        {
            try
            {
                NombrePersona persona = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbNombrePersona";
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
                        persona.idNomPersona = Convert.ToInt32(row["idNomPersona"]);
                        persona.rut = Convert.ToInt32(row["rut"]);

                        //if (!row.IsNull("idArchivoBinSC"))
                        //{
                        //    persona.archivo = new ArchivoBinarioEspecial();
                        //    persona.archivo.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //}
                        persona.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                        persona.nombre = row["nombre"].ToString();
                        //if(!row.IsNull("numeroCI")){
                        //    persona.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        //}
                        //if(!row.IsNull("fechaCI")){
                        //    persona.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                        //}
                        
                        persona.fechaIngresoSistema = Convert.ToDateTime(row["fechaIngresoSistema"]);

                    }
                }

                return persona;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
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
                cnn.procedimiento = "paSelRbNombrePersona";
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
                        persona.idNomPersona = Convert.ToInt32(row["idNomPersona"]);
                        persona.rut = Convert.ToInt32(row["rut"]);

                        //if (!row.IsNull("idArchivoBinSC"))
                        //{
                        //    persona.archivo = new ArchivoBinarioEspecial();
                        //    persona.archivo.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                        //}
                        persona.estadoActual = new ParametroGenerico(Convert.ToInt32(row["idEstadoActual"]), row["nombreEstado"].ToString());
                        persona.nombre = row["nombre"].ToString();
                        //if(!row.IsNull("numeroCI")){
                        //    persona.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        //}
                        //if(!row.IsNull("fechaCI")){
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
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool EliminarContactoPersona(int rutPersona, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbContactoPersona";
                cnn.parametros.Add("@rutPersona", rutPersona);
                if(idContacto>0){
                    cnn.parametros.Add("@idContacto", idContacto);
                }
                
                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarArchivosAdjTitular(int rutPersona, int idArchivoBinSC)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbArchivosAdjTitular";
                cnn.parametros.Add("@rutPersona", rutPersona);
                if (idArchivoBinSC > 0)
                {
                    cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarTitularMatrizSuc(int idMatrizSuc, int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbTitularMatrizSuc";
                cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                if (rutPersona > 0)
                {
                    cnn.parametros.Add("@rutPersona", rutPersona);
                }

                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarNombrePersona(int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbNombrePersona";
                cnn.parametros.Add("@rutPersona", rutPersona);
                DataTable dt = cnn.Execute();

                int resul = Convert.ToInt32(dt.Rows[0]["resultado"]);
                if (resul >= 0) return true;

                return false;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool TiTularEsRPA(int rutPersona)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "SSP_sp_VistaRPA";
            cnn.parametros.Add("@rutPersona", rutPersona);

            DataTable dt = cnn.Execute();

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        public bool TiTularEsAPE(int rutPersona)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCriterioAPE";
                cnn.parametros.Add("@rutPersona", rutPersona);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        bool aux = Convert.ToBoolean(row["cumpleCriterioFinal"]);
                        if(aux){
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            }
        }

        public List<Contacto> ListarContactoPersona(int rutSolicitante, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                List<Contacto> resp = new List<Contacto>();

                Contacto contacto = null;
                cnn.procedimiento = "paSelRbContactoPersona";
                cnn.parametros.Add("@rutPersona", rutSolicitante);

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

                        contacto = new Contacto();
                        contacto.rut = Convert.ToInt32(row["rutPersona"]);
                        contacto.idContacto = Convert.ToInt32(row["idContacto"]);
                        contacto.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
                        contacto.valorContacto = Convert.ToString(row["valorContacto"]);
                        contacto.detalle = Convert.ToString(row["detalle"]);

                        contacto.accion = accion.LISTADO;
                        contacto.index = i;

                        resp.Add(contacto);
                        i++;
                    }
                }
                return resp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }
        //public List<Contacto> ListarContactoOperador(int rutOperador, int idContacto)
        //{
        //    try
        //    {
        //        Conexion cnn = new Conexion();
        //        List<Contacto> resp = new List<Contacto>();

        //        Contacto contacto = null;
        //        cnn.procedimiento = "paSelRbContactoOperador";
        //        cnn.parametros.Add("@rutPersona", rutOperador);

        //        if (idContacto > 0)
        //        {
        //            cnn.parametros.Add("@idContacto", idContacto);
        //        }
        //        DataTable dt = cnn.Execute();

        //        if (dt != null)
        //        {
        //            int i = 0;
        //            foreach (DataRow row in dt.Rows)
        //            {

        //                contacto = new Contacto();
        //                contacto.rut = Convert.ToInt32(row["rutOperador"]);
        //                contacto.idContacto = Convert.ToInt32(row["idContacto"]);
        //                contacto.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
        //                contacto.valorContacto = Convert.ToString(row["valorContacto"]);
        //                contacto.detalle = Convert.ToString(row["detalle"]);

        //                contacto.accion = accion.LISTADO;
        //                contacto.index = i;

        //                resp.Add(contacto);
        //                i++;
        //            }
        //        }
        //        return resp;

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.PrintError(ex);
        //        logger.SendMailError(ex);
        //        return null;
        //    };
        //}
        public List<Contacto> ListarContactoOperador(int rutOperador, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                List<Contacto> resp = new List<Contacto>();

                Contacto contacto = null;
                cnn.procedimiento = "paSelRbContactoOperador";
                cnn.parametros.Add("@rutPersona", rutOperador);

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

                        contacto = new Contacto();
                        contacto.rut = Convert.ToInt32(row["rutOperador"]);
                        contacto.idContacto = Convert.ToInt32(row["idContacto"]);
                        contacto.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
                        contacto.valorContacto = Convert.ToString(row["valorContacto"]);
                        contacto.detalle = Convert.ToString(row["detalle"]);

                        contacto.accion = accion.LISTADO;
                        contacto.index = i;

                        resp.Add(contacto);
                        i++;
                    }
                }
                return resp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }
        public List<Contacto> ListarContactoRepresentante(int rutRepresentante, int idContacto)
        {
            try
            {
                Conexion cnn = new Conexion();
                List<Contacto> resp = new List<Contacto>();

                Contacto contacto = null;
                cnn.procedimiento = "paSelRbContactoRepresentante";
                cnn.parametros.Add("@rutPersona", rutRepresentante);

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

                        contacto = new Contacto();
                        contacto.rut = Convert.ToInt32(row["rutRepLegal"]);
                        contacto.idContacto = Convert.ToInt32(row["idContacto"]);
                        contacto.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
                        contacto.valorContacto = Convert.ToString(row["valorContacto"]);
                        contacto.detalle = Convert.ToString(row["detalle"]);

                        contacto.accion = accion.LISTADO;
                        contacto.index = i;

                        resp.Add(contacto);
                        i++;
                    }
                }
                return resp;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }

        public bool titularPerteneceRPA(int rutPescador)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSel_SSP_RPA";
                cnn.parametros.Add("@rutPescador", rutPescador);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        if (!row.IsNull("RutPescador"))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return true;
            }
        }

        public bool ActualizarEstadoArchivosAdjTitular(int rutPersona, int idArchivoBinSC, int idEstadoVig)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paUpdRbArchivosAdjTitular";
                cnn.parametros.Add("@rutPersona", rutPersona);
                cnn.parametros.Add("@idArchivoBinSC", idArchivoBinSC);
                cnn.parametros.Add("@idEstadoVig", idEstadoVig);

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

    }
}
