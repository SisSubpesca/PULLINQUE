using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Collections;
using System.Transactions;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using Datos.Utilidades;
using Datos.Contantes;
using LogicaNegocio.cl.subpesca.rb.solicitud;
using LogicaNegocio.cl.subpesca.rb.relocalizacion;
using Datos.Entidades.Relocalizacion;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Web;
using LogicaNegocio.cl.subpesca.rb.usuario;
using LogicaNegocio.cl.subpesca.rb.common;
using LogicaNegocio.cl.subpesca.rb.modificacion;





namespace LogicaNegocio.cl.subpesca.rb.servicios.correosEelectronicos
{
    public class EnviarCorreo
    {

        //private string host = "smtp.gmail.com";
        //private int port = 25;
        //private bool enableSSL = true;
        //private bool UseDefaultCredentials = false;
        //private string user = "rioblanco.dos@gmail.com";
        //private string password = "pesca2013";

        Logger logger = new Logger();

        TemplateAvisoDA templateAvisoDA = new TemplateAvisoDA();
        AvisoDA avisoDA = new AvisoDA();

        SolicitudDA solicitudDA = new SolicitudDA();
        SolicitanteDA solicitanteDA = new SolicitanteDA();
        TramiteRelocalizacionDA tramiteRelocalizacionDA = new TramiteRelocalizacionDA();
        DetalleSectorDA detalleSectorDA = new DetalleSectorDA();
        UsuarioDA usuarioDA = new UsuarioDA();
        TipoDA tipoDA = new TipoDA();
        UnidadDependenciaModDA unidadDependenciaModDA = new UnidadDependenciaModDA();
        GrupoSuspendidoDA grupoSuspendidoDA = new GrupoSuspendidoDA();

        Funciones funciones = new Funciones();



        /**
         *  Método para configura el destinarario y el cuerpo del correo electrónico.
         */
        public bool senEmailServiceAlerta(int idSolicitud, string claveTemplate, string subject, bool isHtml, Hashtable reemplazos, string mensaje, string correoDestinatario)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    ArrayList listDestinatarios = new ArrayList();

                    if (correoDestinatario != null)
                    {
                        if (correoDestinatario.Contains(","))
                        {
                            String[] correos = correoDestinatario.Split(',');

                            if (correos.Length > 0)
                            {

                                for (int i = 0; i < correos.Length; i++)
                                {
                                    listDestinatarios.Add(correos[i]);
                                }
                            }
                        }
                        else
                        {
                            listDestinatarios.Add(correoDestinatario);
                        }
                    }

                    this.sendEmailService(listDestinatarios, subject, isHtml, mensaje, reemplazos, idSolicitud, claveTemplate);

