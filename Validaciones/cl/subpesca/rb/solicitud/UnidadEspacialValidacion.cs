using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos.Entidades.Relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.relocalizacion;
using LogicaNegocio.cl.subpesca.rb.servicios.solicitudes.unidadEspacial;
using Datos.Contantes;

namespace Validaciones.cl.subpesca.rb.solicitud
{
    public class UnidadEspacialValidacion
    {


        UnidadEspacialService unidadEspacialService = new UnidadEspacialService();


        #region VALIDA SOLICITUD UNIDAD ESPACIAL 


       
        //public List<string> validaUnidadEspacial(Datos.Entidades.UnidadEspacial unidadEspacial)
        //{
        //    List<String> listaErroresUnidadEspacial = new List<String>();
        //    if (unidadEspacial != null)
        //    {

        //        if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
        //        {

        //            bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

        //            if (codigoCentroUsado)
        //            {
        //                listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
        //            }
        //        }


        //        DateTime systemDate = DateTime.Now;

        //        /* Fecha de Diario Oficial no debe ser mayor a la fecha del día de hoy */
        //        if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial > systemDate)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser mayor a la fecha de hoy.");
        //        }

        //        /* Fecha de Diario Oficial no debe ser menor a 1930 */
        //        DateTime fechaMinima = new DateTime(1930, 01, 01);
        //        if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial < fechaMinima)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser menor a 1930.");
        //        }

        //        /* Fecha de Acta de Entrega no debe ser mayor a la fecha del día de hoy */
        //        if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser mayor a la fecha de hoy.");
        //        }

        //        /* Fecha de Diario Oficial no debe ser menor a 1930 */
        //        DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
        //        if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser menor a 1930.");
        //        }

        //        /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
        //        if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
        //        {
        //            listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
        //        }

        //        /* Plazo de Inicio no debe ser menor a 1930 */
        //        DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
        //        if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
        //        {
        //            listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
        //        }

        //        /* Plazo de Vencimiento no debe ser mayor a la fecha del día de hoy */
        //        if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento > systemDate)
        //        {
        //            listaErroresUnidadEspacial.Add("El Plazo de Vencimiento no debe ser mayor a la fecha de hoy.");
        //        }

        //        /* Plazo de Inicio no debe ser menor a 1930 */
        //        DateTime fechaMinimaPlazoVencimiento = new DateTime(1930, 01, 01);
        //        if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < fechaMinimaPlazoVencimiento)
        //        {
        //            listaErroresUnidadEspacial.Add("El Plazo de Vencimiento no debe ser menor a 1930.");
        //        }

        //        /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
        //        if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
        //            unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
        //        {
        //            listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
        //        }

        //    }
        //    return listaErroresUnidadEspacial;
        //}
        


      
        //public List<string> validaCreacionConcesion(Datos.Entidades.UnidadEspacial unidadEspacial)
        //{
        //    List<String> listaErroresUnidadEspacial = new List<String>();
        //    if (unidadEspacial != null)
        //    {

        //        if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
        //        {

        //            bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

        //            if (codigoCentroUsado)
        //            {
        //                listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
        //            }
        //        }


        //        DateTime systemDate = DateTime.Now;

        //        /* Fecha de Diario Oficial no debe ser mayor a la fecha del día de hoy */
        //        if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial > systemDate)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser mayor a la fecha de hoy.");
        //        }

        //        /* Fecha de Diario Oficial no debe ser menor a 1930 */
        //        DateTime fechaMinima = new DateTime(1930, 01, 01);
        //        if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial < fechaMinima)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser menor a 1930.");
        //        }

        //        /* Fecha de Acta de Entrega no debe ser mayor a la fecha del día de hoy */
        //        if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
        //        {
        //            listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser mayor a la fecha de hoy.");
        //        }

        //    }
        //    return listaErroresUnidadEspacial;

        //}
     


        /*
         * ESTE METODO ES TAMBIEN USADO EN LAS UNIDADES ESPACIALES PARA VALIDAR EL FORMULARIO, EXCEPTO PARA UE AMERB Y UE EXPERIMENTALES
         **/
        public List<string> validaUnidadEspacialConcesion(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();
            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha de Diario Oficial no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Diario Oficial no debe ser menor a 1930 */
                DateTime fechaMinima = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial < fechaMinima)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser menor a 1930.");
                }

