<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="resumenIndicadoresECMPOModAmpliacion.aspx.cs" 
Inherits="SubPesca.Administrador.IndicadoresP3.resumenIndicadoresECMPOModAmpliacion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
         
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle">
            <span id="titulo_modulo">Indicadores</span>
        </td>
    </tr>
    </table>
    <hr style="width:100%;" />


    <fieldset>
        <legend>Resumen de Indicadores de ECMPO Ampliación</legend>
        <br />
        
        
         <!-- -- INDICADOR Nº1 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
            <table class="indicadores" cellpadding="0px" cellspacing="0px">
            <tr>
                <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind01_Numero" runat="server" Text=""></asp:Label></span></td>
            </tr>
            <tr>
                <td class="col1"><span class="item">Nombre del Indicador</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3"><span class="item"><asp:Label ID="Ind01_Nombre" runat="server" Text=""></asp:Label></span></td>
            </tr>
            </table>
        </div>
        <div class="indicadores_abajo">
            <table class="indicadores" cellpadding="0px" cellspacing="0px">

            <tr>
                <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind01_VerDetalle" CssClass="txt_verdetalle" Text="Ingresar" CommandArgument="564,1,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
            </tr>
            </table>
        </div>
        </div>
        
        <!-- -- INDICADOR Nº2 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind02_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind02_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="div_verdetalle">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">

        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind02_VerDetalle" Text="Ingresar" CommandArgument="564,2,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>
        
        <!-- -- INDICADOR Nº3 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind03_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind03_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind03_VerDetalle" Text="Ingresar" CommandArgument="564,3,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>
        
        <!-- -- INDICADOR Nº4 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind04_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind04_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind04_VerDetalle" Text="Ingresar" CommandArgument="564,4,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

       <%--    
        <!-- -- INDICADOR Nº5 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind05_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind05_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind05_VerDetalle" Text="Ingresar" CommandArgument="564,5,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>
        --%>


        
        <!-- -- INDICADOR Nº6 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind06_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind06_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind06_VerDetalle" Text="Ingresar" CommandArgument="564,6,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        <!-- -- INDICADOR Nº7 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind07_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind07_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind07_VerDetalle" Text="Ingresar" CommandArgument="564,7,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        
        <!-- -- INDICADOR Nº8 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind08_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind08_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind08_VerDetalle" Text="Ingresar" CommandArgument="564,8,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>



        <!-- -- INDICADOR Nº9 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind09_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind09_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind09_VerDetalle" Text="Ingresar" CommandArgument="564,9,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

      

        <!-- -- INDICADOR Nº10 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind10_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind10_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind10_VerDetalle" Text="Ingresar" CommandArgument="564,10,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

         <!-- -- INDICADOR Nº11 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind11_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind11_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind11_VerDetalle" Text="Ingresar" CommandArgument="564,11,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- -- INDICADOR Nº12 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind12_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind12_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind12_VerDetalle" Text="Ingresar" CommandArgument="564,12,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>



        <!-- -- INDICADOR Nº13 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind13_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind13_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind13_VerDetalle" Text="Ingresar" CommandArgument="564,13,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        
        <!-- -- INDICADOR Nº14 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind14_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind14_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind14_VerDetalle" Text="Ingresar" CommandArgument="564,14,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>



        <!-- -- INDICADOR Nº15 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind15_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind15_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind15_VerDetalle" Text="Ingresar" CommandArgument="564,15,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        
        <!-- -- INDICADOR Nº16 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind16_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind16_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind16_VerDetalle" Text="Ingresar" CommandArgument="564,16,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        <!-- -- INDICADOR Nº17 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind17_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind17_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind17_VerDetalle" Text="Ingresar" CommandArgument="564,17,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- -- INDICADOR Nº18 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind18_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind18_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind18_VerDetalle" Text="Ingresar" CommandArgument="564,18,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        <!-- -- INDICADOR Nº19 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind19_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind19_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind19_VerDetalle" Text="Ingresar" CommandArgument="564,19,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        <%--
         <!-- -- INDICADOR Nº20 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind20_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind20_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind20_VerDetalle" Text="Ingresar" CommandArgument="564,20,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>
        --%>


        <!-- -- INDICADOR Nº21 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind21_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind21_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
       
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind21_VerDetalle" Text="Ingresar" CommandArgument="564,21,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>



        <!-- -- INDICADOR Nº22 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind22_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind22_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind22_VerDetalle" Text="Ingresar" CommandArgument="564,22,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- INDICADOR Nº23 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind23_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind23_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind23_VerDetalle" Text="Ingresar" CommandArgument="564,23,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        
        <!-- INDICADOR Nº24 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind24_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind24_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind24_VerDetalle" Text="Ingresar" CommandArgument="564,24,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


          <!-- INDICADOR Nº25 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind25_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind25_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind25_VerDetalle" Text="Ingresar" CommandArgument="564,25,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- INDICADOR Nº26 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind26_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind26_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind26_VerDetalle" Text="Ingresar" CommandArgument="564,26,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- INDICADOR Nº27 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind27_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind27_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind27_VerDetalle" Text="Ingresar" CommandArgument="564,27,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- INDICADOR Nº28 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind28_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind28_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind28_VerDetalle" Text="Ingresar" CommandArgument="564,28,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

      <%--  <!-- INDICADOR Nº29 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind29_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind29_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind29_VerDetalle" Text="Ingresar" CommandArgument="564,29,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>--%>


         <!-- INDICADOR Nº30 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind30_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind30_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind30_VerDetalle" Text="Ingresar" CommandArgument="564,30,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>


        <!-- INDICADOR Nº31 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind31_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind31_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind31_VerDetalle" Text="Ingresar" CommandArgument="564,31,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

        <!-- INDICADOR Nº32 ---------------------------------------------------------------------------------------------------------------------------------- !-->
        <div class="indicadores">
        <div class="indicadores_arriba">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">
        <tr>
            <td class="col1" colspan="3"><span class="item">Indicador Nº<asp:Label ID="Ind32_Numero" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td class="col1"><span class="item">Nombre del Indicador</span></td>
            <td class="col2"><span class="item">:</span></td>
            <td class="col3"><span class="item"><asp:Label ID="Ind32_Nombre" runat="server" Text=""></asp:Label></span></td>
        </tr>
        </table>
        </div>
        <div class="indicadores_abajo">
        <table class="indicadores" cellpadding="0px" cellspacing="0px">        
        <tr>
            <td class="td_verdetalle" colspan="3"><span class="item"><asp:LinkButton ID="Ind32_VerDetalle" Text="Ingresar" CommandArgument="564,32,561" OnClick="VerDetalleIndicador_Click" runat="server"></asp:LinkButton></span></td>
        </tr>
        </table>
        </div>
        </div>

                
                
    </fieldset>



</asp:Content>