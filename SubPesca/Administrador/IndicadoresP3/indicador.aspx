<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="indicador.aspx.cs" 
         Inherits="SubPesca.Administrador.IndicadoresP3.indicador" Theme="admin_style" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>    
</asp:Content>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="~/Administrador/IndicadoresP3/indicadoresConcesionAcuicultura.ascx" tagname="indicadoresConcesion" tagprefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>


    <table class="formtop" cellpadding="0px" cellspacing="0px">
    <tr>
        <td align="left" valign="middle"><span id="titulo_modulo">Detalle del Indicador</span></td>
    </tr>
    </table>

    <hr style="width:100%;" />
    
    <asp:Panel ID="Content_Errores" runat="server" CssClass="valSum" Visible="false"></asp:Panel>

    <fieldset>

        <legend>Información del Indicador</legend>
        <br />
      
        <asp:Panel ID="PanelIndicadorConcesion"  Visible="true" runat="server">
            <uc1:indicadoresConcesion ID="indicadoresConcesion" runat="server" />
        </asp:Panel>

    </fieldset>
    
    
    
</asp:Content>