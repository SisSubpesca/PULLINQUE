<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesECMPO.Master" AutoEventWireup="true" CodeBehind="evaluarDocumentoECMPO.aspx.cs" 
Inherits="SubPesca.Solicitudes.ECMPO.evaluarDocumentoECMPO" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"  %>
<%@ Register src="../Registrar/evaluarDocumentoComponente.ascx"            tagname="evaluarDocumentoComponente"                      tagprefix="uc01" %>


<asp:Content  ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
        <uc01:evaluarDocumentoComponente ID="evaluarDocumentoComponente" runat="server" />
    </asp:Panel>
    
</asp:Content>
