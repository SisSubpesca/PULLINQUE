<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="agregarNombres.aspx.cs" Inherits="SubPesca.Mantenedores.Titulares.agregarNombres" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_usuarios.js")); %>" type="text/javascript"></script>
    </asp:Content>

<asp:Content ID="FormularioModificarNombre" ContentPlaceHolderID="rightbody" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManageModificarNombre" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

<fieldset>
    <legend>Modificar Nombre</legend>

    <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_div2">
            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryIngresoNombrePersona" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
    

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Rut Persona</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3"><asp:Label ID="RutPersona" runat="server" Text="RutPersona"></asp:Label></td>
    </tr>
    <tr>
        <td class="col1"><span class="item">Nuevo Nombre Persona</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:TextBox ID="NombrePersona" Width="300px" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator id="RequiredFieldValidatorNombrePersona" runat="server" ControlToValidate="NombrePersona"  ValidationGroup="grupo1"
            ErrorMessage="Nombre Persona" Display="Static">*</asp:RequiredFieldValidator>
           <asp:Label ID="EjemploNombrePN" runat="server" Text="Ejemplo: APELLIDO 1 APELLIDO 2, NOMBRE"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td class="col1" colspan="3">
            <asp:Button ID="ModificarNombre" runat="server" Text="Modificar Nombre" 
                onclick="ModificarNombre_Click" CausesValidation="true" ValidationGroup="grupo1" />
            <asp:Button ID="Volver" runat="server" Text="Volver" onclick="Volver_Click" />
        </td>
    </tr>

    </table>
 
</fieldset>

</asp:Content>
