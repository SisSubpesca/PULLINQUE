using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicaNegocio.cl.subpesca.rb.errores;
using Datos.Entidades;
using System.Data;
using Datos.Contantes;

namespace LogicaNegocio.cl.subpesca.rb.servicios.indicadores
{
    public class IndicadoresService
    {

        Logger logger = new Logger();
        Indicadores indicadores = new Indicadores();

      

      

        public DataTable Indicadores_Ver(int id_indicador)
        {
            try
            {
                return indicadores.VerIndicadores(id_indicador);
            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }





       //NUEVOS INDICADORES

        public Indicador ObtenerIndicador(int id_indicador)
        {
            try
            {

                Indicador indicador = null;

                if (id_indicador == TipoIndicadorP3.INDICADOR_1_PORC_INSPECCION_TERRENO) {

                    indicador = new Indicador();
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_1_PORC_INSPECCION_TERRENO;
                    indicador.nombreIndicador = "Porcentaje de respuesta de SERNAPESCA a los requerimientos de Inspección en terreno";
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador01.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";


                    indicador.consideraciones = " * En el denominador se incluyen todos los requerimientos vigentes de inspección a terreno pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'" + 
                                                "<br> * En el numerador se incluyen todos los requerimientos vigentes y con respuesta de inspección a terreno respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> * El Universo del numerador está contenido en el denominador" +
                                                "<br> * Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";
                                                


                    indicador.fechaNumerador_desde  = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde  = true;
                    indicador.fechaDenominador_hasta  = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;


                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_2_PORC_DIFROL)
                {

                    indicador = new Indicador();
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_2_PORC_DIFROL;
                    indicador.nombreIndicador = "Porcentaje de respuestas de DIFROL";
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador02.jpg";
                    
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";


                    indicador.consideraciones = " *  En el denominador se incluyen todos los requerimientos vigentes de Autorización Difrol pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'."+
                                                "<br> *  En el numerador se incluyen todos los requerimientos vigentes y con respuesta de Autorización Difrol  respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' "+
                                                "<br> *  El Universo del numerador está contenido en el denominador"+
                                                "<br> *  Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_3_PORC_DIRINMAR)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de planos visados por DIRINMAR";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_3_PORC_DIRINMAR;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador03.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones =  "*  En el denominador se incluyen todos los requerimientos vigentes de Oficio Visación planos pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todos los requerimientos vigentes y con respuesta de Oficio Visación planos  respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> *  El Universo del numerador está contenido en el denominador" +
                                                "<br> *  Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";
                    


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_4_PORC_ITC_UOT_POS)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de ITC UOT con resultado positivos en relación al total de solicitudes evaluadas por UOT";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_4_PORC_ITC_UOT_POS;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador04.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " *  En el denominador se incluyen todos los ITC UOT vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." + 
                                                "<br> *  En el numerador se incluyen todos los ITC UOT, con resultado aprueba,  emitidos  dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  El Universo del numerador está contenido en el denominador" + 
                                                "<br> *  Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";
                                                


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_5_PORC_PASAR_EMISION_RADIAL)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Porcentaje de solicitudes que pueden pasar a la etapa de emisión radial";
                    indicador.nombreIndicador = "Porcentaje de solicitudes pendientes de emisión radial";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_5_PORC_PASAR_EMISION_RADIAL;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador05.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = "*  En el denominador se incluyen todas la solicitudes con ITC UOT con resultado aprueba  e Informe de Banco Natural  vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todas la solicitudes con Informe de banco natural, con resultado  sin banco,  vigentes y conformes emitidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  Las solicitudes no deben contar con emisión radial hecha para ser consideras dentro del indicador" +
                                                "<br> *  El Universo del numerador está contenido en el denominador." +
                                                "<br> *  Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_6_PORC_CON_EMISION_RADIAL)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Porcentaje de solicitudes con emisión radial y publicación paginas web, en período t.";
                    indicador.nombreIndicador = "Porcentaje de solicitudes con emisión radial y publicación paginas web, en período t.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_6_PORC_CON_EMISION_RADIAL;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador06.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todas la solicitudes con informe de Banco Natural, con resultado sin banco, vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluyen todas la solicitudes con Difusión de banco natural y Publicación web,  vigentes y conformes emitidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> * El Universo del numerador está contenido en el denominador. " +
                                                "<br> * Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador. ";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_7_PORC_UOT_DAC)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes que tienen Informe DAC, respecto del total de solicitudes que tienen Informe UOT. ";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_7_PORC_UOT_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador07.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes con ITC UOT vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todas las solicitudes con Informe DAC vigentes emitidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  El Universo del numerador está contenido en el denominador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_8_PORC_DAC_SSP)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con Resolución SUBPESCA, respecto del total de solicitudes que tienen Informe DAC.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_8_PORC_DAC_SSP;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador08.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con Informe DAC vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluyen todas las solicitudes con Resolución SSP vigentes emitidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> * El Universo del numerador está contenido en el denominador.";


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_9_PORC_CUATRO_ANIOS)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de casos no resueltos por DAC que poseen un tiempo promedio de tramitación mayor de 4 años.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_9_PORC_CUATRO_ANIOS;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador09.jpg";
                    indicador.descripcionResultado = "porcentaje de casos que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes que a la fecha de consulta lleven cuatro años o más de tramitación desde la fecha de ingreso de la solicitud." +
                                                "<br> * En el numerador se incluyen todas las solicitudes que a la fecha de consulta no poseen Informe DAC vigente emitido, o fecha del Informe DAC es mayor a la fecha de consulta." +
                                                "<br> * El Universo del numerador está contenido en el denominador.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = false;
                    indicador.fechaDenominador_hasta = false;
                    indicador.fechaConsulta = true;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_10_PORC_JURIDICA_DAC)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Porcentaje de solicitudes con IT DAC rectificados o complementados reenviadas a Jurídica.";
                    indicador.nombreIndicador = "Porcentaje de solicitudes con IT DAC emitidos respecto de las devueltas por Jurídica.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_10_PORC_JURIDICA_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador10.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes con Devoluciones de Jurídica  vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluyen todas las solicitudes con nuevo Informe DAC o  Informe DAC complementario vigentes emitidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> * La fecha del nuevo Informe DAC o Informe DAC complementario debe ser mayor a la fecha de la devolución de jurídica." + 
                                                "<br> * El Universo del numerador está contenido en el denominador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }


