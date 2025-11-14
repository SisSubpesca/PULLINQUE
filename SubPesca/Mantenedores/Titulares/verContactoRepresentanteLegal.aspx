<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verContactoRepresentanteLegal.aspx.cs" Inherits="SubPesca.Mantenedores.Titulares.verContactoRepresentanteLegal"
MasterPageFile="~/Administrador/SitioAdmin.Master" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="FormularioAdministracionContactos" ContentPlaceHolderID="rightbody" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionContactos" runat="server"></asp:ToolkitScriptManager>

    <fieldset>
        <legend>Representante Legal</legend>
        <table class="form" cellpadding="0px" cellspacing="0px">
        <tr>
        <td class="col1"><span class="item">Rut</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:Label ID="rutRepresentante" runat="server"></asp:Label>
        </td>
        <td class="col3" align="left">Nombre</td>
        <td class="col3">:</td>
        <td class="col3">
            <asp:Label ID="nombreRepresentante" runat="server"></asp:Label></td>
        
    </tr>
    </table>
    </fieldset>

    <fieldset>
        <legend>Dirección</legend>
        <table class="form" cellpadding="0px" cellspacing="0px">
         <tr>
        <td class="col1"><span class="item">Dirección</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:Label ID="Direccion" runat="server" Text="Label"></asp:Label>
        </td>
        <td class="col3">&nbsp;</td>        
        <td class="col3">&nbsp;</td>            
        <td class="col3">&nbsp;</td>
        
    </tr>
    <tr>
        <td class="col1"><span class="item">Región</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:Label ID="Region" runat="server"></asp:Label>          
        </td>
        <td class="col3">&nbsp;</td>        
        <td class="col3">&nbsp;</td>            
        <td class="col3">&nbsp;</td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Comuna</span></td>
        <td class="col2">:</td>
        <td class="col3">
            <asp:Label ID="Comuna" runat="server"></asp:Label></td>
        <td class="col3">&nbsp;</td>        
        <td class="col3">&nbsp;</td>            
        <td class="col3">&nbsp;</td>
    </tr>
    </table>
    </fieldset>
    
</asp:Content>