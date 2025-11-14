<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="migas.ascx.cs" Inherits="SubPesca.Administrador.includes.migas" %>

<div id="migas">
    <div id="migas_detalle">
        <table>
        <tr>
            <td valign="middle" width="20px"><asp:Image ID="imgmigas" ImageUrl="~/App_Themes/admin_style/images/migas.png" ToolTip="migas" runat="server" /></td>
            <td valign="middle"><asp:Panel ID="Content_Migas" runat="server"></asp:Panel></td>
        </tr>
        </table>
    </div>
    <div id="logout">
        <asp:LinkButton ID="link_logout" CssClass="link_logout" OnClick="Logout_Click" CausesValidation="false" runat="server">Cerrar sesión</asp:LinkButton>
    </div>
    <div id="user">
        <asp:Label ID="Bienvenido" CssClass="usuario" runat="server" Text=""></asp:Label>    
    </div>
</div>
