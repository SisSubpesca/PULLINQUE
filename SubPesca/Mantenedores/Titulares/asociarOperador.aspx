<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="asociarOperador.aspx.cs" 
Inherits="SubPesca.Mantenedores.Titulares.asociarOperador" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
</asp:Content>

    <asp:Content ID="FormularioAdministracionTitulares" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManageAdministracionTitulares" runat="server"></asp:ToolkitScriptManager>

            <div id="divOculto" style="display:none">
                <asp:TextBox ID="RutPersona2" MaxLength="10" Width="100px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="RutPersona2"  ValidationGroup="grupo2"
                 ErrorMessage="Rut Persona" Display="Static"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" ControlToValidate="RutPersona2" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo2"></asp:CustomValidator>

            </div>

    <!-- Título de la página -->
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Asociar Operador</span>
            </td>
            <td align="right" valign="middle">
                
            </td>
        </tr>
    </table>
    
    <hr style="width:100%;" />

    <fieldset>
    <legend>Operador</legend>

    <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_div2">
            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryBuscarrRepresentante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
    <asp:ValidationSummary ID="ValidationSummaryBuscarrRepresentante2" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo2" />

    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1">Rut Titular</td>
        <td class="col2">:</td>
        <td class="col3">
            <asp:Label ID="RutTitular" runat="server" Text="Label"></asp:Label>

        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Rut Operador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            <asp:TextBox ID="RutPersona" MaxLength="10" Width="100px" runat="server"></asp:TextBox>
            <asp:Button ID="BuscarSolicitante" runat="server" Text="Buscar" onclick="ButtonRutPersona_Click" CausesValidation="true" ValidationGroup="grupo1" />
            12345678-K

            <asp:RequiredFieldValidator id="RequiredFieldValidatorRutPersona" runat="server" ControlToValidate="RutPersona"  ValidationGroup="grupo1"
            ErrorMessage="Rut Persona" Display="Static">*</asp:RequiredFieldValidator>

            <asp:CustomValidator ID="ccNumCustVal" ControlToValidate="RutPersona" ErrorMessage="Rut Persona sin formato válido" ForeColor="Red" ClientValidationFunction="validaRUT" Display="Static" Font-Size="10" runat="server" ValidationGroup="grupo1"></asp:CustomValidator>

        </td>
    </tr>
    </table>

    <asp:UpdatePanel ID="UpdatePanelNombreOperador" UpdateMode="Conditional" runat="server">
    <ContentTemplate>

    <asp:Panel ID="PanelNombreOperador" Visible="false" runat="server">
    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><span class="item">Nombre Operador</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3" colspan="4">
            
            <asp:TextBox ID="NombreOperador" runat="server" Width="300px" BackColor="#ddddee" ReadOnly></asp:TextBox>
            
        </td>
    </tr>
    </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>

    </fieldset>

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1">
            <asp:Button ID="AsociarOperadorTitular" runat="server" 
                Text="Asociar Operador" onclick="AsociarOperadorTitular_Click" CausesValidation="true" ValidationGroup="grupo2"/></td>
        <td class="col2" colspan="2"><asp:Button ID="Volver" runat="server" Text="Volver" 
                onclick="Volver_Click"/></td>
        
    </tr>
    </table>

    </asp:Content>