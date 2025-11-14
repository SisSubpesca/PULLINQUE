<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="tipoDocumento.aspx.cs" Inherits="SubPesca.Mantenedores.Transversales.tipoDocumento" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Src="~/Mantenedores/Generales/tipoContenedor.ascx" tagname="tipoContenedor" tagprefix="uc0" %>  



<asp:Content ID="FormularioTipo" ContentPlaceHolderID="rightbody" runat="server">
    
            
    <asp:Panel ID="PanelTipo"  Visible="true" runat="server">
        <uc0:tipoContenedor ID="tipoContenedor" runat="server" />
    </asp:Panel>

</asp:Content>
