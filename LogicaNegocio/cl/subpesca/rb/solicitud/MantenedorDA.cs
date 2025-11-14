using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using System.Data;
using Datos.AccesoDatos;
using Datos.Entidades;
using Datos.Entidades.Resolucion;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class MantenedorDA
    {
        Logger logger = new Logger();

        public DataTable GuardarRegion_Mantenedor(int idRegion, int codRegion, string nombreRegion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbRegion_Mantenedor";

                cnn.parametros.Add("@idRegion", idRegion);
                cnn.parametros.Add("@Codigo", codRegion);
                cnn.parametros.Add("@Region", nombreRegion);

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

        public DataTable ListarRegion_Mantenedor(int idRegion, int codRegion, string nombreRegion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbRegion_Mantenedor";
            if (idRegion > 0)
            {
                cnn.parametros.Add("@idRegion", idRegion);
            }
            if (codRegion > 0)
            {
                cnn.parametros.Add("@codRegion", codRegion);
            }
            if (nombreRegion != null && !nombreRegion.Equals(""))
            {
                cnn.parametros.Add("@nombreRegion", nombreRegion);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<Region> ListarRegion(int idRegion, int codRegion, string nombreRegion)
        {
            DataTable dt = this.ListarRegion_Mantenedor(idRegion, codRegion, nombreRegion);
            List<Region> listaRegion = new List<Region>();

            try
            {
                Region region = null;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        region = new Region();
                        region.id_region = Convert.ToInt32(row["IdRegion"]);
                        region.codigo = Convert.ToInt32(row["Codigo"]);
                        region.region = Convert.ToString(row["Region"]);
                        listaRegion.Add(region);
                    }
                }
                return listaRegion;

            }
            catch
            {
                return null;

            }
        }

        public DataTable EliminarRegion_Mantenedor(int idRegion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbRegion_Mantenedor";
            cnn.parametros.Add("@idRegion", idRegion);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public DataTable GuardarMacrozona_Mantenedor(int IdMacrozona, string nombreMacrozona, int idRegion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbMacrozona_Mantenedor";

                cnn.parametros.Add("@IdMacrozona", IdMacrozona);
                cnn.parametros.Add("@Macrozona", nombreMacrozona);
                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
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

        public DataTable EliminarMacrozona_Mantenedor(int idMacrozona)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbMacrozona_Mantenedor";
            cnn.parametros.Add("@IdMacrozona", idMacrozona);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<Macrozona> ListarMacrozona_Mantenedor(Macrozona macrozonaFiltro)
        {
            try
            {
                List<Macrozona> resp = new List<Macrozona>();
                Macrozona param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbMacrozona_Mantenedor";
                if (macrozonaFiltro.id_macrozona > 0)
                {
                    cnn.parametros.Add("@idMacrozona", macrozonaFiltro.id_macrozona);
                }
                if (macrozonaFiltro.id_region > 0)
                {
                    cnn.parametros.Add("@idRegion", macrozonaFiltro.id_region);
                }
                if (macrozonaFiltro.macrozona != null && !macrozonaFiltro.macrozona.Equals(""))
                {
                    cnn.parametros.Add("@nombreMacrozona", macrozonaFiltro.macrozona);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Macrozona();
                        param.id_macrozona = Convert.ToInt32(row["IdMacrozona"]);
                        param.macrozona = row["Macrozona"].ToString();
                        param.regionMacrozona = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["Region"].ToString());

                        resp.Add(param);

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

        public DataTable GuardarHuso_Mantenedor(int idHuso, string nombreHuso)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbHuso_Mantenedor";

                cnn.parametros.Add("@idHuso", idHuso);
                cnn.parametros.Add("@nombreHuso", nombreHuso);

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

        public DataTable EliminarHuso_Mantenedor(int idHuso)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbHuso_Mantenedor";
            cnn.parametros.Add("@idHuso", idHuso);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> ListarHuso_Mantenedor(ParametroGenerico husoFiltro)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbHuso_Mantenedor";
                if (husoFiltro.id > 0)
                {
                    cnn.parametros.Add("@idHuso", husoFiltro.id);
                }
                if (husoFiltro.descripcion != null && !husoFiltro.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreHuso", husoFiltro.descripcion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        resp.Add(param);

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

        public DataTable GuardarTipoFormaEstructura_Mantenedor(int idTipoForma, string nombreTipoForma)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoFormaEstructura_Mantenedor";

                cnn.parametros.Add("@idTipoForma", idTipoForma);
                cnn.parametros.Add("@nombreTipoForma", nombreTipoForma);

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

        public DataTable EliminarTipoFormaEstructura_Mantenedor(int idTipoForma)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbTipoFormaEstructura_Mantenedor";
            cnn.parametros.Add("@idTipoForma", idTipoForma);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> ListarTipoFormaEstructura_Mantenedor(ParametroGenerico parametroGenerico)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoFormaEstructura_Mantenedor";

                if (parametroGenerico.id > 0)
                {
                    cnn.parametros.Add("@idTipoForma", parametroGenerico.id);
                }

                if (!parametroGenerico.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoForma", parametroGenerico.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idTipoForma"]), row["nombreTipoForma"].ToString());
                        resp.Add(param);

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

        public DataTable GuardarTipo_Mantenedor(ParametroGenerico tipo)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipo_Mantenedor";

                cnn.parametros.Add("@idTipo", tipo.id);
                cnn.parametros.Add("@nombreTipo", tipo.descripcion);
                cnn.parametros.Add("@grupo", tipo.clave);

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

        public DataTable EliminarTipo_Mantenedor(int idTipo)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbTipo_Mantenedor";
            cnn.parametros.Add("@idTipo", idTipo);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> ListarTipo_Mantenedor(ParametroGenerico tipo)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipo_Mantenedor";
                if (tipo.id > 0)
                {
                    cnn.parametros.Add("@idTipo", tipo.id);
                }
                if (tipo.clave != null && !tipo.clave.Equals(""))
                {
                    cnn.parametros.Add("@grupo", tipo.clave);
                }
                if (tipo.descripcion != null && !tipo.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipo", tipo.descripcion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());
                        param.clave = row["grupo"].ToString();
                        resp.Add(param);

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

        public DataTable GuardarDatum_Mantenedor(ParametroGenerico datum)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDatum_Mantenedor";

                cnn.parametros.Add("@IdDatum", datum.id);
                cnn.parametros.Add("@Datum", datum.clave);

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

        public DataTable EliminarDatum_Mantenedor(int idDatum)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbDatum_Mantenedor";
            cnn.parametros.Add("@IdDatum", idDatum);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> ListarDatum_Mantenedor(ParametroGenerico datum)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDatum_Mantenedor";
                if (datum.id > 0)
                {
                    cnn.parametros.Add("@IdDatum", datum.id);
                }

                if (datum.clave != null && !datum.clave.Equals(""))
                {
                    cnn.parametros.Add("@Datum", datum.clave);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["IdDatum"]), "");
                        param.clave = row["Datum"].ToString();
                        resp.Add(param);

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

        public DataTable GuardarTipoMedida_Mantenedor(ParametroGenerico tipoMedida)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoMedida_Mantenedor";

                cnn.parametros.Add("@idTipoMedida", tipoMedida.id);
                cnn.parametros.Add("@nombreTipoMedida", tipoMedida.descripcion);
                cnn.parametros.Add("@siglaMedida", tipoMedida.clave);

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

        public DataTable EliminarTipoMedida_Mantenedor(int idTipoMedida)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbTipoMedida_Mantenedor";
            cnn.parametros.Add("@idTipoMedida", idTipoMedida);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> ListarTipoMedida_Mantenedor(ParametroGenerico tipoMedida)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoMedida_Mantenedor";
                if (tipoMedida.id > 0)
                {
                    cnn.parametros.Add("@idTipoMedida", tipoMedida.id);
                }
                if (tipoMedida.clave != null && !tipoMedida.clave.Equals(""))
                {
                    cnn.parametros.Add("@siglaMedida", tipoMedida.clave);
                }
                if (tipoMedida.descripcion != null && !tipoMedida.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoMedida", tipoMedida.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idTipoMedida"]), row["nombreTipoMedida"].ToString());
                        param.clave = row["siglaMedida"].ToString();
                        resp.Add(param);

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

        public DataTable GuardarEtapaCultivo_Mantenedor(EtapaCultivo etapaCultivo)
        {
            try
            {

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paInsRbEtapaCultivo_Mantenedor";
                if (etapaCultivo != null && etapaCultivo.id_etapaDesarrollo > 0)
                {
                    cnn.parametros.Add("@idEtapaCultivo", etapaCultivo.id_etapaDesarrollo);
                }
                cnn.parametros.Add("@idEspecie", etapaCultivo.idEspecie);
                if (etapaCultivo != null && etapaCultivo.codigo > 0)
                {
                    cnn.parametros.Add("@codigoEtapaDesarrollo", etapaCultivo.codigo);
                }
                if (etapaCultivo != null && !etapaCultivo.nombreEtapaDesarrollo.Equals(""))
                {
                    cnn.parametros.Add("@nombreEtapaCultivo", etapaCultivo.nombreEtapaDesarrollo);
                }
                if (etapaCultivo != null && etapaCultivo.estado != null && etapaCultivo.estado.id > 0)
                {
                    cnn.parametros.Add("@vigencia", etapaCultivo.estado.id);
                }
                if (etapaCultivo.grupoEspecie != null && etapaCultivo.grupoEspecie.id > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", etapaCultivo.grupoEspecie.id);
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

        public DataTable EliminarEtapaCultivo_Mantenedor(int idEtapaCultivo)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbEtapaCultivo_Mantenedor";
            cnn.parametros.Add("@idEtapaCultivo", idEtapaCultivo);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> ListarEtapaCultivo_Mantenedor(EtapaCultivo etapaCultivoFiltro)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEtapaCultivo_Mantenedor";
                if (etapaCultivoFiltro.id_etapaDesarrollo > 0)
                {
                    cnn.parametros.Add("@idEtapaCultivo", etapaCultivoFiltro.id_etapaDesarrollo);
                }
                if (etapaCultivoFiltro.idEspecie > 0)
                {
                    cnn.parametros.Add("@idEspecie", etapaCultivoFiltro.idEspecie);
                }
                if (etapaCultivoFiltro.codigo != null && etapaCultivoFiltro.codigo > 0)
                {
                    cnn.parametros.Add("@CodigoEtapaDesarrollo", etapaCultivoFiltro.codigo);
                }
                if (etapaCultivoFiltro.nombreEtapaDesarrollo != null && !etapaCultivoFiltro.nombreEtapaDesarrollo.Equals(""))
                {
                    cnn.parametros.Add("@EtapaDesarrollo", etapaCultivoFiltro.nombreEtapaDesarrollo);
                }
                if (etapaCultivoFiltro.grupoEspecie != null && etapaCultivoFiltro.grupoEspecie.id > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", etapaCultivoFiltro.grupoEspecie.id);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        //param = new ParametroGenerico(Convert.ToInt32(row["idEtapaCultivo"]), row["nombreEtapaCultivo"].ToString());
                        param = new ParametroGenerico(Convert.ToInt32(row["IdEtapaDesarrollo"]), row["EtapaDesarrollo"].ToString());
                        resp.Add(param);

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

        public DataTable GuardarCapitaniaPuerto_Mantenedor(CapitaniaDePuerto capPuerto)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbCapitaniaPuerto_Mantenedor";

                cnn.parametros.Add("@IdCapitaniaPuerto", capPuerto.idCapitaDePuerto);
                cnn.parametros.Add("@Codigo", capPuerto.codigo);
                cnn.parametros.Add("@CapitaniaPuerto", capPuerto.nombreCapitaniaDePuerto);
                cnn.parametros.Add("@requiereCert", capPuerto.requiereCert);

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

        public DataTable EliminarCapitaniaPuerto_Mantenedor(int idCapitaniaPuerto)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbCapitaniaPuerto_Mantenedor";
            cnn.parametros.Add("@IdCapitaniaPuerto", idCapitaniaPuerto);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<CapitaniaDePuerto> ListarCapitaniaPuerto_Mantenedor(CapitaniaDePuerto capPuertoFiltro)
        {
            try
            {
                List<CapitaniaDePuerto> resp = new List<CapitaniaDePuerto>();
                CapitaniaDePuerto param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCapitaniaPuerto_Mantenedor";
                if (capPuertoFiltro.idCapitaDePuerto > 0)
                {
                    cnn.parametros.Add("@IdCapitaniaPuerto", capPuertoFiltro.idCapitaDePuerto);
                }
                if (capPuertoFiltro.codigo > 0)
                {
                    cnn.parametros.Add("@Codigo", capPuertoFiltro.codigo);
                }
                if (capPuertoFiltro.nombreCapitaniaDePuerto != null && !capPuertoFiltro.nombreCapitaniaDePuerto.Equals(""))
                {
                    cnn.parametros.Add("@CapitaniaPuerto", capPuertoFiltro.nombreCapitaniaDePuerto);
                }
                if (capPuertoFiltro.requiereCert == 1 || capPuertoFiltro.requiereCert == 0)
                {
                    cnn.parametros.Add("@requiereCert", capPuertoFiltro.requiereCert);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new CapitaniaDePuerto();
                        param.idCapitaDePuerto = Convert.ToInt32(row["IdCapitaniaPuerto"]);
                        param.codigo = Convert.ToInt32(row["Codigo"]);
                        param.nombreCapitaniaDePuerto = row["CapitaniaPuerto"].ToString();
                        param.requiereCert = Convert.ToInt32(row["requiereCert"]);
                        resp.Add(param);
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

        public DataTable GuardarTipoConcesion_Mantenedor(int idTipoConcesion, string nombreTipoConcesion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoConcesion_Mantenedor";

                cnn.parametros.Add("@IdTipoConcesion", idTipoConcesion);
                cnn.parametros.Add("@TipoConcesion", nombreTipoConcesion);

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

        public DataTable EliminarTipoConcesion_Mantenedor(int idTipoConcesion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbTipoConcesion_Mantenedor";
            cnn.parametros.Add("@IdTipoConcesion", idTipoConcesion);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<ParametroGenerico> ListarTipoConcesion_Mantenedor(ParametroGenerico parametroGenerico)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoConcesion_Mantenedor";
                if (parametroGenerico.id > 0)
                {
                    cnn.parametros.Add("@IdTipoConcesion", parametroGenerico.id);
                }

                if (!parametroGenerico.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@TipoConcesion", parametroGenerico.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["IdTipoConcesion"]), row["TipoConcesion"].ToString());
                        resp.Add(param);

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

        public DataTable GuardarProvincia_Mantenedor(Provincia prov)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbProvincia_Mantenedor";

                cnn.parametros.Add("@IdProvincia", prov.id_provincia);
                cnn.parametros.Add("@Provincia", prov.provincia);
                cnn.parametros.Add("@IdRegion", prov.id_region);
                if (prov.codigo_provincia > 0)
                {
                    cnn.parametros.Add("@Codigo", prov.codigo_provincia);
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

        public DataTable EliminarProvincia_Mantenedor(int idProvincia)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbProvincia_Mantenedor";
            cnn.parametros.Add("@IdProvincia", idProvincia);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<Provincia> ListarProvincia_Mantenedor(Provincia provinciaFiltro)
        {
            try
            {
                List<Provincia> resp = new List<Provincia>();
                Provincia param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbProvincia_Mantenedor";
                if (provinciaFiltro.id_provincia > 0)
                {
                    cnn.parametros.Add("@IdProvincia", provinciaFiltro.id_provincia);
                }
                if (provinciaFiltro.id_region > 0)
                {
                    cnn.parametros.Add("@IdRegion", provinciaFiltro.id_region);
                }
                if (provinciaFiltro.provincia != null && !provinciaFiltro.provincia.Equals(""))
                {
                    cnn.parametros.Add("@nombreProv", provinciaFiltro.provincia);
                }
                if (provinciaFiltro.codigo_provincia > 0)
                {
                    cnn.parametros.Add("@codProv", provinciaFiltro.codigo_provincia);
                }
                if (provinciaFiltro.nombreReg != null && !provinciaFiltro.nombreReg.Equals(""))
                {
                    cnn.parametros.Add("@nombreReg", provinciaFiltro.nombreReg);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Provincia();
                        param.id_provincia = Convert.ToInt32(row["IdProvincia"]);
                        param.provincia = row["Provincia"].ToString();
                        param.id_region = Convert.ToInt32(row["IdRegion"]);
                        param.nombreReg = row["Region"].ToString();

                        if (!row.IsNull("Codigo"))
                        {
                            param.codigo_provincia = Convert.ToInt32(row["Codigo"]);
                        }
                        resp.Add(param);

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

        public DataTable GuardarComuna_Mantenedor(Comuna comunaAux)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbComuna_Mantenedor";

                cnn.parametros.Add("@IdComuna", comunaAux.id_comuna);
                cnn.parametros.Add("@Comuna", comunaAux.comuna);
                if (comunaAux.codigo > 0)
                {
                    cnn.parametros.Add("@Codigo", comunaAux.codigo);
                }

                cnn.parametros.Add("@IdProvincia", comunaAux.id_provincia);
                cnn.parametros.Add("@esFronteriza", comunaAux.esFronteriza);

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

        public DataTable EliminarComuna_Mantenedor(int idComuna)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbComuna_Mantenedor";
            cnn.parametros.Add("@IdComuna", idComuna);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<Comuna> ListarComuna_Mantenedor(Comuna comFiltro)
        {
            try
            {
                List<Comuna> resp = new List<Comuna>();
                Comuna param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComuna_Mantenedor";
                if (comFiltro.id_comuna > 0)
                {
                    cnn.parametros.Add("@IdComuna", comFiltro.id_comuna);
                }
                if (comFiltro.id_provincia > 0)
                {
                    cnn.parametros.Add("@IdProvincia", comFiltro.id_provincia);
                }
                if (comFiltro.comuna != null && !comFiltro.comuna.Equals(""))
                {
                    cnn.parametros.Add("@comuna", comFiltro.comuna);
                }
                if (comFiltro.codigo > 0)
                {
                    cnn.parametros.Add("@codComuna", comFiltro.codigo);
                }
                if (comFiltro.esFronterizaFiltro == 0 || comFiltro.esFronterizaFiltro == 1)
                {
                    cnn.parametros.Add("@esFronteriza", comFiltro.esFronterizaFiltro);
                }



                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Comuna();
                        param.id_comuna = Convert.ToInt32(row["IdComuna"]);
                        param.comuna = row["Comuna"].ToString();

                        if (!row.IsNull("Codigo"))
                        {
                            param.codigo = Convert.ToInt32(row["Codigo"]);
                        }

                        param.id_provincia = Convert.ToInt32(row["IdProvincia"]);
                        param.nombreProv = row["Provincia"].ToString();
                        param.esFronteriza = Convert.ToBoolean(row["esFronteriza"]);

                        resp.Add(param);

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

        public DataTable GuardarEspecieCultivo_Mantenedor(Especies especie)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEspecieCultivo_Mantenedor";

                cnn.parametros.Add("@IdEspecieCultivo", especie.id_especie);
                cnn.parametros.Add("@IdGrupoEspecie", especie.grupoEspecie.id_grupoEspecie);
                cnn.parametros.Add("@EspecieCultivo", especie.especieNombreComun);
                cnn.parametros.Add("@NombreCientifico", especie.especieNombreCientifico);
                cnn.parametros.Add("@CodigoSernapesca", especie.codigoSernapesca);
                cnn.parametros.Add("@exotica", especie.esExotica);
                cnn.parametros.Add("@esEspecieExperimental", especie.esExperimental);

                if (especie.grupoEspecieAutorizado != null && especie.grupoEspecieAutorizado.id_grupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoAutorizado", especie.grupoEspecieAutorizado.id_grupoEspecie);
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

        public DataTable EliminarEspecieCultivo_Mantenedor(int idEspecieCultivo)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbEspecieCultivo_Mantenedor";
            cnn.parametros.Add("@IdEspecieCultivo", idEspecieCultivo);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<Especies> ListarEspecieCultivo_Mantenedor(Especies especieFiltro)
        {
            try
            {
                List<Especies> resp = new List<Especies>();
                Especies param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEspecieCultivo_Mantenedor";
                if (especieFiltro.id_especie > 0)
                {
                    cnn.parametros.Add("@IdEspecieCultivo", especieFiltro.id_especie);
                }
                if (especieFiltro.grupoEspecie != null && especieFiltro.grupoEspecie.id_grupoEspecie > 0)
                {
                    cnn.parametros.Add("@IdGrupoEspecie", especieFiltro.grupoEspecie.id_grupoEspecie);
                }
                if (especieFiltro.grupoEspecie != null && especieFiltro.grupoEspecie.grupoEspecie != null && !especieFiltro.grupoEspecie.grupoEspecie.Equals(""))
                {
                    cnn.parametros.Add("@nombreGrupoEspecie", especieFiltro.grupoEspecie.grupoEspecie);
                }
                if (especieFiltro.especieNombreComun != null && !especieFiltro.especieNombreComun.Equals(""))
                {
                    cnn.parametros.Add("@nombreEspecieCultivo", especieFiltro.especieNombreComun);
                }
                if (especieFiltro.especieNombreCientifico != null && !especieFiltro.especieNombreCientifico.Equals(""))
                {
                    cnn.parametros.Add("@nombreCientifico", especieFiltro.especieNombreCientifico);
                }
                if (especieFiltro.codigoSernapesca > 0)
                {
                    cnn.parametros.Add("@codSernapesca", especieFiltro.codigoSernapesca);
                }
                if (especieFiltro.esExotica == 0 || especieFiltro.esExotica == 1)
                {
                    cnn.parametros.Add("@exotica", especieFiltro.esExotica);
                }
                if (especieFiltro.esExperimental == 0 || especieFiltro.esExperimental == 1)
                {
                    cnn.parametros.Add("@esEspecieExperimental", especieFiltro.esExperimental);
                }
                if (especieFiltro.grupoEspecieAutorizado != null && especieFiltro.grupoEspecieAutorizado.id_grupoEspecie > 0)
                {
                    cnn.parametros.Add("@idGrupoAutorizado", especieFiltro.grupoEspecieAutorizado.id_grupoEspecie);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Especies();
                        param.id_especie = Convert.ToInt32(row["IdEspecieCultivo"]);
                        param.grupoEspecie = new GrupoEspecie();
                        param.grupoEspecie.id_grupoEspecie = Convert.ToInt32(row["IdGrupoEspecie"]);
                        param.grupoEspecie.grupoEspecie = row["GrupoEspecie"].ToString();
                        if (!row.IsNull("idGrupoAutorizado"))
                        {
                            param.grupoEspecieAutorizado = new GrupoEspecie();
                            param.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(row["idGrupoAutorizado"]);
                        }
                        if (!row.IsNull("GrupoEspecieAutoriz"))
                        {
                            param.grupoEspecieAutorizado.grupoEspecie = row["GrupoEspecieAutoriz"].ToString();
                        }
                        param.especieNombreComun = row["EspecieCultivo"].ToString();

                        if (!row.IsNull("NombreCientifico"))
                        {
                            param.especieNombreCientifico = row["NombreCientifico"].ToString();
                        }

                        if (!row.IsNull("CodigoSernapesca"))
                        {
                            param.codigoSernapesca = Convert.ToInt32(row["CodigoSernapesca"]);
                        }
                        if (!row.IsNull("exotica"))
                        {
                            param.esExotica = Convert.ToInt32(row["exotica"]);
                        }
                        if (!row.IsNull("esEspecieExperimental"))
                        {
                            param.esExperimental = Convert.ToInt32(row["esEspecieExperimental"]);
                        }
                        resp.Add(param);

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

        public Especies obtenerEspeciesCultivo_Mantenedor(int idEspecie)
        {
            try
            {
                Especies param = null;
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbEspecieCultivo_Mantenedor";

                cnn.parametros.Add("@IdEspecieCultivo", idEspecie);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Especies();
                        param.id_especie = Convert.ToInt32(row["IdEspecieCultivo"]);
                        param.grupoEspecie = new GrupoEspecie();
                        param.grupoEspecie.id_grupoEspecie = Convert.ToInt32(row["IdGrupoEspecie"]);
                        param.grupoEspecie.grupoEspecie = row["GrupoEspecie"].ToString();
                        if (!row.IsNull("idGrupoAutorizado"))
                        {
                            param.grupoEspecieAutorizado = new GrupoEspecie();
                            param.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(row["idGrupoAutorizado"]);
                        }
                        if (!row.IsNull("GrupoEspecieAutoriz"))
                        {
                            param.grupoEspecieAutorizado.grupoEspecie = row["GrupoEspecieAutoriz"].ToString();
                        }
                        param.especieNombreComun = row["EspecieCultivo"].ToString();

                        if (!row.IsNull("NombreCientifico"))
                        {
                            param.especieNombreCientifico = row["NombreCientifico"].ToString();
                        }

                        if (!row.IsNull("CodigoSernapesca"))
                        {
                            param.codigoSernapesca = Convert.ToInt32(row["CodigoSernapesca"]);
                        }
                        if (!row.IsNull("exotica"))
                        {
                            param.esExotica = Convert.ToInt32(row["exotica"]);
                        }
                        if (!row.IsNull("esEspecieExperimental"))
                        {
                            param.esExperimental = Convert.ToInt32(row["esEspecieExperimental"]);
                        }
                    }
                }

                return param;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable GuardarEstructuraTecnica_Mantenedor(EstructuraTecnica estructuraTec)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEstructuraTecnica_Mantenedor";

                cnn.parametros.Add("@IdEstructuraTecnica", estructuraTec.idEstructura);
                cnn.parametros.Add("@EstructuraTecnica", estructuraTec.nombreEstructura);

                cnn.parametros.Add("@aplicaArea", estructuraTec.aplicaAreaFiltro);
                cnn.parametros.Add("@aplicaVolumen", estructuraTec.aplicaVolumenFiltro);

                //cnn.parametros.Add("@aplicaArea", estructuraTec.aplicaArea);
                //cnn.parametros.Add("@aplicaVolumen", estructuraTec.aplicaVolumen);

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

        public DataTable EliminarEstructuraTecnica_Mantenedor(int idEstructuraTecnica)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbEstructuraTecnica_Mantenedor";
            cnn.parametros.Add("@IdEstructuraTecnica", idEstructuraTecnica);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<EstructuraTecnica> ListarEstructuraTecnica_Mantenedor(EstructuraTecnica estructuraFiltro)
        {
            try
            {
                List<EstructuraTecnica> resp = new List<EstructuraTecnica>();
                EstructuraTecnica param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraTecnica_Mantenedor";
                if (estructuraFiltro.idEstructura > 0)
                {
                    cnn.parametros.Add("@IdEstructuraTecnica", estructuraFiltro.idEstructura);
                }
                if (estructuraFiltro.nombreEstructura != null && !estructuraFiltro.nombreEstructura.Equals(""))
                {
                    cnn.parametros.Add("@nombreEstructuraTecnica", estructuraFiltro.nombreEstructura);
                }
                if (estructuraFiltro.aplicaAreaFiltro > 0)
                {
                    cnn.parametros.Add("@aplicaArea", estructuraFiltro.aplicaAreaFiltro);
                }
                if (estructuraFiltro.aplicaVolumenFiltro > 0)
                {
                    cnn.parametros.Add("@aplicaVolumen", estructuraFiltro.aplicaVolumenFiltro);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new EstructuraTecnica();
                        param.idEstructura = Convert.ToInt32(row["IdEstructuraTecnica"]);
                        param.nombreEstructura = row["EstructuraTecnica"].ToString();
                        param.aplicaArea = Convert.ToBoolean(row["aplicaArea"]);
                        param.aplicaVolumen = Convert.ToBoolean(row["aplicaVolumen"]);

                        resp.Add(param);

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

        public DataTable GuardarGrupoEspecie_Mantenedor(GrupoEspecie grupoAux)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbGrupoEspecie_Mantenedor";

                cnn.parametros.Add("@IdGrupoEspecie", grupoAux.id_grupoEspecie);
                cnn.parametros.Add("@GrupoEspecie", grupoAux.grupoEspecie);
                //cnn.parametros.Add("@Cultivo", grupoAux.cultivo);
                cnn.parametros.Add("@Cultivo", grupoAux.cultivoFiltro);

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

        public DataTable EliminarGrupoEspecie_Mantenedor(int idGrupoEspecie)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbGrupoEspecie_Mantenedor";
            cnn.parametros.Add("@IdGrupoEspecie", idGrupoEspecie);

            DataTable dt = cnn.Execute();
            return dt;
        }


        public List<GrupoEspecie> ListarGrupoEspecie_Mantenedor(GrupoEspecie grupoEspecieFiltro)
        {
            try
            {
                List<GrupoEspecie> resp = new List<GrupoEspecie>();
                GrupoEspecie param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbGrupoEspecie_Mantenedor";
                if (grupoEspecieFiltro.id_grupoEspecie > 0)
                {
                    cnn.parametros.Add("@IdGrupoEspecie", grupoEspecieFiltro.id_grupoEspecie);
                }
                if (grupoEspecieFiltro.grupoEspecie != null && !grupoEspecieFiltro.grupoEspecie.Equals(""))
                {
                    cnn.parametros.Add("@nombreGrupo", grupoEspecieFiltro.grupoEspecie);
                }
                if (grupoEspecieFiltro.cultivoFiltro == 1 || grupoEspecieFiltro.cultivoFiltro == 0)
                {
                    cnn.parametros.Add("@Cultivo", grupoEspecieFiltro.cultivoFiltro);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new GrupoEspecie();
                        param.id_grupoEspecie = Convert.ToInt32(row["IdGrupoEspecie"]);
                        param.grupoEspecie = row["GrupoEspecie"].ToString();
                        param.cultivo = Convert.ToBoolean(row["Cultivo"]);

                        resp.Add(param);

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

        public DataTable GuardarBarrio_Mantenedor(Barrio barrioAux)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbBarrio_Mantenedor";

                cnn.parametros.Add("@IdBarrio", barrioAux.id_barrio);
                cnn.parametros.Add("@IdRegion", barrioAux.id_region);
                cnn.parametros.Add("@Barrio", barrioAux.barrio);
                cnn.parametros.Add("@IdMacrozona", barrioAux.id_macrozona);
                cnn.parametros.Add("@idTipoBarrio", barrioAux.tipo_barrio.id);
                cnn.parametros.Add("@idEstadoVigencia", barrioAux.vigencia.id);

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

        public DataTable EliminarBarrio_Mantenedor(int idBarrio)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbBarrio_Mantenedor";
            cnn.parametros.Add("@IdBarrio", idBarrio);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<Barrio> ListarBarrio_Mantenedor(Barrio barrioAux)
        {
            try
            {
                List<Barrio> resp = new List<Barrio>();
                Barrio param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbBarrio_Mantenedor";
                if (barrioAux.id_barrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", barrioAux.id_barrio);
                }
                if (barrioAux.barrio != null && !barrioAux.barrio.Equals(""))
                {
                    cnn.parametros.Add("@barrio", barrioAux.barrio);
                }
                if (barrioAux.id_region > 0)
                {
                    cnn.parametros.Add("@idRegion", barrioAux.id_region);
                }
                if (barrioAux.id_macrozona > 0)
                {
                    cnn.parametros.Add("@idMacrozona", barrioAux.id_macrozona);
                }
                if (barrioAux.tipo_barrio != null && barrioAux.tipo_barrio.id > 0)
                {
                    cnn.parametros.Add("@idTipoBarrio", barrioAux.tipo_barrio.id);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Barrio();
                        param.id_barrio = Convert.ToInt32(row["IdBarrio"]);
                        param.id_region = Convert.ToInt32(row["IdRegion"]);
                        param.nombreRegion = row["Region"].ToString();
                        param.barrio = row["Barrio"].ToString();
                        param.id_macrozona = Convert.ToInt32(row["IdMacrozona"]);
                        param.nombreMacrozona = row["Macrozona"].ToString();

                        if (!row.IsNull("idTipoBarrio"))
                        {
                            param.tipo_barrio = new ParametroGenerico(Convert.ToInt32(row["idTipoBarrio"]), row["NombreTipo"].ToString());
                        }

                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            param.vigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }

                        resp.Add(param);

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

        public DataTable GuardarCarta_Mantenedor(Carta cartaAux)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbCarta_Mantenedor";

                cnn.parametros.Add("@idCarta", cartaAux.idCarta);
                cnn.parametros.Add("@idRegion", cartaAux.region.id);
                cnn.parametros.Add("@idTipoCarta", cartaAux.tipoCarta.id);
                cnn.parametros.Add("@idEstadoVigencia", cartaAux.estadoVigencia.id);
                cnn.parametros.Add("@idDatum", cartaAux.datum.id);
                if (cartaAux.huso != null && cartaAux.huso.id > 0)
                {
                    cnn.parametros.Add("@idHuso", cartaAux.huso.id);
                }

                cnn.parametros.Add("@numeroCarta", cartaAux.numeroCarta);
                cnn.parametros.Add("@numEdicion", cartaAux.numeroEdicion);
                cnn.parametros.Add("@anioEdicion", cartaAux.anioEdicion);
                cnn.parametros.Add("@escala", cartaAux.escala);
                cnn.parametros.Add("@A_A_A", cartaAux.a_a_a_Filtro);
                if (cartaAux.reemplazoCarta != null && !cartaAux.reemplazoCarta.Equals(""))
                {
                    cnn.parametros.Add("@reemplazoCarta", cartaAux.reemplazoCarta);
                }
                if (cartaAux.fechaReemplazo != null && cartaAux.fechaReemplazo != default(DateTime))
                {
                    cnn.parametros.Add("@fechaReemplazo", cartaAux.fechaReemplazo);
                }
                if (cartaAux.observaciones != null && !cartaAux.observaciones.Equals(""))
                {
                    cnn.parametros.Add("@observaciones", cartaAux.observaciones);
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

        public DataTable EliminarCarta_Mantenedor(int idCarta)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbCarta_Mantenedor";
            cnn.parametros.Add("@idCarta", idCarta);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<Carta> ListarCarta_Mantenedor(Carta cartaFiltro)
        {
            try
            {

                DateTime date1901 = new DateTime(1901, 1, 1, 0, 0, 0);

                List<Carta> resp = new List<Carta>();
                Carta param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCarta_Mantenedor";
                if (cartaFiltro.idCarta > 0)
                {
                    cnn.parametros.Add("@idCarta", cartaFiltro.idCarta);
                }
                if (cartaFiltro.region != null && cartaFiltro.region.id > 0)
                {
                    cnn.parametros.Add("@idRegion", cartaFiltro.region.id);
                }
                if (cartaFiltro.region != null && cartaFiltro.region.descripcion != null && !cartaFiltro.region.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreRegion", cartaFiltro.region.descripcion);
                }
                if (cartaFiltro.tipoCarta != null && cartaFiltro.tipoCarta.id > 0)
                {
                    cnn.parametros.Add("@idTipoCarta", cartaFiltro.tipoCarta.id);
                }
                if (cartaFiltro.tipoCarta != null && cartaFiltro.tipoCarta.descripcion != null && !cartaFiltro.tipoCarta.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoCarta", cartaFiltro.tipoCarta.descripcion);
                }
                if (cartaFiltro.estadoVigencia != null && cartaFiltro.estadoVigencia.id > 0)
                {
                    cnn.parametros.Add("@idEstadoVigencia", cartaFiltro.estadoVigencia.id);
                }
                if (cartaFiltro.estadoVigencia != null && cartaFiltro.estadoVigencia.descripcion != null && !cartaFiltro.estadoVigencia.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreEstadoVig", cartaFiltro.estadoVigencia.descripcion);
                }
                if (cartaFiltro.datum != null && cartaFiltro.datum.id > 0)
                {
                    cnn.parametros.Add("@idDatum", cartaFiltro.datum.id);
                }
                if (cartaFiltro.datum != null && cartaFiltro.datum.descripcion != null && !cartaFiltro.datum.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreDatum", cartaFiltro.datum.descripcion);
                }
                if (cartaFiltro.huso != null && cartaFiltro.huso.id > 0)
                {
                    cnn.parametros.Add("@idHuso", cartaFiltro.huso.id);
                }
                if (cartaFiltro.huso != null && cartaFiltro.huso.descripcion != null && !cartaFiltro.huso.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreHuso", cartaFiltro.huso.descripcion);
                }
                if (cartaFiltro.numeroCarta != null && !cartaFiltro.numeroCarta.Equals(""))
                {
                    cnn.parametros.Add("@numeroCarta", cartaFiltro.numeroCarta);
                }
                if (cartaFiltro.numeroEdicion > 0)
                {
                    cnn.parametros.Add("@numEdicion", cartaFiltro.numeroEdicion);
                }
                if (cartaFiltro.anioEdicion > 0)
                {
                    cnn.parametros.Add("@anioEdicion", cartaFiltro.anioEdicion);
                }
                if (cartaFiltro.escala != null && !cartaFiltro.escala.Equals(""))
                {
                    cnn.parametros.Add("@escala", cartaFiltro.escala);
                }
                if (cartaFiltro.a_a_a_Filtro == 0 || cartaFiltro.a_a_a_Filtro == 1)
                {
                    cnn.parametros.Add("@aaa", cartaFiltro.a_a_a_Filtro);
                }
                if (cartaFiltro.reemplazoCarta != null && !cartaFiltro.reemplazoCarta.Equals(""))
                {
                    cnn.parametros.Add("@reemplazoCarta", cartaFiltro.reemplazoCarta);
                }
                if (cartaFiltro.fechaReemplazo != null && cartaFiltro.fechaReemplazo != default(DateTime))
                {
                    cnn.parametros.Add("@fechaReemplazo", cartaFiltro.fechaReemplazo);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Carta();
                        param.idCarta = Convert.ToInt32(row["idCarta"]);
                        if (!row.IsNull("idRegion"))
                        {
                            param.region = new ParametroGenerico(Convert.ToInt32(row["idRegion"]), row["Region"].ToString());
                        }
                        if (!row.IsNull("idTipoCarta"))
                        {
                            param.tipoCarta = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                        }
                        if (!row.IsNull("idEstadoVigencia"))
                        {
                            param.estadoVigencia = new ParametroGenerico(Convert.ToInt32(row["idEstadoVigencia"]), row["nombreEstado"].ToString());
                        }
                        if (!row.IsNull("idDatum"))
                        {
                            param.datum = new ParametroGenerico(Convert.ToInt32(row["idDatum"]), row["Datum"].ToString());
                        }

                        if (!row.IsNull("idHuso"))
                        {
                            param.huso = new ParametroGenerico(Convert.ToInt32(row["idHuso"]), row["nombreHuso"].ToString());
                        }
                        if (!row.IsNull("numeroCarta"))
                        {
                            param.numeroCarta = row["numeroCarta"].ToString();
                        }
                        if (!row.IsNull("numEdicion"))
                        {
                            param.numeroEdicion = Convert.ToInt32(row["numEdicion"]);
                        }

                        if (!row.IsNull("anioEdicion"))
                        {
                            param.anioEdicion = Convert.ToInt32(row["anioEdicion"]);
                        }
                        if (!row.IsNull("escala"))
                        {
                            param.escala = row["escala"].ToString();
                        }
                        if (!row.IsNull("A_A_A"))
                        {
                            param.a_a_a = Convert.ToBoolean(row["A_A_A"]);
                        }

                        if (!row.IsNull("reemplazoCarta"))
                        {
                            param.reemplazoCarta = row["reemplazoCarta"].ToString();
                        }
                        if (!row.IsNull("fechaReemplazo") && Convert.ToDateTime(row["fechaReemplazo"]) != default(DateTime) && DateTime.Compare(date1901, Convert.ToDateTime(row["fechaReemplazo"])) != 0)
                        {
                            param.fechaReemplazo = Convert.ToDateTime(row["fechaReemplazo"]);
                        }
                        if (!row.IsNull("observaciones"))
                        {
                            param.observaciones = row["observaciones"].ToString();
                        }


                        resp.Add(param);

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

        public DataTable GuardarEstadosGenerales_Mantenedor(int idEstado, string nombreEstado)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEstadosGenerales_Mantenedor";

                cnn.parametros.Add("@idEstado", idEstado);
                cnn.parametros.Add("@nombreEstado", nombreEstado);

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

        public DataTable EliminarEstadosGenerales_Mantenedor(int idEstado)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbEstadosGenerales_Mantenedor";
            cnn.parametros.Add("@idEstado", idEstado);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> ListarEstadosGenerales_Mantenedor(ParametroGenerico estadoGralFiltro)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstadosGenerales_Mantenedor";
                if (estadoGralFiltro.id > 0)
                {
                    cnn.parametros.Add("@idEstado", estadoGralFiltro.id);
                }
                if (estadoGralFiltro.descripcion != null && !estadoGralFiltro.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreEstado", estadoGralFiltro.descripcion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());
                        resp.Add(param);

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

        public DataTable GuardarPreferenciaRel_Mantenedor(int idPreferenciaRel, string nombrePreferenciaRel)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbPreferenciaRel_Mantenedor";

                cnn.parametros.Add("@idPreferenciaRel", idPreferenciaRel);
                cnn.parametros.Add("@nombrePreferenciaRel", nombrePreferenciaRel);

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

        public DataTable EliminarPreferenciaRel_Mantenedor(int idPreferenciaRel)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbPreferenciaRel_Mantenedor";
            cnn.parametros.Add("@idPreferenciaRel", idPreferenciaRel);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> ListarPreferenciaRel_Mantenedor(int idPreferenciaRel, string nombrePreferenciaRel)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPreferenciaRel_Mantenedor";
                if (idPreferenciaRel > 0)
                {
                    cnn.parametros.Add("@idPreferenciaRel", idPreferenciaRel);
                }
                if (nombrePreferenciaRel != null && !nombrePreferenciaRel.Equals(""))
                {
                    cnn.parametros.Add("@nombrePreferenciaRel", nombrePreferenciaRel);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idPreferenciaRel"]), row["nombrePreferenciaRel"].ToString());
                        resp.Add(param);

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
        public DataTable GuardaResponsableDAC_Mantenedor(int idResponsableDAC, string nombreRespDAC)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbResponsableDAC_Mantenedor";

                cnn.parametros.Add("@idResponsableDAC", idResponsableDAC);
                cnn.parametros.Add("@nombreRespDAC", nombreRespDAC);

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

        public DataTable EliminarResponsableDAC_Mantenedor(int idResponsableDAC)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbResponsableDAC_Mantenedor";
            cnn.parametros.Add("@idResponsableDAC", idResponsableDAC);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> ListarResponsableDAC_Mantenedor(ParametroGenerico responsableDacFiltro)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbResponsableDAC_Mantenedor";

                if (responsableDacFiltro.id > 0)
                {
                    cnn.parametros.Add("@idResponsableDAC", responsableDacFiltro.id);
                }
                if (responsableDacFiltro.descripcion != null && !responsableDacFiltro.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreRespDAC", responsableDacFiltro.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idResponsableDAC"]), row["nombreRespDAC"].ToString());
                        resp.Add(param);

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

        public List<ParametroGenerico> ListarTipoCarta_Mantenedor(int idTipoCarta)
        {
            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoCarta_Mantenedor";
                if (idTipoCarta > 0)
                {
                    cnn.parametros.Add("@idTipoCarta", idTipoCarta);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new ParametroGenerico(Convert.ToInt32(row["idTipoCarta"]), row["nombreTipoCarta"].ToString());
                        resp.Add(param);

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

        public DataTable GuardarEstadoSubRequerimiento_Mantenedor(int idSubRequerimiento, int idEstado)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEstadoSubRequerimiento_Mantenedor";

                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);
                cnn.parametros.Add("@idEstado", idEstado);

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

        public DataTable ListarEstadoSubRequerimiento_Mantenedor(int idSubRequerimiento)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paSelRbEstadoSubRequerimiento_Mantenedor";
            if (idSubRequerimiento > 0)
            {
                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);
            }

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<DocumentoAmbito> ListarEstadoSubReqEstado_Mantenedor(DocumentoAmbito docFiltro)
        {
            try
            {
                DocumentoAmbito docResp = null;
                List<DocumentoAmbito> resp = new List<DocumentoAmbito>();
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstadoSubReqEstado_Mantenedor";
                if (docFiltro.idRequerimiento > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", docFiltro.idRequerimiento);
                }
                if (docFiltro.tipo != null && docFiltro.tipo.descripcion != null && !docFiltro.tipo.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreSubRequerimiento", docFiltro.tipo.descripcion);
                }
                if (docFiltro.estadoResultadoResp != null && docFiltro.estadoResultadoResp.id > 0)
                {
                    cnn.parametros.Add("@idEstado", docFiltro.estadoResultadoResp.id);
                }
                if (docFiltro.estadoResultadoResp != null && docFiltro.estadoResultadoResp.descripcion != null && !docFiltro.estadoResultadoResp.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreEstado", docFiltro.estadoResultadoResp.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        docResp = new DocumentoAmbito();
                        docResp.tipo = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        docResp.estadoResultadoResp = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), row["nombreEstado"].ToString());

                        resp.Add(docResp);
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
        /*  
 //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
        public DataTable GuardarAsocBarrioTipo_Mantenedor(int idBarrio,int idTipoBarrio)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbAsocBarrioTipo";

                cnn.parametros.Add("@idTipoBarrio", idTipoBarrio);
                cnn.parametros.Add("@idBarrio", idBarrio);
                
                DataTable dt = cnn.Execute();
                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            };
        }*/
        /*  
 //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
        public DataTable EliminarAsocBarrioTipo_Mantenedor(int idBarrio, int idTipoBarrio)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbAsocBarrioTipo_Mantenedor";
            cnn.parametros.Add("@idBarrio", idBarrio);
            cnn.parametros.Add("@idTipoBarrio", idTipoBarrio);

            DataTable dt = cnn.Execute();
            return dt;
        }*/

        public DataTable GuardarSubRequerimiento_Mantenedor(int idSubRequerimiento, string nombreSubRequerimiento, bool aplicaReitera, bool aplicaComplementario, bool visacionMasiva)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSubRequerimiento_Mantenedor";

                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);
                cnn.parametros.Add("@nombreSubRequerimiento", nombreSubRequerimiento);
                cnn.parametros.Add("@aplicaReitera", aplicaReitera);
                cnn.parametros.Add("@aplicaComplementario", aplicaComplementario);
                cnn.parametros.Add("@visacionMasiva", visacionMasiva);

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

        public DataTable EliminarSubRequerimiento_Mantenedor(int idSubRequerimiento)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbSubRequerimiento_Mantenedor";
            cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public DataTable GuardarCuerpoAgua_Mantenedor(CuerpoDeAgua cuerpoAgua)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbCuerpoAgua_Mantenedor";

                cnn.parametros.Add("@idCuerpoAgua", cuerpoAgua.idCuerpoDeAgua);
                cnn.parametros.Add("@idTipoCuerpoAgua", cuerpoAgua.tipoCuerpoAgua.id);
                cnn.parametros.Add("@IdComuna", cuerpoAgua.comuna.id_comuna);
                cnn.parametros.Add("@IdRegion", cuerpoAgua.region.id_region);
                cnn.parametros.Add("@nombreCuerpoAgua", cuerpoAgua.nombreCuerpoAgua);

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

        public List<CuerpoDeAgua> ListarCuerpoAgua_Mantenedor(CuerpoDeAgua cuerpoAguaFiltro)
        {
            try
            {
                List<CuerpoDeAgua> resp = new List<CuerpoDeAgua>();
                CuerpoDeAgua cuerpoDeAgua = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCuerpoAgua_Mantenedor";
                if (cuerpoAguaFiltro.idCuerpoDeAgua > 0)
                {
                    cnn.parametros.Add("@idCuerpoAgua", cuerpoAguaFiltro.idCuerpoDeAgua);
                }
                if (cuerpoAguaFiltro.tipoCuerpoAgua != null && cuerpoAguaFiltro.tipoCuerpoAgua.id > 0)
                {
                    cnn.parametros.Add("@idTipoCuerpoAgua", cuerpoAguaFiltro.tipoCuerpoAgua.id);
                }
                if (cuerpoAguaFiltro.comuna != null && cuerpoAguaFiltro.comuna.id_comuna > 0)
                {
                    cnn.parametros.Add("@IdComuna", cuerpoAguaFiltro.comuna.id_comuna);
                }
                if (cuerpoAguaFiltro.region != null && cuerpoAguaFiltro.region.id_region > 0)
                {
                    cnn.parametros.Add("@IdRegion", cuerpoAguaFiltro.region.id_region);
                }
                if (cuerpoAguaFiltro.nombreCuerpoAgua != null && !cuerpoAguaFiltro.nombreCuerpoAgua.Equals(""))
                {
                    cnn.parametros.Add("@nombreCuerpoAgua", cuerpoAguaFiltro.nombreCuerpoAgua);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        cuerpoDeAgua = new CuerpoDeAgua();
                        cuerpoDeAgua.idCuerpoDeAgua = Convert.ToInt32(row["idCuerpoAgua"]);
                        cuerpoDeAgua.nombreCuerpoAgua = Convert.ToString(row["nombreCuerpoAgua"]);
                        cuerpoDeAgua.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(row["idTipoCuerpoAgua"]), Convert.ToString(row["nombreTipoCA"]));
                        cuerpoDeAgua.region = new Region();
                        cuerpoDeAgua.region.id_region = Convert.ToInt32(row["IdRegion"]);
                        cuerpoDeAgua.region.region = Convert.ToString(row["Region"]);
                        cuerpoDeAgua.comuna = new Comuna();
                        cuerpoDeAgua.comuna.id_comuna = Convert.ToInt32(row["IdComuna"]);
                        cuerpoDeAgua.comuna.comuna = Convert.ToString(row["Comuna"]);

                        resp.Add(cuerpoDeAgua);
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
        public DataTable EliminarCuerpoAgua_Mantenedor(int idCuerpoAgua)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbCuerpoAgua_Mantenedor";
            cnn.parametros.Add("@idCuerpoAgua", idCuerpoAgua);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<ParametroGenerico> ListarHolding_Mantenedor(ParametroGenerico holding)
        {
            try
            {
                //ParametroGenerico holding = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbHolding_Mantenedor";
                if (holding.id > 0)
                {
                    cnn.parametros.Add("@idHolding", holding.id);
                }
                if (holding.descripcion != null && !holding.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreHolding", holding.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        holding = new ParametroGenerico(Convert.ToInt32(row["idHolding"]), row["nombreHolding"].ToString());
                        resp.Add(holding);
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

        public DataTable GuardarHolding_Mantenedor(ParametroGenerico holding)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbHolding_Mantenedor";

                cnn.parametros.Add("@idHolding", holding.id);
                cnn.parametros.Add("@nombreHolding", holding.descripcion);

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

        public DataTable EliminarHolding_Mantenedor(int idHolding)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbHolding_Mantenedor";
            cnn.parametros.Add("@idHolding", idHolding);

            DataTable dt = cnn.Execute();
            return dt;
        }
        public DataTable GuardarTipoPersonaJuridica_Mantenedor(ParametroGenerico tipoPersonaJuridica)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoPersonaJuridica";

                cnn.parametros.Add("@idTipoPersJur", tipoPersonaJuridica.id);
                cnn.parametros.Add("@nombreTipoPersJur", tipoPersonaJuridica.descripcion);

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

        public DataTable EliminarTipoPersonaJuridica_Mantenedor(int idTipoPersJur)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbTipoPersonaJuridica";
            cnn.parametros.Add("@idTipoPersJur", idTipoPersJur);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public DataTable GuardarDescansoSanitario_Mantenedor(DescansoSanitario descanso)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDescansoSanitario_Mantenedor";

                cnn.parametros.Add("@IdBarrio", descanso.barrio.id);
                cnn.parametros.Add("@idTipoOperacion", descanso.tipoOperacion.id);
                cnn.parametros.Add("@numOperacion", descanso.numOperacion);
                cnn.parametros.Add("@fechaInicio", descanso.fechaInicio);
                cnn.parametros.Add("@fechaFin", descanso.fechaFin);
                if (descanso.fechaInicioProduccionCero != null && descanso.fechaInicioProduccionCero != default(DateTime))
                {
                    cnn.parametros.Add("@fechaInicioProduccionCero", descanso.fechaInicioProduccionCero);
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

        public DataTable EliminarDescansoSanitario_Mantenedor(int idDescanso)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbDescansoSanitario_Mantenedor";
            cnn.parametros.Add("@idDescanso", idDescanso);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public DescansoSanitario ObtenerDescansoSanitario_Mantenedor(int idDescanso, int idBarrio, DateTime fechaIniDesc, DateTime fechaFinDesc, DateTime fechaIniProd)
        {
            try
            {
                DescansoSanitario descanso = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDescansoSanitario_Mantenedor";
                if (idDescanso > 0)
                {
                    cnn.parametros.Add("@idDescanso", idDescanso);
                }
                if (idBarrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", idBarrio);
                }
                if (fechaIniDesc != null && fechaIniDesc != default(DateTime))
                {
                    cnn.parametros.Add("@fechaInicio", fechaIniDesc);
                }
                if (fechaFinDesc != null && fechaFinDesc != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFin", fechaFinDesc);
                }
                if (fechaIniProd != null && fechaIniProd != default(DateTime))
                {
                    cnn.parametros.Add("@fechaProd", fechaIniProd);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        descanso.idDescanso = Convert.ToInt32(row["idDescanso"]);
                        descanso.barrio = new ParametroGenerico(Convert.ToInt32(row["IdBarrio"]), row["Barrio"].ToString());
                        descanso.tipoOperacion = new ParametroGenerico(Convert.ToInt32(row["idTipoOperacion"]), row["nombreTipo"].ToString());
                        descanso.numOperacion = Convert.ToInt32(row["numOperacion"]);

                        if (!row.IsNull("fechaInicio"))
                        {
                            descanso.fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                        }
                        if (!row.IsNull("fechaFin"))
                        {
                            descanso.fechaFin = Convert.ToDateTime(row["fechaFin"]);
                        }
                        if (!row.IsNull("fechaInsercion"))
                        {
                            descanso.fechaInsercion = Convert.ToDateTime(row["fechaInsercion"]);
                        }

                    }

                }
                return descanso;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DescansoSanitario ObtenerUltimaOperacionDescanso_Mantenedor(int idBarrio)
        {
            try
            {
                DescansoSanitario descanso = new DescansoSanitario();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbUltimoDescansoSanitario_Mantenedor";

                cnn.parametros.Add("@idBarrio", idBarrio);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        descanso.idDescanso = Convert.ToInt32(row["idDescanso"]);
                        descanso.barrio = new ParametroGenerico(Convert.ToInt32(row["IdBarrio"]), row["Barrio"].ToString());
                        descanso.tipoOperacion = new ParametroGenerico(Convert.ToInt32(row["idTipoOperacion"]), row["nombreTipo"].ToString());
                        descanso.numOperacion = Convert.ToInt32(row["numOperacion"]);

                        if (!row.IsNull("fechaInicio"))
                        {
                            descanso.fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                        }
                        if (!row.IsNull("fechaFin"))
                        {
                            descanso.fechaFin = Convert.ToDateTime(row["fechaFin"]);
                        }
                        if (!row.IsNull("fechaInsercion"))
                        {
                            descanso.fechaInsercion = Convert.ToDateTime(row["fechaInsercion"]);
                        }

                    }

                }
                return descanso;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<DescansoSanitario> ListarDescansoSanitario_Mantenedor(int idDescanso, int idBarrio, DateTime fechaIniDesc, DateTime fechaFinDesc, DateTime fechaIniProd)
        {
            try
            {
                DescansoSanitario descanso = null;
                List<DescansoSanitario> resp = new List<DescansoSanitario>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDescansoSanitario_Mantenedor";
                //if (idDescanso > 0)
                //{
                //    cnn.parametros.Add("@idDescanso", idDescanso);
                //}
                //if (idBarrio > 0)
                //{
                //    cnn.parametros.Add("@idBarrio", idBarrio);
                //}

                if (idDescanso > 0)
                {
                    cnn.parametros.Add("@idDescanso", idDescanso);
                }
                if (idBarrio > 0)
                {
                    cnn.parametros.Add("@idBarrio", idBarrio);
                }
                if (fechaIniDesc != null && fechaIniDesc != default(DateTime))
                {
                    cnn.parametros.Add("@fechaInicio", fechaIniDesc);
                }
                if (fechaFinDesc != null && fechaFinDesc != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFin", fechaFinDesc);
                }
                if (fechaIniProd != null && fechaIniProd != default(DateTime))
                {
                    cnn.parametros.Add("@fechaProd", fechaIniProd);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        descanso = new DescansoSanitario();
                        descanso.idDescanso = Convert.ToInt32(row["idDescanso"]);
                        descanso.barrio = new ParametroGenerico(Convert.ToInt32(row["IdBarrio"]), row["Barrio"].ToString());
                        descanso.tipoOperacion = new ParametroGenerico(Convert.ToInt32(row["idTipoOperacion"]), row["nombreTipo"].ToString());
                        descanso.numOperacion = Convert.ToInt32(row["numOperacion"]);

                        if (!row.IsNull("fechaInicio"))
                        {
                            descanso.fechaInicio = Convert.ToDateTime(row["fechaInicio"]);
                        }
                        if (!row.IsNull("fechaFin"))
                        {
                            descanso.fechaFin = Convert.ToDateTime(row["fechaFin"]);
                        }
                        if (!row.IsNull("fechaInsercion"))
                        {
                            descanso.fechaInsercion = Convert.ToDateTime(row["fechaInsercion"]);
                        }

                        resp.Add(descanso);
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

        public DataTable GuardarEtapaDesarrollo_Mantenedor(EtapaCultivo etapaCultivo)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsEtapaDesarrollo_Mantenedor";


                cnn.parametros.Add("@IdEtapaDesarrollo", etapaCultivo.id_etapaDesarrollo);
                if (etapaCultivo.especie != null && etapaCultivo.especie.id_especie > 0)
                {
                    cnn.parametros.Add("@IdEspecie", etapaCultivo.especie.id_especie);
                }
                cnn.parametros.Add("@EtapaDesarrollo", etapaCultivo.nombreEtapaDesarrollo);
                cnn.parametros.Add("@CodigoEtapaDesarrollo", etapaCultivo.codigo);
                cnn.parametros.Add("@vigencia", etapaCultivo.estado.id);
                if (etapaCultivo.grupoEspecie != null && etapaCultivo.grupoEspecie.id > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", etapaCultivo.grupoEspecie.id);
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

        public DataTable EliminarEtapaDesarrollo_Mantenedor(int idEtapaDesarrollo, int idEspecie, int idGrupoEsp)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelEtapaDesarrollo_Mantenedor";

            cnn.parametros.Add("@IdEtapaDesarrollo", idEtapaDesarrollo);
            if(idEspecie>0){
                cnn.parametros.Add("@IdEspecie", idEspecie);
            }
            if (idGrupoEsp > 0)
            {
                cnn.parametros.Add("@idGrupoEspecie", idGrupoEsp);
            }


            DataTable dt = cnn.Execute();
            return dt;
        }

        public List<EtapaCultivo> ListarEtapaDesarrollo_Mantenedor(EtapaCultivo etapaDesarrolloFiltro)
        {
            try
            {
                List<EtapaCultivo> resp = new List<EtapaCultivo>();
                EtapaCultivo param = null;
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelEtapaDesarrollo_Mantenedor";

                if (etapaDesarrolloFiltro != null && etapaDesarrolloFiltro.id_etapaDesarrollo > 0)
                {
                    cnn.parametros.Add("@idEtapaDesarrollo", etapaDesarrolloFiltro.id_etapaDesarrollo);
                }
                if (etapaDesarrolloFiltro != null && etapaDesarrolloFiltro.idEspecie > 0)
                {
                    cnn.parametros.Add("@IdEspecie", etapaDesarrolloFiltro.idEspecie);
                }
                if (etapaDesarrolloFiltro != null && etapaDesarrolloFiltro.codigo > 0)
                {
                    cnn.parametros.Add("@CodigoEtapaDesarrollo", etapaDesarrolloFiltro.codigo);
                }
                if (etapaDesarrolloFiltro != null && !etapaDesarrolloFiltro.nombreEtapaDesarrollo.Equals(""))
                {
                    cnn.parametros.Add("@nombreEtapaDesarrollo", etapaDesarrolloFiltro.nombreEtapaDesarrollo);
                }
                if (etapaDesarrolloFiltro != null && etapaDesarrolloFiltro.estado != null && etapaDesarrolloFiltro.estado.id >= 0)
                {
                    cnn.parametros.Add("@vigencia", etapaDesarrolloFiltro.estado.id);
                }
                if (etapaDesarrolloFiltro != null && etapaDesarrolloFiltro.grupoEspecie != null && etapaDesarrolloFiltro.grupoEspecie.id > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", etapaDesarrolloFiltro.grupoEspecie.id);
                }

                DataTable dt = cnn.Execute();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        param = new EtapaCultivo();
                        param.id_etapaDesarrollo = Convert.ToInt32(row["IdEtapaDesarrollo"]);

                        if (!row.IsNull("IdEspecie"))
                        {
                            param.especie = new Especies();
                            param.especie.id_especie = Convert.ToInt32(row["IdEspecie"]);
                            param.especie.especieNombreComun = row["EspecieCultivo"].ToString();
                        }

                        param.codigo = Convert.ToInt32(row["CodigoEtapaDesarrollo"]);
                        param.nombreEtapaDesarrollo = row["EtapaDesarrollo"].ToString();

                        if (!row.IsNull("idGrupoEspecie") && !row.IsNull("GrupoEspecie"))
                        {
                            param.grupoEspecie = new ParametroGenerico(Convert.ToInt32(row["idGrupoEspecie"]), row["GrupoEspecie"].ToString());
                        }

                        if (!row.IsNull("vigencia"))
                        {
                            param.estado = new ParametroGenerico(Convert.ToInt32(row["vigencia"]), "");
                        }

                        resp.Add(param);

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

        public List<CuerpoDeAgua> ListarCuerpoAgua_Formulario(CuerpoDeAgua cuerpoAguaFiltro)
        {
            try
            {
                List<CuerpoDeAgua> resp = new List<CuerpoDeAgua>();
                CuerpoDeAgua cuerpoDeAgua = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbCuerpoAgua_Lista";
                if (cuerpoAguaFiltro.idCuerpoDeAgua > 0)
                {
                    cnn.parametros.Add("@idCuerpoAgua", cuerpoAguaFiltro.idCuerpoDeAgua);
                }
                if (cuerpoAguaFiltro.tipoCuerpoAgua != null && cuerpoAguaFiltro.tipoCuerpoAgua.id > 0)
                {
                    cnn.parametros.Add("@idTipoCuerpoAgua", cuerpoAguaFiltro.tipoCuerpoAgua.id);
                }
                if (cuerpoAguaFiltro.comunaCad != null && !cuerpoAguaFiltro.comunaCad.Equals(""))
                {
                    cnn.parametros.Add("@comunas", cuerpoAguaFiltro.comunaCad);
                }
                if (cuerpoAguaFiltro.region != null && cuerpoAguaFiltro.region.id_region > 0)
                {
                    cnn.parametros.Add("@IdRegion", cuerpoAguaFiltro.region.id_region);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        cuerpoDeAgua = new CuerpoDeAgua();
                        cuerpoDeAgua.idCuerpoDeAgua = Convert.ToInt32(row["idCuerpoAgua"]);
                        cuerpoDeAgua.nombreCuerpoAgua = Convert.ToString(row["nombreCuerpoAgua"]);
                        cuerpoDeAgua.tipoCuerpoAgua = new ParametroGenerico(Convert.ToInt32(row["idTipoCuerpoAgua"]), Convert.ToString(row["nombreTipoCA"]));
                        cuerpoDeAgua.region = new Region();
                        cuerpoDeAgua.region.id_region = Convert.ToInt32(row["IdRegion"]);
                        cuerpoDeAgua.region.region = Convert.ToString(row["Region"]);
                        cuerpoDeAgua.comuna = new Comuna();
                        cuerpoDeAgua.comuna.id_comuna = Convert.ToInt32(row["IdComuna"]);
                        cuerpoDeAgua.comuna.comuna = Convert.ToString(row["Comuna"]);

                        resp.Add(cuerpoDeAgua);
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

        public DataTable EliminarTipoAlimentoEspecie_Mantenedor(int idEspecieCultivo, int idTipoAlimento)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbTipoAlimentoEspecie_Mantenedor";
            cnn.parametros.Add("@idEspecieCultivo", idEspecieCultivo);
            cnn.parametros.Add("@idTipoAlimento", idTipoAlimento);

            DataTable dt = cnn.Execute();
            return dt;
        }

        public DataTable GuardarTipoAlimentoEspecie_Mantenedor(int idEspecieCultivo, int idTipoAlimento)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbTipoAlimentoEspecie_Mantenedor";

                cnn.parametros.Add("@idTipoAlimento", idTipoAlimento);
                cnn.parametros.Add("@idEspecieCultivo", idEspecieCultivo);

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

        public List<EspecieTipoAlimento> ListarTipoAlimentoEspecie(int idTipoAlimento, int idEspecieCultivo)
        {
            try
            {

                EspecieTipoAlimento especieTipoAlim = null;

                List<EspecieTipoAlimento> result = new List<EspecieTipoAlimento>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoAlimentoEspecie_Mantenedor";
                if (idTipoAlimento > 0)
                {
                    cnn.parametros.Add("@idTipoAlimento", idTipoAlimento);
                }
                if (idEspecieCultivo > 0)
                {
                    cnn.parametros.Add("@IdEspecieCultivo", idEspecieCultivo);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        especieTipoAlim = new EspecieTipoAlimento();
                        especieTipoAlim.tipoAlimento = new ParametroGenerico(Convert.ToInt32(row["idTipoAlimento"]), row["nombreTipo"].ToString());
                        especieTipoAlim.especie = new ParametroGenerico(Convert.ToInt32(row["IdEspecieCultivo"]), row["EspecieCultivo"].ToString());

                        result.Add(especieTipoAlim);

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable GuardarFeriados_Mantenedor(int idFeriado, DateTime fecha, string descripcion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbSipaFeriados_Mantenedor";

                if (idFeriado > 0)
                {
                    cnn.parametros.Add("@idFeriados", idFeriado);
                }

                cnn.parametros.Add("@fecha", fecha);
                cnn.parametros.Add("@descripcion", descripcion);

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

        public List<Feriado> ListarFeriados(int idFeriado, DateTime fecha, string descripcion)
        {
            try
            {

                Feriado feriado = null;

                List<Feriado> result = new List<Feriado>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSipaFeriados_Mantenedor";
                if (idFeriado > 0)
                {
                    cnn.parametros.Add("@idFeriado", idFeriado);
                }
                if (fecha != null && fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", fecha);
                }
                if (descripcion != null && !descripcion.Equals(""))
                {
                    cnn.parametros.Add("@descripcion", descripcion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        feriado = new Feriado();
                        feriado.idFeriado = Convert.ToInt32(row["IdFeriados"]);
                        if (!row.IsNull("Fecha"))
                        {
                            feriado.fecha = Convert.ToDateTime(row["Fecha"]);
                        }
                        if (!row.IsNull("Descripcion"))
                        {
                            feriado.descripcion = row["Descripcion"].ToString();
                        }

                        result.Add(feriado);

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable GuardarEntidadAnalisis_Mantenedor(EntidadMuestreador entidadAnalisis)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEntidadAnalisis_Mantenedor";

                cnn.parametros.Add("@numEntidadAnalisis", entidadAnalisis.numEntidad);
                cnn.parametros.Add("@razonSocial", entidadAnalisis.razonSocial);
                cnn.parametros.Add("@repLegal", entidadAnalisis.repLegal);
                cnn.parametros.Add("@domicilio", entidadAnalisis.domicilio);
                cnn.parametros.Add("@categoria", entidadAnalisis.categoria);
                cnn.parametros.Add("@numero", entidadAnalisis.numero);
                cnn.parametros.Add("@fecha", entidadAnalisis.fecha);
                cnn.parametros.Add("@inicioVigencia", entidadAnalisis.inicioVigencia);
                if (entidadAnalisis != null && entidadAnalisis.finVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@finVigencia", entidadAnalisis.finVigencia);
                }
                cnn.parametros.Add("@condVigencia", entidadAnalisis.condVigencia);
                if (entidadAnalisis != null && !entidadAnalisis.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", entidadAnalisis.correo);
                }
                if (entidadAnalisis != null && !entidadAnalisis.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", entidadAnalisis.fono);
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

        public List<EntidadMuestreador> ListarEntidadAnalisis_Mantenedor(EntidadMuestreador entidad)
        {
            try
            {

                EntidadMuestreador entidadFiltro = null;

                List<EntidadMuestreador> result = new List<EntidadMuestreador>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEntidadAnalisis_Mantenedor";

                if (entidad != null && entidad.numEntidad > 0)
                {
                    cnn.parametros.Add("@numEntidadAnalisis", entidad.numEntidad);
                }
                if (entidad != null && !entidad.razonSocial.Equals(""))
                {
                    cnn.parametros.Add("@razonSocial", entidad.razonSocial);
                }
                if (entidad != null && !entidad.repLegal.Equals(""))
                {
                    cnn.parametros.Add("@repLegal", entidad.repLegal);
                }
                if (entidad != null && !entidad.domicilio.Equals(""))
                {
                    cnn.parametros.Add("@domicilio", entidad.domicilio);
                }
                if (entidad != null && !entidad.categoria.Equals(""))
                {
                    cnn.parametros.Add("@categoria", entidad.categoria);
                }
                if (entidad != null && entidad.numero > 0)
                {
                    cnn.parametros.Add("@numero", entidad.numero);
                }
                if (entidad != null && entidad.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", entidad.fecha);
                }
                if (entidad != null && entidad.inicioVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@inicioVigencia", entidad.inicioVigencia);
                }
                if (entidad != null && entidad.finVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@finVigencia", entidad.finVigencia);
                }
                if (entidad != null && !entidad.condVigencia.Equals(""))
                {
                    cnn.parametros.Add("@condVigencia", entidad.condVigencia);
                }
                if (entidad != null && !entidad.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", entidad.correo);
                }
                if (entidad != null && !entidad.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", entidad.fono);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        entidadFiltro = new EntidadMuestreador();

                        entidadFiltro.numEntidad = Convert.ToInt32(row["numEntidadAnalisis"]);
                        entidadFiltro.razonSocial = row["razonSocial"].ToString();
                        entidadFiltro.repLegal = row["repLegal"].ToString();
                        entidadFiltro.domicilio = row["domicilio"].ToString();
                        entidadFiltro.categoria = row["categoria"].ToString();
                        if (!row.IsNull("numero"))
                        {
                            entidadFiltro.numero = Convert.ToInt32(row["numero"]);
                        }
                        if (!row.IsNull("fecha"))
                        {
                            entidadFiltro.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("inicioVigencia"))
                        {
                            entidadFiltro.inicioVigencia = Convert.ToDateTime(row["inicioVigencia"]);
                        }
                        if (!row.IsNull("finVigencia"))
                        {

                            entidadFiltro.finVigencia = Convert.ToDateTime(row["finVigencia"]);
                        }
                        entidadFiltro.condVigencia = row["condVigencia"].ToString();

                        if (!row.IsNull("correo"))
                        {
                            entidadFiltro.correo = row["correo"].ToString();

                        }
                        if (!row.IsNull("fono"))
                        {
                            entidadFiltro.fono = row["fono"].ToString();

                        }
                        result.Add(entidadFiltro);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<EntidadMuestreador> ListarEntidadAnalisis_Listar(EntidadMuestreador entidad)
        {
            try
            {

                EntidadMuestreador entidadFiltro = null;

                List<EntidadMuestreador> result = new List<EntidadMuestreador>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEntidadAnalisis_Listar";

                if (entidad != null && entidad.numEntidad > 0)
                {
                    cnn.parametros.Add("@numEntidadAnalisis", entidad.numEntidad);
                }
                if (entidad != null && !entidad.razonSocial.Equals(""))
                {
                    cnn.parametros.Add("@razonSocial", entidad.razonSocial);
                }
                if (entidad != null && !entidad.repLegal.Equals(""))
                {
                    cnn.parametros.Add("@repLegal", entidad.repLegal);
                }
                if (entidad != null && !entidad.domicilio.Equals(""))
                {
                    cnn.parametros.Add("@domicilio", entidad.domicilio);
                }
                if (entidad != null && !entidad.categoria.Equals(""))
                {
                    cnn.parametros.Add("@categoria", entidad.categoria);
                }
                if (entidad != null && entidad.numero > 0)
                {
                    cnn.parametros.Add("@numero", entidad.numero);
                }
                if (entidad != null && entidad.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", entidad.fecha);
                }
                if (entidad != null && entidad.inicioVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@inicioVigencia", entidad.inicioVigencia);
                }
                if (entidad != null && entidad.finVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@finVigencia", entidad.finVigencia);
                }
                if (entidad != null && !entidad.condVigencia.Equals(""))
                {
                    cnn.parametros.Add("@condVigencia", entidad.condVigencia);
                }
                if (entidad != null && !entidad.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", entidad.correo);
                }
                if (entidad != null && !entidad.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", entidad.fono);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        entidadFiltro = new EntidadMuestreador();

                        entidadFiltro.numEntidad = Convert.ToInt32(row["numEntidadAnalisis"]);
                        entidadFiltro.razonSocial = row["razonSocial"].ToString();
                        entidadFiltro.repLegal = row["repLegal"].ToString();
                        entidadFiltro.domicilio = row["domicilio"].ToString();
                        entidadFiltro.categoria = row["categoria"].ToString();
                        if (!row.IsNull("numero"))
                        {
                            entidadFiltro.numero = Convert.ToInt32(row["numero"]);
                        }
                        if (!row.IsNull("fecha"))
                        {
                            entidadFiltro.fecha = Convert.ToDateTime(row["fecha"]);
                        }
                        if (!row.IsNull("inicioVigencia"))
                        {
                            entidadFiltro.inicioVigencia = Convert.ToDateTime(row["inicioVigencia"]);
                        }
                        if (!row.IsNull("finVigencia"))
                        {

                            entidadFiltro.finVigencia = Convert.ToDateTime(row["finVigencia"]);
                        }
                        entidadFiltro.condVigencia = row["condVigencia"].ToString();

                        if (!row.IsNull("correo"))
                        {
                            entidadFiltro.correo = row["correo"].ToString();

                        }
                        if (!row.IsNull("fono"))
                        {
                            entidadFiltro.fono = row["fono"].ToString();

                        }
                        result.Add(entidadFiltro);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable EliminarEntidadAnalisis_Mantenedor(int numEntidadAnalisis)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEntidadAnalisis";
                cnn.parametros.Add("@numEntidadAnalisis", numEntidadAnalisis);

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


        //
        public DataTable GuardarMuestreadorAmbiental_Mantenedor(EntidadMuestreador entidadAnalisis)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbMuestreadorAmbiental_Mantenedor";

                cnn.parametros.Add("@numEntidadAnalisis", entidadAnalisis.numEntidad);
                cnn.parametros.Add("@razonSocial", entidadAnalisis.razonSocial);
                cnn.parametros.Add("@repLegal", entidadAnalisis.repLegal);
                cnn.parametros.Add("@domicilio", entidadAnalisis.domicilio);
                cnn.parametros.Add("@categoria", entidadAnalisis.categoria);
                cnn.parametros.Add("@numero", entidadAnalisis.numero);
                cnn.parametros.Add("@fecha", entidadAnalisis.fecha);
                cnn.parametros.Add("@inicioVigencia", entidadAnalisis.inicioVigencia);
                if (entidadAnalisis != null && entidadAnalisis.finVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@finVigencia", entidadAnalisis.finVigencia);
                }
                cnn.parametros.Add("@condVigencia", entidadAnalisis.condVigencia);
                if (entidadAnalisis != null && !entidadAnalisis.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", entidadAnalisis.correo);
                }
                if (entidadAnalisis != null && !entidadAnalisis.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", entidadAnalisis.fono);
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

        public List<EntidadMuestreador> ListarMuestreadorAmbiental_Mantenedor(EntidadMuestreador entidad)
        {
            try
            {

                EntidadMuestreador entidadFiltro = null;

                List<EntidadMuestreador> result = new List<EntidadMuestreador>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbMuestreadorAmbiental_Mantenedor";

                if (entidad != null && entidad.numEntidad > 0)
                {
                    cnn.parametros.Add("@numMuestreadorAmb", entidad.numEntidad);
                }
                if (entidad != null && !entidad.razonSocial.Equals(""))
                {
                    cnn.parametros.Add("@razonSocial", entidad.razonSocial);
                }
                if (entidad != null && !entidad.repLegal.Equals(""))
                {
                    cnn.parametros.Add("@repLegal", entidad.repLegal);
                }
                if (entidad != null && !entidad.domicilio.Equals(""))
                {
                    cnn.parametros.Add("@domicilio", entidad.domicilio);
                }
                if (entidad != null && !entidad.categoria.Equals(""))
                {
                    cnn.parametros.Add("@categoria", entidad.categoria);
                }
                if (entidad != null && entidad.numero > 0)
                {
                    cnn.parametros.Add("@numero", entidad.numero);
                }
                if (entidad != null && entidad.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", entidad.fecha);
                }
                if (entidad != null && entidad.inicioVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@inicioVigencia", entidad.inicioVigencia);
                }
                if (entidad != null && entidad.finVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@finVigencia", entidad.finVigencia);
                }
                if (entidad != null && !entidad.condVigencia.Equals(""))
                {
                    cnn.parametros.Add("@condVigencia", entidad.condVigencia);
                }
                if (entidad != null && !entidad.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", entidad.correo);
                }
                if (entidad != null && !entidad.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", entidad.fono);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        entidadFiltro = new EntidadMuestreador();

                        entidadFiltro.numEntidad = Convert.ToInt32(row["numMuestreadorAmb"]);
                        entidadFiltro.razonSocial = row["razonSocial"].ToString();
                        entidadFiltro.repLegal = row["repLegal"].ToString();
                        entidadFiltro.domicilio = row["domicilio"].ToString();
                        entidadFiltro.categoria = row["categoria"].ToString();
                        entidadFiltro.numero = Convert.ToInt32(row["numero"]);
                        entidadFiltro.fecha = Convert.ToDateTime(row["fecha"]);
                        entidadFiltro.inicioVigencia = Convert.ToDateTime(row["inicioVigencia"]);
                        if (!row.IsNull("finVigencia"))
                        {
                            entidadFiltro.finVigencia = Convert.ToDateTime(row["finVigencia"]);
                        }
                        entidadFiltro.condVigencia = row["condVigencia"].ToString();
                        if (!row.IsNull("correo"))
                        {
                            entidadFiltro.correo = row["correo"].ToString();
                        }
                        if (!row.IsNull("fono"))
                        {
                            entidadFiltro.fono = row["fono"].ToString();
                        }
                        result.Add(entidadFiltro);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<EntidadMuestreador> ListarMuestreadorAmbiental_Listar(EntidadMuestreador entidad)
        {
            try
            {

                EntidadMuestreador entidadFiltro = null;

                List<EntidadMuestreador> result = new List<EntidadMuestreador>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbMuestreadorAmbiental_Listar";

                if (entidad != null && entidad.numEntidad > 0)
                {
                    cnn.parametros.Add("@numMuestreadorAmb", entidad.numEntidad);
                }
                if (entidad != null && !entidad.razonSocial.Equals(""))
                {
                    cnn.parametros.Add("@razonSocial", entidad.razonSocial);
                }
                if (entidad != null && !entidad.repLegal.Equals(""))
                {
                    cnn.parametros.Add("@repLegal", entidad.repLegal);
                }
                if (entidad != null && !entidad.domicilio.Equals(""))
                {
                    cnn.parametros.Add("@domicilio", entidad.domicilio);
                }
                if (entidad != null && !entidad.categoria.Equals(""))
                {
                    cnn.parametros.Add("@categoria", entidad.categoria);
                }
                if (entidad != null && entidad.numero > 0)
                {
                    cnn.parametros.Add("@numero", entidad.numero);
                }
                if (entidad != null && entidad.fecha != default(DateTime))
                {
                    cnn.parametros.Add("@fecha", entidad.fecha);
                }
                if (entidad != null && entidad.inicioVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@inicioVigencia", entidad.inicioVigencia);
                }
                if (entidad != null && entidad.finVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@finVigencia", entidad.finVigencia);
                }
                if (entidad != null && !entidad.condVigencia.Equals(""))
                {
                    cnn.parametros.Add("@condVigencia", entidad.condVigencia);
                }
                if (entidad != null && !entidad.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", entidad.correo);
                }
                if (entidad != null && !entidad.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", entidad.fono);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        entidadFiltro = new EntidadMuestreador();

                        entidadFiltro.numEntidad = Convert.ToInt32(row["numMuestreadorAmb"]);
                        entidadFiltro.razonSocial = row["razonSocial"].ToString();
                        entidadFiltro.repLegal = row["repLegal"].ToString();
                        entidadFiltro.domicilio = row["domicilio"].ToString();
                        entidadFiltro.categoria = row["categoria"].ToString();
                        entidadFiltro.numero = Convert.ToInt32(row["numero"]);
                        entidadFiltro.fecha = Convert.ToDateTime(row["fecha"]);
                        entidadFiltro.inicioVigencia = Convert.ToDateTime(row["inicioVigencia"]);
                        if (!row.IsNull("finVigencia"))
                        {
                            entidadFiltro.finVigencia = Convert.ToDateTime(row["finVigencia"]);
                        }
                        entidadFiltro.condVigencia = row["condVigencia"].ToString();
                        if (!row.IsNull("correo"))
                        {
                            entidadFiltro.correo = row["correo"].ToString();
                        }
                        if (!row.IsNull("fono"))
                        {
                            entidadFiltro.fono = row["fono"].ToString();
                        }
                        result.Add(entidadFiltro);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable EliminarMuestreadorAmbiental_Mantenedor(int numMuestreadorAmb)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbMuestreadorAmbiental";
                cnn.parametros.Add("@numMuestreadorAmb", numMuestreadorAmb);

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


        //-----

        public DataTable GuardarConsultorAmbiental_Mantenedor(ConsultorAmbiental consultor)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbConsultorAmbiental_Mantenedor";

                cnn.parametros.Add("@numInscripcion", consultor.numInscripcion);
                cnn.parametros.Add("@nombre", consultor.nombre);
                cnn.parametros.Add("@fechaIniVigencia", consultor.fechaIniVigencia);
                if (consultor != null && consultor.fechaFinVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFinVigencia", consultor.fechaFinVigencia);
                }
                cnn.parametros.Add("@condVigencia", consultor.condVigencia);
                if (consultor != null && !consultor.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", consultor.correo);
                }
                if (consultor != null && !consultor.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", consultor.fono);
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

        public List<ConsultorAmbiental> ListarConsultorAmbiental_Mantenedor(ConsultorAmbiental consultor)
        {
            try
            {

                ConsultorAmbiental consultorAux = null;

                List<ConsultorAmbiental> result = new List<ConsultorAmbiental>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbConsultorAmbiental_Mantenedor";

                if (consultor != null && consultor.numInscripcion > 0)
                {
                    cnn.parametros.Add("@numInscripcion", consultor.numInscripcion);
                }
                if (consultor != null && !consultor.nombre.Equals(""))
                {
                    cnn.parametros.Add("@nombre", consultor.nombre);
                }
                if (consultor != null && consultor.fechaIniVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIniVigencia", consultor.fechaIniVigencia);
                }
                if (consultor != null && consultor.fechaFinVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFinVigencia", consultor.fechaFinVigencia);
                }
                if (consultor != null && !consultor.condVigencia.Equals(""))
                {
                    cnn.parametros.Add("@condVigencia", consultor.condVigencia);
                }
                if (consultor != null && !consultor.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", consultor.correo);
                }
                if (consultor != null && !consultor.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", consultor.fono);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        consultorAux = new ConsultorAmbiental();

                        consultorAux.numInscripcion = Convert.ToInt32(row["numInscripcion"]);
                        consultorAux.nombre = row["nombre"].ToString();
                        consultorAux.fechaIniVigencia = Convert.ToDateTime(row["fechaIniVigencia"]);
                        if (!row.IsNull("fechaFinVigencia"))
                        {
                            consultorAux.fechaFinVigencia = Convert.ToDateTime(row["fechaFinVigencia"]);
                        }
                        consultorAux.condVigencia = row["condVigencia"].ToString();
                        if (!row.IsNull("correo"))
                        {
                            consultorAux.correo = row["correo"].ToString();
                        }
                        if (!row.IsNull("fono"))
                        {
                            consultorAux.fono = row["fono"].ToString();
                        }
                        result.Add(consultorAux);

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ConsultorAmbiental> ListarConsultorAmbiental_Listar(ConsultorAmbiental consultor)
        {
            try
            {

                ConsultorAmbiental consultorAux = null;

                List<ConsultorAmbiental> result = new List<ConsultorAmbiental>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbConsultorAmbiental_Listar";

                if (consultor != null && consultor.numInscripcion > 0)
                {
                    cnn.parametros.Add("@numInscripcion", consultor.numInscripcion);
                }
                if (consultor != null && !consultor.nombre.Equals(""))
                {
                    cnn.parametros.Add("@nombre", consultor.nombre);
                }
                if (consultor != null && consultor.fechaIniVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@fechaIniVigencia", consultor.fechaIniVigencia);
                }
                if (consultor != null && consultor.fechaFinVigencia != default(DateTime))
                {
                    cnn.parametros.Add("@fechaFinVigencia", consultor.fechaFinVigencia);
                }
                if (consultor != null && !consultor.condVigencia.Equals(""))
                {
                    cnn.parametros.Add("@condVigencia", consultor.condVigencia);
                }
                if (consultor != null && !consultor.correo.Equals(""))
                {
                    cnn.parametros.Add("@correo", consultor.correo);
                }
                if (consultor != null && !consultor.fono.Equals(""))
                {
                    cnn.parametros.Add("@fono", consultor.fono);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        consultorAux = new ConsultorAmbiental();

                        consultorAux.numInscripcion = Convert.ToInt32(row["numInscripcion"]);
                        consultorAux.nombre = row["nombre"].ToString();
                        consultorAux.fechaIniVigencia = Convert.ToDateTime(row["fechaIniVigencia"]);
                        if (!row.IsNull("fechaFinVigencia"))
                        {
                            consultorAux.fechaFinVigencia = Convert.ToDateTime(row["fechaFinVigencia"]);
                        }
                        consultorAux.condVigencia = row["condVigencia"].ToString();
                        if (!row.IsNull("correo"))
                        {
                            consultorAux.correo = row["correo"].ToString();
                        }
                        if (!row.IsNull("fono"))
                        {
                            consultorAux.fono = row["fono"].ToString();
                        }
                        result.Add(consultorAux);

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable EliminarConsultorAmbiental_Mantenedor(int numInscripcion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbConsultorAmbiental";
                cnn.parametros.Add("@numInscripcion", numInscripcion);

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


        //--*************
        public DataTable GuardarDireccionZonal_Mantenedor(DireccionZonal dirZonal)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDireccionZonal_Mantenedor";

                cnn.parametros.Add("@codDirZonal", dirZonal.codDirZonal);
                cnn.parametros.Add("@nombreDirZonal", dirZonal.nombreDirZonal);
                cnn.parametros.Add("@esCentral", dirZonal.esCentral);

                if (dirZonal.descripcion != null && !dirZonal.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@descripcion", dirZonal.descripcion);
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

        public List<DireccionZonal> ListarDireccionZonal_Mantenedor(DireccionZonal dirZonal)
        {
            try
            {

                DireccionZonal dirZonalAux = null;

                List<DireccionZonal> result = new List<DireccionZonal>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDireccionZonal_Mantenedor";

                if (dirZonal != null && dirZonal.codDirZonal != null && !dirZonal.codDirZonal.Equals(""))
                {
                    cnn.parametros.Add("@codDirZonal", dirZonal.codDirZonal);
                }
                if (dirZonal != null && dirZonal.nombreDirZonal != null && !dirZonal.nombreDirZonal.Equals(""))
                {
                    cnn.parametros.Add("@nombreDirZonal", dirZonal.nombreDirZonal);
                }
                if (dirZonal != null && dirZonal.descripcion != null && !dirZonal.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@descripcion", dirZonal.descripcion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        dirZonalAux = new DireccionZonal();

                        dirZonalAux.codDirZonal = row["codDirZonal"].ToString();
                        dirZonalAux.nombreDirZonal = row["nombreDirZonal"].ToString();
                        if (!row.IsNull("descripcion"))
                        {
                            dirZonalAux.descripcion = row["descripcion"].ToString();
                        }

                        result.Add(dirZonalAux);

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable EliminarDireccionZonal_Mantenedor(string codDirZonal)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDireccionZonal";
                cnn.parametros.Add("@codDirZonal", codDirZonal);

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

        public DataTable GuardarDireccionZonalRegion_Mantenedor(DireccionZonal dirZonal)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbDireccionZonalRegion_Mantenedor";

                cnn.parametros.Add("@codDirZonal", dirZonal.codDirZonal);
                cnn.parametros.Add("@idRegion", dirZonal.region.id);

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

        public List<DireccionZonal> ListarDireccionZonalRegion_Mantenedor(string codDirZonal, int idRegion)
        {
            try
            {

                DireccionZonal dirZonalAux = null;

                List<DireccionZonal> result = new List<DireccionZonal>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbDireccionZonalRegion_Mantenedor";

                if (codDirZonal != null && !codDirZonal.Equals("") && !codDirZonal.Equals("-1"))
                {
                    cnn.parametros.Add("@codDirZonal", codDirZonal);
                }
                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        dirZonalAux = new DireccionZonal();

                        dirZonalAux.codDirZonal = row["codDirZonal"].ToString();
                        dirZonalAux.nombreDirZonal = row["nombreDirZonal"].ToString();
                        dirZonalAux.region = new ParametroGenerico(Convert.ToInt32(row["IdRegion"]), row["Region"].ToString());

                        result.Add(dirZonalAux);

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable EliminarDireccionZonalRegion_Mantenedor(string codDirZonal, int idRegion)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbDireccionZonalRegion";
                cnn.parametros.Add("@codDirZonal", codDirZonal);
                if (idRegion > 0)
                {
                    cnn.parametros.Add("@IdRegion", idRegion);
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

        public DataTable EliminarFeriados_Mantenedor(int idFeriado)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbSipaFeriados_Mantenedor";
                cnn.parametros.Add("@IdFeriados", idFeriado);

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

        public bool AplicaEspecieCultivo_Grupo(int idEspecieCultivo, int idGrupoEspecie)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEspecieCultivo_Mantenedor";

                cnn.parametros.Add("@IdEspecieCultivo", idEspecieCultivo);
                cnn.parametros.Add("@IdGrupoEspecie", idGrupoEspecie);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        return true;
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

        //
        public DataTable GuardarMateriaSubRequerimiento_Mantenedor(ResolucionValidacion materiaSubReq)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbMateriaSubRequerimiento_Mantenedor";

                cnn.parametros.Add("@idEquivalencia", materiaSubReq.idEquivalencia);

                if (materiaSubReq.tipoDocumento != null && materiaSubReq.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", materiaSubReq.tipoDocumento.id);
                }

                if (materiaSubReq.origen != null && materiaSubReq.origen.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", materiaSubReq.origen.id);
                }

                if (materiaSubReq.materia != null && materiaSubReq.materia.id > 0)
                {
                    cnn.parametros.Add("@idMateria", materiaSubReq.materia.id);
                }

                if (materiaSubReq.subRequerimiento != null && materiaSubReq.subRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", materiaSubReq.subRequerimiento.id);
                }

                if (materiaSubReq.estadoMateria != null && materiaSubReq.estadoMateria.id > 0)
                {
                    cnn.parametros.Add("@idEstadoMateria", materiaSubReq.estadoMateria.id);
                }

                if (materiaSubReq.estadoSubRequerimiento != null && materiaSubReq.estadoSubRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idEstadoSubReq", materiaSubReq.estadoSubRequerimiento.id);
                }

                cnn.parametros.Add("@numero", materiaSubReq.numero);
                cnn.parametros.Add("@fecha", materiaSubReq.fecha);
                cnn.parametros.Add("@numeroCI", materiaSubReq.numeroCI);
                cnn.parametros.Add("@fechaCI", materiaSubReq.fechaCI);

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

        public DataTable EliminarMateriaSubRequerimiento_Mantenedor(int idEquivalencia)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbMateriaSubRequerimiento_Mantenedor";
                cnn.parametros.Add("@idEquivalencia", idEquivalencia);

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

        public List<ResolucionValidacion> ListarMateriaSubRequerimiento_Mantenedor(ResolucionValidacion filtro)
        {

            try
            {
                List<ResolucionValidacion> resp = new List<ResolucionValidacion>();
                ResolucionValidacion valDocResp = null;

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateriaSubRequerimiento_Mantenedor";

                if (filtro.idEquivalencia > 0)
                {
                    cnn.parametros.Add("@idEquivalencia", filtro.idEquivalencia);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.origen != null && filtro.origen.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.origen.id);
                }
                if (filtro.materia != null && filtro.materia.id > 0)
                {
                    cnn.parametros.Add("@idMateria", filtro.materia.id);
                }
                if (filtro.subRequerimiento != null && filtro.subRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", filtro.subRequerimiento.id);
                }
                if (filtro.estadoMateria != null && filtro.estadoMateria.id > 0)
                {
                    cnn.parametros.Add("@idEstadoMateria", filtro.estadoMateria.id);
                }
                if (filtro.estadoSubRequerimiento != null && filtro.estadoSubRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idEstadoSubReq", filtro.estadoSubRequerimiento.id);
                }
                if (filtro.numero != null && filtro.numero > 0)
                {
                    cnn.parametros.Add("@numero", filtro.numero);
                }
                if (filtro.fecha != null && filtro.fecha > 0)
                {
                    cnn.parametros.Add("@fecha", filtro.fecha);
                }
                if (filtro.numeroCI != null && filtro.numeroCI > 0)
                {
                    cnn.parametros.Add("@numeroCI", filtro.numeroCI);
                }
                if (filtro.fechaCI != null && filtro.fechaCI > 0)
                {
                    cnn.parametros.Add("@fechaCI", filtro.fechaCI);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ResolucionValidacion();
                        valDocResp.idEquivalencia = Convert.ToInt32(row["idEquivalencia"]);
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipo"].ToString());
                        valDocResp.origen = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.materia = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());

                        if (!row.IsNull("idEstadoMateria"))
                        {
                            valDocResp.estadoMateria = new ParametroGenerico(Convert.ToInt32(row["idEstadoMateria"]), row["nombreEstado"].ToString());
                        }

                        if (!row.IsNull("idEstadoSubReq"))
                        {
                            valDocResp.estadoSubRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idEstadoSubReq"]), row["nombreEstadoSuReq"].ToString());
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);

                        resp.Add(valDocResp);
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

        public DataTable GuardarMateria_Mantenedor(ParametroGenerico materia)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbMateria_Mantenedor";

                cnn.parametros.Add("@idMateria", materia.id);
                cnn.parametros.Add("@nombreMateria", materia.descripcion);

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

        public DataTable EliminarMateria_Mantenedor(int idEquivalencia)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbMateria_Mantenedor";
                cnn.parametros.Add("@idMateria", idEquivalencia);

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

        public List<ParametroGenerico> ListarMateria_Mantenedor(ParametroGenerico materia)
        {

            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico mat = null;

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMateria_Mantenedor";

                if (materia.id > 0)
                {
                    cnn.parametros.Add("@idMateria", materia.id);
                }
                if (materia.descripcion != null && !materia.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreMateria", materia.descripcion);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        mat = new ParametroGenerico(Convert.ToInt32(row["idMateria"]), row["nombreMateria"].ToString());
                        resp.Add(mat);
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

        public Especies obtenerEspecieCultivo_Mantenedor(int idEspecie)
        {
            try
            {
                Especies param = null;
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbEspecieCultivo_Mantenedor";

                cnn.parametros.Add("@IdEspecieCultivo", idEspecie);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        param = new Especies();
                        param.id_especie = Convert.ToInt32(row["IdEspecieCultivo"]);
                        param.grupoEspecie = new GrupoEspecie();
                        param.grupoEspecie.id_grupoEspecie = Convert.ToInt32(row["IdGrupoEspecie"]);
                        param.grupoEspecie.grupoEspecie = row["GrupoEspecie"].ToString();
                        if (!row.IsNull("idGrupoAutorizado"))
                        {
                            param.grupoEspecieAutorizado = new GrupoEspecie();
                            param.grupoEspecieAutorizado.id_grupoEspecie = Convert.ToInt32(row["idGrupoAutorizado"]);
                        }
                        if (!row.IsNull("GrupoEspecieAutoriz"))
                        {
                            param.grupoEspecieAutorizado.grupoEspecie = row["GrupoEspecieAutoriz"].ToString();
                        }
                        param.especieNombreComun = row["EspecieCultivo"].ToString();

                        if (!row.IsNull("NombreCientifico"))
                        {
                            param.especieNombreCientifico = row["NombreCientifico"].ToString();
                        }

                        if (!row.IsNull("CodigoSernapesca"))
                        {
                            param.codigoSernapesca = Convert.ToInt32(row["CodigoSernapesca"]);
                        }
                        if (!row.IsNull("exotica"))
                        {
                            param.esExotica = Convert.ToInt32(row["exotica"]);
                        }
                        if (!row.IsNull("esEspecieExperimental"))
                        {
                            param.esExperimental = Convert.ToInt32(row["esEspecieExperimental"]);
                        }
                    }
                }

                return param;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable GuardarEstadosUOT_Mantenedor(ParametroGenerico estadosUOT)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEstadosUOT";

                cnn.parametros.Add("@idEstadoUOT", estadosUOT.id);
                cnn.parametros.Add("@nombreEstadoUOT", estadosUOT.descripcion);

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

        public List<ParametroGenerico> ListarEstadosUOT_Mantenedor(ParametroGenerico estadosUOT)
        {

            try
            {
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                ParametroGenerico estadosUOTAux = null;

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbEstadosUOT";

                if (estadosUOT.id > 0)
                {
                    cnn.parametros.Add("@idEstadoUOT", estadosUOT.id);
                }
                if (estadosUOT.descripcion != null && !estadosUOT.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreEstadoUOT", estadosUOT.descripcion);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        estadosUOTAux = new ParametroGenerico(Convert.ToInt32(row["idEstadoUOT"]), row["nombreEstadoUOT"].ToString());
                        resp.Add(estadosUOTAux);
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

        public DataTable EliminarEstadosUOT_Mantenedor(int idEstadoUOT)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEstadosUOT";
                cnn.parametros.Add("@idEstadoUOT", idEstadoUOT);

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

        public List<EquivalenciaUOT> ListarEquivalenciaEstadoUOT_Mantenedor(int idEstadoUOT, int idEstadoSolicitud, int idTipoUE)
        {
            try
            {
                List<EquivalenciaUOT> resp = new List<EquivalenciaUOT>();

                EquivalenciaUOT equiv = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEquivalenciaEstadoUOT";

                if (idEstadoUOT > 0)
                {
                    cnn.parametros.Add("@idEstadoUOT", idEstadoUOT);
                }
                if (idEstadoSolicitud > 0)
                {
                    cnn.parametros.Add("@idEstadoSolicitud", idEstadoSolicitud);
                }
                if (idTipoUE > 0)
                {
                    cnn.parametros.Add("@idTipoUE", idTipoUE);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        equiv = new EquivalenciaUOT();

                        equiv.estadoUOT = new ParametroGenerico(Convert.ToInt32(row["idEstadoUOT"]), row["nombreEstadoUOT"].ToString());
                        equiv.estadoSolicitud = new ParametroGenerico(Convert.ToInt32(row["idEstadoSolicitud"]), row["nombreEstado"].ToString());
                        equiv.tipoUE = new ParametroGenerico(Convert.ToInt32(row["idTipoUE"]), row["nombreTipo"].ToString());

                        resp.Add(equiv);

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
        public DataTable GuardarEquivalenciaEstadoUOT_Mantenedor(int idEstadoUOT,int idEstadoSolicitud,int idTipoUE)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEquivalenciaEstadoUOT";

                cnn.parametros.Add("@idEstadoUOT", idEstadoUOT);
                cnn.parametros.Add("@idEstadoSolicitud", idEstadoSolicitud);
                cnn.parametros.Add("@idTipoUE", idTipoUE);
                
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

        public DataTable EliminarEquivalenciaEstadoUOT_Mantenedor(int idEstadoUOT, int idEstadoSolicitud)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEquivalenciaEstadoUOT";
                cnn.parametros.Add("@idEstadoUOT", idEstadoUOT);
                cnn.parametros.Add("@idEstadoSolicitud", idEstadoSolicitud);

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

    }
}
