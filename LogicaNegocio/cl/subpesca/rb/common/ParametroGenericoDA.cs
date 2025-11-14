using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;


namespace LogicaNegocio.cl.subpesca.rb.common
{
    public class ParametroGenericoDA
    {
        Logger logger = new Logger();

        /**
        * Obtiene grupo autorizado asociado a un grupo en particular.
        */
        public List<ParametroGenerico> ListarGrupoEspecie(int idGrupo)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelGrupoEspecie";

                if (idGrupo > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupo);
                }
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row["IdGrupoEspecie"]), row["GrupoEspecie"].ToString());
                        resp.Add(grupo);
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

        /**
      * Obtiene grupo informativo asociado a un grupo en particular.
      */
        public List<ParametroGenerico> ListarGrupoEspecieInformativo(int idGrupo)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelGrupoEspecieInformativa";

                if (idGrupo > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupo);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row["IdGrupoEspecie"]), row["GrupoEspecie"].ToString());
                        resp.Add(grupo);
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

        /**
        * Obtiene grupo asoiciado a un poligono en particular.
        */
        public List<ParametroGenerico> ListarEstructuraTecnica(int idEstructuraTecnica)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelEstructuraTecnica";

                if (idEstructuraTecnica > 0)
                {
                    cnn.parametros.Add("@idEstructuraTecnica", idEstructuraTecnica);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]), row["EstructuraTecnica"].ToString());
                        resp.Add(grupo);
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

        /**
       * Lista generica que extrae tipos para listarlos en una colección de parametro generico
       */
        public List<ParametroGenerico> ListarGenerico(int varGenerica, string procedimiento, string nombreVariable,string columnaCodResp, string columnaNombreResp)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = procedimiento;

                if (varGenerica > 0)
                {
                    cnn.parametros.Add(nombreVariable, varGenerica);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row[columnaCodResp]), row[columnaNombreResp].ToString());
                        resp.Add(grupo);
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

        /**
     * Lista generica que extrae tipos para listarlos en una colección de parametro generico
     */
        public List<ParametroGenerico> ListarTiposGenerico(string procedimiento, string grupoVar,string grupoValTipo, string columnaCodResp, string columnaNombreResp)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = procedimiento;
                cnn.parametros.Add(grupoVar, grupoValTipo);
                

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row[columnaCodResp]), row[columnaNombreResp].ToString());
                        resp.Add(grupo);
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



        /**
       * Lista de las etapas de cultivo en base a una especie seleccionada
       */
        public List<ParametroGenerico> ListarEtapaDesarrolloEspecie(int idEspecieCultivo, int idEtapaDesarrollo)
        {
            try
            {
                ParametroGenerico etapa = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();
                
                Conexion cnn = new Conexion();
                
                cnn.procedimiento = "paSelEtapaDesarrolloEspecie";


                if (idEspecieCultivo>0) {
                    cnn.parametros.Add("@idEspecie", idEspecieCultivo);
                }

                if (idEtapaDesarrollo>0)
                {
                    cnn.parametros.Add("@idEtapaDesarrollo", idEtapaDesarrollo);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        etapa = new ParametroGenerico(Convert.ToInt32(row["IdEtapaDesarrollo"]), row["EtapaDesarrollo"].ToString());
                        resp.Add(etapa);
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

        public List<ParametroGenerico> ListarEtapaDesarrolloGrupo(int idGrupo)
        {
            try
            {
                ParametroGenerico etapa = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelEtapaDesarrolloGrupo";


                if (idGrupo > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", idGrupo);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        etapa = new ParametroGenerico(Convert.ToInt32(row["IdEtapaDesarrollo"]), row["EtapaDesarrollo"].ToString());
                        resp.Add(etapa);
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


        /**
      * Lista de especies
      */
        public List<ParametroGenerico> ListarEspecies(int idEspecieCultivo, string nombreEspecie, int idGrupoEspecie)
        {
            try
            {
                ParametroGenerico especie = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelEspecieCultivo";


                if (idEspecieCultivo > 0)
                {
                    cnn.parametros.Add("@IdEspecieCultivo", idEspecieCultivo);
                }

                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@IdGrupoEspecie", idGrupoEspecie);
                }

                if (nombreEspecie != null && !nombreEspecie.Equals(""))
                {
                    cnn.parametros.Add("@nombreEspecie", nombreEspecie);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        especie = new ParametroGenerico(Convert.ToInt32(row["IdEspecieCultivo"]), row["EspecieCultivo"].ToString());
                        resp.Add(especie);
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

       
        public ParametroGenerico ObtenerEspecies(int idEspecieCultivo, string nombreEspecie, int idGrupoEspecie)
        {
            try
            {
                ParametroGenerico especie = null;

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelEspecieCultivo";


                if (idEspecieCultivo > 0)
                {
                    cnn.parametros.Add("@IdEspecieCultivo", idEspecieCultivo);
                }

                if (idGrupoEspecie > 0)
                {
                    cnn.parametros.Add("@IdGrupoEspecie", idGrupoEspecie);
                }

                if (nombreEspecie != null && !nombreEspecie.Equals(""))
                {
                    cnn.parametros.Add("@nombreEspecie", nombreEspecie);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        especie = new ParametroGenerico(Convert.ToInt32(row["IdEspecieCultivo"]), row["EspecieCultivo"].ToString());
                    }
                }
                return especie;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ParametroGenerico> ListarGrupoEspecie(string especies)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbGrupoPorEspecies";

                cnn.parametros.Add("@especies", especies);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row["IdGrupoEspecie"]), row["nombreGrupo"].ToString());
                        resp.Add(grupo);
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

        /**
      * Lista de las formas de una estructura en base a una estructura seleccionada.
      */
        public List<ParametroGenerico> ListarFormaPorEstructura(int idEstructuraTecnica, int idTipoForma, string nombreTipoFormaEstructura)
        {
            try
            {
                ParametroGenerico forma = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbTipoFormaEstructura";


                cnn.parametros.Add("@idEstructuraTecnica", idEstructuraTecnica);
                
                if (idTipoForma > 0)
                {
                    cnn.parametros.Add("@idTipoForma", idTipoForma);
                }
                if (nombreTipoFormaEstructura!=null && !nombreTipoFormaEstructura.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoForma", nombreTipoFormaEstructura);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        forma = new ParametroGenerico(Convert.ToInt32(row["idTipoForma"]), row["nombreTipoForma"].ToString());
                        resp.Add(forma);
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

        public List<ParametroGenerico> ListarPreferenciaRelocalizacion(ParametroGenerico preferenciaFiltro)
        {
            try
            {
                ParametroGenerico preferencia = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbPreferenciaRelocalizacion";

                if (preferenciaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idPreferenciaRel", preferenciaFiltro.id);
                }
                if (preferenciaFiltro.descripcion != null && !preferenciaFiltro.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombrePreferenciaRel", preferenciaFiltro.descripcion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        preferencia = new ParametroGenerico(Convert.ToInt32(row["idPreferenciaRel"]), row["nombrePreferenciaRel"].ToString());
                        resp.Add(preferencia);
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

        public List<ParametroGenerico> ListarComunas(int idRegion)
        {
            try
            {
                ParametroGenerico comuna = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComunaReg";

                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                        resp.Add(comuna);
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

        public ParametroGenerico ObtenerComuna(int idRegion, int idProvincia)
        {
            try
            {
                ParametroGenerico comuna = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComunaReg";
                if(idRegion>0){
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
                
               
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                    }

                }

                return comuna;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ParametroGenerico> ListarComuna(int idRegion, int idProvincia)
        {
            try
            {
                ParametroGenerico comuna = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComunaReg";
                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        comuna = new ParametroGenerico(Convert.ToInt32(row["IdComuna"]), row["Comuna"].ToString());
                        resp.Add(comuna);
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




        public DataTable ListarComunaDataTable(int idRegion, int idProvincia)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbComunaReg";
                if (idRegion > 0)
                {
                    cnn.parametros.Add("@idRegion", idRegion);
                }
                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }


                DataTable dt = cnn.Execute();
                return dt;

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public List<ParametroGenerico> ListarProvinciaReg(int idProvincia, int idRegion)
        {
            try
            {
                ParametroGenerico prov = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelProvincia";

                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
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

                        prov = new ParametroGenerico(Convert.ToInt32(row["IdProvincia"]), row["Provincia"].ToString());
                        resp.Add(prov);
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


        public DataTable ListarProvinciaRegDataTable(int idProvincia, int idRegion)
        {
            try
            {
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelProvincia";

                if (idProvincia > 0)
                {
                    cnn.parametros.Add("@idProvincia", idProvincia);
                }
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
            }
        }

        public List<ParametroGenerico> ListarTipoPersonaJuridica(ParametroGenerico tipoPersonaFiltro)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoPersonaJuridica";

                if (tipoPersonaFiltro.id > 0)
                {
                    cnn.parametros.Add("@idTipoPersJur", tipoPersonaFiltro.id);
                }
                if (tipoPersonaFiltro.descripcion != null && !tipoPersonaFiltro.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoPers", tipoPersonaFiltro.descripcion);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row["idTipoPersJur"]), row["nombreTipoPersJur"].ToString());
                        resp.Add(grupo);
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

        public List<ParametroGenerico> ListarMenu(int idMenu)
        {
            try
            {
                ParametroGenerico menu = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMenu";


                if (idMenu > 0)
                {
                    cnn.parametros.Add("@idMenu", idMenu);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        menu = new ParametroGenerico(Convert.ToInt32(row["idMenu"]), row["nombreMenu"].ToString());
                        resp.Add(menu);
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

        public List<ParametroGenerico> ListarMenuSistema(int idMenu, int idModulo)
        {
            try
            {
                ParametroGenerico menu = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbMenuSistema";


                if (idMenu > 0)
                {
                    cnn.parametros.Add("@idMenu", idMenu);
                }
                if (idModulo > 0)
                {
                    cnn.parametros.Add("@idModulo", idModulo);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        menu = new ParametroGenerico(Convert.ToInt32(row["idMenu"]), row["nombreMenuSist"].ToString());
                        resp.Add(menu);
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

        public DataTable ListarTipoTramiteEstadoSolicitud(int idTipoTramite)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoTramiteEstadoSolicitud";

                if (idTipoTramite > 0)
                {
                    cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                }
                
                DataTable dt = cnn.Execute();

                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public DataTable ListarTipoTramiteSolicitud()
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoTramiteSolicitud";

                DataTable dt = cnn.Execute();

                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public DataTable ListarModuloSistema(int idModuloSistema)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbModuloSistema";

                if (idModuloSistema > 0)
                {
                    cnn.parametros.Add("@idModuloSistema", idModuloSistema);
                }

                DataTable dt = cnn.Execute();

                return dt;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<ParametroGenerico> ListarTipoTramite_UE(int idUE)
        {
            try
            {
                ParametroGenerico tipoTramite = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTipoTramite_UE";

                cnn.parametros.Add("@idUE", idUE);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        tipoTramite = new ParametroGenerico(Convert.ToInt32(row["idTipo"]), row["nombreTipo"].ToString());
                        resp.Add(tipoTramite);
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

        /**
         * Obtiene los documentos que se pueden visar dependiendo del tipo de tramite
         **/ 
        public List<ParametroGenerico> ListarTipoDocumentoVisacion(int idTipoTramite)
        {
            try
            {

                ParametroGenerico tipoVisacion = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSubReq_Visa";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        tipoVisacion = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        resp.Add(tipoVisacion);
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

        public List<ParametroGenerico> ListarTipoDocumentoResultadoVisacion(int idTipoTramite, int idSubRequerimiento)
        {
            try
            {

                ParametroGenerico tipoVisacion = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbSubReqResultado_Visa";
                cnn.parametros.Add("@idTipoTramite", idTipoTramite);
                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        tipoVisacion = new ParametroGenerico(Convert.ToInt32(row["idResultado"]), row["nombreEstado"].ToString());
                        resp.Add(tipoVisacion);
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

        public ParametroGenerico ObtenerParametro(string nombreParametro)
        {
            try
            {
                ParametroGenerico param = null;

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbParametro";

                cnn.parametros.Add("@nombreParametro", nombreParametro);
               
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        param = new ParametroGenerico();
                        param.clave = row["nombreParametro"].ToString();
                        param.descripcion = row["valorParametro"].ToString();
                            
                         
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

        public List<ParametroGenerico> ListarRbGrupoEspecie(ParametroGenerico grupoFiltro)
        {
            try
            {
                ParametroGenerico grupo = null;
                List<ParametroGenerico> resp = new List<ParametroGenerico>();

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbGrupoEspecie";

                if (grupoFiltro!=null && grupoFiltro.id > 0)
                {
                    cnn.parametros.Add("@idGrupoEspecie", grupoFiltro.id);
                }
                if (grupoFiltro != null && grupoFiltro.descripcion!=null && !grupoFiltro.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreGrupo", grupoFiltro.descripcion);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        grupo = new ParametroGenerico(Convert.ToInt32(row["IdGrupoEspecie"]), row["GrupoEspecie"].ToString());
                        resp.Add(grupo);
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
