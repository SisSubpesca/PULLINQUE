using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos.Entidades
{
    [Serializable()]
    public class TemplateAviso
    {

        public int idDestTemplate { get; set; }
        public string claveTemplateAviso { get; set; }
        public ParametroGenerico seccion { get; set; }
        public ParametroGenerico tipoFlujoDocumental { get; set; }
        public ParametroGenerico requerimiento { get; set; }
        public string descrTemplateAviso { get; set; }
        public string descrReempl { get; set; }
        public string templateSubject { get; set; }
        public string templateCuerpo { get; set; }
        public string descrTemplateCuerpo { get; set; }
        public int idSolicitudRevisada { get; set; }
        public int idTipoSolicitudRev { get; set; }
        public List<DestinatarioTemplate> destinatariosTemplate { get; set; }
        public List<DestinatarioTemplate> destinatariosRol { get; set; }
        public bool aplicaEnvio { get; set; }
        public string claveTemplateAvisoDescr { get; set; }

        public TemplateAviso()
        {
        }

        public string aplicaEnvioString
        {

            get
            {
                if (aplicaEnvio)
                    return "Si";
                return "No";
            }
        }

        public string destinatariosTemplateComa {

            get {

                string aux = "";
                int i = 0;
                foreach (DestinatarioTemplate destinatarioTemplate in destinatariosTemplate)
                {
                    //if (destinatarioTemplate.aplicaEnvio)
                    //{
                        if (i == 0)
                        {
                            if (destinatarioTemplate.emailDestinatario != null && !destinatarioTemplate.emailDestinatario.Equals(""))
                            {
                                aux = destinatarioTemplate.emailDestinatario;
                            }
                            else
                            {

                                aux = destinatarioTemplate.usuario.correo;
                            }
                        }

                        else
                        {
                            if (destinatarioTemplate.emailDestinatario != null && !destinatarioTemplate.emailDestinatario.Equals(""))
                            {
                                aux = aux + "," + destinatarioTemplate.emailDestinatario;
                            }
                            else
                            {
                                aux = aux + "," + destinatarioTemplate.usuario.correo;
                            }
                        }

                        i++;
                    }

                //}
                return aux;
            }



        }

        public string destinatariosDireccionTemplateComa
        {

            get
            {

                string aux = "";
                int i = 0;
                foreach (DestinatarioTemplate destinatarioTemplate in destinatariosTemplate)
                {
                    if (destinatarioTemplate.emailDestinatario != null && !destinatarioTemplate.emailDestinatario.Equals(""))
                    {
                        if (i == 0)
                        {
                            aux = destinatarioTemplate.emailDestinatario;
                        }

                        else
                        {
                            aux = aux + "," + destinatarioTemplate.emailDestinatario;
                        }

                    }
                    i++;

                }

                return aux;
            }



        }
        
        public string destinatariosRolTemplateComa
        {

            get
            {

                string aux = "";
                int i = 0;
                foreach (DestinatarioTemplate destinatarioTemplate in destinatariosRol)
                {
                    if (destinatarioTemplate != null && destinatarioTemplate.rol != null && destinatarioTemplate.rol.idRol > 0)
                    {
                        if (i == 0)
                        {
                            aux = destinatarioTemplate.rol.nombreRol;
                        }

                        else
                        {
                            aux = aux + "," + destinatarioTemplate.rol.nombreRol;
                        }
                    }

                    i++;
                }

                return aux;
            }



        }

        [Serializable]
        public class Serializable // (Atributos encapsulados en una subclase con el fin de ser guardados dentro de un ViewState)
        {
            public int idDestTemplate { get; set; }
            public string claveTemplateAviso { get; set; }
            public ParametroGenerico seccion { get; set; }
            public ParametroGenerico tipoFlujoDocumental { get; set; }
            public ParametroGenerico requerimiento { get; set; }
            public string descrTemplateAviso { get; set; }
            public string descrTemplateCuerpo { get; set; }
            public string descrReempl { get; set; }
            public string templateSubject { get; set; }
            public string templateCuerpo { get; set; }
            public int idSolicitudRevisada { get; set; }
            public int idTipoSolicitudRev { get; set; }
            public List<DestinatarioTemplate> destinatariosTemplate { get; set; }
            public List<DestinatarioTemplate> destinatariosRol { get; set; }
            public bool aplicaEnvio { get; set; }
            public string claveTemplateAvisoDescr { get; set; }
        }
    }
}
