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
    public class EstructuraTecnicaPT_DA
    {

        Logger logger = new Logger();

        public bool GuardarEstructuraTecnicaPT(EstructuraTecnicaPT estructuraTecPT, int idUsuario, int idSolicitud)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbEstructuraProyectoTecnico";

                
                cnn.parametros.Add("@idEstructProyTecnico", estructuraTecPT.idEstructPT);
                
                if (estructuraTecPT.idProyTec>0){
                    cnn.parametros.Add("@idProyectoTecnico", estructuraTecPT.idProyTec);
                }
                if (estructuraTecPT.tipoEstructura!=null && estructuraTecPT.tipoEstructura.id>0){
                    cnn.parametros.Add("@IdEstructuraTecnica", estructuraTecPT.tipoEstructura.id);
                }
                if (estructuraTecPT.formaEstructura!=null && estructuraTecPT.formaEstructura.id>0){
                    cnn.parametros.Add("@idTipoForma", estructuraTecPT.formaEstructura.id);
                }
                if (estructuraTecPT.unidadMedida!=null && estructuraTecPT.unidadMedida.id>0){
                    cnn.parametros.Add("@idTipoMedida", estructuraTecPT.unidadMedida.id);
                }
                if (estructuraTecPT.volumenUnidadMedida != null && estructuraTecPT.volumenUnidadMedida.id>0){
                    cnn.parametros.Add("@idTipoVolumenUM", estructuraTecPT.volumenUnidadMedida.id);
                }
                if (estructuraTecPT.tipoAnio!=null && estructuraTecPT.tipoAnio.id > 0){
                    cnn.parametros.Add("@idTipoEstructuraPorAnio", estructuraTecPT.tipoAnio.id);
                }
                cnn.parametros.Add("@idEstado", rbEstadosGenerales.VIGENTE);
                
                if (estructuraTecPT.largo>0){
                    cnn.parametros.Add("@largo", estructuraTecPT.largo);
                }
                if (estructuraTecPT.ancho>0){
                    cnn.parametros.Add("@ancho", estructuraTecPT.ancho);
                }
                if (estructuraTecPT.alto > 0){
                    cnn.parametros.Add("@alto", estructuraTecPT.alto);
                }
                if (estructuraTecPT.diametro>0){
                    cnn.parametros.Add("@diametro", estructuraTecPT.diametro);
                }
                if (estructuraTecPT.volumenValorMedida>0){
                    cnn.parametros.Add("@volumenValorUM", estructuraTecPT.volumenValorMedida);
                }
                if (estructuraTecPT.totalAcumNumero>0){
                    cnn.parametros.Add("@total", estructuraTecPT.totalAcumNumero);
                }
                if(estructuraTecPT.totalAcumDim>0){
                    cnn.parametros.Add("@totalAcumDim", estructuraTecPT.totalAcumDim);
                }
                if (estructuraTecPT.densidadSiembra>0){
                    cnn.parametros.Add("@densidadSiembra", estructuraTecPT.densidadSiembra);
                }
                if (estructuraTecPT.numColectores>=0){
                    cnn.parametros.Add("@numColectores", estructuraTecPT.numColectores);
                }
                if (estructuraTecPT.numLineas>=0){
                    cnn.parametros.Add("@numLineas", estructuraTecPT.numLineas);
                }
                
                if (idUsuario > 0)
                {
                    cnn.parametros.Add("@idUsuario", idUsuario);
                }
                if (idSolicitud > 0)
                {
                    cnn.parametros.Add("@idSolConcesion", idSolicitud);
                }


                DataTable dt = cnn.Execute();
                estructuraTecPT.idEstructPT = Convert.ToInt32(dt.Rows[0]["idEstructProyTecnico"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool GuardarValorEstructuraPorAnio(ValorParametroAnioPT valorParam)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbValorEstructuraPorAnio";
                cnn.parametros.Add("@idRegistroValor", valorParam.idRegistro);
                cnn.parametros.Add("@idEstructProyTecnico", valorParam.idclaveParametro);
                cnn.parametros.Add("@valor", valorParam.valor);
                cnn.parametros.Add("@anio", valorParam.anio);
               
                DataTable dt = cnn.Execute();
                valorParam.idRegistro = Convert.ToInt32(dt.Rows[0]["idRegistroValor"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

        public bool EliminarValorEstructuraPorAnio(int idEstructProyTecnico, int idUsuario, int idSolConcesion )
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbValorEstructuraPorAnio";
                cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);

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

        public bool EliminarEstructuraProyectoTecnico(int idEstructProyTecnico, int idUsuario)
        {
            try
            {
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paDelRbEstructuraProyectoTecnico";
                cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
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


        public EstructuraTecnicaPT ObtenerEstructuraProyectoTecnico(int idProyTecnico, int idEstructProyTecnico)
        {
            try
            {
                EstructuraTecnicaPT estructura = null;
                int idEstPT = 0;
                int idEstPTAux = 0;
                ValorParametroAnioPT valorAnio = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraProyectoTecnico";
                if (idProyTecnico > 0)
                {
                    cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                }
                if (idEstructProyTecnico > 0)
                {
                    cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idEstPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                        if (idEstPT != idEstPTAux)
                        {
                            estructura = new EstructuraTecnicaPT();
                            estructura.idEstructPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                            estructura.idProyTec = Convert.ToInt32(row["idProyectoTecnico"]);
                            if (!row.IsNull("IdEstructuraTecnica"))
                            {
                                estructura.tipoEstructura = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]), row["EstructuraTecnica"].ToString());
                            }
                            if (!row.IsNull("idTipoForma"))
                            {
                                estructura.formaEstructura = new ParametroGenerico(Convert.ToInt32(row["idTipoForma"]), row["nombreTipoForma"].ToString());
                            }
                            if (!row.IsNull("idTipoMedida"))
                            {
                                estructura.unidadMedida = new ParametroGenerico(Convert.ToInt32(row["idTipoMedida"]), row["nombreTipoMedida"].ToString());
                            }
                            if (!row.IsNull("idTipoVolumenUM"))
                            {
                                estructura.volumenUnidadMedida = new ParametroGenerico(Convert.ToInt32(row["idTipoVolumenUM"]), row["nombreTipoVolumen"].ToString());
                            }
                            if (!row.IsNull("idTipoEstructuraPorAnio"))
                            {
                                estructura.tipoAnio = new ParametroGenerico(Convert.ToInt32(row["idTipoEstructuraPorAnio"]), row["nombreTipoEstructAnio"].ToString());
                            }
                            if (!row.IsNull("idEstado"))
                            {
                                estructura.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), "");
                            }
                            if (!row.IsNull("largo"))
                            {
                                estructura.largo = Convert.ToSingle(row["largo"]);
                            }
                            if (!row.IsNull("ancho"))
                            {
                                estructura.ancho = Convert.ToSingle(row["ancho"]);
                            }
                            if (!row.IsNull("alto"))
                            {
                                estructura.alto = Convert.ToSingle(row["alto"]);
                            }
                            if (!row.IsNull("diametro"))
                            {
                                estructura.diametro = Convert.ToSingle(row["diametro"]);
                            }
                            if (!row.IsNull("volumenValorUM"))
                            {
                                estructura.volumenValorMedida = Convert.ToSingle(row["volumenValorUM"]);
                            }
                            if (!row.IsNull("densidadSiembra"))
                            {
                                estructura.densidadSiembra = Convert.ToSingle(row["densidadSiembra"]);
                            }
                            if (!row.IsNull("total"))
                            {
                                estructura.totalAcumNumero = Convert.ToSingle(row["total"]);
                            }
                            if (!row.IsNull("totalAcumDim"))
                            {
                                estructura.totalAcumDim = Convert.ToSingle(row["totalAcumDim"]);
                            }
                            if (!row.IsNull("numColectores"))
                            {
                                estructura.numColectores = Convert.ToInt32(row["numColectores"]);
                            }
                            if (!row.IsNull("numLineas"))
                            {
                                estructura.numColectores = Convert.ToInt32(row["numLineas"]);
                            }

                            estructura.anios = new List<ValorParametroAnioPT>();
                        }

                        if (!row.IsNull("idRegistroValor"))
                        {
                            valorAnio = new ValorParametroAnioPT();
                            valorAnio.idRegistro = Convert.ToInt32(row["idRegistroValor"]);
                            valorAnio.idclaveParametro = Convert.ToInt32(row["idEstructProyTecnico"]);
                            valorAnio.anio = Convert.ToInt32(row["anio"]);
                            valorAnio.valor = Convert.ToInt32(row["valor"]);
                            estructura.anios.Add(valorAnio);
                        }
                        
                        idEstPTAux = idEstPT;
                        
                    }
                }

                return estructura;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<EstructuraTecnicaPT> ListarEstructuraProyectoTecnico(int idProyTecnico, int idEstructProyTecnico)
        {
            try
            {
                List<EstructuraTecnicaPT> resp = new List<EstructuraTecnicaPT>();
                EstructuraTecnicaPT estructura = null;
                int idEstPT = 0;
                int idEstPTAux = 0;
                ValorParametroAnioPT valorAnio = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraProyectoTecnico";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                if (idEstructProyTecnico>0)
                {
                    cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
                }
                

                DataTable dt = cnn.Execute();
                int indes = 0;
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                       idEstPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                       if(idEstPT!=idEstPTAux){
                           estructura = new EstructuraTecnicaPT();
                           estructura.idEstructPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                           estructura.idProyTec = Convert.ToInt32(row["idProyectoTecnico"]);
                           if(!row.IsNull("IdEstructuraTecnica")){
                            estructura.tipoEstructura = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]),row["EstructuraTecnica"].ToString());
                           }
                           if(!row.IsNull("idTipoForma")){
                            estructura.formaEstructura = new ParametroGenerico(Convert.ToInt32(row["idTipoForma"]),row["nombreTipoForma"].ToString());
                           }
                           if(!row.IsNull("idTipoMedida")){
                            estructura.unidadMedida = new ParametroGenerico(Convert.ToInt32(row["idTipoMedida"]),row["nombreTipoMedida"].ToString());
                           }
                           if (!row.IsNull("idTipoVolumenUM")){
                            estructura.volumenUnidadMedida = new ParametroGenerico(Convert.ToInt32(row["idTipoVolumenUM"]),row["nombreTipoVolumen"].ToString());
                           }
                           if (!row.IsNull("idTipoEstructuraPorAnio")){
                                estructura.tipoAnio = new ParametroGenerico(Convert.ToInt32(row["idTipoEstructuraPorAnio"]),row["nombreTipoEstructAnio"].ToString()); 
                           }
                           if (!row.IsNull("idEstado")){
                               estructura.estado = new ParametroGenerico(Convert.ToInt32(row["idEstado"]), "");
                           }
                           if (!row.IsNull("largo")){
                               estructura.largo = Convert.ToSingle(row["largo"]);
                           }
                           if (!row.IsNull("ancho")){
                               estructura.ancho = Convert.ToSingle(row["ancho"]);
                           }
                           if (!row.IsNull("alto")){
                               estructura.alto = Convert.ToSingle(row["alto"]);
                           }
                           if (!row.IsNull("diametro")){
                               estructura.diametro = Convert.ToSingle(row["diametro"]);
                           }
                           if (!row.IsNull("volumenValorUM"))
                           {
                               estructura.volumenValorMedida = Convert.ToSingle(row["volumenValorUM"]);
                           }
                           if (!row.IsNull("densidadSiembra"))
                           {
                               estructura.densidadSiembra = Convert.ToSingle(row["densidadSiembra"]);
                           }
                           if (!row.IsNull("total"))
                           {
                               estructura.totalAcumNumero = Convert.ToSingle(row["total"]);
                           }
                           if (!row.IsNull("totalAcumDim"))
                           {
                               estructura.totalAcumDim = Convert.ToSingle(row["totalAcumDim"]);
                           }
                           if (!row.IsNull("numColectores"))
                           {
                               estructura.numColectores = Convert.ToInt32(row["numColectores"]);
                           }
                           if (!row.IsNull("numLineas"))
                           {
                               estructura.numColectores = Convert.ToInt32(row["numLineas"]);
                           }
                           
                           estructura.anios = new List<ValorParametroAnioPT>();
                           estructura.accion = accion.LISTADO;
                           estructura.index = indes;

                           resp.Add(estructura);
                           indes++;
                         }
                       if (!row.IsNull("idRegistroValor"))
                       {
                           valorAnio = new ValorParametroAnioPT();
                           valorAnio.idRegistro = Convert.ToInt32(row["idRegistroValor"]);
                           valorAnio.idclaveParametro = Convert.ToInt32(row["idEstructProyTecnico"]);
                           valorAnio.anio = Convert.ToInt32(row["anio"]);
                           valorAnio.valor = Convert.ToInt32(row["valor"]);
                           estructura.anios.Add(valorAnio);
                       }
                         idEstPTAux = idEstPT;
 
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

        public HashSet<int> ListaEstructuraMedidas(int idEstructuraTecnica, int idTipoForma)
        {
            try
            {
                Conexion cnn = new Conexion();
                HashSet<int> resp = new HashSet<int>();


                cnn.procedimiento = "paSelRbEstructuraMedidas";
                cnn.parametros.Add("@idEstructuraTecnica", idEstructuraTecnica);
                cnn.parametros.Add("@idTipoForma", idTipoForma);

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        resp.Add(Convert.ToInt32(row["claveMedida"]));
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

        public List<EstructuraTecnica> ListaEstructuraTecnica(int idEstructuraTecnica)
        {
            try
            {
                Conexion cnn = new Conexion();
                EstructuraTecnica estructuraTec = null;
                List<EstructuraTecnica> resp = new List<EstructuraTecnica>();

                cnn.procedimiento = "paSelEstructuraTecnica";

                if (idEstructuraTecnica>0)
                {
                    cnn.parametros.Add("@idEstructuraTecnica", idEstructuraTecnica);
                }
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        estructuraTec = new EstructuraTecnica();
                        estructuraTec.idEstructura = Convert.ToInt32(row["IdEstructuraTecnica"]);
                        estructuraTec.nombreEstructura = row["EstructuraTecnica"].ToString();
                        estructuraTec.aplicaArea = Convert.ToBoolean(row["aplicaArea"]);
                        estructuraTec.aplicaVolumen = Convert.ToBoolean(row["aplicaVolumen"]);

                        resp.Add(estructuraTec);
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
        //--------------DIR SUSTRATO
        public EstructuraTecnicaPT ObtenerEstructuraPT_DirSustrato(int idProyTecnico, int idEstructProyTecnico)
        {
            try
            {
                EstructuraTecnicaPT estructura = null;
                int idEstPT = 0;
                int idEstPTAux = 0;
                ValorParametroAnioPT valorAnio = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraProyectoTecnico_DirSustrato";
                if (idProyTecnico > 0)
                {
                    cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                }
                if (idEstructProyTecnico > 0)
                {
                    cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idEstPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                        if (idEstPT != idEstPTAux)
                        {
                            estructura = new EstructuraTecnicaPT();
                            estructura.idEstructPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                            estructura.idProyTec = Convert.ToInt32(row["idProyectoTecnico"]);
                            if (!row.IsNull("IdEstructuraTecnica"))
                            {
                                estructura.tipoEstructura = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]), row["EstructuraTecnica"].ToString());
                            }
                            if (!row.IsNull("total"))
                            {
                                estructura.totalAcumNumero = Convert.ToSingle(row["total"]);
                            }
                            if (!row.IsNull("idTipoEstructuraPorAnio"))
                            {
                                estructura.tipoAnio = new ParametroGenerico(Convert.ToInt32(row["idTipoEstructuraPorAnio"]), row["nombreTipoEstructAnio"].ToString());
                            }
                            
                            estructura.anios = new List<ValorParametroAnioPT>();
                        }
                        if (!row.IsNull("idRegistroValor"))
                        {
                            valorAnio = new ValorParametroAnioPT();
                            valorAnio.idRegistro = Convert.ToInt32(row["idRegistroValor"]);
                            valorAnio.idclaveParametro = Convert.ToInt32(row["idEstructProyTecnico"]);
                            valorAnio.anio = Convert.ToInt32(row["anio"]);
                            valorAnio.valor = Convert.ToInt32(row["valor"]);
                            estructura.anios.Add(valorAnio);
                        }
                        idEstPTAux = idEstPT;

                    }
                }

                return estructura;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<EstructuraTecnicaPT> ListarEstructuraPT_DirSustrato(int idProyTecnico, int idEstructProyTecnico)
        {
            try
            {
                List<EstructuraTecnicaPT> resp = new List<EstructuraTecnicaPT>();
                EstructuraTecnicaPT estructura = null;
                int idEstPT = 0;
                int idEstPTAux = 0;
                ValorParametroAnioPT valorAnio = null;

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraProyectoTecnico_DirSustrato";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                if (idEstructProyTecnico > 0)
                {
                    cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
                }


                DataTable dt = cnn.Execute();
                int indes = 0;
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idEstPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                        if (idEstPT != idEstPTAux)
                        {
                            estructura = new EstructuraTecnicaPT();
                            estructura.idEstructPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                            estructura.idProyTec = Convert.ToInt32(row["idProyectoTecnico"]);
                            if (!row.IsNull("IdEstructuraTecnica"))
                            {
                                estructura.tipoEstructura = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]), row["EstructuraTecnica"].ToString());
                            }
                            if (!row.IsNull("total"))
                            {
                                estructura.totalAcumNumero = Convert.ToSingle(row["total"]);
                            }
                            if (!row.IsNull("idTipoEstructuraPorAnio"))
                            {
                                estructura.tipoAnio = new ParametroGenerico(Convert.ToInt32(row["idTipoEstructuraPorAnio"]), row["nombreTipoEstructAnio"].ToString());
                            }
                            
                            estructura.anios = new List<ValorParametroAnioPT>();
                            estructura.accion = accion.LISTADO;
                            estructura.index = indes;

                            resp.Add(estructura);
                            indes++;
                        }
                        if (!row.IsNull("idRegistroValor"))
                        {
                            valorAnio = new ValorParametroAnioPT();
                            valorAnio.idRegistro = Convert.ToInt32(row["idRegistroValor"]);
                            valorAnio.idclaveParametro = Convert.ToInt32(row["idEstructProyTecnico"]);
                            valorAnio.anio = Convert.ToInt32(row["anio"]);
                            valorAnio.valor = Convert.ToInt32(row["valor"]);
                            estructura.anios.Add(valorAnio);
                        }
                        idEstPTAux = idEstPT;

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
        //--------------COLECTORES
        public EstructuraTecnicaPT ObtenerEstructuraPT_Colector(int idProyTecnico, int idEstructProyTecnico)
        {
            try
            {
                EstructuraTecnicaPT estructura = null;
                int idEstPT = 0;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraProyectoTecnico_Colector";
                if (idProyTecnico > 0)
                {
                    cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                }
                if (idEstructProyTecnico > 0)
                {
                    cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idEstPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                        estructura = new EstructuraTecnicaPT();
                        estructura.idEstructPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                        estructura.idProyTec = Convert.ToInt32(row["idProyectoTecnico"]);
                        if (!row.IsNull("IdEstructuraTecnica"))
                        {
                            estructura.tipoEstructura = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]), row["EstructuraTecnica"].ToString());
                        }
                        if (!row.IsNull("numColectores"))
                        {
                            estructura.numColectores = Convert.ToInt32(row["numColectores"]);
                        }
                        if (!row.IsNull("numLineas"))
                        {
                            estructura.numLineas = Convert.ToInt32(row["numLineas"]);
                        }

                        
                    }
                }

                return estructura;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public List<EstructuraTecnicaPT> ListarEstructuraPT_Colector(int idProyTecnico, int idEstructProyTecnico)
        {
            try
            {
                List<EstructuraTecnicaPT> resp = new List<EstructuraTecnicaPT>();
                EstructuraTecnicaPT estructura = null;
                int idEstPT = 0;
                int idEstPTAux = 0;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbEstructuraProyectoTecnico_Colector";
                cnn.parametros.Add("@idProyectoTecnico", idProyTecnico);
                if (idEstructProyTecnico > 0)
                {
                    cnn.parametros.Add("@idEstructProyTecnico", idEstructProyTecnico);
                }


                DataTable dt = cnn.Execute();
                int indes = 0;
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        idEstPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                        if (idEstPT != idEstPTAux)
                        {
                            estructura = new EstructuraTecnicaPT();
                            estructura.idEstructPT = Convert.ToInt32(row["idEstructProyTecnico"]);
                            estructura.idProyTec = Convert.ToInt32(row["idProyectoTecnico"]);
                            if (!row.IsNull("IdEstructuraTecnica"))
                            {
                                estructura.tipoEstructura = new ParametroGenerico(Convert.ToInt32(row["IdEstructuraTecnica"]), row["EstructuraTecnica"].ToString());
                            }
                            if (!row.IsNull("numColectores"))
                            {
                                estructura.numColectores = Convert.ToInt32(row["numColectores"]);
                            }
                            if (!row.IsNull("numLineas"))
                            {
                                estructura.numColectores = Convert.ToInt32(row["numLineas"]);
                            }

                            resp.Add(estructura);
                            indes++;
                        }
                       idEstPTAux = idEstPT;

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
