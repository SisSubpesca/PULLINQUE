<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agregarContactoDireccion.aspx.cs" Inherits="SubPesca.Mantenedores.Titulares.agregarContactoDireccion" MasterPageFile="~/Administrador/SitioAdmin.Master" Theme="admin_style" %>

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
    <legend>Direcciones</legend>

    <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla" Visible="false" runat="server">
        <div class="msgGrilla_div1">
            <asp:Image ID="Ico_msgGrillaGral_1" CssClass="Ico_msgGrilla" runat="server" />
        </div>
        <div class="msgGrilla_div2">
            <asp:Label ID="msgGrilla" runat="server"></asp:Label>
        </div>
    </asp:Panel>

    <asp:ValidationSummary ID="ValidationSummaryIngresoSolicitante" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />
    
    <table class="form" cellpadding="0px" cellspacing="0px">
    
    <tr>
        <td class="col1"><span class="item">Dirección</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:TextBox ID="Direccion" runat="server" Width="400px"></asp:TextBox> *

            <asp:RequiredFieldValidator id="RequiredFieldValidatorDireccion" runat="server" ControlToValidate="Direccion"  ValidationGroup="grupo1" ErrorMessage="Dirección" Display="none"></asp:RequiredFieldValidator>
            
        </td>
        <td class="col3">
            
            </td>
        <td class="col3">
            </td>
        <td class="col3">
            
            
            
        </td>
    </tr>
    
    <tr>
        <td class="col1"><span class="item">Región</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
            
            <asp:UpdatePanel ID="UpdatePanelRegion" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="Region" AutoPostBack="true" OnSelectedIndexChanged="Region_OnSelectedIndexChanged" runat="server"></asp:DropDownList> *

                <asp:RequiredFieldValidator id="RequiredFieldValidatorRegion" runat="server" ControlToValidate="Region"  ValidationGroup="grupo1"
                                ErrorMessage="Región" Display="None" InitialValue="-1">*</asp:RequiredFieldValidator>
             </ContentTemplate>
             </asp:UpdatePanel> 
            
        </td>
        <td class="col3" align="left"></td>
        <td class="col3"></td>
        <td class="col3"></td>
    </tr>

    <tr>
        <td class="col1"><span class="item">Comuna</span></td>
        <td class="col2"><span class="item">:</span></td>
        <td class="col3">
             <asp:UpdatePanel ID="UpdatePanelComunas" UpdateMode="Conditional" runat="server">
             <ContentTemplate>
             <asp:DropDownList ID="Comuna" runat="server"></asp:DropDownList> *
                <asp:RequiredFieldValidator id="RequiredFieldValidatorComuna" runat="server" ControlToValidate="Comuna"  ValidationGroup="grupo1" ErrorMessage="Comuna" Display="none" InitialValue="-1"></asp:RequiredFieldValidator>
             </ContentTemplate>
             <Triggers>
             <asp:AsyncPostBackTrigger ControlID="Region" EventName="SelectedIndexChanged" />
             </Triggers>
             </asp:UpdatePanel>     
        </td>
        <td class="col3" align="left">&nbsp;</td>
        <td class="col3">&nbsp;</td>
        <td class="col3">&nbsp;</td>
    </tr>
    </table>
    
    </fieldset>
    <br />

    <table class="form" cellpadding="0px" cellspacing="0px">
    <tr>
        <td class="col1"><asp:Button ID="GuardarDireccion" runat="server" 
                Text="Guardar Dirección" onclick="GuardarDireccion_Click" CausesValidation="true" ValidationGroup="grupo1"/></td>
        <td class="col2" colspan="5"><asp:Button ID="Volver" runat="server" Text="Volver" 
                onclick="Volver_Click" /></td>
    </tr>
    </table>
</asp:Content>