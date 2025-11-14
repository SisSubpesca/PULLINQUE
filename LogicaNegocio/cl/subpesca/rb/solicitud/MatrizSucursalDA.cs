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
    public class MatrizSucursalDA
    {
        public Logger Log { get; set; }

        public bool GuardarMatrizSucursal(MatrizSucursal matrizSuc)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbMatrizSucursal";
                cnn.parametros.Add("@idMatrizSuc", matrizSuc.idMatrizSuc);
                if(matrizSuc.region!=null && matrizSuc.region.comuna!=null && matrizSuc.region.comuna.id_comuna>0){
                    cnn.parametros.Add("@idComuna", matrizSuc.region.comuna.id_comuna);
                }
                cnn.parametros.Add("@direccion", matrizSuc.direccion);
                
                DataTable dt = cnn.Execute();
                matrizSuc.idMatrizSuc = Convert.ToInt32(dt.Rows[0]["idMatrizSuc"]);

                return true;
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
                Log.SendMailError(ex);
                return false;
            };
        }
        /*  
         //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
                public bool GuardarContactoMatrizSuc(int idMatrizSuc, int idContacto)
                {
                    try
                    {

                        Conexion cnn = new Conexion();
                        cnn.procedimiento = "paInsRbContactoMatrizSuc";
                        cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                        cnn.parametros.Add("@idContacto", idContacto);

                        DataTable dt = cnn.Execute();
                        idMatrizSuc = Convert.ToInt32(dt.Rows[0]["idMatrizSuc"]);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log.PrintError(ex);
                        Log.SendMailError(ex);
                        return false;
                    };
                }
        */
                 public List<MatrizSucursal> ListarTitularMatrizSuc(int rutPersona)
                {
                    try
                    {
                        MatrizSucursal claseResp = null;
                        List<MatrizSucursal> resp = new List<MatrizSucursal>();
              
                        Conexion cnn = new Conexion();
                        cnn.procedimiento = "paSelRbTitularMatrizSuc";
                        cnn.parametros.Add("@rutPersona", rutPersona);

                        DataTable dt = cnn.Execute();

                        if (dt != null)
                        {
                            int i = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                claseResp = new MatrizSucursal();

                                claseResp.idMatrizSuc = Convert.ToInt32(row["idMatrizSuc"]);


                                if (!row.IsNull("idRegion")){
                                    claseResp.region = new Region();
                                    claseResp.region.id_region = Convert.ToInt32(row["idRegion"]);
                                    claseResp.region.region = row["Region"].ToString();
                                }

                                if (!row.IsNull("idComuna")){
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
                 /*  
          //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
                           public List<Contacto> ListarContactoMatrizSuc(int idMatrizSuc)
                         {
                             try
                             {
                                 Contacto claseResp = null;
                                 List<Contacto> resp = new List<Contacto>();
                                 Conexion cnn = new Conexion();
                                 cnn.procedimiento = "paSelRbContactoMatrizSuc";
                                 if(idMatrizSuc>0){
                                     cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                                 }
                

                                 DataTable dt = cnn.Execute();

                                 if (dt != null)
                                 {

                                     foreach (DataRow row in dt.Rows)
                                     {
                                         claseResp = new Contacto();
                                         claseResp.idContacto = Convert.ToInt32(row["idContacto"]);
                                         claseResp.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
                                         claseResp.valorContacto = row["valorContacto"].ToString();
                                         claseResp.accesoPublico = Convert.ToBoolean(row["accesoPublico"]);
                                         if (!row.IsNull("detalle"))
                                         {
                                             claseResp.detalle = row["detalle"].ToString();
                                         }
                        

                                         resp.Add(claseResp);
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
                         }*/

                           public bool GuardarTitularMatrizSuc(int idMatrizSuc, int rutPersona)
                           {
                               try
                               {

                                   Conexion cnn = new Conexion();
                                   cnn.procedimiento = "paInsRbTitularMatrizSuc";
                                   cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                                   cnn.parametros.Add("@rutPersona", rutPersona);

                                   DataTable dt = cnn.Execute();
                                   idMatrizSuc = Convert.ToInt32(dt.Rows[0]["idMatrizSuc"]);

                                   return true;
                               }
                               catch (Exception ex)
                               {
                                   Log.PrintError(ex);
                                   Log.SendMailError(ex);
                                   return false;
                               };
                           }
                           /*  
                    //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
                                              public MatrizSucursal ObtieneMatrizSucursalContactos(int idMatrizSuc)
                                            {
                                                try
                                                {
                                                    MatrizSucursal matSuc = null;
                                                    int idMatriz = 0;
                                                    int idMatrizAux = 0;
                                                    Contacto contacto = null;
                                                    Conexion cnn = new Conexion();
                                                    cnn.procedimiento = "paSelRbMatrizSucursalContactos";
                                                    cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);

                                                    DataTable dt = cnn.Execute();

                                                    if (dt != null)
                                                    {

                                                        foreach (DataRow row in dt.Rows)
                                                        {
                                                            idMatriz = Convert.ToInt32(row["idMatrizSuc"]);
                                                            if(idMatrizAux!=idMatriz){
                                                                matSuc = new MatrizSucursal();
                                                                matSuc.idMatrizSuc = Convert.ToInt32(row["idMatrizSuc"]);
                                                                matSuc.region = new Region();
                                                                matSuc.region.id_region = Convert.ToInt32(row["idRegion"]);
                                                                matSuc.region.region = row["Region"].ToString();
                                                                matSuc.region.comuna = new Comuna();
                                                                matSuc.region.comuna.id_comuna = Convert.ToInt32(row["idComuna"]);
                                                                matSuc.region.comuna.comuna = row["Comuna"].ToString();
                                                                if (!row.IsNull("idComunaCasilla"))
                                                                {
                                                                    matSuc.comunaCasilla = new Comuna();
                                                                    matSuc.comunaCasilla.id_comuna = Convert.ToInt32(row["idComunaCasilla"]);
                                                                    matSuc.comunaCasilla.comuna = row["comunaCasilla"].ToString();
                                                                }
                                                                if (!row.IsNull("casilla"))
                                                                {
                                                                    matSuc.casilla = Convert.ToString(row["casilla"]);
                                                                }
                                                                matSuc.direccion = Convert.ToString(row["direccion"]);
                                                                if (!row.IsNull("numeroCI"))
                                                                {
                                                                    matSuc.numeroCI = Convert.ToInt32(row["numeroCI"]);
                                                                }
                                                                if (!row.IsNull("fechaCI"))
                                                                {
                                                                    matSuc.fechaCI = Convert.ToDateTime(row["fechaCI"]);
                                                                }
                                                                matSuc.matriz = Convert.ToBoolean(row["matriz"]);
                                                                if (!row.IsNull("idArchivoBinSC"))
                                                                {
                                                                    matSuc.archivoBinarioEspecial = new ArchivoBinarioEspecial();
                                                                    matSuc.archivoBinarioEspecial.idArchivo = Convert.ToInt32(row["idArchivoBinSC"]);
                                                                    matSuc.archivoBinarioEspecial.nombreFisico = Convert.ToString(row["nombreFisico"]);
                                                                    matSuc.archivoBinarioEspecial.nombreArchivo = Convert.ToString(row["nombreArchivo"]);
                                                                    matSuc.archivoBinarioEspecial.formato = Convert.ToString(row["formato"]);
                                                                    matSuc.archivoBinarioEspecial.bytes = (byte[])row["contenido"];
                            
                                                                }
                                                                matSuc.contactosMatrizSuc = new List<Contacto>();
                                                            }
                                                            if (!row.IsNull("idContacto"))
                                                            {
                                                                contacto = new Contacto();
                                                                contacto.idContacto = Convert.ToInt32(row["idContacto"]);
                                                                contacto.tipoContacto = new ParametroGenerico(Convert.ToInt32(row["idTipoContacto"]), row["nombreTipo"].ToString());
                                                                contacto.valorContacto = row["valorContacto"].ToString();
                                                                contacto.accesoPublico = Convert.ToBoolean(row["accesoPublico"]);
                                                                if (!row.IsNull("detalle"))
                                                                {
                                                                    contacto.detalle = row["detalle"].ToString();                            
                                                                }
                            
                                                                matSuc.contactosMatrizSuc.Add(contacto);
                                                            }
                        
                                                            idMatrizAux = idMatriz;
                                                        }
                                                    }

                                                    return matSuc;
                                                }
                                                catch (Exception ex)
                                                {
                                                    Log.PrintError(ex);
                                                    Log.SendMailError(ex);
                                                    return null;
                                                }
                                            }*/

                                            public bool EliminarMatrizSucursal(int idMatrizSuc)
                                            {
                                                  try
                                                  {
                                                      Conexion cnn = new Conexion();
                                                      cnn.procedimiento = "paDelRbMatrizSucursal";
                                                      cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
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
                                            /*  
                                     //SE ELIMINÓ LA TBLA, POR LO TANTO SE ELIMINA PROCEDIMIENTO
                                            public bool EliminarContactoMatrizSuc(int idMatrizSuc, int idContacto)
                                            {
                                                try
                                                {
                                                    Conexion cnn = new Conexion();
                                                    cnn.procedimiento = "paDelRbContactoMatrizSuc";
                                                    cnn.parametros.Add("@idMatrizSuc", idMatrizSuc);
                                                    if (idContacto>0)
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
                                            }*/

        public MatrizSucursal ObtenerTitularMatrizSucFiltros(int rutPersona, int idMatrizSuc)
        {
            try
            {
                MatrizSucursal claseResp = null;
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbTitularMatrizSuc_filtros";
                if (rutPersona>0)
                {
                    cnn.parametros.Add("@rutPersona", rutPersona);
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
                        claseResp.persona.rutPersona = Convert.ToInt32(row["rutPersona"]);
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


    }
}
