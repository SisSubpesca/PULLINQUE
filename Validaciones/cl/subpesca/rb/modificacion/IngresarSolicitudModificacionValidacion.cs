using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;

namespace Validaciones.cl.subpesca.rb.modificacion
{
    public class IngresarSolicitudModificacionValidacion
    {
        SolicitudDA solicitudDA = new SolicitudDA();
        
        public List<string> validaIngresarSolicitudModificacion(Datos.Entidades.SolicitudConcesion solicitudInicial)
        {

            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudInicial != null)
            {
                if (tieneTipoModificacionRegularizacion(solicitudInicial.tipoModificacionesTram))
                {

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }


                }
                else {
                    DateTime systemDate = DateTime.Now;

                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                    }

                    DateTime fechaMinima = new DateTime(1930, 01, 01);
                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite < fechaMinima)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser inferior a 1930.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion < fechaMinima)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser inferior a 1930.");
                    }

                    bool existePERT = solicitudDA.aplicaPertExistenteModConcesion(solicitudInicial.numPert);
                    if (existePERT)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                    }

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }



        public List<string> validaIngresarSolicitudModificacionAmerb(Datos.Entidades.SolicitudConcesion solicitudInicial)
        {
            List<String> listaErrroresSolicitudModificacionAmerb = new List<String>();
            if (solicitudInicial != null)
            {
                if (tieneTipoModificacionRegularizacion(solicitudInicial.tipoModificacionesTram))
                {

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }


                }
                else
                {
                    DateTime systemDate = DateTime.Now;

                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite > systemDate)
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion > systemDate)
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                    }

                    DateTime fechaMinima = new DateTime(1930, 01, 01);
                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite < fechaMinima)
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("La Fecha de Ingreso a Trámite no debe ser inferior a 1930.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion < fechaMinima)
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("La Fecha de Recepción no debe ser inferior a 1930.");
                    }


                    if (solicitudInicial.datosSolicitudUE == null || solicitudInicial.datosSolicitudUE.numeroCI < 1)
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("Ingrese Número CI");
                    }

                    /* Fecha de CI no debe ser mayor a la fecha del día de hoy */
                    if (solicitudInicial.datosSolicitudUE == null || solicitudInicial.datosSolicitudUE.fechaCI == default(DateTime))
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("Ingrese fecha CI");
                    }
                    else
                    {

                        if (solicitudInicial.datosSolicitudUE.fechaCI != default(DateTime) && solicitudInicial.datosSolicitudUE.fechaCI > systemDate)
                        {
                            listaErrroresSolicitudModificacionAmerb.Add("La Fecha de CI no debe ser mayor a la fecha de hoy.");
                        }
                    }


                    bool existePERT = solicitudDA.aplicaPertExistenteModConcesion(solicitudInicial.numPert);
                    if (existePERT)
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                    }
                    
                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudModificacionAmerb.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }
                }
            }
            return listaErrroresSolicitudModificacionAmerb;
        }

        private bool tieneTipoModificacionAmpliacionReduccion(List<Datos.Entidades.ParametroGenerico> tipoModificacionesTram)
        {
            bool tieneReduccion = false;
            bool tieneAmpliacion = false;

            foreach (ParametroGenerico tipoModificacion in tipoModificacionesTram)
            {
                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE) || (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE))
                {
                    tieneReduccion = true;
                }
                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE) || (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE))
                {
                    tieneAmpliacion = true;
                }
            }

            if (tieneReduccion && tieneAmpliacion) {
                return true;
            }

            return false;
        }

        private bool tieneTipoModificacionRegularizacion(List<Datos.Entidades.ParametroGenerico> tipoModificacionesTram)
        {
            bool tieneRegularizacion = false;
            bool tieneOtroTipoMod = false;
       
            foreach (ParametroGenerico tipoModificacion in tipoModificacionesTram)
            {
                if ((tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION) || (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION) || (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) || (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) || (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION))
                {
                    tieneRegularizacion = true;
                }
                if ((tipoModificacion.id != rbTipo.MOD_CONCESION_REGULARIZACION) && (tipoModificacion.id != rbTipo.MOD_AMERB_REGULARIZACION) && (tipoModificacion.id != rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) && (tipoModificacion.id != rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) && (tipoModificacion.id != rbTipo.MOD_ECMPO_REGULARIZACION))
                {
                    tieneOtroTipoMod = true;
                }

            }
            if (tieneRegularizacion && !tieneOtroTipoMod)
            {
                return true;
            }
            else
            {

                return false;
            }
        }


        public List<string> validaCodigoCentroECMPO(string codigoCentro)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_ECMPO, Convert.ToString(codigoCentro), 0);

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Código de Centro: " + codigoCentro + " no está registrado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validaCodigoCentroAmerb(string codigoCentro)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB, Convert.ToString(codigoCentro), 0);

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Código de Centro: " + codigoCentro + " no está registrado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }


        /**
        * VERIFICA SI EL CODIGO PASADO POR PARAMETRO CORRESPONDE A UNA CONCESION DE ACUICULTURA VALIDA Y VIGENTE
        */ 
        public List<string> validaCodigoCentroConcesionAcuicultura(string codigoCentro)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CONCESION, Convert.ToString(codigoCentro), 0);

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Código de Centro: " + codigoCentro + " no está registrado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        /**
         * VERIFICA SI EL CODIGO PASADO POR PARAMETRO CORRESPONDE A UN CENTRO DE ACOPIO VALIDO Y VIGENTE
         */ 
        public List<string> validaCodigoCentroCentroAcopio(string codigoCentro)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO, Convert.ToString(codigoCentro), 0);

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Código de Centro: " + codigoCentro + " no está registrado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }



        /**
       * VERIFICA SI EL CODIGO PASADO POR PARAMETRO CORRESPONDE A UN CENTRO DE FAENAMIENTO VALIDO Y VIGENTE
       */
        public List<string> validaCodigoCentroCentroFaenamiento(string codigoCentro)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO, Convert.ToString(codigoCentro), 0);

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Código de Centro: " + codigoCentro + " no está registrado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        /**
        * VERIFICA SI EL TITULAR PASADO POR PARAMETRO CORRESPONDE A UN TITULAR DE LA CONCESION DE ACUICULTURA
        */ 
        public List<string> validaTitularCentroConcesionAcuicultura(string codigoCentro,string titularCentro)
        {
            
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (titularCentro != null && !titularCentro.Trim().Equals(""))
            {
                string[] partesRut = titularCentro.Split('-');

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CONCESION, Convert.ToString(codigoCentro), Convert.ToInt32(partesRut[0]));

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Rut Titular: " + titularCentro + " no pertenece al Código Centro ingresado.");
                }
            }

            return listaErrroresSolicitudConcesionAcuicultura;
        }


        /**
      * VERIFICA SI EL TITULAR PASADO POR PARAMETRO CORRESPONDE A UN TITULAR DE UNA ECMPO
      */
        public List<string> validaTitularCentroECMPO(string codigoCentro, string titularCentro)
        {

            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (titularCentro != null && !titularCentro.Trim().Equals(""))
            {
                string[] partesRut = titularCentro.Split('-');

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_ECMPO, Convert.ToString(codigoCentro), Convert.ToInt32(partesRut[0]));

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Rut Titular: " + titularCentro + " no pertenece al Código Centro ingresado.");
                }
            }

            return listaErrroresSolicitudConcesionAcuicultura;
        }

        /**
        * VERIFICA SI EL TITULAR PASADO POR PARAMETRO CORRESPONDE A UN TITULAR DEL CENTRO DE ACOPIO 
        */ 
        public List<string> validaTitularCentroAcopio(string codigoCentro, string titularCentro)
        {

            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (titularCentro != null && !titularCentro.Trim().Equals(""))
            {
                string[] partesRut = titularCentro.Split('-');

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO, Convert.ToString(codigoCentro), Convert.ToInt32(partesRut[0]));

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Rut Titular: " + titularCentro + " no pertenece al Código Centro ingresado.");
                }
            }

            return listaErrroresSolicitudConcesionAcuicultura;
        }

        /**
        * VERIFICA SI EL TITULAR PASADO POR PARAMETRO CORRESPONDE A UN TITULAR DEL CENTRO DE ACOPIO 
        */
        public List<string> validaTitularCentroFaenamiento(string codigoCentro, string titularCentro)
        {

            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (titularCentro != null && !titularCentro.Trim().Equals(""))
            {
                string[] partesRut = titularCentro.Split('-');

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO, Convert.ToString(codigoCentro), Convert.ToInt32(partesRut[0]));

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Rut Titular: " + titularCentro + " no pertenece al Código Centro ingresado.");
                }
            }

            return listaErrroresSolicitudConcesionAcuicultura;
        }
        /** verifica la existencia del centro **/
        public List<string> validaCodigoCentro(string codigoCentro)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (codigoCentro != null && !codigoCentro.Trim().Equals(""))
            {

                SolicitudConcesion solicitudConcesion = solicitudDA.VerificaUnidadEspacialTitularVigente(0, Convert.ToString(codigoCentro), 0);

                if (solicitudConcesion == null)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Código de Centro: " + codigoCentro + " no está registrado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }


        public List<string> validaIngresarSolicitudModificacionAcopio(SolicitudConcesion solicitudInicial)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudInicial != null)
            {
                if (tieneTipoModificacionRegularizacion(solicitudInicial.tipoModificacionesTram))
                {

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }


                }
                else
                {
                    DateTime systemDate = DateTime.Now;

                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                    }

                    DateTime fechaMinima = new DateTime(1930, 01, 01);
                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite < fechaMinima)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser inferior a 1930.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion < fechaMinima)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser inferior a 1930.");
                    }

                    bool existePERT = solicitudDA.aplicaPertExistenteModConcesion(solicitudInicial.numPert);
                    if (existePERT)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                    }

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }



        public List<string> validaIngresarSolicitudModificacionFaenamiento(SolicitudConcesion solicitudInicial)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudInicial != null)
            {
                if (tieneTipoModificacionRegularizacion(solicitudInicial.tipoModificacionesTram))
                {

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }


                }
                else
                {
                    DateTime systemDate = DateTime.Now;

                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                    }

                    DateTime fechaMinima = new DateTime(1930, 01, 01);
                    if (solicitudInicial.fechaIngresoTramite != default(DateTime) && solicitudInicial.fechaIngresoTramite < fechaMinima)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser inferior a 1930.");
                    }

                    if (solicitudInicial.fechaRecepcion != default(DateTime) && solicitudInicial.fechaRecepcion < fechaMinima)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser inferior a 1930.");
                    }

                    bool existePERT = solicitudDA.aplicaPertExistenteModConcesion(solicitudInicial.numPert);
                    if (existePERT)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                    }

                    if (tieneTipoModificacionAmpliacionReduccion(solicitudInicial.tipoModificacionesTram))
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("Debe seleccionar tipo modificación Ampliación de Superfice o Reducción de Superficie no ambas.");
                    }
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }


    }
}
