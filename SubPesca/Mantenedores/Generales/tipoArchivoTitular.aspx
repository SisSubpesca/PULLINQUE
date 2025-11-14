<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="tipoArchivoTitular.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.tipoArchivoTitular" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="tipoContenedor.ascx" tagname="tipoContenedor" tagprefix="uc0" %>  



<asp:Content ID="FormularioTipoArchivoTitular" ContentPlaceHolderID="rightbody" runat="server">
    
            
    <asp:Panel ID="PanelTipoArchivoTitular"  Visible="true" runat="server">
        <uc0:tipoContenedor ID="tipoContenedor" runat="server" />
    </asp:Panel>

</asp:Content>
