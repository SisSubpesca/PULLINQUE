<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacionECMPO.Master" AutoEventWireup="true" CodeBehind="evaluarDocumentoModificacionECMPO.aspx.cs" 
Inherits="SubPesca.Solicitudes.ModificacionECMPO.evaluarDocumentoModificacionECMPO" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"  %>
<%@ Register src="../Registrar/evaluarDocumentoComponente.ascx"            tagname="evaluarDocumentoComponente"                      tagprefix="uc01" %>


<asp:Content  ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <asp:Panel ID="PanelVerDocumento"  Visible="true" runat="server">
        <uc01:evaluarDocumentoComponente ID="evaluarDocumentoComponente" runat="server" />
    </asp:Panel>
    
</asp:Content>
