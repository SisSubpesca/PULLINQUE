<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" AutoEventWireup="true" CodeBehind="tipoCentroAcopio.aspx.cs" Inherits="SubPesca.Mantenedores.Generales.tipoCentroAcopio" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register src="tipoContenedor.ascx" tagname="tipoContenedor" tagprefix="uc0" %>  



<asp:Content ID="FormularioTipo" ContentPlaceHolderID="rightbody" runat="server">
    
    <asp:Panel ID="PanelTipo"  Visible="true" runat="server">
        <uc0:tipoContenedor ID="tipoContenedor" runat="server" />
    </asp:Panel>

</asp:Content>
