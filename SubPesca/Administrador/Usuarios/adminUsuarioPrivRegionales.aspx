<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminUsuarioPrivRegionales.aspx.cs" Inherits="SubPesca.Administrador.Usuarios.adminUsuarioPrivRegionales" Theme="admin_style" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="dialog_alcanceregional" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div style="height:30px;">
        <asp:Panel ID="Content_msgGrilla" CssClass="Content_msgGrilla_dialog" Visible="false" runat="server">
            <div class="msgGrilla_div1">
                <asp:Image ID="Ico_msgGrilla" CssClass="Ico_msgGrilla" runat="server" />
            </div>
            <div class="msgGrilla_div2">
                <asp:Label ID="msgGrilla" runat="server"></asp:Label>
            </div>
            <div class="msgGrilla_div3">
                <a onclick="ocultarObjeto('Content_msgGrilla', 0)"><img src="../../App_Themes/admin_style/images/cerrar.jpg" height="20px" alt="borrar" /></a>
            </div>
        </asp:Panel>
        </div>
        
        <br />
        
        <asp:Panel ID="Content_Botones" Visible="true" runat="server">
            <table class="formtop" cellpadding="1px" cellspacing="0px">
            <tr style="display:none">
                <td style="width:100px"><span class="item">Unidad Espacial</span></td>
                <td style="width:10px; text-align:center;"><span class="item">:</span></td>
                <td colspan="2"></td>
            </tr>

            <tr>
                <asp:UpdatePanel ID="UpdatePanelTipoTramite" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <td style="width:100px"><span class="item">Tipo de Trámite</span></td>
                <td style="width:10px; text-align:center;"><span class="item">:</span></td>
                <td colspan="2">
                    <asp:DropDownList ID="TipoTramite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SubTipo_Tramite"></asp:DropDownList>
                </td>
                </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            <tr>
                <asp:UpdatePanel ID="UpdatePanelSubtipoTramiteMod" UpdateMode="Conditional" runat="server" Visible="false">
                <ContentTemplate>
                <td style="width:100px"><span class="item">Sub Tipo Trámite Modificación</span></td>
                <td style="width:10px; text-align:center;"><span class="item">:</span></td>
                <td colspan="2">
                    <asp:ListBox ID="SubTipoTramiteMod" AutoPostBack="true" runat="server" SelectionMode="Multiple"></asp:ListBox>
                </td>
                </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="col1"><span class="item"></span></td>
                <td class="col2"><span class="item"></span></td>
                <td class="col3"><asp:Button id="BotonFiltrar" runat="server" Text="Filtrar" ValidationGroup="GrupoRolAcceso" onclick="BotonFiltrar_Click" /></td>
            </tr>
            <tr>
                <td style="width:100px"><span class="item">Usuario</span></td>
                <td style="width:10px; text-align:center;"><span class="item">:</span></td>
                <td><asp:Label ID="Usuario" runat="server" Text=""></asp:Label></td>
                <td align="right" valign="middle"></td>
            </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="PanelGuardar" Visible="true" runat="server">

        <table class="formtop" cellpadding="1px" cellspacing="0px">
             <tr>
                <td style="width:100px"></td>
                <td style="width:10px; text-align:center;"></td>
                <td></td>
                <td align="right" valign="middle">
                    <asp:ImageButton ID="Guardar" ImageUrl="~/App_Themes/admin_style/images/guardar.png" Visible="true" OnClick="Guardar_Click" Height="30px" ToolTip="Guardar" runat="server" />
                </td>
            </tr>
        </table>

        </asp:Panel>

        <asp:Panel ID="Content_Privilegios" Visible="true" runat="server">
        
        </asp:Panel>
        
        <asp:HiddenField ID="IdUsuario" runat="server" />
        <asp:HiddenField ID="IdRegion" runat="server" />

    </form>
</body>
</html>
