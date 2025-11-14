using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades.Relocalizacion;
using Datos.Utilidades;
using LogicaNegocio.cl.subpesca.rb.errores;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using Datos.Contantes;
using System.Collections;
using Datos.Entidades;

namespace Validaciones.cl.subpesca.rb.relocalizacion
{
    public class TramiteRelocalizacionRESAValidacion
    {

        Logger logger = new Logger();
        RelocalizacionRESAService relocalizacionRESAService = new RelocalizacionRESAService();
        
        public List<string> validaGenerarSolicitudRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacionRESA)
        {

            List<String> errores = new List<String>();

            try
            {

                DateTime systemDate = DateTime.Now;
                DateTime fechaMinSistema = FechaUtils.fechaMinimaSistema();

                if (tramiteRelocalizacionRESA.fechaRecepcion != default(DateTime) && tramiteRelocalizacionRESA.fechaRecepcion > systemDate)
                {
                    errores.Add("Fecha no puede ser posterior al día de hoy.");
                }
                else
                {
                    if (tramiteRelocalizacionRESA.fechaRecepcion < fechaMinSistema)
                    {
                        errores.Add("Fecha no puede ser anterior al año 1930");
                    }
                }


                if (tramiteRelocalizacionRESA.fechaIngresoTramite != default(DateTime) && tramiteRelocalizacionRESA.fechaIngresoTramite > systemDate)
                {
                    errores.Add("Fecha no puede ser posterior al día de hoy.");
                }
                else
                {
                    if (tramiteRelocalizacionRESA.fechaIngresoTramite < fechaMinSistema)
                    {
                        errores.Add("Fecha no puede ser anterior al año 1930");
                    }
                }

                if (tramiteRelocalizacionRESA.numPert != null)
                {
                    bool existePERT = relocalizacionRESAService.aplicaPertTramiteExistenteRESA(tramiteRelocalizacionRESA.numPert);
                    if (existePERT)
                    {
                        errores.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                    }
                }


            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }
            return errores;
        }



