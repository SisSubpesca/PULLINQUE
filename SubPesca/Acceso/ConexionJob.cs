using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using Subpesca.Jobs;
using SubPesca.Jobs;


namespace Subpesca.Acceso
{
    public class ConexionJob
    {
        
        /**
         * (4) Se debe enviar un correo electrónico cuando un requerimiento que influya en el cambio de estado se encuentre vencido. 
         */
        public void ejecutarRequerimientoCambioEstadoVenc(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<RequerimientoVencidoJob>()
                .WithIdentity("RequerimientoVencidoJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerRequerimientoVencido", "Grupo")
                .WithCronSchedule(CronSchedule.RequerimientoCambioEstadoVenc)
                .StartAt(DateTime.UtcNow)
                .Build();
                 

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("RequerimientoVencidoJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta ejecutarRequerimientoCambioEstadoVenc ejecutada!");
        }

        /**
         * (6) Se debe generar un correo electrónico, si un pert luego de un año no ha tenido movimiento
         * (que no se ha ingresado el informe de cartografía), además este debe ser desplegado  en
         * el reporte de plazos vencidos.
         */
        public void ejecutarRequerimientoSinMov(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<RequerimientoSinMovJob>()
                .WithIdentity("RequerimientoSinMovJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerRequerimientoSinMov", "Grupo")
                .WithCronSchedule(CronSchedule.RequerimientoSinMov)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("RequerimientoSinMovJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta RequerimientoSinMov");
        }

        /**
         * (8) Se debe generar un correo electrónico que indique que si luego de 30 días la carpeta no ha sido
         *  asignado (este cirrei deve ser enviado al coordinador territorial).
         */
        public void ejecutarCarpetaNoAsignada(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<CarpetaNoAsignadaJob>()
                .WithIdentity("CarpetaNoAsignadaJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerCarpetaNoAsignada", "Grupo")
                .WithCronSchedule(CronSchedule.CarpetaNoAsignada)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("CarpetaNoAsignadaJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta CarpetaNoAsignada");
        }
        
        /**
         *  (10) Se debe enviar un correo electrónico cuando la solicitud cambie de estado, a un estado que
         *  tiene plazo.
         */
        public void ejecutarSolicitudConPlazo(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<SolicitudConPlazoJob>()
                .WithIdentity("SolicitudConPlazoJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerSolicitudConPlazo", "Grupo")
                .WithCronSchedule(CronSchedule.SolicitudConPlazo)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("SolicitudConPlazoJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SolicitudConPlazo");
        }

        /**
         * (19) Se debe enviar correo electronico cuando la solicitud cambie de estado, a un estado
         * que tiene plazo.
         */
        public void ejecutarRequerimientoVencido(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<RequerimientoVencidoJob>()
                .WithIdentity("RequerimientoVencidoJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerRequerimientoVencido", "Grupo")
                .WithCronSchedule(CronSchedule.RequerimientoVencido)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("RequerimientoVencidoJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta RequerimientoVencido");
        }
        
