<%@ Page Language="C#" MasterPageFile="~/Unidades/SitioCentroAcopio.Master" AutoEventWireup="true" CodeBehind="administrarDocumentoAcopio.aspx.cs" 
Inherits="SubPesca.Unidades.Acopio.administrarDocumentoAcopio" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
      
<%@ Register src="~/Unidades/Concesion/documentosUnidadEspacialComponente.ascx"     tagname="documentosUnidadEspacialComponente" tagprefix="uc0" %>        

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="FormularioResoluciones" ContentPlaceHolderID="rightbody" runat="server">

    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server" EnablePartialRendering="false"></asp:ToolkitScriptManager>

    <asp:Panel ID="PanelDocumentosUnidadEspacialComponente"  Visible="true" runat="server">
        <uc0:documentosUnidadEspacialComponente ID="documentosUnidadEspacialComponente" runat="server" />
    </asp:Panel>

</asp:Content>