        public List<string> validaIngresoOrigenSectorRESA(OrigenSector nuevoOrigen, List<OrigenSector> origenes, bool esSectorCero)
        {
            List<String> errores = new List<String>();

            try
            {

                if (nuevoOrigen.concesionOrigen == null || nuevoOrigen.concesionOrigen.unidadEspacial == null || nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo == null || nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                {
                    errores.Add("Ingrese Código del Centro");
                }
                else if (nuevoOrigen.superficieRelocalizada <= 0)
                {
                    if (esSectorCero)
                    {
                        errores.Add("Hectáreas Sector 0");
                    }
                    else
                    {
                        errores.Add("Ingrese Superficie Relocalización");
                    }
                }
                else
                {

                    if (origenes != null && origenes.Count > 0)
                    {

                        foreach (OrigenSector aOrigenSector in origenes)
                        {

                            if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                            {
                                if (nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro == aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro)
                                {
                                    errores.Add("Centro ya ingresado en grilla de origenes.");
                                }
                            }
                        }
                    }
                }


                

                //DEBE INDICAR AL  MENOS 1 TIPO DE PREFERENCIAS DE RELOCALIZACION
                if (!esSectorCero) {

                    if (nuevoOrigen.preferencias == null || nuevoOrigen.preferencias.Count() == 0)
                    {
                        errores.Add("Debe seleccionar al menos 1 una preferencia de relocalización");
                    }
                    else
                    {

                        bool marcoNoTienePreferencias = false;

                        //SI INDICO QUE NO TIENE PREFERENCIAS ENTONCES NO PUEDEN ESTAR MARCADAS LAS OTROS TIPOS DE PREFERENCIA
                        foreach (ParametroGenerico prefer in nuevoOrigen.preferencias) { 
                            if(prefer.id == rbPreferenciaRelocalizacion.NO_TIENE_PREFERENCIA ){
                                marcoNoTienePreferencias = true;
                                break;
                            }
                        }

                        if(marcoNoTienePreferencias && nuevoOrigen.preferencias.Count() > 1){
                            errores.Add("Si selecciono \"No tiene preferencias\" no puede indicar otros tipos de preferencias de relocalización");
                        }

                    } 
                }

               

            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            return errores;
        }




        public List<string> validaModificarOrigenSectorRESA(OrigenSector nuevoOrigen, List<OrigenSector> origenes, int indexModificado)
        {
            List<String> errores = new List<String>();

            try
            {

                if (nuevoOrigen.concesionOrigen == null || nuevoOrigen.concesionOrigen.unidadEspacial == null || nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo == null || nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                {
                    errores.Add("Ingrese Código del Centro");
                }
                else if (nuevoOrigen.superficieRelocalizada <= 0)
                {
                    errores.Add("Ingrese Superficie Relocalización");
                }
                else
                {

                    if (origenes != null && origenes.Count > 0)
                    {

                        foreach (OrigenSector aOrigenSector in origenes)
                        {

                            if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                            {
                                if (aOrigenSector.index != indexModificado)
                                {
                                    if (nuevoOrigen.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro == aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro)
                                    {
                                        errores.Add("Centro ya ingresado en grilla de origenes.");
                                    }
                                }
                            }
                        }
                    }
                }



                //DEBE INDICAR AL  MENOS 1 TIPO DE PREFERENCIAS DE RELOCALIZACION
                if (nuevoOrigen.preferencias == null || nuevoOrigen.preferencias.Count() == 0)
                {
                    errores.Add("Debe seleccionar al menos 1 una preferencia de relocalización");
                }
                else
                {

                    bool marcoNoTienePreferencias = false;

                    //SI INDICO QUE NO TIENE PREFERENCIAS ENTONCES NO PUEDEN ESTAR MARCADAS LAS OTROS TIPOS DE PREFERENCIA
                    foreach (ParametroGenerico prefer in nuevoOrigen.preferencias)
                    {
                        if (prefer.id == rbPreferenciaRelocalizacion.NO_TIENE_PREFERENCIA)
                        {
                            marcoNoTienePreferencias = true;
                            break;
                        }
                    }

                    if (marcoNoTienePreferencias && nuevoOrigen.preferencias.Count() > 1)
                    {
                        errores.Add("Si selecciono \"No tiene preferencias\" no puede indicar otros tipos de preferencias de relocalización");
                    }

                }
                
            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            return errores;
        }


        public List<string> validaEliminacionOrigenSectorRESA(OrigenSector aOrigenSector)
        {

            List<String> errores = new List<String>();

            try
            {

            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }
            return errores;
        }


        public List<String> validaIngresoSectorRelocalizacionRESA(DetalleSector sector, List<DetalleSector> sectores)
        {

            List<String> errores = new List<String>();
            Hashtable sectoresCero = new Hashtable();
            Hashtable todosOrigenes = new Hashtable();

            try
            {

                if (sectores != null)
                {
                    foreach (DetalleSector aDetalleSector in sectores)
                    {
                        if (aDetalleSector.esSectorCero && (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO))
                        {
                            sectoresCero.Add(aDetalleSector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, null);
                        }

                        if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO)
                        {
                            foreach (OrigenSector aOrigenSector in aDetalleSector.origenes)
                            {
                                if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                                {
                                    if (!todosOrigenes.ContainsKey(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                                    {
                                        todosOrigenes.Add(aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro, null);
                                    }
                                }
                            }
                        }
                    }
                }


                //ES SECTOR CERO
                if (sector.esSectorCero)
                {
                    if (sector.origenes == null || sector.origenes.Count != 1)
                    {
                        errores.Add("Ingrese Código del Centro");
                    }
                    else
                    {

                        if (sector.origenes[0].concesionOrigen == null || sector.origenes[0].concesionOrigen.unidadEspacial == null || sector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo == null || sector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                        {
                            errores.Add("Ingrese Código del Centro");
                        }
                        else
                        {
                            if (sectoresCero.ContainsKey(sector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro))
                            {
                                errores.Add("Ya existe un sector 0 para Código del centro seleccionado");
                            }
                        }


                        if (sector.origenes[0].superficieRelocalizada <= 0)
                        {
                            errores.Add("Ingrese Hectáreas Sector 0");
                        }

                    }
                }
                else
                {


                    bool tieneOrigenes = false;

                    if (sector.origenes == null || sector.origenes.Count == 0)
                    {
                        errores.Add("Ingrese centro(s) de origen para el sector");
                    }
                    else
                    {


                        foreach (OrigenSector aOrigenSector in sector.origenes)
                        {
                            if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                            {
                                tieneOrigenes = true;
                                break;
                            }
                        }

                        if (!tieneOrigenes)
                        {
                            errores.Add("Ingrese centro(s) de origen para el sector");
                        }
                    }


                    if (sector.tipoRelocalizacion == null || sector.tipoRelocalizacion.id <= 0)
                    {
                        errores.Add("Seleccione tipo de relocalización");
                    }
                    else
                    {

                        if (sector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                        {

                            if (sector.concesionDestino == null || sector.concesionDestino.unidadEspacial == null || sector.concesionDestino.unidadEspacial.centrosDeCultivo == null || sector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                            {
                                errores.Add("Ingrese Código del Centro Destino");
                            }
                            else
                            {

                                //VALIDAR QUE EL CENTRO DE DESTINO NO ESTE DENTRO DE LOS ORIGENES
                                if (tieneOrigenes)
                                {

                                    foreach (OrigenSector aOrigenSector in sector.origenes)
                                    {
                                        if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                                        {
                                            if (aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro == sector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro)
                                            {
                                                errores.Add("El centro de destino esta dentro de los origenes del sector");
                                                break;
                                            }
                                        }
                                    }
                                }



                                //VALIDAR QUE EL CENTRO DE DESTINO NO ESTE COMO DESTINO DE OTRO SECTOR
                                if (sectores != null)
                                {
                                    foreach (DetalleSector aDetalleSector in sectores)
                                    {
                                        if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO)
                                        {

                                            if (aDetalleSector.esSectorCero == false && aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                                            {
                                                if (aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro == sector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro)
                                                {
                                                    errores.Add("El centro de destino ya esta como destino de otro sector");
                                                    break;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            return errores;

        }



        public List<String> validaModificarSectorRelocalizacionRESA(DetalleSector sector, List<DetalleSector> sectores, int indexModificar)
        {

            List<String> errores = new List<String>();

            try
            {
                //ES SECTOR CERO
                if (sector.esSectorCero)
                {
                    if (sector.origenes == null || sector.origenes.Count == 0)
                    {
                        errores.Add("Ingrese Código del Centro");
                    }
                    else
                    {

                        if (sector.origenes[0].concesionOrigen == null || sector.origenes[0].concesionOrigen.unidadEspacial == null || sector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo == null || sector.origenes[0].concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                        {
                            errores.Add("Ingrese Código del Centro");
                        }

                        if (sector.origenes[0].superficieRelocalizada <= 0)
                        {
                            errores.Add("Ingrese Hectáreas Sector 0");
                        }
                    }
                }
                else
                {


                    bool tieneOrigenes = false;

                    if (sector.origenes == null || sector.origenes.Count == 0)
                    {
                        errores.Add("Ingrese centro(s) de origen para el sector");
                    }
                    else
                    {


                        foreach (OrigenSector aOrigenSector in sector.origenes)
                        {
                            if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                            {
                                tieneOrigenes = true;
                                break;
                            }
                        }

                        if (!tieneOrigenes)
                        {
                            errores.Add("Ingrese centro(s) de origen para el sector");
                        }
                    }


                    if (sector.tipoRelocalizacion == null || sector.tipoRelocalizacion.id <= 0)
                    {
                        errores.Add("Seleccione tipo de relocalización");
                    }
                    else
                    {

                        if (sector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                        {

                            if (sector.concesionDestino == null || sector.concesionDestino.unidadEspacial == null || sector.concesionDestino.unidadEspacial.centrosDeCultivo == null || sector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                            {
                                errores.Add("Ingrese Código del Centro Destino");
                            }
                            else
                            {

                                //VALIDAR QUE EL CENTRO DE DESTINO NO ESTE DENTRO DE LOS ORIGENES
                                if (tieneOrigenes)
                                {

                                    foreach (OrigenSector aOrigenSector in sector.origenes)
                                    {
                                        if (aOrigenSector.accion == accion.INGRESAR || aOrigenSector.accion == accion.MODIFICAR || aOrigenSector.accion == accion.LISTADO)
                                        {
                                            if (aOrigenSector.concesionOrigen.unidadEspacial.centrosDeCultivo.codigoCentro == sector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro)
                                            {
                                                errores.Add("El centro de destino esta dentro de los origenes del sector");
                                                break;
                                            }
                                        }
                                    }
                                }



                                //VALIDAR QUE EL CENTRO DE DESTINO NO ESTE COMO DESTINO DE OTRO SECTOR
                                if (sectores != null)
                                {
                                    foreach (DetalleSector aDetalleSector in sectores)
                                    {
                                        if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO)
                                        {

                                            if (aDetalleSector.index != indexModificar)
                                            {

                                                if (aDetalleSector.esSectorCero == false && aDetalleSector.tipoRelocalizacion.id == rbTipo.RELOCALIZACION_FUSIONA)
                                                {
                                                    if (aDetalleSector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro == sector.concesionDestino.unidadEspacial.centrosDeCultivo.codigoCentro)
                                                    {
                                                        errores.Add("El centro de destino ya esta como destino de otro sector");
                                                        break;
                                                    }

                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }

            return errores;

        }



        public List<string> validaTramiteRelocalizacionRESA(TramiteRelocalizacion tramiteRelocalizacion)
        {
            List<String> errores = new List<String>();

            try
            {

                int cantidadSectores = 0;

                if (tramiteRelocalizacion.sectores != null)
                {

                    foreach (DetalleSector aDetalleSector in tramiteRelocalizacion.sectores)
                    {
                        if (aDetalleSector.accion == accion.INGRESAR || aDetalleSector.accion == accion.MODIFICAR || aDetalleSector.accion == accion.LISTADO)
                        {
                            cantidadSectores++;
                        }
                    }

                }


                if (cantidadSectores == 0)
                {
                    errores.Add("Ingrese sectores.");
                }

            }
            catch (Exception ex)
            {
                errores.Add("Ha ocurrido un error al realizar la acción solicitada.");
                logger.PrintError(ex);
                logger.SendMailError(ex);
            }
            return errores;
        }


    }
}
