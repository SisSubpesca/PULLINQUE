using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using Datos.Entidades;
using Datos.Contantes;

namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class InicioSolicitudConcesionValidacion
    {

        SolicitudDA solicitudDA = new SolicitudDA();


        /**
         *  Método que valida el ingreso de la solicitud de concesion de acuicultura.
         */
        public List<String> validaInicioSolicitudConcesionAcuicultura(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                     listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }
                
                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }
                
                /* El PERT ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudConcesion.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }



        /**
         *  Método que valida el ingreso de la solicitud de centro de acopio
         */
        public List<String> validaInicioSolicitudCentroAcopio(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

                /* El PERT ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudConcesion.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }


        /**
       *  Método que valida el ingreso de la solicitud de centro de faenamiento
       */
        public List<String> validaInicioSolicitudCentroFaenamiento(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

                /* El PERT ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudConcesion.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }


        /**
       *  Método que valida el ingreso de la solicitud de acuicultura amerb
       */
        public List<string> validaInicioSolicitudAcuiculturaAmerb(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

                if (solicitudConcesion.datosSolicitudUE == null || solicitudConcesion.datosSolicitudUE.numeroCI < 1) {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Ingrese Número CI");
                }

                /* Fecha de CI no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.datosSolicitudUE == null || solicitudConcesion.datosSolicitudUE.fechaCI == default(DateTime))
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Ingrese fecha CI");
                }
                else {

                    if (solicitudConcesion.datosSolicitudUE.fechaCI != default(DateTime) && solicitudConcesion.datosSolicitudUE.fechaCI > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de CI no debe ser mayor a la fecha de hoy.");
                    }
                }



                /* El Número Identificador Solicitud (PERT) ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudConcesion.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("El Número Identificador Solicitud ya se encuentra ingresado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validaInicioSolicitudExperimentalesAmerb(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudExperimentalesAmerb = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudExperimentalesAmerb.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudExperimentalesAmerb.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

                if (solicitudConcesion.datosSolicitudUE == null || solicitudConcesion.datosSolicitudUE.numeroCI < 1)
                {
                    listaErrroresSolicitudExperimentalesAmerb.Add("Ingrese Número CI");
                }

                /* Fecha de CI no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.datosSolicitudUE == null || solicitudConcesion.datosSolicitudUE.fechaCI == default(DateTime))
                {
                    listaErrroresSolicitudExperimentalesAmerb.Add("Ingrese fecha CI");
                }
                else
                {

                    if (solicitudConcesion.datosSolicitudUE.fechaCI != default(DateTime) && solicitudConcesion.datosSolicitudUE.fechaCI > systemDate)
                    {
                        listaErrroresSolicitudExperimentalesAmerb.Add("La Fecha de CI no debe ser mayor a la fecha de hoy.");
                    }
                }


                /* El Número Identificador Solicitud ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudConcesion.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudExperimentalesAmerb.Add("El Número Identificador Solicitud ya se encuentra ingresado en el sistema.");
                }

            }
            return listaErrroresSolicitudExperimentalesAmerb;
        }


        public List<string> validaInicioSolicitudECMPO(Datos.Entidades.SolicitudConcesion solicitudECMPO)
        {
            List<String> listaErrroresSolicitudECMPO = new List<String>();
            if (solicitudECMPO != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudECMPO.fechaIngresoTramite != default(DateTime) && solicitudECMPO.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudECMPO.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudECMPO.fechaRecepcion != default(DateTime) && solicitudECMPO.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudECMPO.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

                /* El PERT ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudECMPO.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudECMPO.Add("El Número PERT ya se encuentra ingresado en el sistema.");
                }
            }
            return listaErrroresSolicitudECMPO;
        }


        /**
       *  Método que valida el ingreso de la solicitud de colector de semilla
       */
        public List<string> validaInicioSolicitudColectorSemilla(Datos.Entidades.SolicitudConcesion solicitudConcesion)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            if (solicitudConcesion != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Ingreso a Trámite no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaIngresoTramite != default(DateTime) && solicitudConcesion.fechaIngresoTramite > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Ingreso a Trámite no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Recepción no debe ser mayor a la fecha del día de hoy */
                if (solicitudConcesion.fechaRecepcion != default(DateTime) && solicitudConcesion.fechaRecepcion > systemDate)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de Recepción no debe ser mayor a la fecha de hoy.");
                }

                /* El PERT ingresado no debe estar previamente en el sistema */
                bool existeNumIdentificador = solicitudDA.aplicaNumIdentificadorExistente(solicitudConcesion.datosSolicitudUE.numIdentSolicitud);
                if (existeNumIdentificador)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("El Número identificador solicitud ya se encuentra ingresado en el sistema.");
                }
            }
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validarFichaColector(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validarFichaAmerb(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validarFichaExperimentalesAmerb(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validarFichaFaenamiento(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validarFichaAcopio(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }


        public List<string> validarFichaECMPO(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validarFichaExperimentalesConcesion(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();
            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validaInicioSolicitudExperimentalesConcesion(Datos.Entidades.SolicitudConcesion solicitudInicial)
        {
            List<String> listaErrroresSolicitudConcesionAcuicultura = new List<String>();


            if (solicitudInicial != null)
            {
                DateTime systemDate = DateTime.Now;



                if (solicitudInicial.datosSolicitudUE == null || solicitudInicial.datosSolicitudUE.numeroCI < 1)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Ingrese Número CI");
                }

                /* Fecha de CI no debe ser mayor a la fecha del día de hoy */
                if (solicitudInicial.datosSolicitudUE == null || solicitudInicial.datosSolicitudUE.fechaCI == default(DateTime))
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("Ingrese fecha CI");
                }
                else
                {

                    if (solicitudInicial.datosSolicitudUE.fechaCI != default(DateTime) && solicitudInicial.datosSolicitudUE.fechaCI > systemDate)
                    {
                        listaErrroresSolicitudConcesionAcuicultura.Add("La Fecha de CI no debe ser mayor a la fecha de hoy.");
                    }
                }


                /* El Número Identificador Solicitud ingresado no debe estar previamente en el sistema */
                bool existePERT = solicitudDA.aplicaPertExistente(solicitudInicial.numPert);
                if (existePERT)
                {
                    listaErrroresSolicitudConcesionAcuicultura.Add("El Número Identificador Solicitud ya se encuentra ingresado en el sistema.");
                }

            }

            return listaErrroresSolicitudConcesionAcuicultura;
        }

        public List<string> validaCodigoCentroAcuiculturaAmerb(String codigoAcuiculturaAmerb)
        {
            List<String> listaErrroresSolicitudAcuiculturaAmerb = new List<String>();

            if (codigoAcuiculturaAmerb != null && !codigoAcuiculturaAmerb.Equals(""))
            {
                SolicitudConcesion concesion = solicitudDA.Obtiene_UE_Existente(codigoAcuiculturaAmerb, rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB);

                if (concesion == null)
                {
                    listaErrroresSolicitudAcuiculturaAmerb.Add("El Código de Acuicultura en Amerb ingresado no existe en el sistema.");
                }
            }

            return listaErrroresSolicitudAcuiculturaAmerb;
        }

        public List<string> validaAmerbPadre(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudAcuiculturaAmerb = new List<String>();
            return listaErrroresSolicitudAcuiculturaAmerb;
        }

        public List<string> validaECMPOPadre(Datos.Entidades.DetalleDatosSolicitud detalleDatosSolicitud)
        {
            List<String> listaErrroresSolicitudAcuiculturaAmerb = new List<String>();
            return listaErrroresSolicitudAcuiculturaAmerb;
        }
    }
}
