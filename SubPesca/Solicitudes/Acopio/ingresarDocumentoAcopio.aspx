<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesAcopio.Master" AutoEventWireup="true" CodeBehind="ingresarDocumentoAcopio.aspx.cs"
 Inherits="SubPesca.Solicitudes.Acopio.ingresarDocumentoRelocalizacion" Theme="admin_style" %>


<%@ Register src="../Registrar/ingresarDocumentoComponente.ascx"            tagname="ingresarDocumentoComponente"                      tagprefix="uc01" %>



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
