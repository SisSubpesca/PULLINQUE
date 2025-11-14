<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PoligonosMasivos.aspx.cs" Inherits="SubPesca.Mantenedores.PoligonosMasivos.PoligonosMasivos" 
MasterPageFile="~/Administrador/SitioAdmin.Master" Theme="admin_style"%>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">
    <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Subida de Poligonos Masivos</span>
            </td>
        </tr>
    </table>

    <fieldset>
        <br />
        <asp:ValidationSummary ID="ValidationSummaryGrupoSuspendido" CssClass="valSum" style="color:#772222;" runat="server" HeaderText="Ingrese valores válidos en los siguientes campos:" DisplayMode="BulletList" ValidationGroup="grupo1" />    
        <br />
           <asp:Label ID="ErrorLabel" runat="server" Visible="false" BackColor="Red"></asp:Label>
           <asp:Panel ID="PanelNombrePlantilla" runat="server" Visible="true">
                <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item">Nombre Plantilla *</span></td>
                        <td class="col2"><span class="item">:</span></td>
                        <td class="col3"><asp:TextBox  ID="NombrePlantilla" runat="server" AutoPostBack="false"></asp:TextBox></td>
                    </tr>
                </table>
           </asp:Panel>

           <br />
           <asp:Panel ID="PanelArchivo"  Visible="true" runat="server">
                <table class="form" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td class="col1"><span class="item"><asp:Literal ID="ArchivoAdjuntoLiteral" runat="server" Text="<%$Resources:spanish.language,archivoAdjunto%>"/></span></td>
                        <td class="col2"><span class="item">:</span></td><td class="col3"><asp:FileUpload ID="ArchivoAdjunto" MaxLength="40" Width="200px" runat="server"></asp:FileUpload>&nbsp;<asp:Label ID="RequeridoArchivoAdjunto" runat="server"></asp:Label><asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server" ErrorMessage="Formato Archivo Incorrecto" ControlToValidate="ArchivoAdjunto" ValidationExpression= "(.*).(.xlsb|.xls|.csv|.xlsx)$" />*</td>
                    </tr>
                </table>
           </asp:Panel>

           <table class="form" cellpadding="0px" cellspacing="0px">   
                <tr>
                    <td class="col1"></td>
                    <td class="col2"></td>
                    <td class="col3">
                        
                        <asp:Button ID="Guardar" runat="server" Text="Subir Poligonos"  CausesValidation="true"  OnClick="Guardar_Click" />
                        <asp:Button ID="Descargar" runat="server" Text="Descargar Planilla" CausesValidation="true" OnClick="Descargar_Click" />
                    </td>
                </tr>
           </table>
    
    </fieldset>

</asp:Content>