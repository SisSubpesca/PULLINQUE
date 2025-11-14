using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using Datos.Entidades;
using Datos.Contantes;

namespace Datos.Utilidades
{
    public class Funciones
    {

        public static int PROYECTO_TECNICO = 1;
        public static int ANTECEDENTES_DEL_SECTOR = 2;

        public string ConvertToRomano(int num)
        {
            string num_romano = "";
            switch (num)
            {
                case 1:
                    num_romano = "I";
                    break;
                case 2:
                    num_romano = "II";
                    break;
                case 3:
                    num_romano = "III";
                    break;
                case 4:
                    num_romano = "IV";
                    break;
                case 5:
                    num_romano = "V";
                    break;
                case 6:
                    num_romano = "VI";
                    break;
                case 7:
                    num_romano = "VII";
                    break;
                case 8:
                    num_romano = "VIII";
                    break;
                case 9:
                    num_romano = "IX";
                    break;
                case 10:
                    num_romano = "X";
                    break;
                case 11:
                    num_romano = "XI";
                    break;
                case 12:
                    num_romano = "XII";
                    break;
                case 13:
                    num_romano = "RM";
                    break;
                case 14:
                    num_romano = "XIV";
                    break;
                case 15:
                    num_romano = "XV";
                    break;
            };

            return num_romano;
        }
        public bool Pertenece(int numero, int[] array)
        {
            bool resp = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == numero)
                {
                    resp = true;
                    break;
                };
            };
            return resp;
        }

        public ListBox ListBox_ItemsSelected(ListBox lbox, string items)
        {
            ListItemCollection list = lbox.Items;
            int id_item = 0;
            int pos_separador = 0;

            while (items != "")
            {
                pos_separador = items.IndexOfAny(new char[] { ',' });
                if (pos_separador >= 0)
                {
                    id_item = Convert.ToInt32(items.Substring(0, pos_separador));
                    items = items.Substring(pos_separador + 1, items.Length - pos_separador - 1);
                }
                else
                {
                    id_item = Convert.ToInt32(items);
                    items = "";
                };

                foreach (ListItem item in list)
                {
                    if (item.Value == Convert.ToString(id_item))
                    {
                        item.Selected = true;
                    };
                };
            };

            return lbox;
        }
        public int ListBox_ItemsSelectedCount(ListItemCollection list)
        {
            int items_selected = 0;
            foreach (ListItem item in list)
            {
                if (item.Selected)
                {
                    items_selected = items_selected + 1;
                };
            };

            return items_selected;
        }        
        public ListBox ListBox_ItemsSelectedNone(ListBox lbox)
        {
            ListItemCollection list = lbox.Items;
            foreach (ListItem item in list)
            {
                if (item.Selected)
                {
                    item.Selected = false;
                };
            };

            return lbox;
        }
        public string ListBox_ItemsSelectedGET(ListItemCollection list)
        {
            string items = "";
            int count = 0;
            int num_items = list.Count;
            foreach (ListItem item in list)
            {
                if (item.Selected && !item.Value.Trim().Equals("0") && !item.Value.Trim().Equals("-1"))
                {
                    count = count + 1;
                    if (count == 1)
                    {
                        items = item.Value;
                    }
                    else
                    {
                        if (items.Length + 1 + item.Value.Length <= 5000)
                        {
                            items = items + "," + item.Value;
                        }
                        else
                        {
                            break;
                        };
                    };
                };
            }

            return items;
        }

        public Panel muestraErrores(List<string> errores_list, Panel Errores)
        {
            Errores.Controls.Clear();
            HtmlGenericControl ul = new HtmlGenericControl("ul");
            foreach (string error in errores_list)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.InnerHtml = error;
                ul.Controls.Add(li);
            };

            string error_sql = (string)HttpContext.Current.Session["error"];
            if (error_sql != null)
            {
                Errores.Controls.Add(new LiteralControl("Se ha producido un error SQL:"));
                HttpContext.Current.Session.Remove("error");
            }
            else
            {
                Errores.Controls.Add(new LiteralControl("Ingrese valores válidos en los siguientes campos:"));
            };
            Errores.Controls.Add(ul);
            Errores.Visible = true;

            return Errores;
        }

        public string Mes(int mes)
        {
            string mes_text = "";
            switch (mes)
            { 
                case 1:
                    mes_text = "enero";
                    break;
                case 2:
                    mes_text = "febrero";
                    break;
                case 3:
                    mes_text = "marzo";
                    break;
                case 4:
                    mes_text = "abril";
                    break;
                case 5:
                    mes_text = "mayo";
                    break;
                case 6:
                    mes_text = "junio";
                    break;
                case 7:
                    mes_text = "julio";
                    break;
                case 8:
                    mes_text = "agosto";
                    break;
                case 9:
                    mes_text = "septiembre";
                    break;
                case 10:
                    mes_text = "octubre";
                    break;
                case 11:
                    mes_text = "noviembre";
                    break;
                case 12:
                    mes_text = "diciembre";
                    break;
            };

            return mes_text; 
        }

        public bool EsPar(int num)
        {
            bool resp = false;
            if ((num % 2) == 0)
            {
                resp = true;
            };

            return resp;
        }

        public String retornaModulo()
        {

            string segmento = "";
            string modulo = "";
            string nombre_archivo = "";
            string ext = "";
            int i = 0;
            int pos = 0;

            foreach (string S in HttpContext.Current.Request.Url.Segments)
            {
                segmento = HttpContext.Current.Request.Url.Segments[i];
                pos = segmento.LastIndexOf(".");
                if (pos > 0)
                {
                    ext = segmento.Substring(pos);
                    ext = ext.Replace(".", "");
                    if (ext == "aspx")
                    {
                        nombre_archivo = segmento;
                        break;
                    };
                };
                i++;
            };

            modulo = HttpContext.Current.Request.Url.Segments[i - 1];
            modulo = modulo.Replace("/", "");
            return modulo;
        }

        public String retornaTipoModulo() {

            string segmento = "";
            string tipoModulo = "";
            string nombre_archivo = "";
            string ext = "";
            int i = 0;
            int pos = 0;

            foreach (string S in HttpContext.Current.Request.Url.Segments)
            {
                segmento = HttpContext.Current.Request.Url.Segments[i];
                pos = segmento.LastIndexOf(".");
                if (pos > 0)
                {
                    ext = segmento.Substring(pos);
                    ext = ext.Replace(".", "");
                    if (ext == "aspx")
                    {
                        nombre_archivo = segmento;
                        break;
                    };
                };
                i++;
            };

            tipoModulo = HttpContext.Current.Request.Url.Segments[i - 2];
            tipoModulo = tipoModulo.Replace("/", "");
            return tipoModulo;
        }

        public String retornaPagina()
        {

            string segmento = "";
            string nombre_archivo = "";
            string ext = "";
            int i = 0;
            int pos = 0;

            foreach (string S in HttpContext.Current.Request.Url.Segments)
            {
                segmento = HttpContext.Current.Request.Url.Segments[i];
                pos = segmento.LastIndexOf(".");
                if (pos > 0)
                {
                    ext = segmento.Substring(pos);
                    ext = ext.Replace(".", "");
                    if (ext == "aspx")
                    {
                        nombre_archivo = segmento;
                        break;
                    };
                };
                i++;
            };
            return nombre_archivo;
        }

        public void seteaTipoModificacion(SolicitudConcesion solicitudConcesion, ValidacionDocumentacion validacionDocumentacion)
        {
            if (solicitudConcesion != null)
            {
                if (solicitudConcesion.tipoModificacionesTram != null && solicitudConcesion.tipoModificacionesTram.Count > 0)
                {
                    foreach (ParametroGenerico tipoModificacion in solicitudConcesion.tipoModificacionesTram)
                    {

                        //MODIFICACIONES DE CONCESION DE ACUICULTURA
                        if (tipoModificacion.id == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE) 
                        {
                            //validacionDocumentacion.aplicaConcesion = 1;
                            validacionDocumentacion.aplicaModAmpliacion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CONCESION_ESPECIE)
                        {
                            //validacionDocumentacion.aplicaConcesion = 1;
                            validacionDocumentacion.aplicaModEspeciePT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CONCESION_PT)
                        {
                            //validacionDocumentacion.aplicaConcesion = 1;
                            validacionDocumentacion.aplicaModEspeciePT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE)
                        {
                            //validacionDocumentacion.aplicaConcesion = 1;
                            validacionDocumentacion.aplicaModReduccion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                        {
                            //validacionDocumentacion.aplicaConcesion = 1;
                            validacionDocumentacion.aplicaModRegularizacion = 1;
                        }

                        //MODIFICACIONES DE ACOPIO
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaAcopio = 1;
                            validacionDocumentacion.aplicaModAmpliacion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION)
                        {
                            validacionDocumentacion.aplicaAcopio = 1;
                            validacionDocumentacion.aplicaModEspeciePT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE)
                        {
                            validacionDocumentacion.aplicaAcopio = 1;
                            validacionDocumentacion.aplicaModPT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaAcopio = 1;
                            validacionDocumentacion.aplicaModReduccion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION)
                        {
                            validacionDocumentacion.aplicaAcopio = 1;
                            validacionDocumentacion.aplicaModRegularizacion = 1;
                        }

                        //MODIFICACIONES DE FAENAMIENTO
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaFaenamiento = 1;
                            validacionDocumentacion.aplicaModAmpliacion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION)
                        {
                            validacionDocumentacion.aplicaFaenamiento = 1;
                            validacionDocumentacion.aplicaModEspeciePT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE)
                        {
                            validacionDocumentacion.aplicaFaenamiento = 1;
                            validacionDocumentacion.aplicaModPT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaFaenamiento = 1;
                            validacionDocumentacion.aplicaModReduccion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                        {
                            validacionDocumentacion.aplicaFaenamiento = 1;
                            validacionDocumentacion.aplicaModRegularizacion = 1;
                        }

                          //MODIFICACIONES DE ECMPO
                        else if (tipoModificacion.id == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                            validacionDocumentacion.aplicaModAmpliacion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                            validacionDocumentacion.aplicaModReduccion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_ECMPO_ESPECIE)
                        {
                            validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                            validacionDocumentacion.aplicaModEspeciePT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_ECMPO_PT)
                        {
                            validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                            validacionDocumentacion.aplicaModPT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_ECMPO_REGULARIZACION)
                        {
                            validacionDocumentacion.aplicaAcuiculturaEcmpo = 1;
                            validacionDocumentacion.aplicaModRegularizacion = 1;
                        }


                             //MODIFICACIONES DE AMERB
                        else if (tipoModificacion.id == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaAmerb = 1;
                            validacionDocumentacion.aplicaModAmpliacion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE)
                        {
                            validacionDocumentacion.aplicaAmerb = 1;
                            validacionDocumentacion.aplicaModReduccion = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_AMERB_ESPECIE)
                        {
                            validacionDocumentacion.aplicaAmerb = 1;
                            validacionDocumentacion.aplicaModEspeciePT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_AMERB_PT)
                        {
                            validacionDocumentacion.aplicaAmerb = 1;
                            validacionDocumentacion.aplicaModPT = 1;
                        }
                        else if (tipoModificacion.id == rbTipo.MOD_AMERB_REGULARIZACION)
                        {
                            validacionDocumentacion.aplicaAmerb = 1;
                            validacionDocumentacion.aplicaModRegularizacion = 1;
                        }



                    }
                }    
            }
        }

        public string listaTitularesComa(List<Solicitante> listaSolicitantes) {

            string listaTitularesComa = "";
            int i = 0;

            if (listaSolicitantes != null && listaSolicitantes.Count > 0)
            {
                foreach (Solicitante solicitante in listaSolicitantes)
                {
                    if (i == 0)
                    {
                        listaTitularesComa = solicitante.rut + "-" + solicitante.dv + " " + solicitante.nombreSolicitante;
                    }
                    else
                    {
                        listaTitularesComa = listaTitularesComa + "," + solicitante.rut + "-" + solicitante.dv + " " + solicitante.nombreSolicitante;
                    }
                    i++;
                }
            }
            return listaTitularesComa;
        }



        public bool tieneTipoModificacionRegularizacion(List<ParametroGenerico> listTipoModificacion)
        {
            if (listTipoModificacion != null && listTipoModificacion.Count > 0) {

                foreach (ParametroGenerico tipoModificacion in listTipoModificacion)
                {
                    if (tipoModificacion != null && tipoModificacion.id == rbTipo.MOD_CONCESION_REGULARIZACION)
                    {
                        return true;
                    }
                }
            }

            return false;
        }




        /**
         * EN BASE AL TIPO DE FLUJO OBTIENE EL TIPO DE UNIDAD ESPACIAL CORRESPONDIENTE 
         */
        public int equivalenciaIdTipoUE(int idTipoFlujo)
        {


            if (idTipoFlujo == rbTipo.ACUICULTURA_EN_AMERB) //119
            {
                return rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) //537
            {
                return rbTipo.UNID_ESPACIAL_ECMPO;
            }

            else if (idTipoFlujo == rbTipo.UNID_ESPACIAL_MOD_CENTRO_ACOPIO) //118
            {
                return rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO;
            }

            else if (idTipoFlujo == rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO) //37
            {
                return rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO;
            }

            else if (idTipoFlujo == rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA) //120
            {
                return rbTipo.UNID_ESPACIAL_COLECTORES_DE_SEMILLA;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA) //88
            {
                return rbTipo.UNID_ESPACIAL_CONCESION;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) //535
            {
                return rbTipo.UNID_ESPACIAL_EXPERIMENTALES_AMERB;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION) //536
            {
                return rbTipo.UNID_ESPACIAL_EXPERIMENTALES_CONCESION;
            }

            else if (idTipoFlujo == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_AMERB_ESPECIE || idTipoFlujo == rbTipo.MOD_AMERB_PT || idTipoFlujo == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_AMERB_REGULARIZACION) //555 //553 //554 //556 //557
            {
                return rbTipo.UNID_ESPACIAL_ACUICULTURA_EN_AMERB;
            }

            else if (idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) //549 //547 //548  //550 //551
            {
                return rbTipo.UNID_ESPACIAL_CENTRO_DE_ACOPIO;
            }

            else if (idTipoFlujo == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CONCESION_ESPECIE || idTipoFlujo == rbTipo.MOD_CONCESION_PT || idTipoFlujo == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CONCESION_REGULARIZACION) //92 //90 //91 //93 //94
            {
                return rbTipo.UNID_ESPACIAL_CONCESION;
            }

            else if (idTipoFlujo == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_ECMPO_ESPECIE || idTipoFlujo == rbTipo.MOD_ECMPO_PT || idTipoFlujo == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_ECMPO_REGULARIZACION) //561 //559 //560 //562 //563
            {
                return rbTipo.UNID_ESPACIAL_ECMPO;
            }

            else if (idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) //543 //541 //542 //544  //545
            {
                return rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO;
            }

            else if (idTipoFlujo == rbTipo.RELOCALIZACION_CREA_RESA || idTipoFlujo == rbTipo.RELOCALIZACION_FUSIONA_RESA || idTipoFlujo == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA) //613 //614 //612
            {
                return rbTipo.UNID_ESPACIAL_CONCESION;
            }

            if (idTipoFlujo == rbTipo.RELOCALIZACION_CREA || idTipoFlujo == rbTipo.RELOCALIZACION_FUSIONA || idTipoFlujo == rbTipo.RELOCALIZACION_SECTOR_CERO) //86 //87 //85
            {
                return rbTipo.UNID_ESPACIAL_CONCESION;
            }

            return -1;
        }



        /**
         * EN BASE AL TIPO DE FLUJO OBTIENE EL TIPO DE TRAMITE CORRESPONDIENTE 
         */
        public int equivalenciaIdTipoTramite(int idTipoFlujo)
        {


            if (idTipoFlujo == rbTipo.ACUICULTURA_EN_AMERB) //119
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO) //537
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO;
            }

            else if (idTipoFlujo == rbTipo.UNID_ESPACIAL_MOD_CENTRO_ACOPIO) //118
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO;
            }

            else if (idTipoFlujo == rbTipo.UNID_ESPACIAL_CENTRO_DE_FAENAMIENTO) //37
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO;
            }

            else if (idTipoFlujo == rbTipo.COLECTORES_DE_SEMILLA) //120
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA) //88
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB) //535
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB;
            }

            else if (idTipoFlujo == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION) //536
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION;
            }

            else if (idTipoFlujo == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_AMERB_ESPECIE || idTipoFlujo == rbTipo.MOD_AMERB_PT || idTipoFlujo == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_AMERB_REGULARIZACION) //555 //553 //554 //556 //557
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB;
            }

            else if (idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) //549 //547 //548  //550 //551
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO;
            }

            else if (idTipoFlujo == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CONCESION_ESPECIE || idTipoFlujo == rbTipo.MOD_CONCESION_PT || idTipoFlujo == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CONCESION_REGULARIZACION) //92 //90 //91 //93 //94
            {
                return rbTipo.TIPO_TRAMITE_MODIFICACION;
            }

            else if (idTipoFlujo == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_ECMPO_ESPECIE || idTipoFlujo == rbTipo.MOD_ECMPO_PT || idTipoFlujo == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_ECMPO_REGULARIZACION) //561 //559 //560 //562 //563
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO;
            }

            else if (idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) //543 //541 //542 //544  //545
            {
                return rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO;
            }

            else if (idTipoFlujo == rbTipo.RELOCALIZACION_CREA_RESA || idTipoFlujo == rbTipo.RELOCALIZACION_FUSIONA_RESA || idTipoFlujo == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA) //613 //614 //612
            {
                return rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION_RESA;
            }

            if (idTipoFlujo == rbTipo.RELOCALIZACION_CREA || idTipoFlujo == rbTipo.RELOCALIZACION_FUSIONA || idTipoFlujo == rbTipo.RELOCALIZACION_SECTOR_CERO) //86 //87 //85
            {
                return rbTipo.TIPO_TRAMITE_SECTOR_RELOCALIZACION;
            }

            return -1;
        }


        /**
         * EN BASE AL TIPO DE FLUJO OBTIENE EL TIPO DE TIPO DE MODIFICACION O RELOCALIZACION CORRESPONDIENTE 
         */
        public int equivalenciaIdSubTipoTramite(int idTipoFlujo)
        {


            if (idTipoFlujo == rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_AMERB_ESPECIE || idTipoFlujo == rbTipo.MOD_AMERB_PT || idTipoFlujo == rbTipo.MOD_AMERB_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_AMERB_REGULARIZACION) //555 //553 //554 //556 //557
            {
                return idTipoFlujo;
            }

            else if (idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_RENOVACION || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) //549 //547 //548  //550 //551
            {
                return idTipoFlujo;
            }

            else if (idTipoFlujo == rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CONCESION_ESPECIE || idTipoFlujo == rbTipo.MOD_CONCESION_PT || idTipoFlujo == rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CONCESION_REGULARIZACION) //92 //90 //91 //93 //94
            {
                return idTipoFlujo;
            }

            else if (idTipoFlujo == rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_ECMPO_ESPECIE || idTipoFlujo == rbTipo.MOD_ECMPO_PT || idTipoFlujo == rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_ECMPO_REGULARIZACION) //561 //559 //560 //562 //563
            {
                return idTipoFlujo;
            }

            else if (idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_RENOVACION || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE || idTipoFlujo == rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION) //543 //541 //542 //544  //545
            {
                return idTipoFlujo;
            }


            else if (idTipoFlujo == rbTipo.RELOCALIZACION_CREA_RESA || idTipoFlujo == rbTipo.RELOCALIZACION_FUSIONA_RESA || idTipoFlujo == rbTipo.RELOCALIZACION_SECTOR_CERO_RESA) //613 //614 //612
            {
                return idTipoFlujo;
            }

            if (idTipoFlujo == rbTipo.RELOCALIZACION_CREA || idTipoFlujo == rbTipo.RELOCALIZACION_FUSIONA || idTipoFlujo == rbTipo.RELOCALIZACION_SECTOR_CERO) //86 //87 //85
            {
                return idTipoFlujo;
            }

            return -1;
        }



        public static bool EsTramiteUE(int idTipoTramiteAux)
        {

            if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_CONCESION_ACUICULTURA) 
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_ACOPIO)
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_ACUICULTURA_EN_AMERB)
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_COLECTORES_DE_SEMILLA)
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_CENTRO_DE_FAENAMIENTO)
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_AMERB)
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_EXPERIMENTALES_CONCESION)
            {
                return true;
            }
            else if (idTipoTramiteAux == rbTipo.TIPO_TRAMITE_SOLICITUD_ECMPO)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /*
        *  DETERMINA SI LA SOLICITUD DEBE QUEDAR EN MODO "VER" PARA PT Y ANTECEDENTES DEL SECTOR
        *  si tiene asignada la solicitud se debe evaluar si es un tipo de modificación, y dependiendo del del tipo de modificacion se determinará si puede
        *  modificar las secciones de Proyecto técnico y Antededentes del Sector 
        *  •	Las modificaciones de Especie y Proyecto Técnico solo se debe poder modificar Proyecto Técnico.
        *  •	Ampliación, Reducción Y Regularización solo debe poder modificar Antecedentes del Sector.
        *  •	La modificación del tipo renovación solo reemplaza las fechas de vigencia de la unidad espacial.
        * seccion = 1 PT, 2 Antecedentes del sector
        */
        public static void AplicarReglaTipoModificacion(SolicitudConcesion solicitudInicial, int seccion)
        {

            if (solicitudInicial != null && solicitudInicial.tieneAsignadaSolicitud == true)
            {

                if (solicitudInicial.tipoTramite != null && solicitudInicial.tipoTramite.id > 0)
                {
                    if (solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_MODIFICACION || solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_AMERB || solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_ACOPIO || solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_CENTRO_FAENAMIENTO || solicitudInicial.tipoTramite.id == rbTipo.TIPO_TRAMITE_SOLICITUD_MOD_ECMPO)
                    {

                        List<ParametroGenerico> tipoModificacionList = solicitudInicial.tipoModificacionesTram;

                        if (tipoModificacionList != null)
                        {

                            HashSet<Int32> tipoModificacionHash = new HashSet<Int32>();
                            foreach (ParametroGenerico tipoModificacion in tipoModificacionList)
                            {
                                tipoModificacionHash.Add(tipoModificacion.id);
                            }

                            //PROYECTO TECNICO
                            if (seccion == Funciones.PROYECTO_TECNICO && !(
                                
                                tipoModificacionHash.Contains(rbTipo.MOD_CONCESION_PT) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_AMERB_PT) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_ECMPO_PT) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_ACOPIO_PT_ESPECIE) || 
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_FAENAMIENTO_PT_ESPECIE) || 

                                tipoModificacionHash.Contains(rbTipo.MOD_CONCESION_ESPECIE) || 
                                tipoModificacionHash.Contains(rbTipo.MOD_AMERB_ESPECIE) || 
                                tipoModificacionHash.Contains(rbTipo.MOD_ECMPO_ESPECIE) 
                                //renovacion no puede modificar PT
                                )
                                )
                            {
                                solicitudInicial.tieneAsignadaSolicitud = false;
                            }

                            //ANTEDEDENTES DEL SECTOR
                            if (seccion == Funciones.ANTECEDENTES_DEL_SECTOR && !(

                                tipoModificacionHash.Contains(rbTipo.MOD_CONCESION_REDUCE_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_AMERB_REDUCE_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_ECMPO_REDUCE_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_ACOPIO_REDUCE_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_FAENAMIENTO_REDUCE_SUPERFICIE) ||

                                tipoModificacionHash.Contains(rbTipo.MOD_CONCESION_AMPLIA_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_AMERB_AMPLIA_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_ECMPO_AMPLIA_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_ACOPIO_AMPLIA_SUPERFICIE) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_FAENAMIENTO_AMPLIA_SUPERFICIE) ||

                                tipoModificacionHash.Contains(rbTipo.MOD_CONCESION_REGULARIZACION) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_AMERB_REGULARIZACION) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_ECMPO_REGULARIZACION) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_ACOPIO_REGULARIZACION) ||
                                tipoModificacionHash.Contains(rbTipo.MOD_CENTRO_FAENAMIENTO_REGULARIZACION)
                                )
                                )
                            {
                                solicitudInicial.tieneAsignadaSolicitud = false;
                                
                            }
                        }
                    }
                }
            }
        }


    }

}
