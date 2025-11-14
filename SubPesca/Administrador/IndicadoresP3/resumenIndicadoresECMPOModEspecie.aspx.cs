using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.cl.subpesca.rb.servicios.indicadores;
using System.Collections;
using Datos.Contantes;
using System.Data;
using Datos.Entidades;

namespace SubPesca.Administrador.IndicadoresP3
{
    public partial class resumenIndicadoresECMPOModEspecie : System.Web.UI.Page
    {
        Datos.Utilidades.Funciones fnc = new Datos.Utilidades.Funciones();
        IndicadoresService indicadoresService = new IndicadoresService();

        // PAGE_LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Inicializamos el Hashtable contenedor de los filtros de búsqueda
                Initialize_HT_Indicadores();

                // Inicializamos el Formulario
                Initialize_Form();
            };
        }


        // ACCIONES GENERALES
        protected void Initialize_HT_Indicadores()
        {

        }


        protected void Initialize_Form()
        {

            //// Indicador nº1:
            //Indicador indicador1 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_1_PORC_INSPECCION_TERRENO);

            //try
            //{
            //    Ind01_Numero.Text = Convert.ToString(indicador1.idIndicador);
            //    Ind01_Nombre.Text = indicador1.nombreIndicador;

            //}
            //catch { };



            //// Indicador nº2:
            //Indicador indicador2 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_2_PORC_DIFROL);

            //try
            //{
            //    Ind02_Numero.Text = Convert.ToString(indicador2.idIndicador);
            //    Ind02_Nombre.Text = indicador2.nombreIndicador;

            //}
            //catch { };


            //// Indicador nº3:
            //Indicador indicador3 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_3_PORC_DIRINMAR);

            //try
            //{
            //    Ind03_Numero.Text = Convert.ToString(indicador3.idIndicador);
            //    Ind03_Nombre.Text = indicador3.nombreIndicador;

            //}
            //catch { };

            // Indicador nº4:
            Indicador indicador4 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_4_PORC_ITC_UOT_POS);

            try
            {
                Ind04_Numero.Text = Convert.ToString(indicador4.idIndicador);
                Ind04_Nombre.Text = indicador4.nombreIndicador;

            }
            catch { };

            //// Indicador nº5:
            //Indicador indicador5 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_5_PORC_PASAR_EMISION_RADIAL);

            //try
            //{
            //    Ind05_Numero.Text = Convert.ToString(indicador5.idIndicador);
            //    Ind05_Nombre.Text = indicador5.nombreIndicador;

            //}
            //catch { };

            //// Indicador nº6:
            //Indicador indicador6 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_6_PORC_CON_EMISION_RADIAL);

            //try
            //{
            //    Ind06_Numero.Text = Convert.ToString(indicador6.idIndicador);
            //    Ind06_Nombre.Text = indicador6.nombreIndicador;

            //}
            //catch { };

            // Indicador nº7:
            Indicador indicador7 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_7_PORC_UOT_DAC);

            try
            {
                Ind07_Numero.Text = Convert.ToString(indicador7.idIndicador);
                Ind07_Nombre.Text = indicador7.nombreIndicador;

            }
            catch { };


            // Indicador nº8:
            Indicador indicador8 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_8_PORC_DAC_SSP);

            try
            {
                Ind08_Numero.Text = Convert.ToString(indicador8.idIndicador);
                Ind08_Nombre.Text = indicador8.nombreIndicador;

            }
            catch { };

            // Indicador nº9:
            Indicador indicador9 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_9_PORC_CUATRO_ANIOS);

            try
            {
                Ind09_Numero.Text = Convert.ToString(indicador9.idIndicador);
                Ind09_Nombre.Text = indicador9.nombreIndicador;

            }
            catch { };

            // Indicador nº10:
            Indicador indicador10 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_10_PORC_JURIDICA_DAC);

            try
            {
                Ind10_Numero.Text = Convert.ToString(indicador10.idIndicador);
                Ind10_Nombre.Text = indicador10.nombreIndicador;

            }
            catch { };

            // Indicador nº11:
            Indicador indicador11 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_11_PORC_CERTIFICADO_OPERACION);

            try
            {
                Ind11_Numero.Text = Convert.ToString(indicador11.idIndicador);
                Ind11_Nombre.Text = indicador11.nombreIndicador;

            }
            catch { };




            // Indicador nº12:
            Indicador indicador12 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_12_TP_DAC);

            try
            {
                Ind12_Numero.Text = Convert.ToString(indicador12.idIndicador);
                Ind12_Nombre.Text = indicador12.nombreIndicador;

            }
            catch { };


            // Indicador nº13:
            Indicador indicador13 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_13_PORC_DAC);

            try
            {
                Ind13_Numero.Text = Convert.ToString(indicador13.idIndicador);
                Ind13_Nombre.Text = indicador13.nombreIndicador;

            }
            catch { };



            // Indicador nº14:
            Indicador indicador14 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_14_PORC_REPOSICION_DAC);

            try
            {
                Ind14_Numero.Text = Convert.ToString(indicador14.idIndicador);
                Ind14_Nombre.Text = indicador14.nombreIndicador;

            }
            catch { };

            // Indicador nº15:
            Indicador indicador15 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_15_PORC_ITC_UOT);

            try
            {
                Ind15_Numero.Text = Convert.ToString(indicador15.idIndicador);
                Ind15_Nombre.Text = indicador15.nombreIndicador;

            }
            catch { };


            // Indicador nº16:
            Indicador indicador16 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_16_TP_ITC_UOT);

            try
            {
                Ind16_Numero.Text = Convert.ToString(indicador16.idIndicador);
                Ind16_Nombre.Text = indicador16.nombreIndicador;

            }
            catch { };




            // Indicador nº17:
            Indicador indicador17 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_17_TP_ITC_UOT_MO);

            try
            {
                Ind17_Numero.Text = Convert.ToString(indicador17.idIndicador);
                Ind17_Nombre.Text = indicador17.nombreIndicador;

            }
            catch { };


            //// Indicador nº18:
            //Indicador indicador18 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_18_TP_ITC_UOT_PLANO);

            //try
            //{
            //    Ind18_Numero.Text = Convert.ToString(indicador18.idIndicador);
            //    Ind18_Nombre.Text = indicador18.nombreIndicador;

            //}
            //catch { };

            // Indicador nº19:
            Indicador indicador19 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_19_PORC_CPS);

            try
            {
                Ind19_Numero.Text = Convert.ToString(indicador19.idIndicador);
                Ind19_Nombre.Text = indicador19.nombreIndicador;

            }
            catch { };


            //// Indicador nº20:
            //Indicador indicador20 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_20_TP_ITC_UOT_CPS);

            //try
            //{
            //    Ind20_Numero.Text = Convert.ToString(indicador20.idIndicador);
            //    Ind20_Nombre.Text = indicador20.nombreIndicador;

            //}
            //catch { };

            // Indicador nº21:
            Indicador indicador21 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_21_PORC_MARINA_DAC);

            try
            {
                Ind21_Numero.Text = Convert.ToString(indicador21.idIndicador);
                Ind21_Nombre.Text = indicador21.nombreIndicador;

            }
            catch { };


            // Indicador nº22:
            Indicador indicador22 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_22_PORC_ITC_MO_UA);

            try
            {
                Ind22_Numero.Text = Convert.ToString(indicador22.idIndicador);
                Ind22_Nombre.Text = indicador22.nombreIndicador;

            }
            catch { };

            // Indicador nº23:
            Indicador indicador23 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_23_PORC_APRUEBA_DAC);

            try
            {
                Ind23_Numero.Text = Convert.ToString(indicador23.idIndicador);
                Ind23_Nombre.Text = indicador23.nombreIndicador;

            }
            catch { };


            // Indicador nº24:
            Indicador indicador24 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_24_PORC_RECHAZA_REMITE_DAC);

            try
            {
                Ind24_Numero.Text = Convert.ToString(indicador24.idIndicador);
                Ind24_Nombre.Text = indicador24.nombreIndicador;

            }
            catch { };


            // Indicador nº25:
            Indicador indicador25 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_25_TP_DAC_RECHAZA_REMITE);

            try
            {
                Ind25_Numero.Text = Convert.ToString(indicador25.idIndicador);
                Ind25_Nombre.Text = indicador25.nombreIndicador;

            }
            catch { };


            // Indicador nº26:
            Indicador indicador26 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_26_PORC_ITC_OUT_CPS);

            try
            {
                Ind26_Numero.Text = Convert.ToString(indicador26.idIndicador);
                Ind26_Nombre.Text = indicador26.nombreIndicador;

            }
            catch { };


            // Indicador nº27:
            Indicador indicador27 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_27_TP_ITC_OUT_CPS);

            try
            {
                Ind27_Numero.Text = Convert.ToString(indicador27.idIndicador);
                Ind27_Nombre.Text = indicador27.nombreIndicador;

            }
            catch { };


            // Indicador nº28:
            Indicador indicador28 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_28_PORC_ITC_UOT_MO);

            try
            {
                Ind28_Numero.Text = Convert.ToString(indicador28.idIndicador);
                Ind28_Nombre.Text = indicador28.nombreIndicador;

            }
            catch { };


            //// Indicador nº29:
            //Indicador indicador29 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_29_PORC_ITC_UOT_CPS);

            //try
            //{
            //    Ind29_Numero.Text = Convert.ToString(indicador29.idIndicador);
            //    Ind29_Nombre.Text = indicador29.nombreIndicador;

            //}
            //catch { };



            // Indicador nº30:
            Indicador indicador30 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_30_TP_ITC_MO_UA);

            try
            {
                Ind30_Numero.Text = Convert.ToString(indicador30.idIndicador);
                Ind30_Nombre.Text = indicador30.nombreIndicador;

            }
            catch { };


            // Indicador nº31:
            Indicador indicador31 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_31_TP_SEIA);

            try
            {
                Ind31_Numero.Text = Convert.ToString(indicador31.idIndicador);
                Ind31_Nombre.Text = indicador31.nombreIndicador;

            }
            catch { };


            // Indicador nº32:
            Indicador indicador32 = indicadoresService.ObtenerIndicador(TipoIndicadorP3.INDICADOR_32_TP_OFICIO_SEIA);

            try
            {
                Ind32_Numero.Text = Convert.ToString(indicador32.idIndicador);
                Ind32_Nombre.Text = indicador32.nombreIndicador;

            }
            catch { };




        }


        protected void VerDetalleIndicador_Click(object sender, EventArgs e)
        {
            LinkButton boton_clickeado = (LinkButton)sender;
            string id_indicador = boton_clickeado.CommandArgument;
            string id_solicitud;
            string id_subTipoST;

            string[] splitIndicador = id_indicador.Split(new Char[] { ',' });
            id_solicitud = splitIndicador[0];
            id_indicador = splitIndicador[1];
            id_subTipoST = splitIndicador[2];


            int id_indicador1 = Convert.ToInt32(id_indicador);
            int id_solicitud1 = Convert.ToInt32(id_solicitud);
            int id_subTipo = Convert.ToInt32(id_subTipoST);

            Response.Redirect(String.Format("~/Administrador/IndicadoresP3/indicador.aspx?id_indicador={0}&id_solicitud={1}&id_subTipo={2}", id_indicador, id_solicitud, id_subTipo)); ;

        }

    }
}