                /* Fecha de Acta de Entrega no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Diario Oficial no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }
            return listaErroresUnidadEspacial;
        }
       


        public List<string> validaUnidadEspacialExperimentalesConcesion(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha Web Resolución Pesca no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha Web Resolución Pesca no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }

            return listaErroresUnidadEspacial;
        }


        public List<string> validaUnidadEspacialModificacion(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();
            if (unidadEspacial != null)
            {
                DateTime systemDate = DateTime.Now;

                /* Fecha de Diario Oficial no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Diario Oficial no debe ser menor a 1930 */
                DateTime fechaMinima = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial < fechaMinima)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser menor a 1930.");
                }

                /* Para poder hacer efectiva la solicitud de modificación la solicitud debe tener la resol. de ssp aprobada. */
                bool tieneResolSSP = true;
                if (!tieneResolSSP)
                {
                    listaErroresUnidadEspacial.Add("No puede modificar la concesión si aún no posee resolución de ssp aprobada.");
                }
            }
            return listaErroresUnidadEspacial;
        }

        
        public List<string> validaUnidadEspacialRelocalizacion(Datos.Entidades.UnidadEspacial unidadEspacial, int idTramite, int idTipoRelocalizacion)
        {

            RelocalizacionService relocalizacionService = new RelocalizacionService();

            List<String> listaErroresUnidadEspacial = new List<String>();
            if (unidadEspacial != null)
            {
                DateTime systemDate = DateTime.Now;

                //VALIDA QUE EL CODIGO DEL CENTRO NO ESTE YA COMO UNA UNIDAD ESPECIAL, ESTO APLICA SOLO PARA RELOCALIZACION CREA (YA NO SE OCUPARA, SI EL CODIGO YA EXISTIA SE REEMPLAZARA LA INFORMACION)
                if ((idTipoRelocalizacion == rbTipo.RELOCALIZACION_CREA_RESA || idTipoRelocalizacion == rbTipo.RELOCALIZACION_CREA) && unidadEspacial.centrosDeCultivo != null && unidadEspacial.centrosDeCultivo.codigoCentro != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                {
                    //bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, 0);

                    //if (codigoCentroUsado)
                    //{
                    //    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                    //}
                }
                if ((idTipoRelocalizacion == rbTipo.RELOCALIZACION_CREA_RESA || idTipoRelocalizacion == rbTipo.RELOCALIZACION_CREA) && (unidadEspacial.centrosDeCultivo == null || unidadEspacial.centrosDeCultivo.codigoCentro == null || unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals("")))
                {
                    listaErroresUnidadEspacial.Add("Ingrese código de centro.");
                }


                /* Fecha de Diario Oficial no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Acta de Entrega no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser mayor a la fecha de hoy.");
                }

            }
            return listaErroresUnidadEspacial;
        }

        
        public List<string> validaRelocalizacion(Datos.Entidades.UnidadEspacial unidadEspacial, int idTramite, int idTipoRelocalizacion)
        {

            RelocalizacionService relocalizacionService = new RelocalizacionService();

            List<String> listaErroresUnidadEspacial = new List<String>();
            if (unidadEspacial != null)
            {
                DateTime systemDate = DateTime.Now;

                //VALIDA QUE EL CODIGO DEL CENTRO NO ESTE YA COMO UNA UNIDAD ESPECIAL, ESTO APLICA SOLO PARA RELOCALIZACION CREA (YA NO SE OCUPARA, SI EL CODIGO YA EXISTIA SE REEMPLAZARA LA INFORMACION)
                if ((idTipoRelocalizacion == rbTipo.RELOCALIZACION_CREA_RESA || idTipoRelocalizacion == rbTipo.RELOCALIZACION_CREA) && unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
                {
                    //bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, 0);

                    //if (codigoCentroUsado)
                    //{
                    //    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                    //}
                }


                /* Fecha de Diario Oficial no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaDiarioOficial != default(DateTime) && unidadEspacial.fechaDiarioOficial > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Diario Oficial no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha de Acta de Entrega no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("La Fecha de Acta de Entrega no debe ser mayor a la fecha de hoy.");
                }


                if (!relocalizacionService.Tramite_resolucion_SSpCompleto(idTramite))
                {
                    listaErroresUnidadEspacial.Add("Todos los sectores deben tener resolución SSP vigente asociada para realizar la accion solicitada");
                }


            }
            return listaErroresUnidadEspacial;
        }

       
        public List<string> validaUnidadEspacialFaenamiento(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }
            else
            {
                listaErroresUnidadEspacial.Add("Debe ingresar código de centro para la creación de la unidad espacial.");

            }

            return listaErroresUnidadEspacial;
        }


        public List<string> validaUnidadEspacialAcopio(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            return listaErroresUnidadEspacial;
        }


        public List<string> validaUnidadEspacialECMPO(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }
            else
            {
                listaErroresUnidadEspacial.Add("Debe ingresar código de centro para la creación de la unidad espacial.");

            }

            return listaErroresUnidadEspacial;
        }


        public List<string> validaUnidadEspacialAmerb(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }
            else
            {
                listaErroresUnidadEspacial.Add("Debe ingresar código de centro para la creación de la unidad espacial.");

            }


            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha Web Resolución Pesca no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha Web Resolución Pesca no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }

            return listaErroresUnidadEspacial;
        }

       
        public List<string> validaUnidadEspacialExperimentalesAmerb(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial.centrosDeCultivo != null && !unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(unidadEspacial.centrosDeCultivo.codigoCentro, unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha Web Resolución Pesca no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha Web Resolución Pesca no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }

            return listaErroresUnidadEspacial;
        }


        public List<string> validaUnidadEspacialColector(Datos.Entidades.UnidadEspacial unidadEspacial)
        {


            List<String> listaErroresUnidadEspacial = new List<String>();
           

            //LA VALIDACION SE HACE EN LA INTERFAZ

            if (unidadEspacial != null)
            {
                
                
                //DateTime systemDate = DateTime.Now;

                
                //if (unidadEspacial.fechaInicioPerAut == default(DateTime))
                //{
                //    listaErroresUnidadEspacial.Add("Ingrese Fecha Inicio Periodo Autorizado.");
                //}

                //if (unidadEspacial.mesesAut < 1)
                //{
                //    listaErroresUnidadEspacial.Add("Ingrese Cantidad de Meses Autorizados");
                //}

                

                //if (unidadEspacial.plazoInicio == default(DateTime))
                //{

                //    listaErroresUnidadEspacial.Add("Ingrese Fecha para Plazo de Inicio.");
                //}

                //if (unidadEspacial.numPlazo < 1)
                //{
                //    listaErroresUnidadEspacial.Add("Ingrese Nº de Mes/Año.");
                //}

            }
           

            return listaErroresUnidadEspacial;
        }



        #endregion



        #region VALIDACION DE CREACION DE UNIDADES ESPACIALES


        public List<string> validaCreacionConcesionAcuicultura(Datos.Entidades.SolicitudConcesion ConcesionAcuicultura)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (ConcesionAcuicultura.unidadEspacial.centrosDeCultivo != null && !ConcesionAcuicultura.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(ConcesionAcuicultura.unidadEspacial.centrosDeCultivo.codigoCentro, ConcesionAcuicultura.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            return listaErroresUnidadEspacial;

        }


        public List<string> validaCreacionExperimentalesConcesion(Datos.Entidades.SolicitudConcesion ExperimentalConcesion)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (ExperimentalConcesion.unidadEspacial.centrosDeCultivo != null && !ExperimentalConcesion.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(ExperimentalConcesion.unidadEspacial.centrosDeCultivo.codigoCentro, ExperimentalConcesion.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            return listaErroresUnidadEspacial;
        }


        public List<string> validaCreacionConcesionFaenamiento(Datos.Entidades.SolicitudConcesion CentroFaenamiento)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();

            if (CentroFaenamiento.unidadEspacial.centrosDeCultivo != null && !CentroFaenamiento.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(CentroFaenamiento.unidadEspacial.centrosDeCultivo.codigoCentro, CentroFaenamiento.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            return listaErroresUnidadEspacial;

        }


        public List<string> validaCreacionConcesionAcopio(Datos.Entidades.SolicitudConcesion CentroAcopio)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (CentroAcopio.unidadEspacial.centrosDeCultivo != null && !CentroAcopio.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(CentroAcopio.unidadEspacial.centrosDeCultivo.codigoCentro, CentroAcopio.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            return listaErroresUnidadEspacial;

        }


        public List<string> validaCreacionECMPO(Datos.Entidades.SolicitudConcesion AcuiculturaECMPO)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (AcuiculturaECMPO.unidadEspacial.centrosDeCultivo != null && !AcuiculturaECMPO.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(AcuiculturaECMPO.unidadEspacial.centrosDeCultivo.codigoCentro, AcuiculturaECMPO.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }
            else
            {
                listaErroresUnidadEspacial.Add("Debe ingresar código de centro para la creación de la unidad espacial.");

            }

            return listaErroresUnidadEspacial;
        }


        public List<string> validaCreacionConcesionAmerb(Datos.Entidades.SolicitudConcesion AcuiculturaAmerb)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (AcuiculturaAmerb.unidadEspacial.centrosDeCultivo != null && !AcuiculturaAmerb.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(AcuiculturaAmerb.unidadEspacial.centrosDeCultivo.codigoCentro, AcuiculturaAmerb.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }
            else
            {
                listaErroresUnidadEspacial.Add("Debe ingresar código de centro para la creación de la unidad espacial.");

            }

            return listaErroresUnidadEspacial;
        }


        public List<string> validaCreacionExperimentalesAmerb(Datos.Entidades.SolicitudConcesion ExperimentalAmerb)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (ExperimentalAmerb.unidadEspacial.centrosDeCultivo != null && !ExperimentalAmerb.unidadEspacial.centrosDeCultivo.codigoCentro.Trim().Equals(""))
            {

                bool codigoCentroUsado = unidadEspacialService.ValidaCentroUnidadesEspaciales(ExperimentalAmerb.unidadEspacial.centrosDeCultivo.codigoCentro, ExperimentalAmerb.unidadEspacial.idUnidadEspacial);

                if (codigoCentroUsado)
                {
                    listaErroresUnidadEspacial.Add("El código de centro ya ha sido ocupada en otra unidad espacial.");
                }
            }


            return listaErroresUnidadEspacial;
        }


        public List<string> validaCreacionConcesionColector(Datos.Entidades.SolicitudConcesion ColectoresSemillas)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();

            //NO TIENE CODIGO SIEP

            return listaErroresUnidadEspacial;
        }


        #endregion



        #region VALIDACION UNIDADES ESPACIALES CREADAS

        public List<string> validaUnidadEspacialAmerbCreada(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha Web Resolución Pesca no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha Web Resolución Pesca no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }

            return listaErroresUnidadEspacial;
        }

        public List<string> validaUnidadEspacialExperimentalesConcesionCreada(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha Web Resolución Pesca no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha Web Resolución Pesca no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }

            return listaErroresUnidadEspacial;
        }

        public List<string> validaUnidadEspacialExperimentalesAmerbCreada(Datos.Entidades.UnidadEspacial unidadEspacial)
        {
            List<String> listaErroresUnidadEspacial = new List<String>();


            if (unidadEspacial != null)
            {

                DateTime systemDate = DateTime.Now;

                /* Fecha Web Resolución Pesca no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega > systemDate)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser mayor a la fecha de hoy.");
                }

                /* Fecha Web Resolución Pesca no debe ser menor a 1930 */
                DateTime fechaMinimaActaEntrega = new DateTime(1930, 01, 01);
                if (unidadEspacial.fechaActaEntrega != default(DateTime) && unidadEspacial.fechaActaEntrega < fechaMinimaActaEntrega)
                {
                    listaErroresUnidadEspacial.Add("Fecha Web Resolución Pesca no debe ser menor a 1930.");
                }

                /* Plazo de Inicio no debe ser mayor a la fecha del día de hoy */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio > systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser mayor a la fecha de hoy.");
                }

                /* Plazo de Inicio no debe ser menor a 1930 */
                DateTime fechaMinimaPlazoInicio = new DateTime(1930, 01, 01);
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoInicio < fechaMinimaPlazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser menor a 1930.");
                }

                /* Plazo de Vencimiento no debe ser menor a la fecha de hoy */
                if (unidadEspacial.plazoVencimiento != default(DateTime) && unidadEspacial.plazoVencimiento < systemDate)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Vencimiento debe ser mayor a la fecha de hoy.");
                }

                /* El Plazo de Inicio no debe ser mayor al Plazo de Vencimiento */
                if (unidadEspacial.plazoInicio != default(DateTime) && unidadEspacial.plazoVencimiento != default(DateTime) &&
                    unidadEspacial.plazoVencimiento < unidadEspacial.plazoInicio)
                {
                    listaErroresUnidadEspacial.Add("El Plazo de Inicio no debe ser superior al Plazo de Vencimiento.");
                }

            }

            return listaErroresUnidadEspacial;
        }


        #endregion

    }
}
