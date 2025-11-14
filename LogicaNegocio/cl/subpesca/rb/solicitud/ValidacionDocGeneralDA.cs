using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.AccesoDatos;
using System.Data;
using Datos.Entidades;
using System.Collections;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class ValidacionDocGeneralDA
    {

        Logger logger = new Logger();

       
        /**
       * Lista la validacion de campos que se despliega en una interfaz de acuerdo a la pestaña y su tipo de entrada/salida
       */
        public Hashtable ListaValidacionDocGeneral(int idPestana, int idTipoIO, int idTipoDocumento)
        {
            try
            {
                ValidacionDocGeneral valAux = null;
                Hashtable ht = new Hashtable();
                
                Conexion cnn = new Conexion();
                cnn.procedimiento = "paSelRbValidacionDocGeneral";

                if (idPestana > 0)
                {
                    cnn.parametros.Add("@idPestana", idPestana);
                }
                if (idTipoIO > 0)
                {
                    cnn.parametros.Add("@idTipoIO", idTipoIO);
                }
                if (idTipoDocumento > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                }


                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        valAux = new ValidacionDocGeneral();
                        valAux.idValDocGeneral = Convert.ToInt32(row["idValDocGeneral"]);
                        if (!row.IsNull("idPestana"))
                        {
                            valAux.pestania = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), "");
                        }
                        valAux.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), "");
                        valAux.numero = Convert.ToInt32(row["numero"]);
                        valAux.tipoDoc = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]));
                        valAux.fecha = Convert.ToInt32(row["fecha"]);
                        valAux.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valAux.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valAux.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        valAux.subRequerimiento = Convert.ToInt32(row["subRequerimiento"]);
                        valAux.tipoDocumento = Convert.ToInt32(row["tipoDocumento"]);
                        valAux.tipoOrigen = Convert.ToInt32(row["tipoOrigen"]);
                        valAux.tipoDestinatario = Convert.ToInt32(row["tipoDestinatario"]);
                        valAux.numRequerimiento = Convert.ToInt32(row["numRequerimiento"]);
                        valAux.ambito = Convert.ToInt32(row["ambito"]);
                        valAux.panelRequerimientos = Convert.ToInt32(row["panelRequerimientos"]);


                        if (!ht.Contains(valAux.tipoIO.id)){
                            ht.Add(valAux.tipoIO.id, new Hashtable());
                        }

                        if (!((Hashtable)ht[valAux.tipoIO.id]).Contains(valAux.pestania.id))
                        {
                            ((Hashtable)ht[valAux.tipoIO.id]).Add(valAux.pestania.id, new Hashtable());
                        }


                        if (!((Hashtable)((Hashtable)ht[valAux.tipoIO.id])[valAux.pestania.id]).Contains(valAux.tipoDoc.id)) {

                            ((Hashtable)((Hashtable)ht[valAux.tipoIO.id])[valAux.pestania.id]).Add(valAux.tipoDoc.id, valAux);
                        }

                    }
                }
                return ht;
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
