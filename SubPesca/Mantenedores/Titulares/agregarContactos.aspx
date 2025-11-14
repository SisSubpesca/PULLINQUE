<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarContactos.aspx.cs" MasterPageFile="~/Administrador/SitioAdmin.Master" Inherits="SubPesca.Mantenedores.Titulares.agregarContactos" Theme="admin_style" %>

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
<asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionContactos" runat="server" EnablePartialRendering="true" EnablePageMethods="true" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>

    <fieldset>
    <legend>Contactos</legend>

    <asp:Panel ID="PanelContacto" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_Contacto" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_div2">
            <asp:Label ID="LabelContacto" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryContactoMatrizSucursales" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1"><span class="item">Tipo Contacto</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:DropDownList ID="TipoContacto" runat="server"></asp:DropDownList> *
            <asp:RequiredFieldValidator id="RequiredFieldValidatorTipoContacto" runat="server" ControlToValidate="TipoContacto"  ValidationGroup="grupo2" ErrorMessage="Tipo Contacto" Display="none" InitialValue="-1"></asp:RequiredFieldValidator>
            
        </td>
        <td class="col3">Valor Contacto</td>
        <td class="col3">:</td>
        <td class="col3">
            
            <asp:TextBox ID="ValorContacto" runat="server" Width="300px"></asp:TextBox> *
            <asp:RequiredFieldValidator id="RequiredFieldValidatorValorContacto" runat="server" ControlToValidate="ValorContacto"  ValidationGroup="grupo2" ErrorMessage="Valor Contacto" Display="none"></asp:RequiredFieldValidator>
            
        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Detalle</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3"><asp:TextBox ID="DetalleContacto" runat="server" Width="300px"></asp:TextBox></td>
        <td class="col3">&nbsp;</td>
        <td class="col3">&nbsp;</td>
        <td class="col3">&nbsp;</td>
    </tr>
      
    </table>
    </fieldset>

    <br />

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><asp:Button ID="GuardarContacto" runat="server" 
                Text="Guardar Contacto" CausesValidation="true" ValidationGroup="grupo1" 
                onclick="GuardarContacto_Click"/></td>
        <td class="col2" colspan="5"><asp:Button ID="Volver" runat="server" Text="Volver" 
                onclick="Volver_Click" /></td>
    </tr>
    </table>
</asp:Content>