                    transactionScope.Complete();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Método que envía el correo electrónico. 
         */
        public bool sendEmailService(ArrayList to, string subject, bool isHtml, string mensaje, Hashtable reemplazos, int idSolicitud, string claveTemplate)
        {

            using (TransactionScope transactionScope = new TransactionScope())
            {

                try
                {
                    if (to != null && to.Count > 0 && subject != null && mensaje != null)
                    {
                        MailMessage email = new MailMessage();

                        foreach (string destinatario in to)
                        {
                            email.To.Add(new MailAddress(destinatario));
                        }

                        //email.From = new MailAddress(this.user); //TOMARA LA INFORMACION DEL ARCHIVO DE CONFIGURACION
                        email.Subject = subject;
                        email.IsBodyHtml = isHtml;

                        String cuerpoMensaje = "";
                        if (isHtml)
                        {
                            cuerpoMensaje = UtilMail.parseMensaje(mensaje, reemplazos);
                            email.BodyEncoding = Encoding.UTF8;
                            email.Body = cuerpoMensaje;
                        }
                        else
                        {
                            cuerpoMensaje = UtilMail.parseMensaje(mensaje, reemplazos);
                            email.Body = cuerpoMensaje;

                        }

                        SmtpClient smtp = new SmtpClient(); //TOMARA LA INFORMACION DEL ARCHIVO DE CONFIGURACION
                        //smtp.Host = this.host;
                        //smtp.Port = this.port;
                        //smtp.EnableSsl = this.enableSSL;
                        //smtp.UseDefaultCredentials = this.UseDefaultCredentials;
                        //smtp.Credentials = new NetworkCredential(this.user, this.password);

                        smtp.Send(email);
                        email.Dispose();


                        if (claveTemplate != null && claveTemplate.Equals(clavesTemplateAviso.HECTAREAS_UTILIZADAS_RELOCALIZACION))
                        {
                            //EN ESTE CASO NO HAY ID DE SOLICITUD, SOLO ID DE TRAMITE, SI SE GUARDA GENERARA ERROR DE CLAVE FORANEA
                        }
                        else
                        {
                            this.guardarEnvioAlerta(idSolicitud, claveTemplate, subject, cuerpoMensaje, to);
                        }

                    }

                    transactionScope.Complete();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Guarda una copia del correo electrónico en la base de datos y los destinatarios a los que fue enviado. 
         */
        private bool guardarEnvioAlerta(int idSolicitud, string claveTemplate, string subject, string mensaje, ArrayList to)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {

                    Aviso aviso = new Aviso();
                    aviso.solicitud = new SolicitudConcesion();
                    aviso.solicitud.idSolConcesion = idSolicitud;
                    aviso.claveTemplate = claveTemplate;
                    aviso.subject = subject;
                    aviso.mensaje = mensaje;

                    /* Guardar Aviso enviado */
                    if (!avisoDA.GuardarAviso(aviso))
                    {
                        return false;
                    }

                    foreach (string destinatario in to)
                    {
                        /* Guardar Destinatarios a los que fue enviado */
                        if (!avisoDA.GuardarDestinatariosAviso(aviso.idAviso, destinatario))
                        {
                            return false;
                        }
                    }


                    transactionScope.Complete();
                    return true;
                }

                catch (Exception ex)
                {
                    logger.PrintError(ex);
                    logger.SendMailError(ex);
                    return false;
                }
            }
        }

        /**
         * Envío de Correo Electrónico para Informe DAC
         * para el tipo salida y los temas visación de it dac, corrección it dac y it dac firmada por jefatura.
         */
        public void envioCorreoElectronicoInformeDAC(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                //Sección Informe DAC es rbSeccion.rbSeccion.INFORME_DAC
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_IT_DAC)
                            {

                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_IT_DAC;
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (listaSolicitante != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                            else
                            {

                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_IT_DAC)
                                {
                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_DE_IT_DAC;
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                    if (listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }
                                else
                                {

                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.IT_DAC_FIRMADO_POR_JEFATURA)
                                    {
                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.IT_DAC_PARA_FIRMA_JEFATURA;
                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                        if (listaSolicitante != null)
                                        {
                                            Hashtable reemplazos = new Hashtable();
                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                        }
                                    }
                                    else
                                    {
                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_IT_DAC_UTS)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_IT_DAC_UTS;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (listaSolicitante != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                                reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                            }
                                        }
                                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_IT_DAC_UTS)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_IT_DAC_UTS;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (listaSolicitante != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));


                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


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

        /**
         * Envío de Correo Electrónico para Planos
         * para el tipo salida y los temas visación carta plano, visacion carta corrige plano, carta plano firmada por jefatura,
         * carta corrige plano firmada por jefatura, correccion de carta plano, correccion carta plano, correccion de carta corrige plano,
         * carta corrige plano firmada por jefatura.
         */
        public void envioCorreoElectronicoPlanos(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                //Sección Planos es rbSeccion.rbSeccion.PLANOS
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_PLANO)
                            {
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_PLANO;
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null && listaSolicitante != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                            else
                            {

                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_CORRIGE_PLANO)
                                {
                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_CORRIGE_PLANO;
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                    }
                                }
                                else
                                {

                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_PLANO_FIRMADA_POR_JEFATURA)
                                    {
                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_PLANO_PARA_FIRMA_JEFATURA;
                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                        if (templateAvisoFiltro != null && listaSolicitante != null)
                                        {
                                            Hashtable reemplazos = new Hashtable();
                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                        }
                                    }
                                    else
                                    {
                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_CORRIGE_PLANO_FIRMADA_POR_JEFATURA)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_CORRIGE_PLANO_PARA_FIRMA_JEFATURA;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (templateAvisoFiltro != null && listaSolicitante != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                                reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                            }
                                        }
                                        else
                                        {

                                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_DE_CARTA_PLANO)
                                            {
                                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_PLANO;
                                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                if (templateAvisoFiltro != null && listaSolicitante != null)
                                                {
                                                    Hashtable reemplazos = new Hashtable();
                                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                }
                                            }
                                            else
                                            {
                                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_DE_CARTA_CORRIGE_PLANO)
                                                {
                                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_DE_CARTA_CORRIGE_PLANO;
                                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                                    {
                                                        Hashtable reemplazos = new Hashtable();
                                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                    }
                                                }
                                                else
                                                {
                                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECION_CARTA_PLANOS_UOT)
                                                    {
                                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECION_CARTA_PLANOS_UOT;
                                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                        if (templateAvisoFiltro != null && listaSolicitante != null)
                                                        {
                                                            Hashtable reemplazos = new Hashtable();
                                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_PLANOS_UTS)
                                                        {

                                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_PLANOS_UTS;
                                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                            if (templateAvisoFiltro != null && listaSolicitante != null)
                                                            {
                                                                Hashtable reemplazos = new Hashtable();
                                                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_CORRIGE_PLANOS)
                                                            {
                                                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_CORRIGE_PLANOS;
                                                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                {
                                                                    Hashtable reemplazos = new Hashtable();
                                                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_PLANOS_UTS)
                                                                {
                                                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_PLANOS_UTS;
                                                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                    {
                                                                        Hashtable reemplazos = new Hashtable();
                                                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                    }
                                                                }
                                                                else
                                                                {

                                                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_CORRIGE_PLANOS_UTS)
                                                                    {
                                                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_CORRIGE_PLANOS_UTS;
                                                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                        if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                        {
                                                                            Hashtable reemplazos = new Hashtable();
                                                                            reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                            reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                            reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_PLANOS_FIRMA_JEFATURA)
                                                                        {
                                                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_PLANOS_FIRMA_JEFATURA;
                                                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                            if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                            {
                                                                                Hashtable reemplazos = new Hashtable();
                                                                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                                                                reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                                                                            }

                                                                        }
                                                                        else
                                                                        {
                                                                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_CORRIGE_PLANOS_FIRMA_JEFATURA)
                                                                            {
                                                                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_CORRIGE_PLANOS_FIRMA_JEFATURA;
                                                                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                                if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                                {
                                                                                    Hashtable reemplazos = new Hashtable();
                                                                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                                                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ESPERA_REVISION_PLANOS)
                                                                                {
                                                                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ESPERA_REVISION_PLANOS;
                                                                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                                    {
                                                                                        Hashtable reemplazos = new Hashtable();
                                                                                        reemplazos.Add("[REEM_1]", clavesTemplateAviso.ESPERA_REVISION_PLANOS);
                                                                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                                                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_OFICIO_VISACION_PLANOS)
                                                                                    {
                                                                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_OFICIO_VISACION_PLANOS;
                                                                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                                        if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                                        {
                                                                                            Hashtable reemplazos = new Hashtable();
                                                                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                                                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                                        }

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_OFICIO_VISACION_PLANOS)
                                                                                        {
                                                                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_OFICIO_VISACION_PLANOS;
                                                                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                                            if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                                            {
                                                                                                Hashtable reemplazos = new Hashtable();
                                                                                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                                                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                                            }

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.OFICIO_VISACION_PLANOS_FIRMA_JEFATURA)
                                                                                            {
                                                                                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.OFICIO_VISACION_PLANOS_FIRMA_JEFATURA;
                                                                                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                                                if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                                                {
                                                                                                    Hashtable reemplazos = new Hashtable();
                                                                                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                                                                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                                                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                                                }

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_CORRIGE_PLANOS_UOT)
                                                                                                {
                                                                                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_CORRIGE_PLANOS_UOT;
                                                                                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                                                                                    {
                                                                                                        Hashtable reemplazos = new Hashtable();
                                                                                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                                                                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


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
            }
            else
            {
                if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
                {
                    //Los recursos de reposición se ingresan a través de la sección Resol SSP.

                    //if (requerimiento.ambitoTipo != null)
                    //{
                    //    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    //    {
                    //        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RECURSO_DE_REPOSICION)
                    //        {
                    //            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    //            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                    //            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                    //            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RECURSO_DE_REPOSICION_PLANOS;

                    //            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    //            if (templateAvisoFiltro != null)
                    //            {
                    //                Hashtable reemplazos = new Hashtable();
                    //                reemplazos.Add("[REEM_1]", requerimiento.numero + "-" + requerimiento.fecha);

                    //                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                    //                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    //            }
                    //        }
                    //    }
                    //}

                }
                else
                {
                    this.alertaRequerimientoConRespuesta(requerimiento);
                }
            }
        }

        /**
         * Envío de Correo Electrónico para Informe Carografía
         * para el tipo salida y los temas corrección carta it uot y actualización it uot.
         */
        public void envioCorreoElectronicoInformeCartografia(Requerimiento requerimiento)
        {

            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                //Sección Informe Carografía es rbSeccion.INFORME_DE_CARTOGRAFIA
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;

                        List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECION_IT_UOT)
                        {
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_IT_OUT;
                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null && listaSolicitante != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                
                                string listaTitulares = funciones.listaTitularesComa(listaSolicitante);
                                if (listaTitulares != null && !listaTitulares.Equals(""))
                                {
                                    reemplazos.Add("[REEM_4]", listaTitulares);
                                }
                                else {
                                    reemplazos.Add("[REEM_4]", "(sin titulares definidos)");
                                }

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;

                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                            }
                        }
                        else
                        {

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ACTUALIZACION_IT_OUT)
                            {
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ACTUALIZACION_IT_UOT;
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null && listaSolicitante != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);

                                    string listaTitulares = funciones.listaTitularesComa(listaSolicitante);

                                    if (listaTitulares != null && !listaTitulares.Equals(""))
                                    {
                                        reemplazos.Add("[REEM_4]", listaTitulares);
                                    }
                                    else {
                                        reemplazos.Add("[REEM_4]", "(sin titulares definidos)");
                                    }
                                    
                                    reemplazos.Add("[REEM_5]", requerimiento.solicitud.fechaIngresoTramite);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;

                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                            else
                            {
                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_DAC_REFORM_CARTOGRAFICA)
                                {
                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_DAC_REFORM_CARTOGRAFICA;
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);

                                        string listaTitulares = funciones.listaTitularesComa(listaSolicitante);

                                        if (listaTitulares != null && !listaTitulares.Equals(""))
                                        {
                                            reemplazos.Add("[REEM_4]", listaTitulares);
                                        }
                                        else
                                        {
                                            reemplazos.Add("[REEM_4]", "(sin titulares definidos)");
                                        }
                                        
                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;

                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }
                                else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_DAC_REFORM_CARTOGRAFICA_PARA_FIRMA_JEFATURA)
                                {

                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_DAC_REFORM_CARTOGRAFICA_PARA_FIRMA_JEFATURA;
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                    if (templateAvisoFiltro != null && listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();

                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);

                                        string listaTitulares = funciones.listaTitularesComa(listaSolicitante);

                                        if (listaTitulares != null && !listaTitulares.Equals(""))
                                        {
                                            reemplazos.Add("[REEM_3]", listaTitulares);
                                        }
                                        else
                                        {
                                            reemplazos.Add("[REEM_3]", "(sin titulares definidos)");
                                        }

                                        reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;

                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }
                                else
                                {
                                    this.alertaRequerimientoConRespuesta(requerimiento);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        //if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RECURSO_DE_REPOSICION)
                        //{
                        //    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        //    templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        //    templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                        //    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RECURSO_DE_REPOSICION_INFORME_CARTOGRAFIA;

                        //    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                        //    if (templateAvisoFiltro != null && templateAvisoFiltro != null)
                        //    {
                        //        Hashtable reemplazos = new Hashtable();
                        //        reemplazos.Add("[REEM_1]", requerimiento.numero + "-" + requerimiento.fecha);

                        //        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                        //        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                        //    }

                        //}
                        //else 
                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.EVALUACION_CARTOGRAFICA)
                        {

                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.EVALUACION_CARTOGRAFICA;
                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (templateAvisoFiltro != null && listaSolicitante != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", clavesTemplateAviso.EVALUACION_CARTOGRAFICA);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == 0) //Completar con el id de requerimiento (10)
                        {

                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ASIGNADOR_EVALUCION_CARTO;
                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (templateAvisoFiltro != null && listaSolicitante != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", clavesTemplateAviso.ASIGNADOR_EVALUCION_CARTO);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                            }
                        
                        
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ACTUALIZACION_IT_OUT)
                        {

                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ACTUALIZACION_CARTO_REPUESTA;
                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (templateAvisoFiltro != null && listaSolicitante != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", clavesTemplateAviso.ASIGNADOR_EVALUCION_CARTO);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                
                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                            }


                        }

                    }
                }

            }
        }

        /**
         * Envío de Correo Electrónico para Carta Ambiental
         * para el tipo salida y los temas visación de carta, corrección carta mo y carta firmada por jefatura.
         */
        public void envioCorreoElectronicoCartaAmbiental(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {

                //Sección Carta Ambiental es rbSeccion.CARTA_AMBIENTAL
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;


                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_DE_CARTA)
                            {
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_AMB;
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                            else
                            {
                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECION_CARTA_MO)
                                {
                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_DE_CARTA_MO;
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                    if (listaSolicitantes != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                                    }
                                }
                                else
                                {
                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_FIRMADA_POR_JEFATURA)
                                    {
                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_AMB_FIRMA_JEFATURA;
                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                        if (listaSolicitantes != null)
                                        {
                                            Hashtable reemplazos = new Hashtable();
                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                        }
                                    }
                                    else
                                    {

                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_DE_CARTA_MO_COLECTORES;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (listaSolicitantes != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                            }
                                        }
                                        else
                                        {
                                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_AMBIENTAL_UTS)
                                            {
                                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_AMBIENTAL_UTS;
                                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                if (listaSolicitantes != null)
                                                {
                                                    Hashtable reemplazos = new Hashtable();
                                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                }
                                            }
                                            else
                                            {
                                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_MO_UTS)
                                                {
                                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_MO_UTS;
                                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                    if (listaSolicitantes != null)
                                                    {
                                                        Hashtable reemplazos = new Hashtable();
                                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                    }
                                                }

                                                else
                                                {
                                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_RECOPILACION_CPS_E_INFAS_UOT)
                                                    {
                                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_RECOPILACION_CPS_E_INFAS_UOT;
                                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                        if (listaSolicitantes != null)
                                                        {
                                                            Hashtable reemplazos = new Hashtable();
                                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_RECOPILACION_CPS_E_INFAS_UOT)
                                                        {
                                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_RECOPILACION_CPS_E_INFAS_UOT;
                                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                            if (listaSolicitantes != null)
                                                            {
                                                                Hashtable reemplazos = new Hashtable();
                                                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_RECOPILACION_CPS_E_INFAS_UTS)
                                                            {
                                                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_RECOPILACION_CPS_E_INFAS_UTS;
                                                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                if (listaSolicitantes != null)
                                                                {
                                                                    Hashtable reemplazos = new Hashtable();
                                                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_RECOPILACION_CPS_E_INFAS_UTS)
                                                                {
                                                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_RECOPILACION_CPS_E_INFAS_UTS;
                                                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                    if (listaSolicitantes != null)
                                                                    {
                                                                        Hashtable reemplazos = new Hashtable();
                                                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_RECOPILACION_CPS_E_INFAS_FIRMA_JEFATURA)
                                                                    {
                                                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_RECOPILACION_CPS_E_INFAS_FIRMA_JEFATURA;
                                                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                        if (listaSolicitantes != null)
                                                                        {
                                                                            Hashtable reemplazos = new Hashtable();
                                                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                        }

                                                                    }
                                                                    else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_TITULAR_MO)
                                                                    {
                                                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_TITULAR_MO;
                                                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                                                        //Agregar PL para obtener el calculo entre la fecha de la carta titular mo y su respuesta.
                                                                        int numeroPlazo = solicitudDA.DiasHabilesCarta_MO(requerimiento.solicitud.idSolConcesion);

                                                                        if (numeroPlazo > 0)
                                                                        {
                                                                            Hashtable reemplazos = new Hashtable();
                                                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                                            reemplazos.Add("[REEM_2]", "CARTA TITULAR MO");
                                                                            reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                                            reemplazos.Add("[REEM_4]", numeroPlazo);

                                                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        this.alertaRequerimientoConRespuesta(requerimiento);
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
                        }
                    }
                }
            }
            else
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        //if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RECURSO_DE_REPOSICION)
                        //{
                        //    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        //    templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        //    templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                        //    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RECURSO_DE_REPOSICION_INFORME_SEA;

                        //    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                        //    if (templateAvisoFiltro != null)
                        //    {
                        //        Hashtable reemplazos = new Hashtable();
                        //        reemplazos.Add("[REEM_1]", requerimiento.numero + "-" + requerimiento.fecha);

                        //        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                        //        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                        //    }
                        //}
                        //else 
                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_TITULAR_MO)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_TITULAR_MO;
                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            //Agregar PL para obtener el calculo entre la fecha de la carta titular mo y su respuesta.
                            int numeroPlazo = solicitudDA.DiasHabilesCarta_MO(requerimiento.solicitud.idSolConcesion);

                            if (numeroPlazo > 0)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_2]", "CARTA TITULAR MO");
                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                reemplazos.Add("[REEM_4]", numeroPlazo);

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                            }

                        }
                    }
                }

            }
        }

        /**
         * Envío de Correo Electrónico para Informe de Unidad Ambiental
         * para el tipo salida y los temas Informe Ambiental de MO y Informe UOT de MO
         */
        public void envioCorreoElectronicoInformeUnidadAmbiental(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.INFORME_AMBIENTAL_DE_MO)
                            {
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INFORME_AMBIENTAL_MO;

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                            else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ITC_MO_UOT)
                            {
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ITC_MO_UOT;

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                            else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.EVALUACION_CPS_E_INFAS)
                            {
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.EVALUACION_CPS_E_INFAS;

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                            else
                            {
                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.INFORME_AMBIENTAL_RECOPILACION_CPS_E_INFAS)
                                {
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INFORME_AMBIENTAL_RECOPILACION_CPS_E_INFAS;

                                    if (listaSolicitantes != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", clavesTemplateAviso.INFORME_AMBIENTAL_RECOPILACION_CPS_E_INFAS);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                        reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                    }
                                }
                                else
                                {
                                    this.alertaRequerimientoConRespuesta(requerimiento);
                                }

                            }
                        }
                    }
                }
            }
        }

        /**
         * Envío de Correo Electrónico para Carta Titular
         * para el tipo salida y los temas visación de carta titular, corrección carta seia y carta firmada por jefatura.
         */
        public void envioCorreoElectronicoCartaTitular(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                //Sección Carta Titular es rbSeccion.CARTA_TITULAR_SEA
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_DE_CARTA)
                            {
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_SEIA;
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                            else
                            {
                                if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_SEIA)
                                {
                                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_SEIA;
                                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                    if (listaSolicitantes != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                    }
                                }
                                else
                                {
                                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_FIRMADA_POR_JEFATURA)
                                    {
                                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_SEIA_PARA_FIRMA_JEFATURA;
                                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                        if (listaSolicitantes != null)
                                        {
                                            Hashtable reemplazos = new Hashtable();
                                            reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                            reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                            reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                        }
                                    }
                                    else
                                    {
                                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ITC_CPS_UOT)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ITC_CPS_UOT;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (listaSolicitantes != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                                reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);

                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                            }

                                        }
                                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_TITULAR_UTS)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_TITULAR_UTS;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (listaSolicitantes != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                                reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                            }
                                        }
                                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_SEIA_UTS)
                                        {
                                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_SEIA_UTS;
                                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                            if (listaSolicitantes != null)
                                            {
                                                Hashtable reemplazos = new Hashtable();
                                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                            }

                                        }
                                        else
                                        {

                                            this.alertaRequerimientoConRespuesta(requerimiento);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    { 
                     TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ASIGNADOR_UA)
                            {
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ASIGNADOR_UA;
                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.ASIGNADOR_UA);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                        }
                    }
                }
            }
        }

        /**
         * Se debe enviar un correo electrónico avisando que se ingresó una resolución de SSFFAA teniendo un Informe Técnico DAC
         * y/o una resolución de Subpesca en estado pendiente.
         */
        public void alertaResolucionSSFFAA(Requerimiento requerimiento)
        {

            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INFORME_DAC_RES_SSPP_PENDIENTE;

                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                        if (templateAvisoFiltro != null)
                        {
                            /*
                             * Enviar un correo electrónico avisando que se ingresó una resolución de SSFFAA teniendo un Informe Técnico DAC y/o una 
                             * resolución de SUBPESCA en estado pendiente.
                             */
                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RESOLUCION_SSFFAA)
                            {

                                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneResolucionSSP_ITDAC_Solicitud(requerimiento.solicitud.idSolConcesion);

                                if (solicitudConcesion != null && (!solicitudConcesion.tieneIT || !solicitudConcesion.tieneSSP))
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                this.alertaRequerimientoConRespuesta(requerimiento);
            }
        }

        /**
         * Se debe enviar un orreo electrónico avisando que se ingreso una resolución de subpesca teniendo un informe técnico DAC
         * en estado pendiente.
         */
        public void alertaResolucionSSP(Requerimiento requerimiento)
        {

            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (requerimiento.solicitud != null && requerimiento.solicitud.tipoTramite != null && requerimiento.solicitud.tipoTramite.id == rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION)
                        {

                            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            /**
                             * Enviar correo cuando el trámite de relocalización tiene su resolución SSP Aprobada. Indicar partes que faltan para ser enviado a SSFFAA. 
                             */
                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RESOLUCION_SSP &&
                                documentoAmbito.estadoResultadoResp != null && documentoAmbito.estadoResultadoResp.id == rbEstadosGenerales.APRUEBA)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RESOL_SSP_APRUEBA;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    //TramiteRelocalizacion tramiteRelocalizacion = tramiteRelocalizacionDA.ObtieneResolucionSSpTramiteRel(requerimiento.solicitud.idSolConcesion);

                                    //Se incluye la descripción de todos los sectores que incluyen el trámite.
                                    TramiteRelocalizacion tramiteRelocalizacion = tramiteRelocalizacionDA.ObtieneResolucionSSpTramiteRelSolicitud(requerimiento.solicitud.idSolConcesion);

                                    if (tramiteRelocalizacion != null && listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        //reemplazos.Add("[REEM_1]", requerimiento.numero);
                                        reemplazos.Add("[REEM_2]", requerimiento.numero + "-" + requerimiento.fecha);
                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.sectorRelocalizacion.numSector);
                                        reemplazos.Add("[REEM_4]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_5]", funciones.listaTitularesComa(listaSolicitante));
                                        reemplazos.Add("[REEM_6]", tramiteRelocalizacion.cantSSP);
                                        reemplazos.Add("[REEM_7]", tramiteRelocalizacion.descripcion);

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                    }
                                }
                            }
                        }

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RESOLUCION_SSP)
                        {

                            /*
                             * Enviar un correo electrónico avisando que se ingresó una resolución de SUBPESCA teniendo un Informe Técnico DAC en estado pendiente.
 
                             */
                            SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneResolucionSSP_ITDAC_Solicitud(requerimiento.solicitud.idSolConcesion);

                            if (solicitudConcesion != null && !solicitudConcesion.tieneIT)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INFORME_TECNICO_DAC_PENDIENTE;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }

                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RECURSO_DE_REPOSICION)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RECURSO_DE_REPOSICION;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", requerimiento.numero + "-" + requerimiento.fecha);

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                            }
                        }
                    }
                }
            }
            else
            {
                this.alertaRequerimientoConRespuesta(requerimiento);
            }
        }

        /**
         * Se debe generar un correo electrónico que de aviso a cartografía cada vez que se ingrese un nuevo PERT (este correo debe ser del perfil
         * asignador de PERT)
         */
        public void alertaIngresoNuevaSolicitud(SolicitudConcesion solicitudInicial)
        {

            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INGRESO_NUEVO_PERT;

            //templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatarioSC(templateAvisoFiltro); //EL METODO SOLO RETORNABA SI HABIA ALGUNA CONCESION QUE QUE FUERA DE LA MISMA RECION DEL USUARIO, POR ESO SE CAMBIA
            List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudInicial.idSolConcesion, 0, "", 6);

            if (templateAvisoFiltro != null)
            {
                if (listaSolicitante != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    if (solicitudInicial != null && solicitudInicial.tipoTramite != null && solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION)
                    {
                        reemplazos.Add("[REEM_1]", solicitudInicial.DescripcionTipoModificacion);
                        templateAvisoFiltro.templateSubject = "Trámite Modificación " + solicitudInicial.DescripcionTipoModificacion + " " + solicitudInicial.numPert + " " + templateAvisoFiltro.templateSubject;
                        reemplazos.Add("[REEM_2]", solicitudInicial.numPert);
                    }
                    else if (solicitudInicial != null && solicitudInicial.tipoTramite != null && solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
                    {
                        reemplazos.Add("[REEM_1]", solicitudInicial.tipoTramite.descripcion);
                        templateAvisoFiltro.templateSubject = solicitudInicial.tipoTramite.descripcion + " " + solicitudInicial.datosSolicitudUE.numIdentSolicitud + " " + templateAvisoFiltro.templateSubject;
                        reemplazos.Add("[REEM_2]", solicitudInicial.datosSolicitudUE.numIdentSolicitud);
                    }
                    else
                    {
                        reemplazos.Add("[REEM_1]", solicitudInicial.tipoTramite.descripcion);
                        templateAvisoFiltro.templateSubject = solicitudInicial.tipoTramite.descripcion + " " + solicitudInicial.numPert + " " + templateAvisoFiltro.templateSubject;
                        reemplazos.Add("[REEM_2]", solicitudInicial.numPert);
                    }

                    reemplazos.Add("[REEM_3]", "sin titulares definidos");

                    this.senEmailServiceAlerta(solicitudInicial.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }


            if (solicitudInicial.tipoTramite != null && solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
            {
                bool tieneTramiteExistente = solicitudDA.aplicaTitularesOtroColector(solicitudInicial.idSolConcesion);

                if (tieneTramiteExistente)
                {
                    /* Enviar correo si el titular tiene mas trámites de colector asociados */
                    templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudInicial.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudInicial.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SOLICITUD_COLECTOR_EXISTENTE;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        if (listaSolicitante != null)
                        {
                            Hashtable reemplazos = new Hashtable();
                            reemplazos.Add("[REEM_1]", solicitudInicial.datosSolicitudUE.numIdentSolicitud);
                            reemplazos.Add("[REEM_2]", funciones.listaTitularesComa(listaSolicitante));

                            templateAvisoFiltro.templateSubject = "Colector de Semillas " + solicitudInicial.datosSolicitudUE.numIdentSolicitud + " " + templateAvisoFiltro.templateSubject;
                            this.senEmailServiceAlerta(solicitudInicial.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);


                        }
                    }
                }
            }

            /* Enviar correo electrónico cuando exista el nombre de la amerb 
            if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
            {
                bool tieneNombreExistente = false;

                if (tieneNombreExistente)
                {

                    templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudInicial.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudInicial.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SOLICITUD_AMERB_NOMBRE_EXISTENTE;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", solicitudInicial.numPert);
                        reemplazos.Add("[REEM_2]", "");

                        templateAvisoFiltro.templateSubject = "Acuicultura en Amerb " + solicitudInicial.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudInicial.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }

            }*/
        }

        /**
         * Se debe generar un correo electrónico cada vez que se genere un trámite de relocalización
         */
        public void alertaIngresoNuevaSolicitud(Datos.Entidades.Relocalizacion.TramiteRelocalizacion tramiteRelocalizacion)
        {
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INGRESO_NUEVO_PERT;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                reemplazos.Add("[REEM_1]", "Trámite de Relocalización");
                reemplazos.Add("[REEM_2]", tramiteRelocalizacion.numPert);
                reemplazos.Add("[REEM_3]", "sin titulares definidos");

                templateAvisoFiltro.templateSubject = "Trámite de Relocalización " + tramiteRelocalizacion.numPert + " " + templateAvisoFiltro.templateSubject;
                this.senEmailServiceAlerta(tramiteRelocalizacion.idTramiteRel, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
            }
        }

        public void alertaCreacionCentroECMPO(SolicitudConcesion solicitudCentroECMPO)
        {

            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = solicitudCentroECMPO.idSolConcesion;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();

                SolicitudConcesion solicitudCentroECMPOFull = solicitudDA.ObtieneCamposSolicitudAviso(solicitudCentroECMPO.idSolConcesion);

                if (solicitudCentroECMPOFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudCentroECMPOFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudCentroECMPOFull.titularesCad); // Titulares
                    reemplazos.Add("[REEM_3]", solicitudCentroECMPOFull.tipoSolicitudCad); // Tipo Concesión
                    reemplazos.Add("[REEM_4]", solicitudCentroECMPO.unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudCentroECMPOFull.resolucionSSFFAA); // Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudCentroECMPOFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Acuicultura en ECMPO " + solicitudCentroECMPO.unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(solicitudCentroECMPO.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }
        }

        public void alertaCreacionCentroAcopio(UnidadEspacial unidadEspacial)
        {
            /* Enviar un correo cuando se cree un centro de acopio */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
            //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                SolicitudConcesion solicitudAmerbFull = solicitudDA.ObtieneCamposSolicitudAviso(unidadEspacial.idSolicitud);

                if (solicitudAmerbFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudAmerbFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudAmerbFull.titularesCad); // Titulares
                    reemplazos.Add("[REEM_3]", solicitudAmerbFull.tipoSolicitudCad); // Tipo Concesión
                    reemplazos.Add("[REEM_4]", unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudAmerbFull.resolucionSSFFAA); // Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudAmerbFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Centro de Acopio " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }


            /* Enviar un correo cuando se cree el centro de acopio y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(solicitudCentroDeAcopio.idSolConcesion);
            if (!estaCertificadoCapitania)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitudCentroDeAcopio.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {

                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudCentroDeAcopio.idSolConcesion, 0, "", 6);

                    if (listaSolicitante != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", solicitudCentroDeAcopio.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitudCentroDeAcopio.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = solicitudCentroDeAcopio.tipoTramite.descripcion + " " + solicitudCentroDeAcopio.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudCentroDeAcopio.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/
        }


        public void alertaCreacionAmerb(UnidadEspacial unidadEspacial)
        {
            /* Enviar un correo cuando se cree amerb */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
            //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                SolicitudConcesion solicitudAmerbFull = solicitudDA.ObtieneCamposSolicitudAviso(unidadEspacial.idSolicitud);

                if (solicitudAmerbFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudAmerbFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudAmerbFull.titularesCad); // Titulares
                    reemplazos.Add("[REEM_3]", solicitudAmerbFull.tipoSolicitudCad); // Tipo Concesión
                    reemplazos.Add("[REEM_4]", unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudAmerbFull.resolucionSSFFAA); // Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudAmerbFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Acuicultura en Amerb " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }

            /* Enviar un correo cuando se cree el amerb y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(unidadEspacial.idSolicitud);
            if (!estaCertificadoCapitania)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    SolicitudConcesion solicitud = solicitudDA.ObtieneSolicitudConcesion(unidadEspacial.idSolicitud, 0);
                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitud.idSolConcesion, 0, "", 6);

                    if (solicitud != null && listaSolicitante != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitud.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitud.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = solicitud.tipoTramite.descripcion + " " + solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/

            bool existeNombreAmerb = solicitudDA.SolicitudAmerCodRepetido(unidadEspacial.idSolicitud);

            if (existeNombreAmerb)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SOLICITUD_AMERB_NOMBRE_EXISTENTE;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    SolicitudConcesion solicitud = solicitudDA.ObtieneSolicitudConcesion(unidadEspacial.idSolicitud, 0);
                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitud.idSolConcesion, 0, "", 6);

                    if (solicitud != null && listaSolicitante != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitud.numPert);
                        reemplazos.Add("[REEM_2]", ""); //Nombre de la Amerb
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = "Acuicultura en Amerb " + solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }
        }


        public void alertaCreacionColectorSemillas(SolicitudConcesion solicitudColector)
        {
            /* Enviar un correo cuando se cree colector */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = solicitudColector.idSolConcesion;
            //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA;

            //28-03-2016 Se comenta por tener destinatario diferente a otras creaciones de unidades espaciales.
            //templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION; 

            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_COLECTOR;
            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                SolicitudConcesion solicitudColectorFull = solicitudDA.ObtieneCamposSolicitudAviso(solicitudColector.idSolConcesion);

                if (solicitudColectorFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudColectorFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudColectorFull.titularesCad); // Titulares

                    //28-03-2016 Se comenta por tener destinatario diferente a otras creaciones de unidades espaciales.
                    //reemplazos.Add("[REEM_3]", solicitudColectorFull.tipoSolicitudCad); // Tipo Concesión
                    //reemplazos.Add("[REEM_4]", solicitudColector.unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    //reemplazos.Add("[REEM_5]", solicitudColectorFull.resolucionSSFFAA); // Resolución SSFFAA
                    //reemplazos.Add("[REEM_6]", solicitudColectorFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Colector de Semillas " + solicitudColectorFull.numPert + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(solicitudColector.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }

            /* Enviar un correo cuando se cree el colector y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(solicitudColector.idSolConcesion);

            if (!estaCertificadoCapitania)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitudColector.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudColector.idSolConcesion, 0, "", 6);

                    if (listaSolicitante != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitudColector.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitudColector.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = solicitudColector.tipoTramite.descripcion + " " + solicitudColector.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudColector.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/

        }


        public void alertaCreacionCentroFaenamiento(SolicitudConcesion solicitudCentroDeFaenamiento)
        {
            /* Enviar un correo cuando se cree centro de faenamiento */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = solicitudCentroDeFaenamiento.idSolConcesion;
            //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                SolicitudConcesion solicitudFaenamientoFull = solicitudDA.ObtieneCamposSolicitudAviso(solicitudCentroDeFaenamiento.idSolConcesion);

                if (solicitudFaenamientoFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudFaenamientoFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudFaenamientoFull.titularesCad); // Titulares
                    reemplazos.Add("[REEM_3]", solicitudFaenamientoFull.tipoSolicitudCad); // Tipo Concesión
                    reemplazos.Add("[REEM_4]", solicitudCentroDeFaenamiento.unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudFaenamientoFull.resolucionSSFFAA); // Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudFaenamientoFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Centro de Faenamiento " + solicitudCentroDeFaenamiento.unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(solicitudCentroDeFaenamiento.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }

            /* Enviar un correo cuando se cree el centro de faenamiento y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(solicitudCentroDeFaenamiento.idSolConcesion);
            if (!estaCertificadoCapitania)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitudCentroDeFaenamiento.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();
                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudCentroDeFaenamiento.idSolConcesion, 0, "", 6);

                    if (listaSolicitante != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitudCentroDeFaenamiento.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitudCentroDeFaenamiento.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = solicitudCentroDeFaenamiento.tipoTramite.descripcion + " " + solicitudCentroDeFaenamiento.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudCentroDeFaenamiento.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/
        }


        public void alertaCreacionConcesion(UnidadEspacial unidadEspacial)
        {
            /* Enviar un correo cuando se cree concesión */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
            //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();

                SolicitudConcesion solicitudConcesion = solicitudDA.ObtieneCamposSolicitudAviso(unidadEspacial.idSolicitud);

                if (solicitudConcesion != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudConcesion.numPert); //Número Pert
                    reemplazos.Add("[REEM_2]", solicitudConcesion.titularesCad); //Rut - Titular
                    reemplazos.Add("[REEM_3]", solicitudConcesion.tipoSolicitudCad); //Tipo Concesión
                    reemplazos.Add("[REEM_4]", unidadEspacial.centrosDeCultivo.codigoCentro); //Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudConcesion.resolucionSSFFAA); //Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudConcesion.resolucionSSP); //Resolución SSP
                }

                templateAvisoFiltro.templateSubject = "Concesión de Acuicultura " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

            }

            /* Enviar un correo cuando se relocalice y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(unidadEspacial.idSolicitud);

            if (!estaCertificadoCapitania)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    SolicitudConcesion solicitud = solicitudDA.ObtieneSolicitudConcesion(unidadEspacial.idSolicitud, 0);
                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitud.idSolConcesion, 0, "", 6);

                    if (solicitud != null && listaSolicitante != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitud.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitud.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = solicitud.tipoTramite.descripcion + " " + solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/
        }


        public void alertaModificacionConcesion(UnidadEspacial unidadEspacial)
        {
            /* Enviar correo electrónico cuando se modifica */
            SolicitudConcesion solicitud = solicitudDA.ObtieneSolicitudConcesionMod(unidadEspacial.idSolicitud, 0);
            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(unidadEspacial.idSolicitud, 0, "", 6);

            if (solicitud.tipoModificacionString().Contains("Regularización")) //Correo por si se contiene una regularización.
            {
                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitud.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_MODIFICACION;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REGULARIZACION_CONCESION;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    if (listaSolicitantes != null)
                    {
                        reemplazos.Add("[REEM_1]", unidadEspacial.centrosDeCultivo.codigoCentro);
                        reemplazos.Add("[REEM_2]", funciones.listaTitularesComa(listaSolicitantes));

                        templateAvisoFiltro.templateSubject = " Modificación de Regularización " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }
            else
            {

                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitud.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_MODIFICACION;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.MODIFICACION_CONCESION;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                if (templateAvisoFiltro != null)
                {
                    if (listaSolicitantes != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", unidadEspacial.centrosDeCultivo.codigoCentro);
                        reemplazos.Add("[REEM_2]", funciones.listaTitularesComa(listaSolicitantes));
                        reemplazos.Add("[REEM_3]", solicitud.numPert);

                        templateAvisoFiltro.templateSubject = "Modificación Concesión " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                    }
                }
            }

            /* Enviar un correo cuando se modifique la concesión y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(unidadEspacial.idSolicitud);

            if (!estaCertificadoCapitania)
            {
                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitud.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_MODIFICACION;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();

                    if (listaSolicitantes != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitud.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitud.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));

                        templateAvisoFiltro.templateSubject = solicitud.tipoTramite.descripcion + " " + solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/

        }


        public void alertaRelocalizacionConcesion(SolicitudConcesion solicitudRelocalizacion)
        {
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = solicitudRelocalizacion.idSolConcesion;
            //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RELOCALIZACION_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();

                SolicitudConcesion solicitudRelocalizacionFull = solicitudDA.ObtieneCamposSolicitudAviso(solicitudRelocalizacion.idSolConcesion);

                if (solicitudRelocalizacionFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudRelocalizacionFull.numPert);
                    reemplazos.Add("[REEM_2]", solicitudRelocalizacionFull.titularesCad); //Titulares
                    reemplazos.Add("[REEM_3]", solicitudRelocalizacionFull.sectorRelocalizacion.tipoRelocalizacion.descripcion); //Tipo Relocalización
                    reemplazos.Add("[REEM_4]", solicitudRelocalizacion.sectorRelocalizacion.origenesDetalle); //Código Centro
                    reemplazos.Add("[REEM_5]", solicitudRelocalizacionFull.resolucionSSFFAA); //Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudRelocalizacionFull.resolucionSSP); //Resolución SSP

                    templateAvisoFiltro.templateSubject = "Relocalización " + solicitudRelocalizacionFull.numPert + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(solicitudRelocalizacion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }

            /* Enviar un correo cuando se relocalice y no este el certificado de capitania de puerto 
            bool estaCertificadoCapitania = solicitudDA.aplicaSolicitudConCapitaniaPuerto(solicitudRelocalizacion.idSolConcesion);

            if (!estaCertificadoCapitania)
            {
                templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = solicitudRelocalizacion.idSolConcesion;
                //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();
                    List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudRelocalizacion.idSolConcesion, 0, "", 6);

                    if (listaSolicitante != null)
                    {
                        reemplazos.Add("[REEM_1]", solicitudRelocalizacion.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitudRelocalizacion.numPert);
                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                        templateAvisoFiltro.templateSubject = solicitudRelocalizacion.tipoTramite.descripcion + " " + solicitudRelocalizacion.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudRelocalizacion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }*/

        }

        /**
         * Se debe enviar un correo electrónico cuando se solicite la publicación en radio de una solicitud.
         * Además se debe enviar un correo electronico cuando se solicite la publicación web y radial.
         */
        public void alertaDifusionBancoNatural(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.PUBLICACION_WEB)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.PUBLICACION_WEB;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                if (listaSolicitante != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));
                                    reemplazos.Add("[REEM_4]", requerimiento.solicitud.tipoTramite.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.REITERA_PUBLICACION_WEB)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REITERA_PUBLICACION_WEB;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                if (listaSolicitante != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                        }
                        else
                        {
                            if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.PUBLICACION_RADIAL)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                                templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                                templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                                templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.PUBLICACION_RADIAL;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    if (listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }

                            }
                            else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.REITERA_PUBLICACION_RADIAL)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                                templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                                templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                                templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REITERA_PUBLICACION_RADIAL;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    if (listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }

                            }
                            else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_PUBLICACION_RADIAL)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                                templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                                templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                                templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_PUBLICACION_RADIAL;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    if (listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoTramite.descripcion);
                                        reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }

                            }
                            else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.REITERA_CORRECCION_PUBLICACION_RADIAL)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                                templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                                templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                                templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REITERA_CORRECCION_PUBLICACION_RADIAL;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    if (listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }
                            }
                            else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.INFORMATIVO)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                                templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                                templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                                templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                                templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.DIFUSION_BN_INFORMATIVO;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    if (listaSolicitante != null)
                                    {
                                        Hashtable reemplazos = new Hashtable();
                                        reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                        reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                        reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                        templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                        this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                    }
                                }
                            }
                            else
                            {

                                this.alertaRequerimientoConRespuesta(requerimiento);
                            }
                        }
                    }
                }
            }
        }

        /**
         * Se debe enviar un correo electrónico cuando ingrese un recurso de reposición en DIFROL
         */
        public void alertaDifrol(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    //foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    //{

                    //    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.RECURSO_DE_REPOSICION)
                    //    {
                    //        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    //        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                    //        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                    //        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.RECURSO_DE_REPOSICION_DIFROL;

                    //        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    //        if (templateAvisoFiltro != null)
                    //        {
                    //            Hashtable reemplazos = new Hashtable();
                    //            reemplazos.Add("[REEM_1]", requerimiento.numero + "-" + requerimiento.fecha);

                    //            templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                    //            this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    //        }
                    //    }
                    //}
                }
            }
            else if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                {

                    if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_DIFROL)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_DIFROL;

                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (listaSolicitantes != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                            }

                        }
                    }
                    else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_DIFROL)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_DIFROL;

                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (listaSolicitantes != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                            }

                        }
                    }
                    else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.DIFROL_PARA_FIRMA_JEFATURA)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                        templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                        templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                        templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                        templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.DIFROL_PARA_FIRMA_JEFATURA;

                        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                        if (templateAvisoFiltro != null)
                        {
                            List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                            if (listaSolicitantes != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                            }

                        }
                    }
                }


            }
            else
            {
                this.alertaRequerimientoConRespuesta(requerimiento);

            }
        }

        /* Envío de correo electrónico cuando se ingresen varias solicitudes en paralelo de un mismo centro de origen y todas las hectáreas del centro fueron utilizadas */
        public void alertaRelocalizacion(Datos.Entidades.Relocalizacion.TramiteRelocalizacion tramiteRelocalizacion)
        {

            /* Listado de solicitudes que tienen un centro mencionado en otros trámites de relocalización */
            List<TramiteRelocalizacion> listaTramiteRelocalizacion = tramiteRelocalizacionDA.aplicaCentroEnOtroTramiteRel(tramiteRelocalizacion.idTramiteRel);
            if (listaTramiteRelocalizacion != null)
            {
                foreach (TramiteRelocalizacion tramiteRelocalizacionEspecial in listaTramiteRelocalizacion)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.HECTAREAS_UTILIZADAS_RELOCALIZACION;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {

                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", tramiteRelocalizacionEspecial.centroOrigenFiltro.id); //Código de Centro
                        reemplazos.Add("[REEM_2]", tramiteRelocalizacionEspecial.numPert); //Pert
                        reemplazos.Add("[REEM_3]", tramiteRelocalizacionEspecial.centroOrigenFiltro.descripcion); //Titulares

                        templateAvisoFiltro.templateSubject = "Relocalización " + tramiteRelocalizacionEspecial.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(tramiteRelocalizacion.idTramiteRel, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }

        }

        /**
         * Se debe enviar un correo electrónico cuando transcurra los dos meses de la publicación radial.
         * (Alerta 23)
         */
        public void avisoPublicacionRadial(int idSolicitud)
        {

            List<SolicitudConcesion> listaSolicitudConcesionPublicacionRadialOK = solicitudDA.ListarSolicitud_PublicacionRadialOk(idSolicitud);

            if (listaSolicitudConcesionPublicacionRadialOK != null && listaSolicitudConcesionPublicacionRadialOK.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesion in listaSolicitudConcesionPublicacionRadialOK)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesion.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.DOS_MESES_PUBLICACION_RADIAL;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", solicitudConcesion.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", solicitudConcesion.numPert);

                        templateAvisoFiltro.templateSubject = solicitudConcesion.tipoTramite.descripcion + " " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }
        }

        /**
         * Se debe generar un correo electrónico que indique que si luego de 30 días la carpeta no ha sido asignado
         * (este correo debe ser enviado al Coordinador Territorial)
         * (Alerta 8)
         */
        public void carpetaNoAsignada(int idSolicitud)
        {
            List<SolicitudConcesion> listaSolicitudCarpetaNoAsignada = solicitudDA.ListarSolicitudesSinAsignacionUsuario(idSolicitud);

            if (listaSolicitudCarpetaNoAsignada != null && listaSolicitudCarpetaNoAsignada.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesion in listaSolicitudCarpetaNoAsignada)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    //templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
                    //templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesion.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARPETA_NO_ASIGNADA;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(solicitudConcesion.idSolConcesion, 0, "", 6);

                        if (listaSolicitantes != null)
                        {
                            Hashtable reemplazos = new Hashtable();
                            reemplazos.Add("[REEM_1]", solicitudConcesion.tipoTramite.descripcion);
                            reemplazos.Add("[REEM_2]", solicitudConcesion.numPert);
                            //reemplazos.Add("[REEM_3]", solicitudConcesion.idSolConcesion);
                            reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                            templateAvisoFiltro.templateSubject = solicitudConcesion.tipoTramite.descripcion + " " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                            this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                        }
                    }
                }
            }
        }

        /**
         * Se debe generar un correo electrónico, si un Pert luego de un año no ha tenido movimiento (que no  se ha ingresado
         * el informe de cartografía), además este debe ser desplegado en el reporte de plazos vencidos.
         * (Alerta 6)
         */
        public void alertaRequerimientoSinMov(int idSolicitud)
        {

            List<SolicitudConcesion> listaSolicitudRequerimientoSinMov = solicitudDA.ListarSolicitudesSinMov(idSolicitud);
            if (listaSolicitudRequerimientoSinMov != null && listaSolicitudRequerimientoSinMov.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesion in listaSolicitudRequerimientoSinMov)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesion.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.PERT_SIN_MOVIMIENTO;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(solicitudConcesion.idSolConcesion, 0, "", 6);

                        if (listaSolicitantes != null)
                        {
                            Hashtable reemplazos = new Hashtable();
                            reemplazos.Add("[REEM_1]", solicitudConcesion.tipoTramite.descripcion);
                            reemplazos.Add("[REEM_2]", solicitudConcesion.numPert);
                            reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                            reemplazos.Add("[REEM_4]", "estado sin movimiento");

                            templateAvisoFiltro.templateSubject = solicitudConcesion.tipoTramite.descripcion + " " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                            this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                        }
                    }
                }
            }
        }

        /**
         * Se debe enviar un correo electrónico cuando la solicitud cambie de estado, a un estado que tiene plazo. Indicando los plazos.
         * (10)
         */
        public void solicitudCambiaEstadoConPlazo(int idSolicitud)
        {

            List<SolicitudConcesion> listaSolicitudCambiaEstadoConPlazo = solicitudDA.ListarSolicitud_SubReqPlazo_Estado(idSolicitud);

            if (listaSolicitudCambiaEstadoConPlazo != null && listaSolicitudCambiaEstadoConPlazo.Count > 0)
            {
                foreach (SolicitudConcesion solicitudConcesion in listaSolicitudCambiaEstadoConPlazo)
                {

                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesion.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SOLICITUD_CAMBIA_ESTADO_CON_PLAZO;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", solicitudConcesion.numPert);
                        reemplazos.Add("[REEM_2]", solicitudConcesion.estadoActual.descripcion); //Incorporar nombre de estado
                        reemplazos.Add("[REEM_3]", solicitudConcesion.diasCantidad.id + " " + solicitudConcesion.diasCantidad.descripcion); //Incorporar plazo
                        reemplazos.Add("[REEM_4]", solicitudConcesion.fechaRangoFiltro1); //Incorporar día que comienza el plazo

                        templateAvisoFiltro.templateSubject = solicitudConcesion.tipoUnidadEspacial.descripcion + " " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                    }
                }
            }
        }

        /**
         * Se debe enviar un correo electrónico cuando un requerimiento que influya en el cambio de estado se encuentre vencido.
         * (Alerta 4)
         */
        public void alertaRequerimientoCambioEstadoPendiente(int idSolicitud)
        {

            List<SolicitudConcesion> listaSolucitudesvencidas = solicitudDA.ListarSolicitud_SubReqPlazo_EstadoVencido(idSolicitud);

            if (listaSolucitudesvencidas != null && listaSolucitudesvencidas.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesionVencidas in listaSolucitudesvencidas)
                {
                    if (solicitudConcesionVencidas != null && solicitudConcesionVencidas.tipoUnidadEspacial != null)
                    {

                        if (solicitudConcesionVencidas.tipoUnidadEspacial.id != rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
                        {

                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = solicitudConcesionVencidas.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesionVencidas.tipoTramite.id;

                            //templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REQUERIMIENTO_CAMBIO_ESTADO_PENDIENTE;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REQUERIMIENTO_CAMBIO_ESTADO_PENDIENTE_1;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudConcesionVencidas.idSolConcesion, 0, "", 6);

                                if (listaSolicitante != null)
                                {
                                    reemplazos.Add("[REEM_1]", solicitudConcesionVencidas.temaReq.descripcion); //Incorporar documento que solicita y que da plazo
                                    reemplazos.Add("[REEM_2]", solicitudConcesionVencidas.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                    templateAvisoFiltro.templateSubject = solicitudConcesionVencidas.tipoTramite.descripcion + " " + solicitudConcesionVencidas.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(solicitudConcesionVencidas.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }

                        }
                        else if (solicitudConcesionVencidas.tipoUnidadEspacial.id == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA)
                        {

                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = solicitudConcesionVencidas.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesionVencidas.tipoTramite.id;

                            //templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REQUERIMIENTO_CAMBIO_ESTADO_PENDIENTE;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REQUERIMIENTO_CAMBIO_ESTADO_PENDIENTE_2;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudConcesionVencidas.idSolConcesion, 0, "", 6);

                                if (listaSolicitante != null)
                                {
                                    reemplazos.Add("[REEM_1]", solicitudConcesionVencidas.temaReq.descripcion); //Incorporar documento que solicita y que da plazo
                                    reemplazos.Add("[REEM_2]", solicitudConcesionVencidas.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

                                    templateAvisoFiltro.templateSubject = solicitudConcesionVencidas.tipoTramite.descripcion + " " + solicitudConcesionVencidas.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(solicitudConcesionVencidas.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }

                        }

                    }
                }
            }
        }


        public void alertaRequerimientoCambioEstadoPendienteNew(int idSolicitud)
        {
            //Conjunto de solicitudes que tenga requerimientos pendientes, incluye el destinatario de acuerdo al destinatario del requerimiento pendiente.

            List<SolicitudConcesion> listaSolucitudesvencidas = solicitudDA.ListarSolicitud_SubReqPlazo_Vencido_Usuario();

            if (listaSolucitudesvencidas != null && listaSolucitudesvencidas.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesionVencidas in listaSolucitudesvencidas)
                {
                    if (solicitudConcesionVencidas != null && solicitudConcesionVencidas.tipoUnidadEspacial != null)
                    {
                        TemplateAviso templateAvisoFiltro = new TemplateAviso();
                        templateAvisoFiltro.idSolicitudRevisada = solicitudConcesionVencidas.idSolConcesion;
                        templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesionVencidas.tipoTramite.id;
                        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REQUERIMIENTO_CAMBIO_ESTADO_PENDIENTE;


                        if (templateAvisoFiltro != null)
                        {
                            foreach (ParametroGenerico parametroGenerico in solicitudConcesionVencidas.temaDestinatarioDinamico)
                            {
                                Hashtable reemplazos = new Hashtable();
                                reemplazos.Add("[REEM_1]", parametroGenerico.clave);
                                reemplazos.Add("[REEM_2]", solicitudConcesionVencidas.numPert);
                                reemplazos.Add("[REEM_3]", solicitudConcesionVencidas.titularesCad);

                                templateAvisoFiltro.templateSubject = solicitudConcesionVencidas.tipoTramite.descripcion + " " + solicitudConcesionVencidas.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(solicitudConcesionVencidas.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, parametroGenerico.descripcion);
                            }
                        }

                    }
                }
            }


        }


        public void alertaPlazoVencido(int idSolicitud)
        {

            List<SolicitudConcesion> listaSolucitudesvencidas = solicitudDA.ListarSolicitud_SubReqPlazo_Vencido(idSolicitud);

            if (listaSolucitudesvencidas != null && listaSolucitudesvencidas.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesionVencidas in listaSolucitudesvencidas)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudConcesionVencidas.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesionVencidas.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.PLAZOS_VENCIDOS;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudConcesionVencidas.idSolConcesion, 0, "", 6);

                        if (listaSolicitante != null)
                        {
                            reemplazos.Add("[REEM_1]", solicitudConcesionVencidas.tipoUnidadEspacial.descripcion);
                            reemplazos.Add("[REEM_2]", solicitudConcesionVencidas.tipoTramite.descripcion);
                            reemplazos.Add("[REEM_3]", solicitudConcesionVencidas.numPert);
                            reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitante));
                            reemplazos.Add("[REEM_5]", solicitudConcesionVencidas.temaReq.descripcion);

                            templateAvisoFiltro.templateSubject = solicitudConcesionVencidas.tipoUnidadEspacial.descripcion + " " + solicitudConcesionVencidas.numPert + " " + templateAvisoFiltro.templateSubject;
                            this.senEmailServiceAlerta(solicitudConcesionVencidas.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                        }
                    }
                }
            }
        }

        public void alertaSectorRelocalizacionRechazado(int idSolicitud)
        {
            List<SolicitudConcesion> listaSectorRelocalizacionRechazado = solicitudDA.ListarSectoresRel_Rechazado(idSolicitud);

            if (listaSectorRelocalizacionRechazado != null && listaSectorRelocalizacionRechazado.Count > 0)
            {

                foreach (SolicitudConcesion solicitudConcesion in listaSectorRelocalizacionRechazado)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
                    //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SECTOR_RELOCALIZACION_RECHAZADO;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();

                        reemplazos.Add("[REEM_1]", solicitudConcesion.sectorRelocalizacion.numSector); //Número Sector
                        reemplazos.Add("[REEM_2]", solicitudConcesion.numPert);
                        reemplazos.Add("[REEM_3]", solicitudConcesion.titularesCad);

                        String mensaje = UtilMail.parseMensaje(templateAvisoFiltro.templateCuerpo, reemplazos);

                        if (mensaje != null && !mensaje.Equals(""))
                        {
                            bool existeAvisoEnviado = avisoDA.ExisteAvisoEnviado(solicitudConcesion.idSolConcesion, clavesTemplateAviso.SECTOR_RELOCALIZACION_RECHAZADO, mensaje);

                            if (!existeAvisoEnviado)
                            {
                                templateAvisoFiltro.templateSubject = "Relocalización " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                                this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                            }
                        }

                    }
                }
            }
        }

        /**
         * Se debe enviar un correo electrónico cuando se generen requerimientos que requieren respuesta. 
         */
        public void alertaRequerimientoConRespuesta(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {

                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {
                        if (requerimiento.tipoSalida.id == rbTipo.REQUERIMIENTO_CON_RESPUESTA)
                        {

                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            //templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            //templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            //templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.REQUERIMIENTO_CON_RESPUESTA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                Hashtable reemplazos = new Hashtable();
                                List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitante != null)
                                {
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoTramite.descripcion);
                                    
                                    string listaTitulares = funciones.listaTitularesComa(listaSolicitante);
                                    if (listaTitulares != null && !listaTitulares.Equals(""))
                                    {
                                        reemplazos.Add("[REEM_2]", listaTitulares);
                                    }
                                    else {
                                        reemplazos.Add("[REEM_2]", "(sin titulares definidos)");
                                    }

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                        }
                    }
                }
            }
        }


        public void alertaTitularColectorExistente(int idSolicitud, int rutTitular)
        {

            DatosSolicitudUE datosSolicitudUE = solicitudDA.ObtieneDatosSolicitudUE(idSolicitud);

            if (datosSolicitudUE != null)
            {
                bool tieneTramiteExistente = solicitudDA.aplicaTitularesOtroColector(idSolicitud);

                if (tieneTramiteExistente)
                {
                    /* Enviar correo si el titular tiene mas trámites de colector asociados */
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = idSolicitud;
                    //templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SOLICITUD_COLECTOR_EXISTENTE;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);
                    if (templateAvisoFiltro != null)
                    {
                        List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(idSolicitud, 0, "", 6);

                        if (listaSolicitante != null)
                        {
                            Hashtable reemplazos = new Hashtable();
                            reemplazos.Add("[REEM_1]", datosSolicitudUE.numIdentSolicitud);
                            reemplazos.Add("[REEM_2]", funciones.listaTitularesComa(listaSolicitante));

                            templateAvisoFiltro.templateSubject = "Colector de Semillas " + datosSolicitudUE.numIdentSolicitud + " " + templateAvisoFiltro.templateSubject;
                            this.senEmailServiceAlerta(idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                        }
                    }
                }
            }

        }


        public void alertaCapitaniaDePuerto(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_CERTIFICADO_DISTANCIA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_CERTIFICADO_DISTANCIA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                    }
                }
            }
            else
            {

                this.alertaRequerimientoConRespuesta(requerimiento);

            }
        }


        public void alertaInspeccionEnTerreno(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_OFICIO_INSPECCION_TERRENO)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_OFICIO_INSPECCION_TERRENO;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.INSPECCION_TERRENO_PARA_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INSPECCION_TERRENO_PARA_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();

                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                    }
                }
            }
            else
            {

                this.alertaRequerimientoConRespuesta(requerimiento);

            }
        }


        public void alertaAntecedentesComplementarios(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_ANTECEDENTES_COMPLEMENTARIOS)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_ANTECEDENTES_COMPLEMENTARIOS;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_ANTECEDENTES_COMPLEMENTARIOS)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_ANTECEDENTES_COMPLEMENTARIOS;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_ANTECEDENTES_COMPLEMENTARIOS_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_ANTECEDENTES_COMPLEMENTARIOS_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_NOTIFICACION_REFORMULACION)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_NOTIFICACION_REFORMULACION;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_NOTIFICACION_REFORMULACION)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_NOTIFICACION_REFORMULACION;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_NOTIFICACION_REFORMULACION_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_NOTIFICACION_REFORMULACION_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_NOTIFICACION_POR_INSUFICIENCIA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_NOTIFICACION_POR_INSUFICIENCIA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_NOTIFICACION_POR_SUFICIENCIA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_NOTIFICACION_POR_SUFICIENCIA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_NOTIFICACION_POR_INSUFICIENCIA_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_NOTIFICACION_POR_INSUFICIENCIA_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.seccion = new ParametroGenerico(documentoAmbito.seccion.id);
                            templateAvisoFiltro.requerimiento = new ParametroGenerico(documentoAmbito.tipo.id);
                            templateAvisoFiltro.tipoFlujoDocumental = new ParametroGenerico(requerimiento.flujoDocumental.id);
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_DEVOLUCION_ANTICIPADA_POR_INSUFICIENCIA_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                    }
                }
            }
            else
            {

                this.alertaRequerimientoConRespuesta(requerimiento);

            }
        }


        public void alertaAntecedentesURB(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ANTECEDENTES_AMERB)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ANTECEDENTES_AMERB;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.ANTECEDENTES_AMERB);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);

                                    String titulares = funciones.listaTitularesComa(listaSolicitantes);

                                    if (titulares != null && !titulares.Equals(""))
                                    {
                                        reemplazos.Add("[REEM_4]", titulares);
                                    }
                                    else
                                    {
                                        reemplazos.Add("[REEM_4]", "Sin titulares definidos.");
                                    }

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.EVALUACION_AMERB)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.EVALUACION_AMERB;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.EVALUACION_AMERB);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);

                                    String titulares = funciones.listaTitularesComa(listaSolicitantes);

                                    if (titulares != null && !titulares.Equals(""))
                                    {
                                        reemplazos.Add("[REEM_4]", titulares);
                                    }
                                    else
                                    {
                                        reemplazos.Add("[REEM_4]", "Sin titulares definidos.");
                                    }

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                    }
                }
            }
            else
            {

                this.alertaRequerimientoConRespuesta(requerimiento);

            }
        }


        public void alertaReferenciasGlobales(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.ENTRADA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.ANTECEDENTES_DIRECCION_ZONAL)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.ANTECEDENTES_DIRECCION_ZONAL;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }

                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.INGRESO_SOLICITUD)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INGRESO_SOLICITUD;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.INGRESO_SOLICITUD);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        } 
                    }
                }
            }
            else
            {

                this.alertaRequerimientoConRespuesta(requerimiento);

            }

        }


        public void alertaCertificadoOperacion(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_CERTIFICADO_REGISTRO_OPERACION)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_CERTIFICADO_REGISTRO_OPERACION;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_CERTIFICADO_REGISTRO_OPERACION)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_CERTIFICADO_REGISTRO_OPERACION;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CERTIFICADO_REGISTRO_OPERACION_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CERTIFICADO_REGISTRO_OPERACION_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }
                            }
                        }
                        else
                        {
                            this.alertaRequerimientoConRespuesta(requerimiento);
                        }
                    }
                }
            }
        }


        public void alertaNotificacionSMA(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.VISACION_OFICIO_NOTIFICACION_SMA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_OFICIO_NOTIFICACION_SMA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CORRECCION_OFICIO_NOTIFICACION_SMA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CORRECCION_OFICIO_NOTIFICACION_SMA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", documentoAmbito.tipo.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_3]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_4]", funciones.listaTitularesComa(listaSolicitantes));

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.OFICIO_NOTIFICACION_SMA_PARA_FIRMA_JEFATURA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.OFICIO_NOTIFICACION_SMA_PARA_FIRMA_JEFATURA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                List<Solicitante> listaSolicitantes = solicitanteDA.listarSolicitante(requerimiento.solicitud.idSolConcesion, 0, "", 6);

                                if (listaSolicitantes != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitantes));
                                    reemplazos.Add("[REEM_4]", documentoAmbito.tipo.descripcion);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }


                            }
                        }
                        else
                        {
                            this.alertaRequerimientoConRespuesta(requerimiento);
                        }
                    }
                }
            }
        }

        //public void sinCertificadoCapitaniaPuertoJob(int idSolicitud)
        //{

        //    //Obtener todas las solicitudes que estando en el estado de "Espera Elaboración ITC" no se escuentre el certificado de capitanía de puerto.
        //    List<SolicitudConcesion> solicitudesSinCapitaniaPuerto = solicitudDA.ListarSolicitudesSinCapitania();

        //    if (solicitudesSinCapitaniaPuerto != null && solicitudesSinCapitaniaPuerto.Count > 0)
        //    {

        //        foreach (SolicitudConcesion solicitudConcesion in solicitudesSinCapitaniaPuerto)
        //        {
        //            TemplateAviso templateAvisoFiltro = new TemplateAviso();
        //            templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
        //            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SIN_CERTIFICADO_CAPITANIA_PUERTO;

        //            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

        //            if (templateAvisoFiltro != null)
        //            {

        //                List<Solicitante> listaSolicitante = solicitanteDA.listarSolicitante(solicitudConcesion.idSolConcesion, 0, "", 6);

        //                if (listaSolicitante != null)
        //                {
        //                    Hashtable reemplazos = new Hashtable();
        //                    reemplazos.Add("[REEM_1]", solicitudConcesion.tipoTramite.descripcion);
        //                    reemplazos.Add("[REEM_2]", solicitudConcesion.numPert);
        //                    reemplazos.Add("[REEM_3]", funciones.listaTitularesComa(listaSolicitante));

        //                    templateAvisoFiltro.templateSubject = solicitudConcesion.tipoTramite.descripcion + " " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
        //                    this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

        //                }
        //            }
        //        }
        //    }
        //}

        public void alertaNombreAmerbYaExistente(DetalleDatosSolicitud detalleDatosSolicitud, string numeroPert)
        {

            bool amerbExistente = true;
            if (amerbExistente)
            {
                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.idSolicitudRevisada = detalleDatosSolicitud.idSolConcesion;
                templateAvisoFiltro.idTipoSolicitudRev = rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB;
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SOLICITUD_AMERB_NOMBRE_EXISTENTE;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();
                    reemplazos.Add("[REEM_1]", numeroPert);
                    reemplazos.Add("[REEM_2]", detalleDatosSolicitud.nombreAmerbPadre);

                    templateAvisoFiltro.templateSubject = "Acuicultura en Amerb " + numeroPert + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(detalleDatosSolicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }


        }

        public void pertAsingnadoUsuario(int idUsuario, int idTipoTrámite, List<int> agregadosIDs)
        {

            Usuario usuarioFiltro = new Usuario();
            usuarioFiltro.id_usuario = idUsuario;

            usuarioFiltro = usuarioDA.ObtieneRbUsuario(usuarioFiltro);

            if (usuarioFiltro != null)
            {
                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.NOTIFICA_ASIGNACION_DE_TRAMITE;

                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                if (templateAvisoFiltro != null)
                {
                    Hashtable reemplazos = new Hashtable();
                    reemplazos.Add("[REEM_1]", obtenerNombreTipoTramite(idTipoTrámite)); //Nombre del Tipo de Tramite

                    string cadenaId = "";
                    int i = 0;
                    foreach (int id in agregadosIDs)
                    {
                        if (i == 0)
                        {
                            cadenaId = Convert.ToString(id);
                        }
                        else
                        {
                            cadenaId = cadenaId + "," + Convert.ToString(id);
                        }
                        i++;
                    }

                    reemplazos.Add("[REEM_2]", cadenaId);

                    this.senEmailServiceAlerta(0, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, usuarioFiltro.correo);
                }
            }
        }


        private string obtenerNombreTipoTramite(int idTipoTramite)
        {

            List<ParametroGenerico> listTipoTramite = tipoDA.ListarTipo("TIPO_TRAMITE");
            if (listTipoTramite != null)
            {

                foreach (ParametroGenerico tipoTramite in listTipoTramite)
                {
                    if (tipoTramite != null && tipoTramite.id == idTipoTramite)
                    {
                        return tipoTramite.descripcion;
                    }
                }
            }
            return "";
        }

        public void alertaResolucionCalificacionAmbiental(Requerimiento requerimiento)
        {
            if (requerimiento.flujoDocumental.id == rbTipo.SALIDA)
            {
                if (requerimiento.ambitoTipo != null)
                {
                    foreach (DocumentoAmbito documentoAmbito in requerimiento.ambitoTipo)
                    {

                        if (documentoAmbito.tipo != null && documentoAmbito.tipo.id == rbSubRequerimiento.CARTA_SOMETIMIENTO_SEA_RCA)
                        {
                            TemplateAviso templateAvisoFiltro = new TemplateAviso();
                            templateAvisoFiltro.idSolicitudRevisada = requerimiento.solicitud.idSolConcesion;
                            templateAvisoFiltro.idTipoSolicitudRev = requerimiento.solicitud.tipoTramite.id;
                            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CARTA_SOMETIMIENTO_SEA_RCA;

                            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                            if (templateAvisoFiltro != null)
                            {
                                //Calcular el número de días hábiles entre la fecha de la carta sometimiento sea rca y la resolucion de calificación ambiental.
                                int numeroDias = solicitudDA.DiasHabilesCartaTitular_RCA(requerimiento.solicitud.idSolConcesion);

                                if (numeroDias > 0)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", requerimiento.solicitud.tipoUnidadEspacial.descripcion);
                                    reemplazos.Add("[REEM_2]", requerimiento.solicitud.numPert);
                                    reemplazos.Add("[REEM_3]", numeroDias);

                                    templateAvisoFiltro.templateSubject = requerimiento.solicitud.tipoUnidadEspacial.descripcion + " " + requerimiento.solicitud.numPert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(requerimiento.solicitud.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                                }

                            }
                        }
                        else
                        {

                            this.alertaRequerimientoConRespuesta(requerimiento);
                        }
                    }
                }
            }

        }

        public void solicitudUnidadDependenciaCambiaEstado(int idSolicitud)
        {
            ////Conjunto de solicitudes que tengan unidades de dependencias que hayan cambiado de estado.
            //List<UnidadDependenciaMod> listaSolucitudesConDependencias = unidadDependenciaModDA.ListarUnidadDependencia_SolEstados();

            //if (listaSolucitudesConDependencias != null && listaSolucitudesConDependencias.Count > 0)
            //{

            //    foreach (UnidadDependenciaMod unidadDependenciaMod in listaSolucitudesConDependencias)
            //    {
            //        TemplateAviso templateAvisoFiltro = new TemplateAviso();
            //        templateAvisoFiltro.idSolicitudRevisada = unidadDependenciaMod.solicitudPadre.idSolConcesion;
            //        templateAvisoFiltro.idTipoSolicitudRev = unidadDependenciaMod.solicitudPadre.tipoTramite.id;
            //        templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.UNIDAD_DEPENDENCIA_CAMBIA_ESTADO;

            //        templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            //        if (templateAvisoFiltro != null)
            //        {
            //            Hashtable reemplazos = new Hashtable();
            //            reemplazos.Add("[REEM_1]", unidadDependenciaMod.solicitudPadre.tipoTramite);
            //            reemplazos.Add("[REEM_2]", unidadDependenciaMod.solicitudPadre.numPert);
            //            reemplazos.Add("[REEM_3]", unidadDependenciaMod.unidadDependenciaString());

            //            templateAvisoFiltro.templateSubject = unidadDependenciaMod.solicitudPadre.tipoTramite + " " + unidadDependenciaMod.solicitudPadre.numPert + " " + templateAvisoFiltro.templateSubject;
            //            this.senEmailServiceAlerta(unidadDependenciaMod.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
            //        }

            //    }
            //}
        }

        public void obtenerUnidadesEspacialesVencidas(int idSolicitud)
        {
            List<SolicitudConcesion> listaUnidadesEspacialesVencidas = solicitudDA.ListarSolicitud_ColectorVencido(idSolicitud);
            if (listaUnidadesEspacialesVencidas != null && listaUnidadesEspacialesVencidas.Count > 0)
            {
                foreach (SolicitudConcesion solicitudConcesion in listaUnidadesEspacialesVencidas)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = solicitudConcesion.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = solicitudConcesion.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VENCIMIENTO_UE;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", "Solicitud de Colectores de Semillas");
                        reemplazos.Add("[REEM_2]", solicitudConcesion.numPert + " " + solicitudConcesion.titularesCad);

                        templateAvisoFiltro.templateSubject = "Solicitud de Colectores de Semillas " + solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                    }
                }
            }
        }

        public void supeditadaTerminadaRechazada(int idSolicitud)
        {
            List<DependenciaSupeditados> listaSupeditadaTerminadaRechazada = grupoSuspendidoDA.ListarDependenciaSupeditados_Rechazo(idSolicitud);
            if (listaSupeditadaTerminadaRechazada != null && listaSupeditadaTerminadaRechazada.Count > 0)
            {

                foreach (DependenciaSupeditados dependenciaSupeditados in listaSupeditadaTerminadaRechazada)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = dependenciaSupeditados.solicitudConcesionDep.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = dependenciaSupeditados.solicitudConcesionDep.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SUPEDITADA_TERMINADA_RECHAZADA;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", dependenciaSupeditados.solicitudConcesionDep.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", dependenciaSupeditados.solicitudConcesionDep.numPert);
                        reemplazos.Add("[REEM_3]", "Número y fecha de la resolución que rechaza");
                        reemplazos.Add("[REEM_4]", "número identificador de la solicitud de solicitudes que supeditaba");

                        templateAvisoFiltro.templateSubject = dependenciaSupeditados.solicitudConcesionDep.tipoTramite.descripcion + " " + dependenciaSupeditados.solicitudConcesionDep.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(dependenciaSupeditados.solicitudConcesionDep.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                    }

                }
            }
        }

        public void supeditadaTerminadaAprobada(int idSolicitud)
        {

            List<DependenciaSupeditados> listaSupeditadaTerminadaAprobada = grupoSuspendidoDA.ListarDependenciaSupeditados(idSolicitud);
            if (listaSupeditadaTerminadaAprobada != null && listaSupeditadaTerminadaAprobada.Count > 0)
            {

                foreach (DependenciaSupeditados dependencia in listaSupeditadaTerminadaAprobada)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = dependencia.solicitudConcesionDep.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = dependencia.solicitudConcesionDep.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SUPEDITADA_TERMINADA_APROBADA;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", dependencia.solicitudConcesionDep.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_2]", dependencia.solicitudConcesionDep.numPert);
                        reemplazos.Add("[REEM_3]", dependencia.solicitudConcesionDep.resolucionSSP);
                        reemplazos.Add("[REEM_4]", dependencia.listPertSupeditados);

                        templateAvisoFiltro.templateSubject = dependencia.solicitudConcesionDep.tipoTramite.descripcion + " " + dependencia.solicitudConcesionDep.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(dependencia.solicitudConcesionDep.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                    }

                }
            }
        }

        public void alertaInicioVisacion(List<VisacionMasiva> iniciovisaciones)
        {
            if (iniciovisaciones != null && iniciovisaciones.Count > 0)
            {
                foreach (VisacionMasiva visacionMasiva in iniciovisaciones)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = visacionMasiva.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = visacionMasiva.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.INICIO_VISACION;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", clavesTemplateAviso.INICIO_VISACION);
                        reemplazos.Add("[REEM_2]", visacionMasiva.tipoVisacion.descripcion);
                        reemplazos.Add("[REEM_3]", visacionMasiva.tipoTramite.descripcion);
                        reemplazos.Add("[REEM_4]", visacionMasiva.pert);

                        templateAvisoFiltro.templateSubject = visacionMasiva.tipoTramite.descripcion + " " + visacionMasiva.pert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(visacionMasiva.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                    }

                }
            }
        }
        
        public void alertaVisacionesFirmasCorrecciones(List<VisacionMasiva> visaFirma)
        {
            if (visaFirma != null && visaFirma.Count > 0)
            {
                foreach (VisacionMasiva visacionMasiva in visaFirma)
                {
                    if (visacionMasiva != null)
                    {
                        if (!visacionMasiva.esFirmaJefatura) {

                            //Visación NO
                            if (visacionMasiva.corrige)
                            {

                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = visacionMasiva.idSolConcesion;
                                templateAvisoFiltro.idTipoSolicitudRev = visacionMasiva.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_NO;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.VISACION_NO);
                                    reemplazos.Add("[REEM_2]", visacionMasiva.tipoVisacion.descripcion);
                                    reemplazos.Add("[REEM_3]", visacionMasiva.tipoTramite.descripcion);
                                    reemplazos.Add("[REEM_4]", visacionMasiva.pert);

                                    templateAvisoFiltro.templateSubject = visacionMasiva.tipoTramite.descripcion + " " + visacionMasiva.pert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(visacionMasiva.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }

                            //Visación SI
                            else
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = visacionMasiva.idSolConcesion;
                                templateAvisoFiltro.idTipoSolicitudRev = visacionMasiva.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_SI;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.VISACION_SI);
                                    reemplazos.Add("[REEM_2]", visacionMasiva.tipoVisacion.descripcion);
                                    reemplazos.Add("[REEM_3]", visacionMasiva.tipoTramite.descripcion);
                                    reemplazos.Add("[REEM_4]", visacionMasiva.pert);

                                    templateAvisoFiltro.templateSubject = visacionMasiva.tipoTramite.descripcion + " " + visacionMasiva.pert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(visacionMasiva.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                        }

                        // Firma de Jefatura
                        else {

                            //Firma Jefatura SI
                            if (!visacionMasiva.corrige)
                            {
                                TemplateAviso templateAvisoFiltro = new TemplateAviso();
                                templateAvisoFiltro.idSolicitudRevisada = visacionMasiva.idSolConcesion;
                                templateAvisoFiltro.idTipoSolicitudRev = visacionMasiva.tipoTramite.id;
                                templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.VISACION_FIRMA_JEFATURA;

                                templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                                if (templateAvisoFiltro != null)
                                {
                                    Hashtable reemplazos = new Hashtable();
                                    reemplazos.Add("[REEM_1]", clavesTemplateAviso.VISACION_FIRMA_JEFATURA);
                                    reemplazos.Add("[REEM_2]", visacionMasiva.tipoVisacion.descripcion);
                                    reemplazos.Add("[REEM_3]", visacionMasiva.tipoTramite.descripcion);
                                    reemplazos.Add("[REEM_4]", visacionMasiva.pert);

                                    templateAvisoFiltro.templateSubject = visacionMasiva.tipoTramite.descripcion + " " + visacionMasiva.pert + " " + templateAvisoFiltro.templateSubject;
                                    this.senEmailServiceAlerta(visacionMasiva.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void alertaGrupoSuspendido(GrupoSuspendidos grupoSuspendido)
        {
            List<AsocGrupoSolicitud> solicitudesSuspendidasList = grupoSuspendido.asocGrupoSolicitud;

            if (solicitudesSuspendidasList != null && solicitudesSuspendidasList.Count > 0)
            {
                foreach (AsocGrupoSolicitud asocGrupoSolicitud in solicitudesSuspendidasList)
                {
                    TemplateAviso templateAvisoFiltro = new TemplateAviso();
                    templateAvisoFiltro.idSolicitudRevisada = asocGrupoSolicitud.solicitudConcesion.idSolConcesion;
                    templateAvisoFiltro.idTipoSolicitudRev = asocGrupoSolicitud.solicitudConcesion.tipoTramite.id;
                    templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.SUSPENDIDO_GRUPO_CREADO;

                    templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

                    if (templateAvisoFiltro != null)
                    {
                        Hashtable reemplazos = new Hashtable();
                        reemplazos.Add("[REEM_1]", grupoSuspendido.tipoAgrupacion.descripcion + " " + grupoSuspendido.nombreGrupoSuspend);
                        reemplazos.Add("[REEM_2]", asocGrupoSolicitud.solicitudConcesion.idSolConcesion);
                        reemplazos.Add("[REEM_3]", asocGrupoSolicitud.solicitudConcesion.tipoTramite.descripcion);

                        templateAvisoFiltro.templateSubject = asocGrupoSolicitud.solicitudConcesion.tipoTramite.descripcion + " " + asocGrupoSolicitud.solicitudConcesion.numPert + " " + templateAvisoFiltro.templateSubject;
                        this.senEmailServiceAlerta(asocGrupoSolicitud.solicitudConcesion.idSolConcesion, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);
                    }
                }
            }
        }

        public void alertaCreacionExperimentalesAmerb(UnidadEspacial unidadEspacial)
        {
            /* Enviar un correo cuando se cree un Experimentales Amerb */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                SolicitudConcesion solicitudAmerbFull = solicitudDA.ObtieneCamposSolicitudAviso(unidadEspacial.idSolicitud);

                if (solicitudAmerbFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudAmerbFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudAmerbFull.titularesCad); // Titulares
                    reemplazos.Add("[REEM_3]", solicitudAmerbFull.tipoSolicitudCad); // Tipo Concesión
                    reemplazos.Add("[REEM_4]", unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudAmerbFull.resolucionSSFFAA); // Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudAmerbFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Experimentales Amerb " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }
        }

        public void alertaCreacionExperimentalesConcesion(UnidadEspacial unidadEspacial)
        {
            /* Enviar un correo cuando se cree un Experimentales de Concesión */
            TemplateAviso templateAvisoFiltro = new TemplateAviso();
            templateAvisoFiltro.idSolicitudRevisada = unidadEspacial.idSolicitud;
            templateAvisoFiltro.claveTemplateAviso = clavesTemplateAviso.CREAR_CONCESION;

            templateAvisoFiltro = templateAvisoDA.ObtieneTemplateAvisoDestinatario(templateAvisoFiltro);

            if (templateAvisoFiltro != null)
            {
                Hashtable reemplazos = new Hashtable();
                SolicitudConcesion solicitudAmerbFull = solicitudDA.ObtieneCamposSolicitudAviso(unidadEspacial.idSolicitud);

                if (solicitudAmerbFull != null)
                {
                    reemplazos.Add("[REEM_1]", solicitudAmerbFull.numPert); // Pert
                    reemplazos.Add("[REEM_2]", solicitudAmerbFull.titularesCad); // Titulares
                    reemplazos.Add("[REEM_3]", solicitudAmerbFull.tipoSolicitudCad); // Tipo Concesión
                    reemplazos.Add("[REEM_4]", unidadEspacial.centrosDeCultivo.codigoCentro); // Código de Centro
                    reemplazos.Add("[REEM_5]", solicitudAmerbFull.resolucionSSFFAA); // Resolución SSFFAA
                    reemplazos.Add("[REEM_6]", solicitudAmerbFull.resolucionSSP); // Resolución SSP

                    templateAvisoFiltro.templateSubject = "Experimentales de Concesión " + unidadEspacial.centrosDeCultivo.codigoCentro + " " + templateAvisoFiltro.templateSubject;
                    this.senEmailServiceAlerta(unidadEspacial.idSolicitud, templateAvisoFiltro.claveTemplateAviso, templateAvisoFiltro.templateSubject, true, reemplazos, templateAvisoFiltro.templateCuerpo, templateAvisoFiltro.destinatariosTemplateComa);

                }
            }
        }
    }
}