                else if (id_indicador == TipoIndicadorP3.INDICADOR_11_PORC_CERTIFICADO_OPERACION)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de Certificados de operación correctamente emitidos por el servicio en periodo N";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_11_PORC_CERTIFICADO_OPERACION;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador11.jpg";
                    indicador.descripcionResultado = "porcentaje de certificados que cumplen con el criterio";

                    indicador.consideraciones = " *  En el denominador se incluyen todas las solicitudes con  requerimientos de Certificado de operación vigentes pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todos las solicitudes  con respuesta a requerimientos de Certificado de operación vigentes dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> *  El Universo del numerador está contenido en el denominador";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_12_TP_DAC)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio para emitir informe DAC Aprueba, en período t.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_12_TP_DAC;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador12.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes con  Informe DAC Aprueba vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluye la suma de los días que demora una solicitud en obtener su Informe DAC Aprueba desde la fecha del Oficio de ingreso de la solicitud." +
                                                "<br> *  Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_13_PORC_DAC)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Porcentaje de casos informados por DAC, respecto del total de solicitudes pendientes en DAC para cultivo, en periódo D.";
                    indicador.nombreIndicador = "Porcentaje de solicitudes con IT DAC, respecto del total de solicitudes pendientes en DAC, en periódo D.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_13_PORC_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador13.jpg";
                    indicador.descripcionResultado = "porcentaje de casos que cumplen con el criterio";

                    indicador.consideraciones = " *  En el denominador se incluyen todas las solicitudes sin Informe DAC vigente o la fecha del  Informe DAC está fuera  del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todas las solicitudes con Informe DAC vigente emitido dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  El Universo del numerador está contenido en el denominador.";


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_14_PORC_REPOSICION_DAC)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Porcentaje de casos informados por DAC, respecto del total de casos que presentan recursos o reclamaciones ingresados a DAC. Ello en perído N.";
                    indicador.nombreIndicador = "Porcentaje de solicitudes con IT DAC, respecto del total de casos que presentan recursos o reclamaciones ingresados a DAC. Ello en perído N.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_14_PORC_REPOSICION_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador14.jpg";
                    indicador.descripcionResultado = "porcentaje de casos que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes con Recurso de reposición vigente con fecha dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluyen todas las solicitudes con nuevo informe DAC vigente emitido dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> * La fecha del nuevo Informe DAC debe estar posterior a la fecha del recurso de reposición." +
                                                "<br> * El Universo del numerador está contenido en el denominador."; 


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_15_PORC_ITC_UOT)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con ITC UOT respecto del total de solicitudes Ingresadas.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_15_PORC_ITC_UOT;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador15.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con fecha de ingreso dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluyen todas las solicitudes con ITC UOT vigente emitido dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'. " +
                                                "<br> * El Universo del numerador está contenido en el denominador."; 



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }


                else if (id_indicador == TipoIndicadorP3.INDICADOR_16_TP_ITC_UOT)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC UOT.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_16_TP_ITC_UOT;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador16.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";
                    
                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con  ITC OUT vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'."  +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener ITC OUT  desde la fecha de ingreso de la solicitud." +
                                                "<br> * Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = true;
                }

              

                else if (id_indicador == TipoIndicadorP3.INDICADOR_17_TP_ITC_UOT_MO)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC UOT de MO.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_17_TP_ITC_UOT_MO;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador17.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con  ITC UOT MO vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener ITC UOT MO desde la fecha de ingreso de la solicitud." +
                                                "<br> * Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = true;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_18_TP_ITC_UOT_PLANO)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC UOT de plano.";
                    indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC UOT de plano 14 ter.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_18_TP_ITC_UOT_PLANO;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador18.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con  ITC UOT  plano vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener ITC UOT  plano desde la fecha del Oficio de ingreso de la solicitud." +
                                                "<br> * Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = true;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_19_PORC_CPS)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con  evaluación de CPS en el SEIA";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_19_PORC_CPS;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador19.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes con Oficio Evaluación CPS (ENTRADA) vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluyen todas las solicitudes con Oficio Evaluación CPS (SALIDA) vigente emitido dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> * El Universo del numerador está contenido en el denominador.";
                                                


                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_20_TP_ITC_UOT_CPS)
                {

                    indicador = new Indicador();
                    //indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC UOT de CPS.";
                    indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC CPS UOT.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_20_TP_ITC_UOT_CPS;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador20.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con  ITC UOT de CPS vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener ITC UOT de CPS desde la fecha " +
                                                "<br> * Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = true;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_21_PORC_MARINA_DAC)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de casos informados por DAC, que han sido devueltos por SSFFAA(M).";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_21_PORC_MARINA_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador21.jpg";
                    indicador.descripcionResultado = "porcentaje de casos que cumplen con el criterio";

                    indicador.consideraciones =  "*  En el denominador se incluyen todas las solicitudes con Devoluciones de SSFFAA vigentes emitidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todas las solicitudes con nuevo Informe DAC vigentes emitidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  La fecha del nuevo Informe DAC o Informe DAC complementario debe ser mayor a la fecha de la devolución de SSFFAA." +
                                                "<br> * El Universo del numerador está contenido en el denominador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_22_PORC_ITC_MO_UA)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con IT MO UA";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_22_PORC_ITC_MO_UA;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador22.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = "<br> *  En el denominador se incluyen todos los requerimientos vigentes de Informe Ambiental de MO pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todos los requerimientos vigentes y con respuesta de Informe Ambiental de MO respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> *  El Universo del numerador está contenido en el denominador";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_23_PORC_APRUEBA_DAC)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con IT DAC aprueba, respecto del total de solicitudes pendientes que puede emitirse el ITDAC en DAC, en periódo D.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_23_PORC_APRUEBA_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador23.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " *  En el denominador se incluyen todas las solicitudes sin Informe DAC vigente o la fecha del  Informe DAC está fuera  del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todas las solicitudes con Informe DAC con resultado aprueba vigente emitido dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  El Universo del numerador está contenido en el denominador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_24_PORC_RECHAZA_REMITE_DAC)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con IT DAC rechaza o remite, respecto del total de solicitudes pendientes que puede emitirse el ITDAC en DAC, en periódo D.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_24_PORC_RECHAZA_REMITE_DAC;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador24.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " *  En el denominador se incluyen todas las solicitudes sin Informe DAC vigente o la fecha del  Informe DAC está fuera  del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> *  En el numerador se incluyen todas las solicitudes con Informe DAC con resultado rechaza o remite vigente emitido dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta'." +
                                                "<br> *  El Universo del numerador está contenido en el denominador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }

                else if (id_indicador == TipoIndicadorP3.INDICADOR_25_TP_DAC_RECHAZA_REMITE)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio para emitir informe DAC rechaza o remite, en período t.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_25_TP_DAC_RECHAZA_REMITE;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador25.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = " * En el denominador se incluyen todas las solicitudes con  Informe DAC rechaza o remite vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                               "<br> *  En el numerador se incluye la suma de los días que demora una solicitud en obtener su Informe DAC rechaza o remite desde la fecha del Oficio de ingreso de la solicitud." +
                                               "<br> *  Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_26_PORC_ITC_OUT_CPS)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con ITC CPS UOT respecto del total de solicitudes requeridas de evaluación de CPS.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_26_PORC_ITC_OUT_CPS;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador26.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todos los requerimientos vigentes de ITC CPS UOT pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'" +
                                                "<br> * En el numerador se incluyen todos los requerimientos vigentes y con respuesta de ITC CPS UOT respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> * El Universo del numerador está contenido en el denominador" +
                                                "<br> * Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_27_TP_ITC_OUT_CPS)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio en que se emite un ITC CPS UOT.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_27_TP_ITC_OUT_CPS;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador27.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con  ITC CPS OUT vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener ITC CPS OUT desde la fecha que se solicito el ITC CPS OUT." +
                                                "<br> * Solo se consideran días hábiles.";



                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = true;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_28_PORC_ITC_UOT_MO)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con ITC MO UOT; respecto del total de solicitudes requeridas de evaluación de MO.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_28_PORC_ITC_UOT_MO;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador28.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todos los requerimientos vigentes de ITC MO UOT pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'" +
                                                "<br> * En el numerador se incluyen todos los requerimientos vigentes y con respuesta de ITC MO UOT respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> * El Universo del numerador está contenido en el denominador" +
                                                "<br> * Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_29_PORC_ITC_UOT_CPS)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Porcentaje de solicitudes con ITC Recopilación CPS E INFAS UOT; respecto del total de solicitudes requeridas de evaluación de CPS e INFA.";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_29_PORC_ITC_UOT_CPS;
                    indicador.unidadResultado = "%";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador29.jpg";
                    indicador.descripcionResultado = "porcentaje de solicitudes que cumplen con el criterio";

                    indicador.consideraciones = " * En el denominador se incluyen todos los requerimientos vigentes de ITC Recopilación CPS E INFAS UOT pedidos dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'" +
                                                "<br> * En el numerador se incluyen todos los ITC Recopilación CPS E INFAS UOT vigentes respondidos dentro del rango 'Fecha Numerador desde' y 'Fecha Numerador hasta' " +
                                                "<br> * El Universo del numerador está contenido en el denominador" +
                                                "<br> * Si la solicitud tiene IT DAC o Resolución SSP no se considera para el cálculo del indicador.";



                    indicador.fechaNumerador_desde = true;
                    indicador.fechaNumerador_hasta = true;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_30_TP_ITC_MO_UA)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio en que se emite IT UA MO";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_30_TP_ITC_MO_UA;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador30.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con Informe Ambiental de MO vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener el Informe Ambiental de MO desde la fecha que se solicitó el Informe Ambiental de MO." +
                                                "<br> * Solo se consideran días hábiles.";




                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_31_TP_SEIA)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio en que se emite informe evaluación SEIA";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_31_TP_SEIA;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador31.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con Informe Ambiental Recopilación CPS E INFAS vigente emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud en obtener el Informe Ambiental Recopilación CPS E INFAS desde la fecha que se solicitó el Informe Ambiental Recopilación CPS E INFAS." +
                                                "<br> * Solo se consideran días hábiles.";


                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }
                else if (id_indicador == TipoIndicadorP3.INDICADOR_32_TP_OFICIO_SEIA)
                {

                    indicador = new Indicador();
                    indicador.nombreIndicador = "Tiempo promedio en que se emite oficio respuesta SEIA";
                    indicador.idIndicador = TipoIndicadorP3.INDICADOR_32_TP_OFICIO_SEIA;
                    indicador.unidadResultado = "día";
                    indicador.tipoPeriodo = "";
                    indicador.formula = "~/App_Themes/admin_style/images/indicadores/Indicador32.jpg";
                    indicador.descripcionResultado = "días promedio de tramitación";

                    indicador.consideraciones = "* En el denominador se incluyen todas las solicitudes con Oficio Evaluación CPS (Entrada y Salida) emitido dentro del rango 'Fecha Denominador desde' y 'Fecha Denominador hasta'." +
                                                "<br> * En el numerador se incluye la suma de los días que demora una solicitud entre la Entrada y la salida del  Oficio Evaluación CPS." +
                                                "<br> * Solo se consideran días hábiles.";




                    indicador.fechaNumerador_desde = false;
                    indicador.fechaNumerador_hasta = false;
                    indicador.fechaDenominador_desde = true;
                    indicador.fechaDenominador_hasta = true;
                    indicador.fechaConsulta = false;
                    indicador.regiones = false;
                }


                return indicador;


            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }


        public DataTable CalcularIndicador(int tipoIndicador, int idTipoTramite, int idSubTipo, string fechaNumerador_desde, string fechaNumerador_hasta, string fechaDenominador_desde, string fechaDenominador_hasta, string fechaConsulta, string regiones)
        {
            try
            {

                String procedimiento = "";


                if (tipoIndicador == TipoIndicadorP3.INDICADOR_1_PORC_INSPECCION_TERRENO)
                {
                    procedimiento = "paSelRbIndicador01";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_2_PORC_DIFROL)
                {
                    procedimiento = "paSelRbIndicador02";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_3_PORC_DIRINMAR)
                {
                    procedimiento = "paSelRbIndicador03";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_4_PORC_ITC_UOT_POS)
                {
                    procedimiento = "paSelRbIndicador04";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_5_PORC_PASAR_EMISION_RADIAL)
                {
                    procedimiento = "paSelRbIndicador05";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_6_PORC_CON_EMISION_RADIAL)
                {
                    procedimiento = "paSelRbIndicador06";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_7_PORC_UOT_DAC)
                {
                    procedimiento = "paSelRbIndicador07";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_8_PORC_DAC_SSP)
                {
                    procedimiento = "paSelRbIndicador08";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_9_PORC_CUATRO_ANIOS)
                {
                    procedimiento = "paSelRbIndicador09";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_10_PORC_JURIDICA_DAC)
                {
                    procedimiento = "paSelRbIndicador10";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_11_PORC_CERTIFICADO_OPERACION)
                {
                    procedimiento = "paSelRbIndicador11";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_12_TP_DAC)
                {
                    procedimiento = "paSelRbIndicador12";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_13_PORC_DAC)
                {
                    procedimiento = "paSelRbIndicador13";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_14_PORC_REPOSICION_DAC)
                {
                    procedimiento = "paSelRbIndicador14";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_15_PORC_ITC_UOT)
                {
                    procedimiento = "paSelRbIndicador15";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_16_TP_ITC_UOT)
                {
                    procedimiento = "paSelRbIndicador16";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_17_TP_ITC_UOT_MO)
                {
                    procedimiento = "paSelRbIndicador17";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_18_TP_ITC_UOT_PLANO)
                {
                    procedimiento = "paSelRbIndicador18";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_19_PORC_CPS)
                {
                    procedimiento = "paSelRbIndicador19";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_20_TP_ITC_UOT_CPS)
                {
                    procedimiento = "paSelRbIndicador20";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_21_PORC_MARINA_DAC)
                {
                    procedimiento = "paSelRbIndicador21";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_22_PORC_ITC_MO_UA)
                {
                    procedimiento = "paSelRbIndicador22";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_23_PORC_APRUEBA_DAC)
                {
                    procedimiento = "paSelRbIndicador23";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_24_PORC_RECHAZA_REMITE_DAC)
                {
                    procedimiento = "paSelRbIndicador24";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_25_TP_DAC_RECHAZA_REMITE)
                {
                    procedimiento = "paSelRbIndicador25";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_26_PORC_ITC_OUT_CPS)
                {
                    procedimiento = "paSelRbIndicador26";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_27_TP_ITC_OUT_CPS)
                {
                    procedimiento = "paSelRbIndicador27";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_28_PORC_ITC_UOT_MO)
                {
                    procedimiento = "paSelRbIndicador28";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_29_PORC_ITC_UOT_CPS)
                {
                    procedimiento = "paSelRbIndicador29";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_30_TP_ITC_MO_UA)
                {
                    procedimiento = "paSelRbIndicador30";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_31_TP_SEIA)
                {
                    procedimiento = "paSelRbIndicador31";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_32_TP_OFICIO_SEIA)
                {
                    procedimiento = "paSelRbIndicador32";
                }
                


                if (!procedimiento.Trim().Equals(""))
                {
                    return indicadores.CalcularIndicador(procedimiento, idTipoTramite, idSubTipo, fechaNumerador_desde, fechaNumerador_hasta, fechaDenominador_desde, fechaDenominador_hasta, fechaConsulta, regiones);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                logger.PrintError(ex);
                logger.SendMailError(ex);
                return null;
            }
        }




        public DataTable ListarDetalleIndicador(int tipoIndicador, int idTipoTramite, int idSubTipo, string fechaNumerador_desde, string fechaNumerador_hasta, string fechaDenominador_desde, string fechaDenominador_hasta, string fechaConsulta, string regiones)
        {
            try
            {

                String procedimiento = "";


                if (tipoIndicador == TipoIndicadorP3.INDICADOR_1_PORC_INSPECCION_TERRENO)
                {
                    procedimiento = "paSelRbIndicador01Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_2_PORC_DIFROL)
                {
                    procedimiento = "paSelRbIndicador02Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_3_PORC_DIRINMAR)
                {
                    procedimiento = "paSelRbIndicador03Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_4_PORC_ITC_UOT_POS)
                {
                    procedimiento = "paSelRbIndicador04Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_5_PORC_PASAR_EMISION_RADIAL)
                {
                    procedimiento = "paSelRbIndicador05Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_6_PORC_CON_EMISION_RADIAL)
                {
                    procedimiento = "paSelRbIndicador06Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_7_PORC_UOT_DAC)
                {
                    procedimiento = "paSelRbIndicador07Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_8_PORC_DAC_SSP)
                {
                    procedimiento = "paSelRbIndicador08Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_9_PORC_CUATRO_ANIOS)
                {
                    procedimiento = "paSelRbIndicador09Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_10_PORC_JURIDICA_DAC)
                {
                    procedimiento = "paSelRbIndicador10Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_11_PORC_CERTIFICADO_OPERACION)
                {
                    procedimiento = "paSelRbIndicador11Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_12_TP_DAC)
                {
                    procedimiento = "paSelRbIndicador12Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_13_PORC_DAC)
                {
                    procedimiento = "paSelRbIndicador13Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_14_PORC_REPOSICION_DAC)
                {
                    procedimiento = "paSelRbIndicador14Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_15_PORC_ITC_UOT)
                {
                    procedimiento = "paSelRbIndicador15Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_16_TP_ITC_UOT)
                {
                    procedimiento = "paSelRbIndicador16Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_17_TP_ITC_UOT_MO)
                {
                    procedimiento = "paSelRbIndicador17Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_18_TP_ITC_UOT_PLANO)
                {
                    procedimiento = "paSelRbIndicador18Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_19_PORC_CPS)
                {
                    procedimiento = "paSelRbIndicador19Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_20_TP_ITC_UOT_CPS)
                {
                    procedimiento = "paSelRbIndicador20Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_21_PORC_MARINA_DAC)
                {
                    procedimiento = "paSelRbIndicador21Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_22_PORC_ITC_MO_UA)
                {
                    procedimiento = "paSelRbIndicador22Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_23_PORC_APRUEBA_DAC)
                {
                    procedimiento = "paSelRbIndicador23Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_24_PORC_RECHAZA_REMITE_DAC)
                {
                    procedimiento = "paSelRbIndicador24Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_25_TP_DAC_RECHAZA_REMITE)
                {
                    procedimiento = "paSelRbIndicador25Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_26_PORC_ITC_OUT_CPS)
                {
                    procedimiento = "paSelRbIndicador26Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_27_TP_ITC_OUT_CPS)
                {
                    procedimiento = "paSelRbIndicador27Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_28_PORC_ITC_UOT_MO)
                {
                    procedimiento = "paSelRbIndicador28Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_29_PORC_ITC_UOT_CPS)
                {
                    procedimiento = "paSelRbIndicador29Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_30_TP_ITC_MO_UA)
                {
                    procedimiento = "paSelRbIndicador30Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_31_TP_SEIA)
                {
                    procedimiento = "paSelRbIndicador31Detalle";
                }
                else if (tipoIndicador == TipoIndicadorP3.INDICADOR_32_TP_OFICIO_SEIA)
                {
                    procedimiento = "paSelRbIndicador32Detalle";
                }


                if (!procedimiento.Trim().Equals(""))
                {
                    return indicadores.ListarDetalleIndicador(procedimiento, idTipoTramite, idSubTipo, fechaNumerador_desde, fechaNumerador_hasta, fechaDenominador_desde, fechaDenominador_hasta, fechaConsulta, regiones);
                }
                else
                {
                    return null;
                }

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
