<%@ Page    Language="C#" MasterPageFile="~/Solicitudes/SitioCatastroUnidades.Master" AutoEventWireup="true"  CodeBehind="ingresarDocumento.aspx.cs" 
            Inherits="SubPesca.Solicitudes.Registrar.ingresarDocumento" Theme="admin_style" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<%@ Register src="ingresarDocumentoComponente.ascx"            tagname="ingresarDocumentoComponente"                      tagprefix="uc01" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>
    

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
        <uc01:ingresarDocumentoComponente ID="ingresarDocumentoComponente" runat="server" />
    </asp:Panel>

</asp:Content>
