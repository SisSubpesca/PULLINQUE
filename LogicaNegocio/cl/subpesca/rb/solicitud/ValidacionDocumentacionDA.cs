using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.AccesoDatos;
using System.Collections;
using LogicaNegocio.cl.subpesca.rb.common;

namespace LogicaNegocio.cl.subpesca.rb.solicitud
{
    public class ValidacionDocumentacionDA
    {

        Logger logger = new Logger();


        
        /**
        * Obtiene una ValidacionDocumentacion en base a filtros.
        */
        public Hashtable ListaValidacionDocGeneral_Mantenedor(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                Hashtable ht = new Hashtable();
                Hashtable htAux = new Hashtable();
                Conexion cnn = new Conexion();
              
                cnn.procedimiento = "paSelRbValidacionDocumentacion_Mantenedor";
                if(filtro.idValDocumentacion>0){
                    cnn.parametros.Add("@idValDocumentacion", filtro.idValDocumentacion);
                }
                if(filtro.tipoIO!=null && filtro.tipoIO.id>0){
                    cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                }
                if(filtro.tipoDestinatario!=null && filtro.tipoDestinatario.id>0){
                    cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);
                }
                if(filtro.ambito!=null && filtro.ambito.id>0){
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if(filtro.tipoDocumento!=null && filtro.tipoDocumento.id>0){
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if(filtro.seccion!=null && filtro.seccion.id>0){
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                if(filtro.subRequerimiento!=null && filtro.subRequerimiento.id>0){
                    cnn.parametros.Add("@idSubRequerimiento", filtro.subRequerimiento.id);
                }
                if (filtro.flujoDocumental != null && filtro.flujoDocumental.id > 0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                }
                //--------------------------------------------------------------------------
                if (filtro.flujoDocumental != null && !filtro.flujoDocumental.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoFlujDoc", filtro.flujoDocumental.descripcion);
                }
                if (filtro.tipoIO != null && !filtro.tipoIO.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoIO", filtro.tipoIO.descripcion);
                }
                if (filtro.tipoDocumento != null && !filtro.tipoDocumento.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoDoc", filtro.tipoDocumento.descripcion);
                }
                if (filtro.tipoDestinatario != null && !filtro.tipoDestinatario.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreTipoDestinatario", filtro.tipoDestinatario.descripcion);
                }
                if (filtro.ambito!=null && !filtro.ambito.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombrePestana", filtro.ambito.descripcion);
                }
                if (filtro.seccion != null && !filtro.seccion.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreSeccion", filtro.seccion.descripcion);
                }
                if (filtro.subRequerimiento != null && !filtro.subRequerimiento.descripcion.Equals(""))
                {
                    cnn.parametros.Add("@nombreSubRequerimiento", filtro.subRequerimiento.descripcion);
                }
                if (filtro.reqInternoFiltro == 0 || filtro.reqInternoFiltro == 1)
                {
                    cnn.parametros.Add("@reqInterno", filtro.reqInternoFiltro);
                }
                if (filtro.numero == 0 || filtro.numero == 1)
                {
                    cnn.parametros.Add("@numero", filtro.numero);
                }
                if (filtro.fecha == 0 || filtro.fecha == 1)
                {
                    cnn.parametros.Add("@fecha", filtro.fecha);
                }
                if (filtro.numeroCI == 0 || filtro.numeroCI == 1)
                {
                    cnn.parametros.Add("@numeroCI", filtro.numeroCI);
                }
                if (filtro.fechaCI == 0 || filtro.fechaCI == 1)
                {
                    cnn.parametros.Add("@fechaCI", filtro.fechaCI);
                }
                if (filtro.verificaConformeFiltro == 0 || filtro.verificaConformeFiltro == 1)
                {
                    cnn.parametros.Add("@verificaConforme", filtro.verificaConformeFiltro);
                }
                if (filtro.nuevaFecha == 0 || filtro.nuevaFecha == 1)
                {
                    cnn.parametros.Add("@extensionFecha", filtro.nuevaFecha);
                }
                if (filtro.verificaAmpPlazo == 0 || filtro.verificaAmpPlazo == 1)
                {
                    cnn.parametros.Add("@verificaAmpPlazo", filtro.verificaAmpPlazo);
                }
                if (filtro.verificaAmpExtension == 0 || filtro.verificaAmpExtension == 1)
                {
                    cnn.parametros.Add("@verificaAmpExtension", filtro.verificaAmpExtension);
                }
                //--------------------------------------------------------------------------
                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());

                        if (!row.IsNull("reqInterno"))
                        {
                           valDocResp.reqInterno = Convert.ToBoolean(row["reqInterno"]);
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        if (!row.IsNull("verificaConforme"))
                        {
                            valDocResp.verificaConforme = Convert.ToBoolean(row["verificaConforme"]);
                        }
                        valDocResp.aplicaConcesion = Convert.ToInt32(row["aplicaConcesion"]);
                        valDocResp.aplicaModAmpliacion = Convert.ToInt32(row["aplicaModAmpliacion"]);
                        valDocResp.aplicaModReduccion = Convert.ToInt32(row["aplicaModReduccion"]);
                        valDocResp.aplicaModEspeciePT = Convert.ToInt32(row["aplicaModEspeciePT"]);
                        valDocResp.aplicaModRegularizacion = Convert.ToInt32(row["aplicaModRegularizacion"]);
                        valDocResp.aplicaRelocalizacion = Convert.ToInt32(row["aplicaRelocalizacion"]);
                        valDocResp.aplicaAmerb = Convert.ToInt32(row["aplicaAmerb"]);
                        valDocResp.aplicaFaenamiento = Convert.ToInt32(row["aplicaFaenamiento"]);
                        valDocResp.aplicaAcopio = Convert.ToInt32(row["aplicaAcopio"]);
                        valDocResp.aplicaColectores = Convert.ToInt32(row["aplicaColectores"]);
                        valDocResp.nuevaFecha = Convert.ToInt32(row["extensionFecha"]);
                        valDocResp.verificaAmpPlazo = Convert.ToInt32(row["verificaAmpPlazo"]);
                        valDocResp.verificaAmpExtension = Convert.ToInt32(row["verificaAmpExtension"]);
                        valDocResp.aplicaExperimentalesAmerb = Convert.ToInt32(row["aplicaExAmerb"]);
                        valDocResp.aplicaExpConcesion = Convert.ToInt32(row["aplicaExConcesion"]);
                        valDocResp.aplicaAcuiculturaEcmpo = Convert.ToInt32(row["aplicaECMPO"]);
                        valDocResp.aplicaModECMPOAmpl = Convert.ToInt32(row["aplicaModECMPOAmpl"]);
                        valDocResp.aplicaModECMPOReduc = Convert.ToInt32(row["aplicaModECMPOReduc"]);
                        valDocResp.aplicaModECMPOEspecie = Convert.ToInt32(row["aplicaModECMPOEspecie"]);
                        valDocResp.aplicaModECMPO_PT = Convert.ToInt32(row["aplicaModECMPO_PT"]);
                        valDocResp.aplicaModECMPO_Regulariz = Convert.ToInt32(row["aplicaModECMPO_Regulariz"]);
                        valDocResp.aplicaModAcopioAmpl = Convert.ToInt32(row["aplicaModAcopioAmpl"]);
                        valDocResp.aplicaModAcopioReduc = Convert.ToInt32(row["aplicaModAcopioReduc"]);
                        valDocResp.aplicaModAcopioEspecie = Convert.ToInt32(row["aplicaModAcopioEspecie"]);
                        valDocResp.aplicaModAcopio_PT = Convert.ToInt32(row["aplicaModAcopio_PT"]);
                        valDocResp.aplicaModAcopioRegulariz = Convert.ToInt32(row["aplicaModAcopioRegulariz"]);
                        valDocResp.aplicaModFaenamAmpl = Convert.ToInt32(row["aplicaModFaenamAmpl"]);
                        valDocResp.aplicaModFaenamReduc = Convert.ToInt32(row["aplicaModFaenamReduc"]);
                        valDocResp.aplicaModFaenamEspecie = Convert.ToInt32(row["aplicaModFaenamEspecie"]);
                        valDocResp.aplicaModFaenam_PT = Convert.ToInt32(row["aplicaModFaenam_PT"]);
                        valDocResp.aplicaModFaenamRegulariz = Convert.ToInt32(row["aplicaModFaenamRegulariz"]);
                        valDocResp.aplicaModAmerbAmpl = Convert.ToInt32(row["aplicaModAmerbAmpl"]);
                        valDocResp.aplicaModAmerbReduc = Convert.ToInt32(row["aplicaModAmerbReduc"]);
                        valDocResp.aplicaModAmerbEspecie = Convert.ToInt32(row["aplicaModAmerbEspecie"]);
                        valDocResp.aplicaModAmerb_PT = Convert.ToInt32(row["aplicaModAmerb_PT"]);
                        valDocResp.aplicaModAmerbRegulariz = Convert.ToInt32(row["aplicaModAmerbRegulariz"]);


                        if (!ht.Contains(valDocResp.tipoIO.id))
                        {
                           ht.Add(valDocResp.tipoIO.id, new Hashtable());
                        }


                        htAux  = (Hashtable)ht[valDocResp.tipoIO.id];

                        if (!htAux.Contains(valDocResp.tipoDestinatario.id))
                        {
                           htAux.Add(valDocResp.tipoDestinatario.id, new Hashtable());
                        }


                        htAux = (Hashtable)htAux[valDocResp.tipoDestinatario.id];

                        if (!htAux.Contains(valDocResp.ambito.id))
                        {
                           htAux.Add(valDocResp.ambito.id, new Hashtable());
                        }


                        htAux = (Hashtable)htAux[valDocResp.ambito.id];

                        if (!htAux.Contains(valDocResp.tipoDocumento.id))
                        {
                            htAux.Add(valDocResp.tipoDocumento.id, new Hashtable());
                        }


                        htAux = (Hashtable)htAux[valDocResp.tipoDocumento.id];

                        if (!htAux.Contains(valDocResp.subRequerimiento.id))
                        {
                            htAux.Add(valDocResp.subRequerimiento.id, valDocResp);
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

        public Hashtable ListaValidacionDocGeneral(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                Hashtable ht = new Hashtable();
                Hashtable htAux = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                if (filtro.idValDocumentacion > 0)
                {
                    cnn.parametros.Add("@idValDocumentacion", filtro.idValDocumentacion);
                }
                if (filtro.tipoIO != null && filtro.tipoIO.id > 0)
                {
                    cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                }
                if (filtro.tipoDestinatario != null && filtro.tipoDestinatario.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);
                }
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                if (filtro.subRequerimiento != null && filtro.subRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", filtro.subRequerimiento.id);
                }
                if (filtro.flujoDocumental != null && filtro.flujoDocumental.id > 0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                }
                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());

                        if (!row.IsNull("reqInterno"))
                        {
                            valDocResp.reqInterno = Convert.ToBoolean(row["reqInterno"]);
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        if (!row.IsNull("verificaConforme"))
                        {
                            valDocResp.verificaConforme = Convert.ToBoolean(row["verificaConforme"]);
                        }
                        valDocResp.aplicaConcesion = Convert.ToInt32(row["aplicaConcesion"]);
                        valDocResp.aplicaModAmpliacion = Convert.ToInt32(row["aplicaModAmpliacion"]);
                        valDocResp.aplicaModReduccion = Convert.ToInt32(row["aplicaModReduccion"]);
                        valDocResp.aplicaModEspeciePT = Convert.ToInt32(row["aplicaModEspeciePT"]);
                        valDocResp.aplicaModRegularizacion = Convert.ToInt32(row["aplicaModRegularizacion"]);
                        valDocResp.aplicaRelocalizacion = Convert.ToInt32(row["aplicaRelocalizacion"]);
                        valDocResp.aplicaAmerb = Convert.ToInt32(row["aplicaAmerb"]);
                        valDocResp.aplicaFaenamiento = Convert.ToInt32(row["aplicaFaenamiento"]);
                        valDocResp.aplicaAcopio = Convert.ToInt32(row["aplicaAcopio"]);
                        valDocResp.aplicaColectores = Convert.ToInt32(row["aplicaColectores"]);
                        valDocResp.nuevaFecha = Convert.ToInt32(row["extensionFecha"]);
                        valDocResp.verificaAmpPlazo = Convert.ToInt32(row["verificaAmpPlazo"]);
                        valDocResp.verificaAmpExtension = Convert.ToInt32(row["verificaAmpExtension"]);
                        valDocResp.aplicaExperimentalesAmerb = Convert.ToInt32(row["aplicaExAmerb"]);
                        valDocResp.aplicaExpConcesion = Convert.ToInt32(row["aplicaExConcesion"]);
                        valDocResp.aplicaAcuiculturaEcmpo = Convert.ToInt32(row["aplicaECMPO"]);
                        valDocResp.aplicaModECMPOAmpl = Convert.ToInt32(row["aplicaModECMPOAmpl"]);
                        valDocResp.aplicaModECMPOReduc = Convert.ToInt32(row["aplicaModECMPOReduc"]);
                        valDocResp.aplicaModECMPOEspecie = Convert.ToInt32(row["aplicaModECMPOEspecie"]);
                        valDocResp.aplicaModECMPO_PT = Convert.ToInt32(row["aplicaModECMPO_PT"]);
                        valDocResp.aplicaModECMPO_Regulariz = Convert.ToInt32(row["aplicaModECMPO_Regulariz"]);
                        valDocResp.aplicaModAcopioAmpl = Convert.ToInt32(row["aplicaModAcopioAmpl"]);
                        valDocResp.aplicaModAcopioReduc = Convert.ToInt32(row["aplicaModAcopioReduc"]);
                        valDocResp.aplicaModAcopioEspecie = Convert.ToInt32(row["aplicaModAcopioEspecie"]);
                        valDocResp.aplicaModAcopio_PT = Convert.ToInt32(row["aplicaModAcopio_PT"]);
                        valDocResp.aplicaModAcopioRegulariz = Convert.ToInt32(row["aplicaModAcopioRegulariz"]);
                        valDocResp.aplicaModFaenamAmpl = Convert.ToInt32(row["aplicaModFaenamAmpl"]);
                        valDocResp.aplicaModFaenamReduc = Convert.ToInt32(row["aplicaModFaenamReduc"]);
                        valDocResp.aplicaModFaenamEspecie = Convert.ToInt32(row["aplicaModFaenamEspecie"]);
                        valDocResp.aplicaModFaenam_PT = Convert.ToInt32(row["aplicaModFaenam_PT"]);
                        valDocResp.aplicaModFaenamRegulariz = Convert.ToInt32(row["aplicaModFaenamRegulariz"]);
                        valDocResp.aplicaModAmerbAmpl = Convert.ToInt32(row["aplicaModAmerbAmpl"]);
                        valDocResp.aplicaModAmerbReduc = Convert.ToInt32(row["aplicaModAmerbReduc"]);
                        valDocResp.aplicaModAmerbEspecie = Convert.ToInt32(row["aplicaModAmerbEspecie"]);
                        valDocResp.aplicaModAmerb_PT = Convert.ToInt32(row["aplicaModAmerb_PT"]);
                        valDocResp.aplicaModAmerbRegulariz = Convert.ToInt32(row["aplicaModAmerbRegulariz"]);


                        if (!ht.Contains(valDocResp.tipoIO.id))
                        {
                            ht.Add(valDocResp.tipoIO.id, new Hashtable());
                        }


                        htAux = (Hashtable)ht[valDocResp.tipoIO.id];

                        if (!htAux.Contains(valDocResp.tipoDestinatario.id))
                        {
                            htAux.Add(valDocResp.tipoDestinatario.id, new Hashtable());
                        }


                        htAux = (Hashtable)htAux[valDocResp.tipoDestinatario.id];

                        if (!htAux.Contains(valDocResp.ambito.id))
                        {
                            htAux.Add(valDocResp.ambito.id, new Hashtable());
                        }


                        htAux = (Hashtable)htAux[valDocResp.ambito.id];

                        if (!htAux.Contains(valDocResp.tipoDocumento.id))
                        {
                            htAux.Add(valDocResp.tipoDocumento.id, new Hashtable());
                        }


                        htAux = (Hashtable)htAux[valDocResp.tipoDocumento.id];

                        if (!htAux.Contains(valDocResp.subRequerimiento.id))
                        {
                            htAux.Add(valDocResp.subRequerimiento.id, valDocResp);
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


        public List<ValidacionDocumentacion> ListaValidacionDocumentacion(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> listaValidacionDocumentacion = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";

                if (filtro.idValDocumentacion > 0)
                {
                    cnn.parametros.Add("@idValDocumentacion", filtro.idValDocumentacion);
                    
                }
                if (filtro.tipoIO != null && filtro.tipoIO.id > 0)
                {
                    cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                }
                if (filtro.tipoDestinatario != null && filtro.tipoDestinatario.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);
                }
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                if (filtro.subRequerimiento != null && filtro.subRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", filtro.subRequerimiento.id);
                }
                if (filtro.flujoDocumental != null && filtro.flujoDocumental.id > 0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                }
                if (filtro.aplicaConcesion >= 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion >= 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion >= 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT >= 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion >= 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion >= 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb >= 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento >= 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio >= 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores >= 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb >= 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion >= 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo >= 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT >= 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());

                        if (!row.IsNull("reqInterno"))
                        {
                            valDocResp.reqInterno = Convert.ToBoolean(row["reqInterno"]);
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        if (!row.IsNull("verificaConforme"))
                        {
                            valDocResp.verificaConforme = Convert.ToBoolean(row["verificaConforme"]);
                        }
                        valDocResp.aplicaConcesion = Convert.ToInt32(row["aplicaConcesion"]);
                        valDocResp.aplicaModAmpliacion = Convert.ToInt32(row["aplicaModAmpliacion"]);
                        valDocResp.aplicaModReduccion = Convert.ToInt32(row["aplicaModReduccion"]);
                        valDocResp.aplicaModEspeciePT = Convert.ToInt32(row["aplicaModEspeciePT"]);
                        valDocResp.aplicaModRegularizacion = Convert.ToInt32(row["aplicaModRegularizacion"]);
                        valDocResp.aplicaRelocalizacion = Convert.ToInt32(row["aplicaRelocalizacion"]);
                        valDocResp.aplicaAmerb = Convert.ToInt32(row["aplicaAmerb"]);
                        valDocResp.aplicaFaenamiento = Convert.ToInt32(row["aplicaFaenamiento"]);
                        valDocResp.aplicaAcopio = Convert.ToInt32(row["aplicaAcopio"]);
                        valDocResp.aplicaColectores = Convert.ToInt32(row["aplicaColectores"]);
                        valDocResp.nuevaFecha = Convert.ToInt32(row["extensionFecha"]);
                        valDocResp.verificaAmpPlazo = Convert.ToInt32(row["verificaAmpPlazo"]);
                        valDocResp.verificaAmpExtension = Convert.ToInt32(row["verificaAmpExtension"]);
                        valDocResp.aplicaExperimentalesAmerb = Convert.ToInt32(row["aplicaExAmerb"]);
                        valDocResp.aplicaExpConcesion = Convert.ToInt32(row["aplicaExConcesion"]);
                        valDocResp.aplicaAcuiculturaEcmpo = Convert.ToInt32(row["aplicaECMPO"]);
                        valDocResp.aplicaModECMPOAmpl = Convert.ToInt32(row["aplicaModECMPOAmpl"]);
                        valDocResp.aplicaModECMPOReduc = Convert.ToInt32(row["aplicaModECMPOReduc"]);
                        valDocResp.aplicaModECMPOEspecie = Convert.ToInt32(row["aplicaModECMPOEspecie"]);
                        valDocResp.aplicaModECMPO_PT = Convert.ToInt32(row["aplicaModECMPO_PT"]);
                        valDocResp.aplicaModECMPO_Regulariz = Convert.ToInt32(row["aplicaModECMPO_Regulariz"]);
                        valDocResp.aplicaModAcopioAmpl = Convert.ToInt32(row["aplicaModAcopioAmpl"]);
                        valDocResp.aplicaModAcopioReduc = Convert.ToInt32(row["aplicaModAcopioReduc"]);
                        valDocResp.aplicaModAcopioEspecie = Convert.ToInt32(row["aplicaModAcopioEspecie"]);
                        valDocResp.aplicaModAcopio_PT= Convert.ToInt32(row["aplicaModAcopio_PT"]);
                        valDocResp.aplicaModAcopioRegulariz = Convert.ToInt32(row["aplicaModAcopioRegulariz"]);
                        valDocResp.aplicaModFaenamAmpl = Convert.ToInt32(row["aplicaModFaenamAmpl"]);
                        valDocResp.aplicaModFaenamReduc = Convert.ToInt32(row["aplicaModFaenamReduc"]);
                        valDocResp.aplicaModFaenamEspecie = Convert.ToInt32(row["aplicaModFaenamEspecie"]);
                        valDocResp.aplicaModFaenam_PT = Convert.ToInt32(row["aplicaModFaenam_PT"]);
                        valDocResp.aplicaModFaenamRegulariz = Convert.ToInt32(row["aplicaModFaenamRegulariz"]);
                        valDocResp.aplicaModAmerbAmpl = Convert.ToInt32(row["aplicaModAmerbAmpl"]);
                        valDocResp.aplicaModAmerbReduc = Convert.ToInt32(row["aplicaModAmerbReduc"]);
                        valDocResp.aplicaModAmerbEspecie = Convert.ToInt32(row["aplicaModAmerbEspecie"]);
                        valDocResp.aplicaModAmerb_PT = Convert.ToInt32(row["aplicaModAmerb_PT"]);
                        valDocResp.aplicaModAmerbRegulariz = Convert.ToInt32(row["aplicaModAmerbRegulariz"]);
                        
                        listaValidacionDocumentacion.Add(valDocResp);

                    }
                }

                return listaValidacionDocumentacion;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public ValidacionDocumentacion ObtenerValidacionDocumentacion(int idValDocumentacion)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                
                cnn.parametros.Add("@idValDocumentacion", idValDocumentacion);
                
                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());

                        if (!row.IsNull("reqInterno"))
                        {
                            valDocResp.reqInterno = Convert.ToBoolean(row["reqInterno"]);
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        if (!row.IsNull("verificaConforme"))
                        {
                            valDocResp.verificaConforme = Convert.ToBoolean(row["verificaConforme"]);
                        }
                        valDocResp.aplicaConcesion = Convert.ToInt32(row["aplicaConcesion"]);
                        valDocResp.aplicaModAmpliacion = Convert.ToInt32(row["aplicaModAmpliacion"]);
                        valDocResp.aplicaModReduccion = Convert.ToInt32(row["aplicaModReduccion"]);
                        valDocResp.aplicaModEspeciePT = Convert.ToInt32(row["aplicaModEspeciePT"]);
                        valDocResp.aplicaModRegularizacion = Convert.ToInt32(row["aplicaModRegularizacion"]);
                        valDocResp.aplicaRelocalizacion = Convert.ToInt32(row["aplicaRelocalizacion"]);
                        valDocResp.aplicaAmerb = Convert.ToInt32(row["aplicaAmerb"]);
                        valDocResp.aplicaFaenamiento = Convert.ToInt32(row["aplicaFaenamiento"]);
                        valDocResp.aplicaAcopio = Convert.ToInt32(row["aplicaAcopio"]);
                        valDocResp.aplicaColectores = Convert.ToInt32(row["aplicaColectores"]);
                        valDocResp.nuevaFecha = Convert.ToInt32(row["extensionFecha"]);
                        valDocResp.verificaAmpPlazo = Convert.ToInt32(row["verificaAmpPlazo"]);
                        valDocResp.verificaAmpExtension = Convert.ToInt32(row["verificaAmpExtension"]);
                        valDocResp.aplicaExperimentalesAmerb = Convert.ToInt32(row["aplicaExAmerb"]);
                        valDocResp.aplicaExpConcesion = Convert.ToInt32(row["aplicaExConcesion"]);
                        valDocResp.aplicaAcuiculturaEcmpo = Convert.ToInt32(row["aplicaECMPO"]);
                        valDocResp.aplicaModECMPOAmpl = Convert.ToInt32(row["aplicaModECMPOAmpl"]);
                        valDocResp.aplicaModECMPOReduc = Convert.ToInt32(row["aplicaModECMPOReduc"]);
                        valDocResp.aplicaModECMPOEspecie = Convert.ToInt32(row["aplicaModECMPOEspecie"]);
                        valDocResp.aplicaModECMPO_PT = Convert.ToInt32(row["aplicaModECMPO_PT"]);
                        valDocResp.aplicaModECMPO_Regulariz = Convert.ToInt32(row["aplicaModECMPO_Regulariz"]);
                        valDocResp.aplicaModAcopioAmpl = Convert.ToInt32(row["aplicaModAcopioAmpl"]);
                        valDocResp.aplicaModAcopioReduc = Convert.ToInt32(row["aplicaModAcopioReduc"]);
                        valDocResp.aplicaModAcopioEspecie = Convert.ToInt32(row["aplicaModAcopioEspecie"]);
                        valDocResp.aplicaModAcopio_PT = Convert.ToInt32(row["aplicaModAcopio_PT"]);
                        valDocResp.aplicaModAcopioRegulariz = Convert.ToInt32(row["aplicaModAcopioRegulariz"]);
                        valDocResp.aplicaModFaenamAmpl = Convert.ToInt32(row["aplicaModFaenamAmpl"]);
                        valDocResp.aplicaModFaenamReduc = Convert.ToInt32(row["aplicaModFaenamReduc"]);
                        valDocResp.aplicaModFaenamEspecie = Convert.ToInt32(row["aplicaModFaenamEspecie"]);
                        valDocResp.aplicaModFaenam_PT = Convert.ToInt32(row["aplicaModFaenam_PT"]);
                        valDocResp.aplicaModFaenamRegulariz = Convert.ToInt32(row["aplicaModFaenamRegulariz"]);
                        valDocResp.aplicaModAmerbAmpl = Convert.ToInt32(row["aplicaModAmerbAmpl"]);
                        valDocResp.aplicaModAmerbReduc = Convert.ToInt32(row["aplicaModAmerbReduc"]);
                        valDocResp.aplicaModAmerbEspecie = Convert.ToInt32(row["aplicaModAmerbEspecie"]);
                        valDocResp.aplicaModAmerb_PT = Convert.ToInt32(row["aplicaModAmerb_PT"]);
                        valDocResp.aplicaModAmerbRegulariz = Convert.ToInt32(row["aplicaModAmerbRegulariz"]);

                    }
                }

                return valDocResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        /**
      * Obtiene una ValidacionDocumentacion en base a filtros.
      */
        public Hashtable ListarValidacionDocumentacionCombobox(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                Hashtable ht = new Hashtable();
                Hashtable htAux = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                if (filtro.idValDocumentacion > 0)
                {
                    cnn.parametros.Add("@idValDocumentacion", filtro.idValDocumentacion);
                }
                if (filtro.tipoIO != null && filtro.tipoIO.id > 0)
                {
                    cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                }
                if (filtro.tipoDestinatario != null && filtro.tipoDestinatario.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);
                }
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                if (filtro.subRequerimiento != null && filtro.subRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", filtro.subRequerimiento.id);
                }
                if (filtro.flujoDocumental != null && filtro.flujoDocumental.id > 0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                }


                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());

                        if (!row.IsNull("reqInterno"))
                        {
                            valDocResp.reqInterno = Convert.ToBoolean(row["reqInterno"]);
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        valDocResp.nuevaFecha = Convert.ToInt32(row["extensionFecha"]);
                        valDocResp.verificaAmpPlazo = Convert.ToInt32(row["verificaAmpPlazo"]);
                        valDocResp.verificaAmpExtension = Convert.ToInt32(row["verificaAmpExtension"]);

                        if (!row.IsNull("verificaConforme"))
                        {
                            valDocResp.verificaConforme = Convert.ToBoolean(row["verificaConforme"]);
                        }


                        if (!ht.Contains(valDocResp.flujoDocumental.id))
                        {
                            ht.Add(valDocResp.flujoDocumental.id, new Combobox(valDocResp.flujoDocumental.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(ht[valDocResp.flujoDocumental.id])).hash;

                        if (!htAux.Contains(valDocResp.tipoIO.id))
                        {
                            htAux.Add(valDocResp.tipoIO.id, new Combobox(valDocResp.tipoIO.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(htAux[valDocResp.tipoIO.id])).hash;

                        if (!htAux.Contains(valDocResp.tipoDestinatario.id))
                        {
                            htAux.Add(valDocResp.tipoDestinatario.id, new Combobox(valDocResp.tipoDestinatario.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(htAux[valDocResp.tipoDestinatario.id])).hash;

                        if (!htAux.Contains(valDocResp.ambito.id))
                        {
                            htAux.Add(valDocResp.ambito.id, new Combobox(valDocResp.ambito.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(htAux[valDocResp.ambito.id])).hash;

                        if (!htAux.Contains(valDocResp.tipoDocumento.id))
                        {
                            htAux.Add(valDocResp.tipoDocumento.id, new Combobox(valDocResp.tipoDocumento.descripcion));
                        }


                        htAux = (Hashtable)((Combobox)(htAux[valDocResp.tipoDocumento.id])).hash;

                        if (!htAux.Contains(valDocResp.subRequerimiento.id))
                        {
                            htAux.Add(valDocResp.subRequerimiento.id, valDocResp);
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

        /**
         * Listado de ValidacionDocumentacion en base a filtros.
        */
        public List<ValidacionDocumentacion> ListarValidacionDocumentacion(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                if (filtro.idValDocumentacion > 0)
                {
                    cnn.parametros.Add("@idValDocumentacion", filtro.idValDocumentacion);
                }
                if (filtro.tipoIO != null && filtro.tipoIO.id > 0)
                {
                    cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                }
                if (filtro.tipoDestinatario != null && filtro.tipoDestinatario.id > 0)
                {
                    cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);
                }
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                if (filtro.subRequerimiento != null && filtro.subRequerimiento.id > 0)
                {
                    cnn.parametros.Add("@idSubRequerimiento", filtro.subRequerimiento.id);
                }
                if (filtro.flujoDocumental != null && filtro.flujoDocumental.id > 0)
                {
                    cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                }

                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());

                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }

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




        /**
        * LISTA POSIBLES FLUJO DOCUMENTAL, EN BASE AL AMBITO Y SECCION
        */
        public List<ValidacionDocumentacion> ListarFlujoDocumentalFiltro(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }

                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }
                
                               
                DataTable dt = cnn.Execute();
                Hashtable hash = new Hashtable();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        
                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }


                        if (!hash.ContainsKey(valDocResp.flujoDocumental.id))
                        {
                            hash.Add(valDocResp.flujoDocumental.id, valDocResp.flujoDocumental);
                            resp.Add(valDocResp);
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



        /**
        * LISTA POSIBLES ENTRADA/SALIDA, EN BASE AL AMBITO, SECCION Y FLUJO DOCUMENTAL
        */
        public List<ValidacionDocumentacion> ListarEntradaSalidaFiltro(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";


                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }

                cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);

                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();
                Hashtable hash = new Hashtable();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }


                        if (!hash.ContainsKey(valDocResp.tipoIO.id))
                        {
                            hash.Add(valDocResp.tipoIO.id, valDocResp.tipoIO);
                            resp.Add(valDocResp);
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


        /**
        * LISTA POSIBLES DESTINATARIOS, EN BASE AL AMBITO, SECCION, TIPO SALIDA Y FLUJO DOCUMENTAL
        */
        public List<ValidacionDocumentacion> ListarTipoDestinatarioFiltro(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);


                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();
                Hashtable hash = new Hashtable();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }


                        if (!hash.ContainsKey(valDocResp.tipoDestinatario.id))
                        {
                            hash.Add(valDocResp.tipoDestinatario.id, valDocResp.tipoDestinatario);
                            resp.Add(valDocResp);
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



       




        /**
     * LISTA POSIBLES ORIGEN, EN BASE AL AMBITO, SECCION, TIPO ENTRADA  Y FLUJO DOCUMENTAL
     */
        public List<ValidacionDocumentacion> ListarTipoOrigenFiltro(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);

                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();
                Hashtable hash = new Hashtable();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }


                        if (!hash.ContainsKey(valDocResp.tipoDestinatario.id))
                        {
                            hash.Add(valDocResp.tipoDestinatario.id, valDocResp.tipoDestinatario);
                            resp.Add(valDocResp);
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




        /**
        * LISTA POSIBLES TIPOS DE DOCUMENTO, EN BASE AL AMBITO, SECCION, TIPO ENTRADA, FLUJO DOCUMENTAL Y TIPO ORIGEN/DESTINATARIO
        */
        public List<ValidacionDocumentacion> ListarTipoDocumentoFiltro(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);

                if (filtro.ambito != null && filtro.ambito.id > 0)
                {
                    cnn.parametros.Add("@idPestana", filtro.ambito.id);
                }
                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }


                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();
                Hashtable hash = new Hashtable();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }


                        if (!hash.ContainsKey(valDocResp.tipoDocumento.id))
                        {
                            hash.Add(valDocResp.tipoDocumento.id, valDocResp.tipoDocumento);
                            resp.Add(valDocResp);
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




        /**
       * LISTA POSIBLES TIPOS DE AMBITO, EN BASE A LA SECCION, TIPO ENTRADA/SALIDA, FLUJO DOCUMENTAL, TIPO ORIGEN/DESTINATARIO Y TIPO DE DOCUMENTO
       */
        public List<ValidacionDocumentacion> ListarTipoAmbitoFiltro(ValidacionDocumentacion filtro)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                List<ValidacionDocumentacion> resp = new List<ValidacionDocumentacion>();

                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                cnn.parametros.Add("@idTipoFlujoDoc", filtro.flujoDocumental.id);
                cnn.parametros.Add("@idTipoIO", filtro.tipoIO.id);
                cnn.parametros.Add("@idTipoDestinatario", filtro.tipoDestinatario.id);

                if (filtro.seccion != null && filtro.seccion.id > 0)
                {
                    cnn.parametros.Add("@idSeccion", filtro.seccion.id);
                }
                if (filtro.tipoDocumento != null && filtro.tipoDocumento.id > 0)
                {
                    cnn.parametros.Add("@idTipoDocumento", filtro.tipoDocumento.id);
                }

                if (filtro.aplicaConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaConcesion", filtro.aplicaConcesion);
                }
                if (filtro.aplicaModAmpliacion > 0)
                {
                    cnn.parametros.Add("@aplicaModAmpliacion", filtro.aplicaModAmpliacion);
                }
                if (filtro.aplicaModReduccion > 0)
                {
                    cnn.parametros.Add("@aplicaModReduccion", filtro.aplicaModReduccion);
                }
                if (filtro.aplicaModEspeciePT > 0)
                {
                    cnn.parametros.Add("@aplicaModEspeciePT", filtro.aplicaModEspeciePT);
                }
                if (filtro.aplicaModRegularizacion > 0)
                {
                    cnn.parametros.Add("@aplicaModRegularizacion", filtro.aplicaModRegularizacion);
                }
                if (filtro.aplicaRelocalizacion > 0)
                {
                    cnn.parametros.Add("@aplicaRelocalizacion", filtro.aplicaRelocalizacion);
                }
                if (filtro.aplicaAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaAmerb", filtro.aplicaAmerb);
                }
                if (filtro.aplicaFaenamiento > 0)
                {
                    cnn.parametros.Add("@aplicaFaenamiento", filtro.aplicaFaenamiento);
                }
                if (filtro.aplicaAcopio > 0)
                {
                    cnn.parametros.Add("@aplicaAcopio", filtro.aplicaAcopio);
                }
                if (filtro.aplicaColectores > 0)
                {
                    cnn.parametros.Add("@aplicaColectores", filtro.aplicaColectores);
                }
                if (filtro.aplicaExperimentalesAmerb > 0)
                {
                    cnn.parametros.Add("@aplicaExAmerb", filtro.aplicaExperimentalesAmerb);
                }
                if (filtro.aplicaExpConcesion > 0)
                {
                    cnn.parametros.Add("@aplicaExConcesion", filtro.aplicaExpConcesion);
                }
                if (filtro.aplicaAcuiculturaEcmpo > 0)
                {
                    cnn.parametros.Add("@aplicaEcmpo", filtro.aplicaAcuiculturaEcmpo);
                }
                if (filtro.aplicaModPT > 0)
                {
                    cnn.parametros.Add("@aplicaModPT", filtro.aplicaModPT);
                }

                DataTable dt = cnn.Execute();
                Hashtable hash = new Hashtable();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        if (!row.IsNull("idSubRequerimiento"))
                        {
                            valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        }


                        if (!hash.ContainsKey(valDocResp.ambito.id))
                        {
                            hash.Add(valDocResp.ambito.id, valDocResp.ambito);
                            resp.Add(valDocResp);
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



        /**
        * Obtiene la seccion a la que pertenece un subrequerimiento, en base a filtros
        */
        public ValidacionDocumentacion obtenerSeccionporTipo(int tipoIO, int idOrigenDestinatario, int idTipoDocumento, int idTipoAmbito, int idSubRequerimiento)
        {
            try
            {
                ValidacionDocumentacion valDocResp = null;
                Hashtable ht = new Hashtable();
                Hashtable htAux = new Hashtable();
                Conexion cnn = new Conexion();

                cnn.procedimiento = "paSelRbValidacionDocumentacion";
                
                cnn.parametros.Add("@idTipoIO", tipoIO);
                cnn.parametros.Add("@idTipoDestinatario", idOrigenDestinatario);
                cnn.parametros.Add("@idPestana", idTipoAmbito);
                cnn.parametros.Add("@idTipoDocumento", idTipoDocumento);
                cnn.parametros.Add("@idSubRequerimiento", idSubRequerimiento);

              

                DataTable dt = cnn.Execute();

                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {

                        valDocResp = new ValidacionDocumentacion();
                        valDocResp.idValDocumentacion = Convert.ToInt32(row["idValDocumentacion"]);
                        valDocResp.flujoDocumental = new ParametroGenerico(Convert.ToInt32(row["idTipoFlujoDocumental"]), row["nombreTipoFlujDoc"].ToString());
                        valDocResp.tipoIO = new ParametroGenerico(Convert.ToInt32(row["idTipoIO"]), row["nombreTipoIO"].ToString());
                        valDocResp.tipoDestinatario = new ParametroGenerico(Convert.ToInt32(row["idTipoDestinatario"]), row["nombreTipoDestinatario"].ToString());
                        valDocResp.ambito = new ParametroGenerico(Convert.ToInt32(row["idPestana"]), row["nombrePestana"].ToString());
                        valDocResp.seccion = new ParametroGenerico(Convert.ToInt32(row["idSeccion"]), row["nombreSeccion"].ToString());
                        valDocResp.subRequerimiento = new ParametroGenerico(Convert.ToInt32(row["idSubRequerimiento"]), row["nombreSubRequerimiento"].ToString());
                        valDocResp.tipoDocumento = new ParametroGenerico(Convert.ToInt32(row["idTipoDocumento"]), row["nombreTipoDoc"].ToString());

                        if (!row.IsNull("reqInterno"))
                        {
                            valDocResp.reqInterno = Convert.ToBoolean(row["reqInterno"]);
                        }

                        valDocResp.numero = Convert.ToInt32(row["numero"]);
                        valDocResp.fecha = Convert.ToInt32(row["fecha"]);
                        valDocResp.numeroCI = Convert.ToInt32(row["numeroCI"]);
                        valDocResp.fechaCI = Convert.ToInt32(row["fechaCI"]);
                        valDocResp.archivoBinario = Convert.ToInt32(row["archivoBinario"]);
                        valDocResp.nuevaFecha = Convert.ToInt32(row["extensionFecha"]);
                        valDocResp.verificaAmpPlazo = Convert.ToInt32(row["verificaAmpPlazo"]);
                        valDocResp.verificaAmpExtension = Convert.ToInt32(row["verificaAmpExtension"]);

                        if (!row.IsNull("verificaConforme"))
                        {
                            valDocResp.verificaConforme = Convert.ToBoolean(row["verificaConforme"]);
                        }
                      
                        
                       
                    }
                }

                return valDocResp;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }

        public bool GuardarValidacionDocumentacion(ValidacionDocumentacion valDocumentacion)
        {
            try
            {

                Conexion cnn = new Conexion();
                cnn.procedimiento = "paInsRbValidacionDocumentacion";
                cnn.parametros.Add("@idValDocumentacion", valDocumentacion.idValDocumentacion);
                cnn.parametros.Add("@idTipoFlujoDocumental", valDocumentacion.flujoDocumental.id);
                cnn.parametros.Add("@idTipoIO", valDocumentacion.tipoIO.id);
                cnn.parametros.Add("@idTipoDestinatario", valDocumentacion.tipoDestinatario.id);
                cnn.parametros.Add("@idPestana", valDocumentacion.ambito.id);
                cnn.parametros.Add("@idSeccion", valDocumentacion.seccion.id);
                cnn.parametros.Add("@idSubRequerimiento", valDocumentacion.subRequerimiento.id);
                cnn.parametros.Add("@idTipoDocumento", valDocumentacion.tipoDocumento.id);
                cnn.parametros.Add("@numero", valDocumentacion.numero);
                cnn.parametros.Add("@fecha", valDocumentacion.fecha);
                cnn.parametros.Add("@numeroCI", valDocumentacion.numeroCI);
                cnn.parametros.Add("@fechaCI", valDocumentacion.fechaCI);
                cnn.parametros.Add("@archivoBinario", valDocumentacion.archivoBinario);
                cnn.parametros.Add("@verificaConforme", valDocumentacion.verificaConforme);
                cnn.parametros.Add("@aplicaConcesion", valDocumentacion.aplicaConcesion);
                cnn.parametros.Add("@aplicaModAmpliacion", valDocumentacion.aplicaModAmpliacion);
                cnn.parametros.Add("@aplicaModReduccion", valDocumentacion.aplicaModAmpliacion);
                cnn.parametros.Add("@aplicaModEspeciePT", valDocumentacion.aplicaModEspeciePT);
                cnn.parametros.Add("@aplicaModRegularizacion", valDocumentacion.aplicaModRegularizacion);
                cnn.parametros.Add("@aplicaRelocalizacion", valDocumentacion.aplicaRelocalizacion);
                cnn.parametros.Add("@aplicaAmerb", valDocumentacion.aplicaAmerb);
                cnn.parametros.Add("@aplicaFaenamiento", valDocumentacion.aplicaFaenamiento);
                cnn.parametros.Add("@aplicaAcopio", valDocumentacion.aplicaAcopio);
                cnn.parametros.Add("@aplicaColectores", valDocumentacion.aplicaColectores);
                cnn.parametros.Add("@extensionFecha", valDocumentacion.nuevaFecha);
                cnn.parametros.Add("@verificaAmpPlazo", valDocumentacion.verificaAmpPlazo);
                cnn.parametros.Add("@verificaAmpExtension", valDocumentacion.verificaAmpExtension);

                cnn.parametros.Add("@aplicaExAmerb", valDocumentacion.aplicaExperimentalesAmerb);
                cnn.parametros.Add("@aplicaExConcesion", valDocumentacion.aplicaExpConcesion);
                cnn.parametros.Add("@aplicaECMPO", valDocumentacion.aplicaAcuiculturaEcmpo);
                cnn.parametros.Add("@aplicaModECMPOAmpl", valDocumentacion.aplicaModAcopioAmpl);
                cnn.parametros.Add("@aplicaModECMPOReduc", valDocumentacion.aplicaModECMPOReduc);
                cnn.parametros.Add("@aplicaModECMPOEspecie", valDocumentacion.aplicaModECMPOEspecie);
                cnn.parametros.Add("@aplicaModECMPO_PT", valDocumentacion.aplicaModECMPO_PT);
                cnn.parametros.Add("@aplicaModECMPO_Regulariz", valDocumentacion.aplicaModECMPO_Regulariz);
                cnn.parametros.Add("@aplicaModAcopioAmpl", valDocumentacion.aplicaModAcopioAmpl);
                cnn.parametros.Add("@aplicaModAcopioReduc", valDocumentacion.aplicaModAcopioReduc);
                cnn.parametros.Add("@aplicaModAcopioEspecie", valDocumentacion.aplicaModAcopioEspecie);
                cnn.parametros.Add("@aplicaModAcopio_PT", valDocumentacion.aplicaModAcopio_PT);
                cnn.parametros.Add("@aplicaModAcopioRegulariz", valDocumentacion.aplicaModAcopioRegulariz);
                cnn.parametros.Add("@aplicaModFaenamAmpl", valDocumentacion.aplicaModFaenamAmpl);
                cnn.parametros.Add("@aplicaModFaenamReduc", valDocumentacion.aplicaModFaenamReduc);
                cnn.parametros.Add("@aplicaModFaenamEspecie", valDocumentacion.aplicaModFaenamEspecie);
                cnn.parametros.Add("@aplicaModFaenam_PT", valDocumentacion.aplicaModFaenam_PT);
                cnn.parametros.Add("@aplicaModFaenamRegulariz", valDocumentacion.aplicaModFaenamRegulariz);
                cnn.parametros.Add("@aplicaModAmerbAmpl", valDocumentacion.aplicaModAmerbAmpl);
                cnn.parametros.Add("@aplicaModAmerbReduc", valDocumentacion.aplicaModAmerbReduc);
                cnn.parametros.Add("@aplicaModAmerbEspecie", valDocumentacion.aplicaModAmerbEspecie);
                cnn.parametros.Add("@aplicaModAmerb_PT", valDocumentacion.aplicaModAmerb_PT);
                cnn.parametros.Add("@aplicaModAmerbRegulariz", valDocumentacion.aplicaModAmerbRegulariz);


                DataTable dt = cnn.Execute();
                valDocumentacion.idValDocumentacion = Convert.ToInt32(dt.Rows[0]["idValDocumentacion"]);

                return true;
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return false;
            };
        }

       public DataTable EliminarValidacionDocumentacion(int idValDocumentacion)
        {
            Conexion cnn = new Conexion();
            cnn.procedimiento = "paDelRbValidacionDocumentacion";
            cnn.parametros.Add("@idValDocumentacion", idValDocumentacion);

            DataTable dt = cnn.Execute();
            return dt;
        }
    }
}