        /**
         * (23) Se debe enviar un correo electrónico cuando transcurra los dos meses de la publicación radial. 
         */
        public void ejecutarAvisoPublicacionRadial(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<AvisoPublicacionRadialJob>()
                .WithIdentity("AvisoPublicacionRadialJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerAvisoPublicacionRadial", "Grupo")
                .WithCronSchedule(CronSchedule.AvisoPublicacionRadial)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("AvisoPublicacionRadialJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            Console.WriteLine("Alerta AvisoPublicacionRadial");
        }

        public void ejecutarSectorRelocalizacionRechazado(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<SectorRelocalizacionRechazadoJob>()
                .WithIdentity("SectorRelocalizacionRechazadoJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerSectorRelocalizacionRechazadoJob", "Grupo")
                .WithCronSchedule(CronSchedule.SectorRelocalizacionRechazado)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("SectorRelocalizacionRechazadoJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }


        //public void ejecutarSinCertificadoCapitaniaPuerto(IScheduler scheduler)
        //{

        //    IJobDetail primerJob = JobBuilder.Create<SinCertificadoCapitaniaPuertoJob>()
        //        .WithIdentity("SinCertificadoCapitaniaPuertoJob", "Grupo")
        //        .RequestRecovery()
        //        .Build();

        //    // Crea un trigger de tipo CRON que es un formato utilizado en
        //    // UNIX en el que se puede programar una tarea. Una herramienta
        //    // para crear este tipo de expresiones es:
        //    // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
        //    ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
        //        .WithIdentity("TriggerSinCertificadoCapitaniaPuertoJob", "Grupo")
        //        .WithCronSchedule(CronSchedule.SinCertificadoCapitaniaPuerto)
        //        .StartAt(DateTime.UtcNow)
        //        .Build();

        //    // En un entorno real pueden existir varios Jobs ejecutandose
        //    // para evitar problemas verificamos si nuestro proceso ya
        //    // existe en el programador de tares
        //    JobKey jobKey = new JobKey("SinCertificadoCapitaniaPuertoJob", "Grupo");
        //    if (scheduler.CheckExists(jobKey))
        //    {
        //        scheduler.DeleteJob(jobKey);
        //    }

        //    // Ahora se puede programar cada cierto tiempo
        //    // establecido en el trigger que se ejecute nuestro primer job
        //    scheduler.ScheduleJob(primerJob, cronTrigger);
        //    //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        //}



        public void ejecutarUnidadDependenciaCambiaEstado(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<UnidadDependenciaCambiaEstadoJob>()
                .WithIdentity("UnidadDependenciaCambiaEstadoJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerUnidadDependenciaCambiaEstadoJob", "Grupo")
                .WithCronSchedule(CronSchedule.UnidadDependenciaCambiaEstado)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("UnidadDependenciaCambiaEstadoJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }



        public void ejecutarVencimientoUE(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<VencimientoUEJob>()
                .WithIdentity("VencimientoUEJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerVencimientoUEJob", "Grupo")
                .WithCronSchedule(CronSchedule.VencimientoUE)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("VencimientoUEJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }

        public void supeditadaTerminadaRechazada(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<SupeditadaTerminadaRechazadaJob>()
                .WithIdentity("SupeditadaTerminadaRechazadaJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TriggerSupeditadaTerminadaRechazadaJob", "Grupo")
                .WithCronSchedule(CronSchedule.SupeditadaTerminadaRechazada)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("SupeditadaTerminadaRechazadaJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }


        public void supeditadaTerminadaAprobada(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<SupeditadaTerminadaAprobadaJob>()
                .WithIdentity("SupeditadaTerminadaAprobadaJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("SupeditadaTerminadaAprobadaJob", "Grupo")
                .WithCronSchedule(CronSchedule.SupeditadaTerminadaAprobada)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("SupeditadaTerminadaAprobadaJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }


        public void titularesPendientesCreacion(IScheduler scheduler)
        {

            IJobDetail primerJob = JobBuilder.Create<TitularesPendientesCreacionJob>()
                .WithIdentity("TitularesPendientesCreacionJob", "Grupo")
                .RequestRecovery()
                .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TitularesPendientesCreacionJob", "Grupo")
                .WithCronSchedule(CronSchedule.TitularesPendientesCreacionJob)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("TitularesPendientesCreacionJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }

        //job el cual actualiza los datos de la tabla y modifica los datos que ya estan corregidos
        public void TitularesPendientesCreacionActualizacion(IScheduler scheduler)
        {
            IJobDetail primerJob = JobBuilder.Create<TitularesPendientesCreacionJob>()
               .WithIdentity("TitularesPendientesCreacionActualizacionJob", "Grupo")
               .RequestRecovery()
               .Build();

            // Crea un trigger de tipo CRON que es un formato utilizado en
            // UNIX en el que se puede programar una tarea. Una herramienta
            // para crear este tipo de expresiones es:
            // La expresion "0 0/1 * 1/1 * ? *" significa que se ejecutara cada min
            ICronTrigger cronTrigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity("TitularesPendientesCreacionActualizacionJob", "Grupo")
                .WithCronSchedule(CronSchedule.TitularesPendientesCreacionActualizacionJob)
                .StartAt(DateTime.UtcNow)
                .Build();

            // En un entorno real pueden existir varios Jobs ejecutandose
            // para evitar problemas verificamos si nuestro proceso ya
            // existe en el programador de tares
            JobKey jobKey = new JobKey("TitularesPendientesCreacionActualizacionJob", "Grupo");
            if (scheduler.CheckExists(jobKey))
            {
                scheduler.DeleteJob(jobKey);
            }

            // Ahora se puede programar cada cierto tiempo
            // establecido en el trigger que se ejecute nuestro primer job
            scheduler.ScheduleJob(primerJob, cronTrigger);
            //Console.WriteLine("Alerta SectorRelocalizacionRechazado");
        }
    }
}
