<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesRelocalizacion.Master" AutoEventWireup="true"  CodeBehind="administrarDocumentoRelocalizacion.aspx.cs" 
Inherits="SubPesca.Solicitudes.Relocalizacion.administrarDocumentoRelocalizacion"  Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="../Registrar/administrarDocumentoComponente.ascx" tagname="administrarDocumentoComponente" tagprefix="uc0" %>          


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
            <uc0:administrarDocumentoComponente ID="administrarDocumentoComponente" runat="server" />
        </asp:Panel>

   
</asp:Content>
