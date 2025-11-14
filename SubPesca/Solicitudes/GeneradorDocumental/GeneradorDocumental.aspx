<%@ Page Language="C#" MasterPageFile="~/Administrador/SitioAdmin.Master" CodeBehind="GeneradorDocumental.aspx.cs" 
AutoEventWireup="true" Theme="admin_style" Inherits="SubPesca.Solicitudes.GeneradorDocumental.GeneradorDocumental" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>



    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Generador Documental</span>
            </td>
        </tr>
    </table>
    <fieldset>
        <legend>Generador de Documentos<span id="titulo_modulo0"> 
            <span id="titulo_modulo1"></span> </span></legend>

             <asp:ValidationSummary ID="ValidationSummaryGrupoSuspendido" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />

        <table class="form" cellpadding="0px" cellspacing="0px">
        
            <tr>
                <td class="col1"><span class="item">Número Pert</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3" colspan="4">
                    <asp:UpdatePanel ID="UpdatePanelNPert" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="Identificador" Width="80px" AutoPostBack="true" runat="server" onChange="return onlyNumeric(this)" onKeyUp="return onlyNumeric(this)" MaxLength="10"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>

            <tr>
                <td class="col1"><span class="item">Seleccione Unidad Espacial</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:UpdatePanel ID="UpdatePanelUnidadEsp" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="UnidadEsp"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="UnidadEsp_changed" ></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
           
                <td class="col1"><span class="item">Tipo Documento</span></td>
                <td class="col2"><span class="item">:</span></td>
                <td class="col3">
                    <asp:UpdatePanel ID="UpdatePanelDoc" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="TipoDoc" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table class="form" cellpadding="0px" cellspacing="0px">   
            <tr>
                <td class="col1"></td>
                <td class="col2"></td>
                <td class="col3">
                    <asp:Button ID="Generar"  runat="server" Text="Generar Documento"   CausesValidation="true" OnClick="Generar_Doc" />
                </td>
            </tr>
        </table>
         
    </fieldset>
</asp:Content>